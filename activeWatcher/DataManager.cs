using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace ActiveWatcher
{
	internal class DataManager
	{
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
			throw new NotImplementedException();
		}

		public static void SaveProcesses(List<ProcessDetails> list)
		{

		}

		public static void SaveData()
		{
			//Create data directory if needed
			if (!System.IO.Directory.Exists("Data")) System.IO.Directory.CreateDirectory("Data");
		}

	}
}
