using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace ActiveWatcher
{
	class ProcessManager
	{
		#region Icon grabbing stuff
			const int GCL_HICONSM = -34;
			const int GCL_HICON = -14;

			const int ICON_SMALL = 0;
			const int ICON_BIG = 1;
			const int ICON_SMALL2 = 2;

			const int WM_GETICON = 0x7F;

			static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
			{
				if (IntPtr.Size > 4)
					return GetClassLongPtr64(hWnd, nIndex);
				else
					return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
			}

		internal int getProcessCount()
		{
			int protectedProcesses = 3;
			//if (processList.ContainsKey("ActiveWatcher")) ++protectedProcesses;
			//if (processList.ContainsKey(Watcher.WatcherConfig.IDLENAME)) ++protectedProcesses;
			//if (processList.ContainsKey("Idle")) ++protectedProcesses;

			return processList.Count - protectedProcesses;
		}

		[DllImport("user32.dll", EntryPoint = "GetClassLong")]
			static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

			[DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
			static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

			[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
			static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

			[DllImport("user32.dll")]
			static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
		#endregion

		Dictionary<int, ProcessDetails> processByID;
		public List<ProcessDetails> processList { get; set; }

		public ProcessManager()
		{
			processList = new List<ProcessDetails>();
		}

		internal void loadProcesses()
		{
			processList = DataManager.LoadProcesses();
			processByID = new Dictionary<int, ProcessDetails>();

			//No process history, add IDLE to list
			if (processList.Count == 0)
			{
				ProcessDetails idle = new ProcessDetails("User Idle", "IDLE", Properties.Resources.ZZZ);
				processByID.Add(0, idle);
				processList.Add(idle);
			}
			//Otherwise, find idle and link ID
			else
			{
				foreach (ProcessDetails item in processList)
				{
					if (item.Descriptor == "IDLE")
					{
						processByID.Add(0, item);
						break;
					}
				}
			}
		}

		internal ProcessDetails addProcess(int ID)
		{
			//First, check if in list already
			ProcessDetails pd;
			if (processByID.TryGetValue(ID, out pd))
			{
				pd.AddHook(ID);
				return pd;
			}

			//Process ID not registered, get process object and search known list
			Process p = Process.GetProcessById(ID);

			//Program description (used as key for programs)
			string s;
			try
			{
				s = p.MainModule.FileVersionInfo.FileDescription;
				if (s == null || s.Length < 1)
				{
					StringBuilder sb = new StringBuilder(256);
					GetWindowText(p.MainWindowHandle, sb, 256);
					s = sb.ToString() ?? "Unknown Title";
				}
			}
			catch
			{
				s = "System Program";
			}

			//Search through list
			foreach (ProcessDetails process in processList)
				if (process.Descriptor == s)
				{
					//Add second ID to list
					processByID.Add(ID, process);
					return process;
				}

			//Actually new process, continue gathering info
			Bitmap icon;
			try
			{
				icon = GetAppIcon(p.MainWindowHandle)?.ToBitmap();

				if (icon == null)
					icon = Icon.ExtractAssociatedIcon(p.MainModule.FileName)?.ToBitmap();
			}
			catch
			{
				icon = Properties.Resources.ActiveWatcherIcon.ToBitmap();
			}

			ProcessDetails proc = new ProcessDetails();
			proc.DisplayName = s;
			proc.Descriptor = s;
			proc.Icon = icon;
			proc.ID = DataManager.getProcessGuid(proc);
			proc.Color = GetColor(proc.Icon);

			//Add new process to lists
			processByID.Add(ID, proc);
			processList.Add(proc);

			Console.WriteLine("Added process " + proc.Descriptor + ", saving list...");

			DataManager.SaveProcesses(processList);

			//Return created WProcess
			return proc;
		}

		Color GetColor(Bitmap icon)
		{
			//Shrink to 3x3 and get color of center pixel
			Color color;
			using (Bitmap getColor = new Bitmap(icon, 3, 3))
			{
				color = getColor.GetPixel(1, 1);
			}

			return color;
		}

		public ProcessDetails getProcess(int ID)
		{
			//If process is in list, return the object
			ProcessDetails r;
			if (processByID.TryGetValue(ID, out r))
				return r;

			//else, return null
			return null;
		}

		//Code from https://codeutopia.net/blog/2007/12/18/find-an-applications-icon-with-winapi/
		public Icon GetAppIcon(IntPtr hwnd)
		{
			IntPtr iconHandle = SendMessage(hwnd, WM_GETICON, ICON_BIG, 0);
			if (iconHandle == IntPtr.Zero)
				iconHandle = GetClassLongPtr(hwnd, GCL_HICON);
			if (iconHandle == IntPtr.Zero)
				iconHandle = GetClassLongPtr(hwnd, GCL_HICONSM);

			if (iconHandle == IntPtr.Zero)
				return null;

			Icon icn = Icon.FromHandle(iconHandle);

			return icn;
		}

		internal void ResetTimers()
		{
			throw new NotImplementedException();
		}
	}
	public class ProcessDetails
	{
		public string DisplayName { get; set; }

		public Guid ID { get; set; }

		public string Descriptor { get; set; }

		[JsonIgnore]
		public System.Drawing.Bitmap Icon { get; set; }

		public Color Color { get; set; }

		[JsonIgnore]
		public bool Active { get => hooks.Count > 0; }
		List<int> hooks;

		public DateTime LastActive { get; set; }

		public static long totalTime = 0;
		public long currentTime;

		public string IconSerialized
		{
			get
			{
				using (MemoryStream ms = new MemoryStream())
				{
					Icon.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
					byte[] imageBytes = ms.ToArray();
					return Convert.ToBase64String(imageBytes);
				}
			}
			set
			{
				byte[] imageBytes = Convert.FromBase64String(value);
				MemoryStream ms = new MemoryStream(imageBytes);
				Icon = (Bitmap)Bitmap.FromStream(ms);
			}
		}

		internal ProcessDetails(string dName, string desc, System.Drawing.Bitmap i)
		{
			hooks = new List<int>();
			DisplayName = dName;
			Descriptor = desc;
			
			Icon = i;
		}

		public ProcessDetails()
		{
			hooks = new List<int>();
		}

		public override string ToString()
		{
			return String.Format("{0:D}:{1:D2}:{2:D2}/{3,3:F0}%", (currentTime / 3600), ((currentTime % 3600) / 60), (currentTime % 60), (currentTime * 100f / totalTime));
		}

		public void tick()
		{
			currentTime++;
			totalTime++;
			LastActive = DateTime.Now;
		}

		public void SetTime(long time)
		{
			totalTime -= currentTime;
			totalTime += time;
			currentTime = time;
		}

		public void AddHook(int i)
		{
			hooks.Add(i);
		}

		internal IEnumerable<int> getHooks()
		{
			return hooks;
		}

		internal void clearHooks()
		{
			hooks.Clear();
		}
	}
}
