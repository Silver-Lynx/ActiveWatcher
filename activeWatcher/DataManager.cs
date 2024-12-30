using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Xml.Serialization;

namespace ActiveWatcher
{
	internal static class DataManager
	{
		static int TagMaxID = 0;
		public static List<ProcessTag> TagList {get; private set;}
		static FileStream activeTimes;
		static DateTime activeDate = DateTime.Now;
		static Guid ProgramID = new Guid("9a5b8d72-4b3d-477a-9b6b-3cb03f5ff0c2");
		static string folderPath = ""; //Default to program folder

		public static Watcher.WatcherConfig LoadConfig()
		{
			try
			{
				string raw = File.ReadAllText(folderPath + "config.json");

				return JsonSerializer.Deserialize<Watcher.WatcherConfig>(raw);

			}
			catch (Exception e)
			{
				Watcher.WatcherConfig config = new Watcher.WatcherConfig();

				if (e is FileNotFoundException)
					SaveConfig(config);
				else
					MessageBox.Show("Error loading config file.\n" + e.Message);

				return config;
			}
		}

		public static void SaveConfig(Watcher.WatcherConfig config)
		{
			try
			{
				string data = JsonSerializer.Serialize(config);

				File.WriteAllText(folderPath + "config.json", data);
			}
			catch
			{
				MessageBox.Show("Error while serializing settings.\nIf this error persists, manually delete 'config.json' file in the program directory.");
			}
		}

		public static List<ProcessDetails> LoadProcesses()
		{
			List<ProcessDetails> newlist;
			try
			{
				string data = File.ReadAllText(folderPath + "Data/Processes.json");

				newlist = new List<ProcessDetails>(JsonSerializer.Deserialize<ProcessDetails[]>(data));

				int nulls = newlist.RemoveAll(p => p == null);

				if (nulls > 0)
					Console.WriteLine("Found "+nulls+" Null Processes");
				Console.WriteLine("Loaded " + newlist.Count + " Process definitions");
			}
			catch
			{
				newlist = new List<ProcessDetails>();

				Console.WriteLine("Error loading process definitions");
			}

			return newlist;
		}

		public static void SaveProcesses(List<ProcessDetails> list)
		{
			string data = JsonSerializer.Serialize(list);

			if (!Directory.Exists(folderPath+"Data"))
				Directory.CreateDirectory(folderPath+"Data");

			File.WriteAllText(folderPath + "Data/Processes.json", data);

		}

		public static void SaveProcessChange(ProcessDetails process)
		{
			//Time file hasn't been opened yet. Open one
			if(activeTimes == null)
				LoadTimes();

			activeTimes.WriteByte((byte)'F');
			activeTimes.Write(process.ID.ToByteArray(), 0, 16);
			activeTimes.Write(BitConverter.GetBytes(DateTime.Now.ToBinary()), 0, 8);
			//Pad to 64 bytes for standard sizing
			for (int i = 0; i < 39; i++)
			{
				activeTimes.WriteByte(0);
			}
			Console.WriteLine("Saved-F"+process.ID+"|"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
		}

		class TimeLoadData
		{
			public DateTime LastTime;
			public long Total;
		}

		public static void LoadTimes()
		{
			DateTime now = System.DateTime.Now;

			string path = folderPath+"Data";
			string filepath = Path.Combine(path, now.ToString("yyyy-MM-dd") + ".dat");


			Console.WriteLine("Attempting to load " + filepath);

			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);

			Dictionary<Guid, TimeLoadData> times = new Dictionary<Guid, TimeLoadData>();
			Guid id = Guid.Empty;
			DateTime start = DateTime.MaxValue;
			Guid nextid = Guid.Empty;
			DateTime ending = DateTime.MaxValue;
			bool skip = false;

			using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
			{
				byte[] data = new byte[64];

				Console.WriteLine("Loading time data...");
				//Read up to end of file to populate process times
				while (fs.Read(data,0,64) == 64)
				{
					//New focus, get id to start next time frame
					if ((char)data[0] == 'F')
					{
						byte[] dec = new byte[16];
						Array.Copy(data, 1, dec, 0, 16);

						nextid = new Guid(dec);
						ending = DateTime.FromBinary((long)BitConverter.ToUInt64(data, 17));
						skip = false;
					}
					//Closed app, just a generic end point
					else if ((char)data[0] == 'X')
					{
						nextid = Guid.Empty;
						ending = DateTime.FromBinary((long)BitConverter.ToUInt64(data, 1));
						skip = true;
					}
					else
						continue;

					//If positive time between
					if (start < ending && id != nextid)
					{
						long time = (long)(ending - start).TotalSeconds;

						TimeLoadData current;

						if (times.TryGetValue(id, out current))
						{
							current.Total += time;
							current.LastTime = ending;
							Console.WriteLine("+ " + time);
						}
						else
						{
							current = new TimeLoadData();
							current.Total = time;
							current.LastTime = ending;
							times[id] = current;
							Console.WriteLine("= " + time);
						}
					}

					Console.WriteLine(nextid);
					id = nextid;
					start = skip ? DateTime.MaxValue : ending;
				}
			}
			Console.WriteLine("Done!");

			//Apply found values to process list
			foreach (ProcessDetails item in Watcher.instance.procManager.processList)
			{
				TimeLoadData value;
				if (times.TryGetValue(item.ID, out value))
				{
					item.SetTime(value.Total);
					item.LastActive = value.LastTime;
					Console.WriteLine(item.ID+" = "+value);
				}
			}

			//Re-open again in append mode
			activeDate = now;
			activeTimes = new FileStream(filepath, FileMode.Append);
		}

		internal static Guid getProcessGuid(ProcessDetails proc)
		{
			// Convert the namespace and name to byte arrays
			byte[] namespaceBytes = ProgramID.ToByteArray(); 
			byte[] nameBytes = Encoding.UTF8.GetBytes(proc.Descriptor); 
			
			// Combine the namespace and name byte arrays
			byte[] hashInput = new byte[namespaceBytes.Length + nameBytes.Length]; 
			Buffer.BlockCopy(namespaceBytes, 0, hashInput, 0, namespaceBytes.Length); 
			Buffer.BlockCopy(nameBytes, 0, hashInput, namespaceBytes.Length, nameBytes.Length);

			// Hash the combined byte array using SHA-1
			using (System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create()) { 
				byte[] hashBytes = sha1.ComputeHash(hashInput); 
				// Set the version to 5 (SHA-1) and adjust the variant
				hashBytes[6] = (byte)((hashBytes[6] & 0x0F) | 0x50); // Version 5
				hashBytes[8] = (byte)((hashBytes[8] & 0x3F) | 0x80); // Variant 
				
				// Create a new GUID from the hashed bytes
				return new Guid(hashBytes.Take(16).ToArray()); }
		}

		internal static void CloseTimes()
		{
			if (activeTimes != null)
			{
				activeTimes.WriteByte((byte)'X');
				activeTimes.Write(BitConverter.GetBytes(DateTime.Now.ToBinary()), 0, 8);
				//Pad to 64 bytes for standard sizing
				for (int i = 0; i < 55; i++)
				{
					activeTimes.WriteByte(0);
				}

				activeTimes.Close();
			}
		}

		internal static void CheckTime()
		{
			if (DateTime.Now.Date != activeDate.Date)
			{
				CloseTimes();
				LoadTimes();
			}
		}

		public class ProcessTag
		{
			public int ID { get; set; }
			public string TagName { get; set; }
			public string[] DefaultPaths { get; set; }

			public ProcessTag() { }

			public bool CheckPath(string path)
			{
				foreach (string item in DefaultPaths)
				{
					string pattern = "^.*" + Regex.Escape(item).Replace("*", ".*").Replace("?", ".") + ".*$";

					Console.WriteLine(pattern);

					if (Regex.IsMatch(path,pattern,RegexOptions.IgnoreCase))
						return true;
				}
				return false;
			}

			public static implicit operator int(ProcessTag t) => t.ID;

			public override string ToString()
			{
				return TagName;
			}
		}

		public static void LoadTags()
		{
			try
			{
				string data = File.ReadAllText(folderPath + "Data/Tags.json");

				List<ProcessTag> tags = new List<ProcessTag>(JsonSerializer.Deserialize<ProcessTag[]>(data));

				TagList = tags;

				//Get max ID number
				foreach (ProcessTag tag in tags)
					if(tag.ID > TagMaxID) TagMaxID = tag.ID;
			}
			catch
			{
				TagList = new List<ProcessTag>();

				ProcessTag system = new ProcessTag();
				system.ID = 0;
				system.TagName = "System";
				system.DefaultPaths = new string[] { ":\\windows"};
				TagList.Add(system);

				ProcessTag work = new ProcessTag();
				work.ID = 1;
				work.TagName = "Work";
				work.DefaultPaths = new string[] { "office", "microsoft", "adobe", "apple", "autodesk", "corel", "jetbrains", "zoom", "cisco", "notepad", "sublime", "\\atom\\", "figma", "nodejs", "\\gimp" };
				TagList.Add(work);

				ProcessTag media = new ProcessTag();
				media.ID = 2;
				media.TagName = "Media";
				media.DefaultPaths = new string[] { "chrome", "opera", "firefox", "microsoft\\edge", "discord", "telegram", "element", "pidgin", "trillian", "whatsapp", "signal", "viber", "facebook", "twitter", "tiktok", "\\VLC" };
				TagList.Add(media);

				ProcessTag games = new ProcessTag();
				games.ID = 3;
				games.TagName = "Games";
				games.DefaultPaths = new string[] { "games", "steam", "origin", "blizzard", "ubisoft", "\\epic" };
				TagList.Add(games);

				TagMaxID = 3;

				SaveTags();
			}
		}

		public static void SaveTags()
		{
			string data = JsonSerializer.Serialize(TagList);

			if (!Directory.Exists(folderPath + "Data"))
				Directory.CreateDirectory(folderPath + "Data");

			File.WriteAllText(folderPath + "Data/Tags.json", data);
		}

		public static ProcessTag NewTag(string name, string[] defaultpaths)
		{
			ProcessTag t = new ProcessTag();
			t.ID = ++TagMaxID;
			t.TagName = name;
			t.DefaultPaths = defaultpaths;

			SaveTags();

			return t;
		}
	}
}
