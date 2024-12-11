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
		int maxID = 0;

		public ProcessManager()
		{
			processList = new List<ProcessDetails>();
		}

		internal void loadProcesses()
		{
			processList = DataManager.LoadProcesses();
			processByID = new Dictionary<int, ProcessDetails>();

			ProcessDetails idle = new ProcessDetails("User Idle", "IDLE", Properties.Resources.ZZZ);
			processList.Insert(0, idle);
			processByID.Add(0, idle);

			/*
			//Open DB connection
			//SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
			database.Open();
			
			//Do query for processes
			using (SQLiteCommand comm = new SQLiteCommand())
			{
				//Build command
				comm.CommandType = System.Data.CommandType.Text;
				comm.CommandText = "SELECT process_ndx, process_name, common_name, icon FROM process_ref";
				comm.Connection = database;

				//Run command and get data
				using (SQLiteDataReader data = comm.ExecuteReader())
				{
					//Data exists
					if (data.HasRows)
					{
						//While data still exists
						while (data.Read())
						{
							//Decode icon
							Image icon = Utility.imageFromString(data.GetString(3));

							//Add process to list
							processes.Add(data.GetString(1),
								new WProcess(data.GetInt32(0),
									data.GetString(1),
									data.GetString(2),
									icon));

							//Update maximum ID
							if (data.GetInt32(0) > maxID) maxID = data.GetInt32(0) + 1;
						}
					}
					//Close data reader
					data.Close();
				}
			}

			//close DB Connection
			database.Close();
			*/
		}
		internal void saveProcess(ProcessDetails p)
		{
			/*
			//Open DB connection
			SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
			database.Open();

			//Do query for processes
			using (SQLiteCommand comm = new SQLiteCommand())
			{
				//Build command
				comm.CommandType = System.Data.CommandType.Text;
				comm.CommandText = @"INSERT INTO process_ref (process_name, common_name, icon) 
					VALUES (@pname, @cname, @icon);";
				comm.Connection = database;

				string iconText = "";
				if (p.icon != null)
				{
					iconText = Utility.stringFromImage(p.icon);
				}

				comm.Parameters.Add("@pname", System.Data.DbType.AnsiString).Value = p.processName;
				comm.Parameters.Add("@cname", System.Data.DbType.AnsiString).Value = p.commonName;
				comm.Parameters.Add("@icon", System.Data.DbType.AnsiString).Value = iconText;

				comm.ExecuteNonQuery();
				int id = (int)database.LastInsertRowId;

				if (id > 0) maxID = id;

				comm.Parameters.Clear();

			}
			database.Close();
			*/
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

			ProcessDetails proc = new ProcessDetails(
					p.ProcessName,
					s,
					icon
				);

			//Add new process to lists
			processByID.Add(ID, proc);
			processList.Add(proc);

			saveProcess(proc);

			//Return created WProcess
			return proc;
		}

		public ProcessDetails getProcess(int ID)
		{
			//If process is in list, return the object
			if (processByID.ContainsKey(ID))
				return processList[ID];

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
	internal class ProcessDetails
	{
		public string DisplayName { get; private set; }
		public string Descriptor { get; private set; }

		[JsonIgnore]
		public System.Drawing.Image Icon { get; private set; }

		public string IconSerialized
		{
			get
			{
				using (MemoryStream ms = new MemoryStream())
				{
					Icon.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
					byte[] imageBytes = ms.ToArray();
					return Convert.ToBase64String(imageBytes);
				}
			}
			set
			{
				byte[] imageBytes = Convert.FromBase64String(value);
				using (MemoryStream ms = new MemoryStream(imageBytes))
				{
					Icon = Bitmap.FromStream(ms);
				}
			}
		}

		public bool Active { get => hooks.Count > 0; }
		List<int> hooks;

		public static int totalTime = 0;
		public int currentTime;

		internal ProcessDetails(string dName, string desc, System.Drawing.Image i)
		{
			hooks = new List<int>();
			DisplayName = dName;
			Descriptor = desc;
			Icon = i;
		}

		public override string ToString()
		{
			return String.Format("{0:D}:{1:D2}:{2:D2}/{3,3:F0}%", (currentTime / 3600), ((currentTime % 3600) / 60), (currentTime % 60), (currentTime * 100f / totalTime));
		}

		public void tick()
		{
			currentTime++;
			totalTime++;
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
