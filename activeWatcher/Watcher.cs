using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace ActiveWatcher
{
	class Watcher : IDisposable
	{
		public class WatcherConfig
		{
			public int IDLEMAX { get; set; } = 30;
			public int DISPLAYCOUNT { get; set; } = 5;
			public double HIDDENOPACITY { get; set; } = 0.2;
			public bool PASSTHROUGH { get; set; } = false;
			public bool SHOWTOTAL { get; set; } = true;
			public bool SAVETIMES { get; set; } = true;
		}
		public static WatcherConfig settings;

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

		System.Timers.Timer CLOCK;
		Point mouseWatch = new Point(0, 0);
		public ProcessManager procManager { get; private set; }
		public int idleTime = 0;

		public delegate void resize(int processCount);
		public delegate void tick();
		public event resize onResize;
		public event tick onTick;
		internal List<Rule> Rules { get; private set; }

		int activeProcessID = 0;
		ProcessDetails activeProcess;

		public static void initialize()
		{
			settings = DataManager.LoadConfig();

			if (instance != null) return;
			instance = new Watcher();
			instance.procManager = new ProcessManager();
			instance.initClock();

			//Load rules
			instance.Rules = new List<Rule>();
			instance.loadRules();

			//Load times
			//instance.loadTimes();
		}

		public void initClock()
		{
			//Set CLOCK to a one second timer
			CLOCK = new System.Timers.Timer(1000);
			CLOCK.Elapsed += CLOCK_Tick;

			//Start clocking
			CLOCK.Start();
		}

		//Clock tick event
		private void CLOCK_Tick(object sender, EventArgs e)
		{
			//Get active process ID
			int focusID;
			GetWindowThreadProcessId(GetForegroundWindow(),out focusID);

			//Test for idle time, select IDLE process
			if (GetInactiveTime() >= settings.IDLEMAX)
				focusID = 0;

			//Only lookup the timer if its a new ID
			if (focusID != activeProcessID)
			{
				activeProcess = procManager.getProcess(activeProcessID);

				//Detail not added, add it to list
				if (activeProcess == null)
					activeProcess = procManager.addProcess(activeProcessID);
			}

			activeProcessID = focusID;
	
			activeProcess.tick();

			//Call tick event
			onTick?.Invoke();
		}

		#region File Management

		void loadRules()
		{
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load("Data/Rules.xml");
			}catch
			{
				return;
			}

			bool err = false;
			foreach (XmlNode rule in doc.FirstChild)
			{
				try
				{
					/*
					addRule(new Rule(
						rule.SelectSingleNode("Label").InnerText,
						rule.SelectSingleNode("Process").InnerText,
						false,
						int.Parse(rule.SelectSingleNode("Limit").InnerText)
						//(Rule.RuleResult)Enum.Parse(typeof(Rule.RuleResult), rule.SelectSingleNode("Action").InnerText)
						));
					*/
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

			/*
			foreach (ProcessTimer p in timers.Values)
			{
				XmlElement timer = doc.CreateElement("ProcessTime");

				XmlElement val = doc.CreateElement("Process");
				val.InnerText = p.process.ProcessName;
				timer.AppendChild(val);
				
				val = doc.CreateElement("Time");
				val.InnerText = p.secondsActive.ToString();
				timer.AppendChild(val);

				times.AppendChild(timer);
			}
			*/

			//Add Times list to main document
			doc.AppendChild(times);

			//Save to File
			doc.Save("Data/Times.xml");

		}

		#endregion

		#region Clock Management

		public void resetAll()
		{
			CLOCK.Stop();
			procManager?.ResetTimers();
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

		public void Dispose()
		{
			CLOCK.Stop();
			CLOCK.Dispose();
			//instance.database.Close();
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
				return settings.IDLEMAX;
			}
		}

		/*
		public static SQLiteDataReader QueryDB(string query)
		{
			using (SQLiteCommand cmd = new SQLiteCommand())
			{
				cmd.Connection = instance.database;
				cmd.CommandType = System.Data.CommandType.Text;
				cmd.CommandText = query;

				if (instance.database.State != System.Data.ConnectionState.Open)
					instance.database.Open();

				return cmd.ExecuteReader();
			}
		}
		*/
	}
}
