using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace ActiveWatcher
{
    class Watcher : IDisposable
    {
        public static Watcher instance;

        #region Imports
        //Process Grabbing methods
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern int GetForegroundWindow();

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        #endregion

        #region Statics
        public static int IDLEMAX = 30;
        public static int DISPLAYCOUNT = 5;
        public static bool doSaveTimes = true;
        public static double HIDDENOPACITY = 0.2;
        public const string IDLENAME = "ACTIVEWATCHERIDLE";
        /*
        public static string DBCONNECTION = @"Data Source=(LocalDB)\MSSQLLocalDB;
                AttachDbFilename=|DataDirectory|\ProcessTimes.mdf;
                Integrated Security=True";
        */
        public static string DBCONNECTION = @"Data Source = Data/ProcessData.sqlite";
        #endregion

        System.Timers.Timer CLOCK;
        //Timers only shows processes that HAVE BEEN active
        Dictionary<string,ProcessTimer> timers;
        Dictionary<string, int> prevTimes;
        Point mouseWatch = new Point(0, 0);
        ProcessManager procManager;
        SQLiteConnection database;
        public int idleTime = 0;

        public delegate void resize(int processCount);
        public delegate void tick();
        public event resize onResize;
        public event tick onTick;
        internal List<Rule> Rules { get; private set; }

        public static void initialize()
        {
            if (instance != null) return;
            instance = new Watcher();
            instance.procManager = new ProcessManager();
            
            //Create data directory if needed
            if (!System.IO.Directory.Exists("Data")) System.IO.Directory.CreateDirectory("Data");

            //Create database file if needed
            if (!System.IO.File.Exists("Data/ProcessData.sqlite")) SQLiteConnection.CreateFile("Data/ProcessData.sqlite");

            //Make database connection and connect
            instance.database = new SQLiteConnection("Data Source=Data/ProcessData.sqlite");

            instance.database.Open();

            //Create tables if needed
            using (SQLiteCommand makeTables = new SQLiteCommand(
                    @"CREATE TABLE IF NOT EXISTS
                    [process_ref] (
                    [process_ndx]     INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    [process_name]   VARCHAR(30) NULL,
                    [common_name]   VARCHAR(50) NULL,
                    [icon]          VARCHAR NULL);
                    CREATE TABLE IF NOT EXISTS
                    [process_time_data] (
                    [time_stamp]    DATETIME NOT NULL PRIMARY KEY UNIQUE,
                    [process_ndx]   INTEGER NULL);", instance.database))
                makeTables.ExecuteNonQuery();

            instance.database.Close();

            instance.procManager.loadProcesses();

            //Load rules
            instance.Rules = new List<Rule>();
            instance.loadRules();

            //Load times
            instance.loadTimes();

        }

        private Watcher()
        {
            //Load configuration file
            loadConfig();

            //Set CLOCK to a one second timer
            CLOCK = new System.Timers.Timer(1000);
            CLOCK.Elapsed += CLOCK_Tick;

            //Initialize timers
            timers = new Dictionary<string, ProcessTimer>();

            //Start clocking
            CLOCK.Start();
        }

        //Clock tick event
        private void CLOCK_Tick(object sender, EventArgs e)
        {
            //Increment total seconds
            ProcessTimer.total++;

            //Currently active process
            int focusID;
            GetWindowThreadProcessId(GetForegroundWindow(),out focusID);
            Process focus = Process.GetProcessById(focusID);
            string activeProcess = focus.ProcessName;

            int idleTime = GetInactiveTime();

            //Test for idle time
            if (idleTime >= IDLEMAX)
                activeProcess = IDLENAME;
            //if (mouseWatch != Cursor.Position)
            //    resetIdle();
            //mouseWatch = Cursor.Position;
    
            //Check if focused ID is in timers
            if (timers.ContainsKey(activeProcess))
            {
                //If found, tick up that process timer
                timers[activeProcess].tick();

                //If saving times
                if (doSaveTimes)
                {
                    //Write tick to database
                    using (SQLiteCommand cmd = new SQLiteCommand())
                    {
                        cmd.CommandText = "INSERT INTO process_time_data (time_stamp, process_ndx) VALUES (DATETIME('now'),@ndx)";
                        cmd.Connection =   instance.database;
                        cmd.CommandType = System.Data.CommandType.Text;

                        cmd.Parameters.Add("@ndx", System.Data.DbType.Int32).Value = timers[activeProcess].process.ID;

                        instance.database.Open();

                        cmd.ExecuteNonQuery();

                        instance.database.Close();
                    }
                }
            }
            //If not found in dictionary
            else
            {
                //Get process
                WProcess proc = procManager.getProcess(activeProcess);

                //If no process found, register new process
                if(proc == null)
                {
                    //Add to process manager and get return
                    proc = procManager.addProcess(focus);
                }

                //Add the newly focused process to the list
                ProcessTimer p = new ProcessTimer(proc);

                //Dont apply rules to idle process
                if (activeProcess != IDLENAME)
                    //Check for rules to add alarms
                    foreach (Rule r in Rules)
                    {
                        if (r.checkApply(p))
                            p.applyRule(r);
                    }

                //Load previous time
                //if (prevTimes.ContainsKey(p.process.processName))
                //    p.secondsActive = prevTimes[p.process.processName];

                ProcessTimer.total += p.secondsActive;

                timers.Add(p.process.processName, p);
                p.tick();

                //Call the resize event
                onResize?.Invoke(timers.Count);
            }

            //Call tick event
            onTick?.Invoke();
        }

        #region File Management

        internal void saveConfig()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement config = doc.CreateElement("Configuration");
            
            XmlElement val = doc.CreateElement("DisplayCount");
            val.InnerText = DISPLAYCOUNT.ToString();
            config.AppendChild(val);

            val = doc.CreateElement("IdleLimit");
            val.InnerText = IDLEMAX.ToString();
            config.AppendChild(val);

            val = doc.CreateElement("SaveTimes");
            val.InnerText = doSaveTimes.ToString();
            config.AppendChild(val);

            val = doc.CreateElement("HideOpacity");
            val.InnerText = HIDDENOPACITY.ToString();
            config.AppendChild(val);

            //Add Rules to main document
            doc.AppendChild(config);

            //Save to File
            doc.Save("Config.xml");
        }

        void loadConfig()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("Config.xml");
            }catch
            {
                return;
            }

            try
            {
                //Load idle seconds
                IDLEMAX = int.Parse(doc.FirstChild.SelectSingleNode("IdleLimit").InnerText);

                //Load timer display count
                DISPLAYCOUNT = int.Parse(doc.FirstChild.SelectSingleNode("DisplayCount").InnerText);

                //Load display hidden opacity
                HIDDENOPACITY = double.Parse(doc.FirstChild.SelectSingleNode("HideOpacity")?.InnerText);

                //Load time saving switch
                doSaveTimes = bool.Parse(doc.FirstChild.SelectSingleNode("SaveTimes").InnerText);
            }
            catch
            {
                //Show error if any exceptions happened
                MessageBox.Show(" Warning: There were errors while loading configuration.", "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void loadRules()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("Rules.xml");
            }catch
            {
                return;
            }

            bool err = false;
            foreach (XmlNode rule in doc.FirstChild)
            {
                try
                {
                    addRule(new Rule(
                        rule.SelectSingleNode("Label").InnerText,
                        rule.SelectSingleNode("Process").InnerText,
                        false,
                        int.Parse(rule.SelectSingleNode("Limit").InnerText),
                        (Rule.RuleResult)Enum.Parse(typeof(Rule.RuleResult), rule.SelectSingleNode("Action").InnerText)
                        ));
                }
                catch
                {
                    err = true;
                }
            }

            //Show error if any exceptions happened
            if (err) MessageBox.Show(" Warning: There were errors while loading saved rules.", "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        internal void saveTimes()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement times = doc.CreateElement("ProcessTimes");

            foreach (ProcessTimer p in timers.Values)
            {
                XmlElement timer = doc.CreateElement("ProcessTime");

                XmlElement val = doc.CreateElement("Process");
                val.InnerText = p.process.processName;
                timer.AppendChild(val);
                
                val = doc.CreateElement("Time");
                val.InnerText = p.secondsActive.ToString();
                timer.AppendChild(val);

                times.AppendChild(timer);
            }

            //Add Times list to main document
            doc.AppendChild(times);

            //Save to File
            doc.Save("Times.xml");

        }

        void loadTimes()
        {
            return;

            prevTimes = new Dictionary<string, int>();

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("Times.xml");
            }
            catch
            {
                return;
            }

            bool err = false;
            foreach (XmlNode process in doc.FirstChild)
            {
                try
                {
                    prevTimes.Add(
                        process.SelectSingleNode("Process").InnerText,
                        int.Parse(process.SelectSingleNode("Time").InnerText)
                        );
                    ProcessTimer.total += int.Parse(process.SelectSingleNode("Time").InnerText);
                }
                catch
                {
                    err = true;
                }
            }

            //Show error if any exceptions happened
            if (err) MessageBox.Show(" Warning: There were errors while loading saved times.", "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        #endregion

        #region Rules stuff

        internal void addRule(Rule rule)
        {
            Rules.Add(rule);

            //Check rule against all timers
            foreach(KeyValuePair<string,ProcessTimer> p in timers)
            {
                if (rule.checkApply(p.Value))
                    p.Value.applyRule(rule);
            }
        }

        internal void removeRule(Rule rule)
        {
            //Check rule against all timers
            foreach (KeyValuePair<string, ProcessTimer> p in timers)
            {
                if (rule.checkApply(p.Value))
                    p.Value.removeRule(rule);
            }

            //Remove from Rule list
            Rules.Remove(rule);
        }

        #endregion

        #region Clock Management

        public void resetAll()
        {
            CLOCK.Stop();
            timers.Clear();
            ProcessTimer.total = 0;
            CLOCK.Start();
        }

        public void registerTick(System.Timers.ElapsedEventHandler func)
        {
            CLOCK.Elapsed += func;
        }

        internal void unregisterTick(System.Timers.ElapsedEventHandler func)
        {
            CLOCK.Elapsed -= func;
        }
        #endregion

        public Dictionary<string, ProcessTimer> getTimers()
        {
            return timers;
        }

        public void Dispose()
        {
            CLOCK.Stop();
            CLOCK.Dispose();
        }

        public static int GetInactiveTime()
        {
            LASTINPUTINFO info = new LASTINPUTINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            if (GetLastInputInfo(ref info))
            {
                return (int)TimeSpan.FromMilliseconds((uint)Environment.TickCount - info.dwTime).TotalSeconds;
            }
            else
            {
                return IDLEMAX;
            }
        }
    }
    class ProcessTimer
    {
        public static int total = 0;
        public int secondsActive { get; internal set; }
        public WProcess process;
        List<RuleAlarm> alarms;

        public ProcessTimer(WProcess p,int s = 0)
        {
            this.process = p;
            secondsActive = s;
            alarms = new List<RuleAlarm>();
        }

        public override string ToString()
        {
            return String.Format("{0:D}:{1:D2}:{2:D2}/{3,3:F0}%", (secondsActive / 3600), ((secondsActive % 3600) / 60), (secondsActive % 60), (secondsActive * 100f / total));
        }

        public void tick()
        {
            secondsActive++;

            foreach(RuleAlarm a in alarms)
            {
                a.evaluate(secondsActive, this);
            }
        }

        public Image getIcon()
        {
            return process.icon;
        }

        internal void applyRule(Rule r)
        {
            alarms.Add(r.getAlarm());
        }

        internal void removeRule(Rule r)
        {
            foreach(RuleAlarm a in alarms.ToArray())
            {
                if (a.Parent == r)
                {
                    alarms.Remove(a);
                    a.delete();
                }
            }
        }
    }
}
