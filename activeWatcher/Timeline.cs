using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;
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
using LiveCharts.Defaults;

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

            cartesianChart1.AxisY.Add(new Axis
            {
                MinValue = 0,
                MaxValue = 60
            });

            refreshData();
        }

        void refreshData()
        {
            processes = new List<WProcess>();
            processTotal = 0;

            SeriesCollection timeSeries = new SeriesCollection();

            //Create a dictionary of chart values defined as datetime-int pairs, representing all lines to be drawn, linked to their index
            Dictionary<int, StackedColumnSeries> seriesHold = new Dictionary<int, StackedColumnSeries>();

            //Get time type
            string dateString = ((string)dateType.SelectedItem).ToUpper();
            double hourMult;
            switch (dateString)
            {
                case "HOURS":
                    hourMult = 1.0;
                    break;
                case "DAYS":
                    hourMult = 24.0;
                    break;
                default:
                    hourMult = 0.0;
                    break;
            }
            //Get time value
            int dateVal = (int)dateValue.Value;

            //Reset Axes
            cartesianChart1.Series.Configuration = Mappers.Xy<DateTimePoint>()
                .X(value => (double)value.DateTime.Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks)
                .Y(value => value.Value);

            cartesianChart1.AxisX.Clear();

            cartesianChart1.AxisX.Add(new Axis
            {
                MinValue = (double)DateTime.Now.AddHours(-1 * dateVal * hourMult).Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks,
                MaxValue = (double)DateTime.Now.Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks,
                Separator = new Separator
                {
                    Step = hourMult == 24.0 ? 12 : 30,
                    IsEnabled = true
                },

                LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks)).ToString("t")
            });

            cartesianChart1.AxisY[0].MaxValue = hourMult == 24.0 ? 3600 : 60;

            //Open DB connection
            SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
            database.Open();
            
            //Do query for times
            using (SQLiteCommand comm = new SQLiteCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = string.Format(@"
                    SELECT COALESCE(p.process_ndx,0) AS 'process_ndx', t.counter, t.time_stamp
                    FROM ( SELECT process_ndx, COUNT(*) AS 'counter', DATETIME(ROUND(JULIANDAY(time_stamp) * 24.0 * {2}) / 24.0 / {2}, 'localtime') AS 'time_stamp'
	                    FROM process_time_data
	                    WHERE time_stamp BETWEEN DATETIME('NOW','-{0} {1}') AND DATETIME('NOW')
	                    GROUP BY process_ndx, ROUND(JULIANDAY(time_stamp) * 24.0 * {2})) t
                    left join process_ref p
                    ON t.process_ndx = p.process_ndx
                    ORDER BY t.time_stamp", dateVal, dateString, hourMult == 24.0 ? "1.0" : "60.0");
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
                            int index = data.GetInt32(0);
                            DateTime date = data.GetDateTime(2);

                            //If index is not in dictionary
                            if (!seriesHold.ContainsKey(index))
                            {
                                StackedColumnSeries hold = new StackedColumnSeries();
                                hold.Values = new ChartValues<DateTimePoint>();
                                //hold.LineSmoothness = 0;
                                hold.ScalesXAt = 0;
                                hold.MaxColumnWidth = 100;
                                hold.ColumnPadding = 0;
                                hold.PointGeometry = null;
                                
                                seriesHold.Add(index, hold);
                            }

                            /*
                            //Add a cutoff line if a process isn't used for a tick
                            if (seriesHold[index].Values.Count > 0) {
                                DateTime lastPoint = ((DateTimePoint)seriesHold[index].Values[seriesHold[index].Values.Count - 1]).DateTime;
                                if (TimeSpan.FromTicks(date.Ticks - lastPoint.Ticks).TotalMinutes > 1) {
                                    seriesHold[index].Values.Add(new DateTimePoint(lastPoint.AddMinutes(1), 0));
                                    seriesHold[index].Values.Add(new DateTimePoint(date.AddMinutes(-1), 0));
                                }
                            }
                            //Start at 0
                            else
                            {
                                seriesHold[index].Values.Add(new DateTimePoint(date.AddMinutes(-1), 0));
                            }
                            */

                            //Add value to series data
                            seriesHold[index].Values.Add(new DateTimePoint(date, data.GetInt32(1)));
                        }
                    }
                    //Close data reader
                    data.Close();
                }
            }

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

            processes.Sort((x, y) => int.Parse(x.processName) > int.Parse(y.processName) ? -1 : 1 );

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

                //Set matching series label for each process
                if (seriesHold.ContainsKey(p.ID))
                {
                    seriesHold[p.ID].Title = p.commonName;
                    seriesHold[p.ID].Values.Add(new DateTimePoint(((DateTimePoint)seriesHold[p.ID].Values[seriesHold[p.ID].Values.Count-1]).DateTime.AddMinutes(1), 0));
                }
            }

            //Build chart
            cartesianChart1.Series.Clear();

            cartesianChart1.Series.AddRange(seriesHold.Values);

            cartesianChart1.Refresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
