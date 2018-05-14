using System;
using System.Collections.Generic;
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

        internal void loadFile()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(filePath);
            }
            catch
            {
                return;
            }
            foreach (XmlElement e in doc.FirstChild)
            {
                if (e.Name != "Process") continue;

                //Parse encoded icon into Image object using stream reading
                byte[] iconData = Convert.FromBase64String(e.SelectSingleNode("Icon").InnerText);
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
                processes.Add(e.SelectSingleNode("ProcessName").InnerText,
                    new WProcess(int.Parse(e.SelectSingleNode("ID").InnerText),
                        e.SelectSingleNode("ProcessName").InnerText,
                        e.SelectSingleNode("CommonName").InnerText,
                        icon)
                );
            }
        }

        internal void saveFile()
        {
            //New XML file to write to
            XmlDocument doc = new XmlDocument();
            XmlElement main = doc.CreateElement("ProcessData");

            foreach (WProcess p in processes.Values)
            {
                XmlElement hold = doc.CreateElement("Process");

                //Check if icon exists and prepare for writing
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

                //Add all elements to parent element
                XmlElement val = doc.CreateElement("ID");
                val.InnerText = p.ID.ToString();
                hold.AppendChild(val);

                val = doc.CreateElement("ProcessName");
                val.InnerText = p.processName;
                hold.AppendChild(val);

                val = doc.CreateElement("CommonName");
                val.InnerText = p.commonName;
                hold.AppendChild(val);

                val = doc.CreateElement("Icon");
                val.InnerText = iconText;
                hold.AppendChild(val);

                //Add process element to total list
                main.AppendChild(hold);
            }

            //Add list to file
            doc.AppendChild(main);

            //Save file
            doc.Save(filePath);
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
