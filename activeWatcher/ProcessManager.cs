﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
            int protectedProcesses = 0;
            if (processes.ContainsKey("ActiveWatcher")) ++protectedProcesses;
            if (processes.ContainsKey(Watcher.IDLENAME)) ++protectedProcesses;
            if (processes.ContainsKey("Idle")) ++protectedProcesses;

            return processes.Count - protectedProcesses;
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

        internal Dictionary<string, WProcess> processes { get; private set; }
        int maxID = 0;

        public ProcessManager()
        {
            processes = new Dictionary<string, WProcess>();
        }

        internal void loadProcesses()
        {
            processes?.Clear();

            processes.Add(Watcher.IDLENAME,new WProcess(-1, Watcher.IDLENAME, "User Idle", Properties.Resources.ZZZ));

            //Open DB connection
            SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
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
        }
        internal void saveProcess(WProcess p)
        {
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
        }
        
        internal WProcess addProcess(Process p)
        {
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

            WProcess proc = new WProcess(maxID+1,
                    p.ProcessName,
                    s,
                    icon
                );

            //Add new process to list
            processes.Add(p.ProcessName, proc);

            saveProcess(proc);

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
