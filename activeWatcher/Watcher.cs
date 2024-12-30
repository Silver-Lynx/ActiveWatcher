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
		public event tick onTick;
		internal List<Rule> Rules { get; private set; }

		int activeProcessID = 0;
		ProcessDetails activeProcess;

		public static void initialize()
		{
			settings = DataManager.LoadConfig();
			DataManager.LoadTags();

			if (instance != null) return;
			instance = new Watcher();
			instance.procManager = new ProcessManager();
			instance.procManager.loadProcesses();
			instance.initClock();

			//Load rules
			instance.Rules = new List<Rule>();
			instance.loadRules();

			//Load times
			//instance.loadTimes();
			DataManager.LoadTimes();
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
				activeProcess = procManager.getProcess(focusID);

				//Detail not added, add it to list
				if (activeProcess == null)
					activeProcess = procManager.addProcess(focusID);

				Console.WriteLine("New active "+focusID+" - " + activeProcess.Descriptor);

				//Save process change to file
				DataManager.SaveProcessChange(activeProcess);
			}

			activeProcessID = focusID;

			activeProcess.tick();

			//Call tick event
			onTick?.Invoke();

			DataManager.CheckTime();
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
	}
}
