using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveWatcher
{
    public partial class Timeline : Form
    {
        List<WProcess> processes;
        int processTotal;

        public Timeline()
        {
            InitializeComponent();

            dateType.SelectedIndex = 0;

            refreshData();
        }

        void refreshData()
        {
            processes = new List<WProcess>();
            processTotal = 0;

            //Get time type
            string dateString = ((string)dateType.SelectedItem).ToUpper();

            //Get time value
            int dateVal = (int)dateValue.Value;

            //Open DB connection
            SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
            database.Open();

            /*
            //Do query for times
            using (SQLiteCommand comm = new SQLiteCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = string.Format(@"
                    SELECT COALESCE(p.process_ndx,0) AS 'process_ndx', t.counter, t.time_stamp
                    FROM ( SELECT process_ndx, COUNT(*) AS 'counter', DATETIME(ROUND(JULIANDAY(time_stamp) * 24.0 * 60.0) / 24.0 / 60.0) AS 'time_stamp'
	                    FROM process_time_data
	                    WHERE time_stamp BETWEEN DATETIME('NOW','-{0} {1}') AND DATETIME('NOW')
	                    GROUP BY process_ndx, ROUND(JULIANDAY(time_stamp) * 24.0 * 60.0)) t
                    left join process_ref p
                    ON t.process_ndx = p.process_ndx
                    ORDER BY t.time_stamp", dateVal, dateString);
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
                            
                        }
                    }
                    //Close data reader
                    data.Close();
                }
            }
            */

            //Do query for processes
            using (SQLiteCommand comm = new SQLiteCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = string.Format(@"
                    SELECT COALESCE(p.process_ndx,0), p.common_name, p.icon, t.counter
                    FROM (SELECT process_ndx, COUNT(*) AS 'counter'
                         FROM process_time_data
                         WHERE time_stamp BETWEEN DATETIME('NOW', '-{0} {1}') AND DATETIME('NOW')
                         GROUP BY process_ndx) t
                    LEFT JOIN process_ref p
                    ON p.process_ndx = t.process_ndx
                    WHERE t.counter > 0
                    ORDER BY t.counter", dateVal, dateString);
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
                            WProcess hold;
                            if(data.GetInt32(0) == 0)
                            {
                                hold = new WProcess(0, data.GetInt32(3).ToString(), "Idle Time", Properties.Resources.ZZZ);
                            }
                            else
                            {
                                hold = new WProcess(data.GetInt32(0), data.GetInt32(3).ToString(), data.GetString(1), Utility.imageFromString(data.GetString(2)));
                            }

                            processes.Add(hold);
                            processTotal += int.Parse(hold.processName);
                        }
                    }
                    //Close data reader
                    data.Close();
                }
            }

            //Clear label list
            foreach(Control c in labelPanel.Controls)
            {
                c.Dispose();
            }
            labelPanel.Controls.Clear();
            labelPanel.Invalidate();

            processes.Sort((x, y) => { return int.Parse(x.processName) > int.Parse(y.processName) ? -1 : 1; });

            //Repopulate Label list
            foreach(WProcess p in processes)
            {
                IconLabel hold = new IconLabel();
                hold.displayText = p.commonName;
                hold.Image = p.icon;
                hold.fillPercent = double.Parse(p.processName) / processTotal;
                hold.resizeText();
                hold.Location = new Point(0, processes.IndexOf(p) * 32);

                labelPanel.Controls.Add(hold);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
