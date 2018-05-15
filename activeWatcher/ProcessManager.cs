using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ActiveWatcher
{
    class ProcessManager
    {
        Dictionary<string, WProcess> processes;
        int maxID = 0;
        string filePath = "processes.xml";

        public ProcessManager(string filePath = "processes.xml")
        {
            processes = new Dictionary<string, WProcess>();
        }

        internal void loadProcesses()
        {
            //Open DB connection
            SqlConnection database = new SqlConnection(Watcher.DBConnection);
            database.Open();
            
            //Do query for processes
            using (SqlCommand comm = new SqlCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = "SELECT process_ndx, process_name, common_name, icon FROM process_ref";
                comm.Connection = database;

                //Run command and get data
                using (SqlDataReader data = comm.ExecuteReader())
                {
                    //Data exists
                    if (data.HasRows)
                    {
                        //While data still exists
                        while (data.Read())
                        {
                            //Parse encoded icon into Image object using stream reading
                            byte[] iconData = Convert.FromBase64String(data.GetString(3));
                            //Make stream and fill with string
                            MemoryStream stream = new MemoryStream();
                            BinaryWriter writer = new BinaryWriter(stream);
                            writer.Write(iconData);
                            //Move pointer to start of stream
                            writer.Flush();
                            stream.Position = 0;
                            //Load stream as image
                            Image icon = Bitmap.FromStream(stream);

                            //Add process to list
                            processes.Add(data.GetString(1),
                                new WProcess(data.GetInt32(0),
                                    data.GetString(1),
                                    data.GetString(2),
                                    icon));
                        }
                    }
                    //Close data reader
                    data.Close();
                }
            }

            //close DB Connection
            database.Close();
        }

        internal void saveProcesses()
        {
            //Open DB connection
            SqlConnection database = new SqlConnection(Watcher.DBConnection);
            database.Open();

            //Do query for processes
            using (SqlCommand comm = new SqlCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = "INSERT INTO process_ref (process_name, common_name, icon) VALUES (@pname, @cname, @icon)";
                comm.Connection = database;

                foreach(WProcess p in processes.Values) {

                    string iconText = "";
                    if (p.icon != null)
                    {
                        //Parse icon into encoded string using stream reading
                        MemoryStream stream = new MemoryStream();
                        p.icon.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        stream.Position = 0;
                        byte[] data = stream.ToArray();
                        iconText = Convert.ToBase64String(data);
                    }

                    comm.Parameters.Add("@pname", System.Data.SqlDbType.NVarChar).Value = p.processName;
                    comm.Parameters.Add("@cname", System.Data.SqlDbType.NVarChar).Value = p.commonName;
                    comm.Parameters.Add("@icon", System.Data.SqlDbType.NVarChar).Value = iconText;

                    comm.ExecuteNonQuery();

                    comm.Parameters.Clear();
                }
            }
            database.Close();
        }

        internal WProcess addProcess(Process p)
        {
            WProcess proc = new WProcess(maxID++,
                    p.ProcessName,
                    p.MainModule.FileVersionInfo.FileDescription,
                    Icon.ExtractAssociatedIcon(p.MainModule.FileName).ToBitmap()
                );

            //Add new process to list
            processes.Add(p.ProcessName, proc);

            //Return created WProcess
            return proc;
        }

        public WProcess getProcess(string processName)
        {
            //If process is in list, return the object
            if (processes.ContainsKey(processName))
                return processes[processName];

            //else, return null
            return null;
        }

    }
    internal class WProcess
    {
        public int ID { get; private set; }
        public string processName { get; private set; }
        public string commonName { get; private set; }
        public System.Drawing.Image icon { get; private set; }
        List<Process> hooks;

        internal WProcess(int id, string pName, string cName, System.Drawing.Image i)
        {
            ID = id;
            processName = pName;
            commonName = cName;
            icon = i;
            hooks = new List<Process>();
        }

        internal void registerHook(Process p)
        {
            if(!hooks.Contains(p))
                hooks.Add(p);
        }

        internal List<Process> getHooks()
        {
            return hooks;
        }
    }
}
