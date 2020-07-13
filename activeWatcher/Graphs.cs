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
using System.Windows.Forms.DataVisualization.Charting;

namespace ActiveWatcher
{
    public partial class Graphs : Form
    {
        public Graphs()
        {
            InitializeComponent();

            refreshData();
        }

        private void refreshData()
        {
            Dictionary<string, Series> seriesHold = new Dictionary<string, Series>();
            List<Series> ignoreEmpty = new List<Series>();
            //processes = new List<WProcess>();
            //processTotal = 0;

            //SeriesCollection timeSeries = new SeriesCollection();

            ////Create a dictionary of chart values defined as datetime-int pairs, representing all lines to be drawn, linked to their index
            //Dictionary<int, StackedColumnSeries> seriesHold = new Dictionary<int, StackedColumnSeries>();

            ////Get time type
            //string dateString = ((string)dateType.SelectedItem).ToUpper();
            //double hourMult;
            //switch (dateString)
            //{
            //    case "HOURS":
            //        hourMult = 1.0;
            //        break;
            //    case "DAYS":
            //        hourMult = 24.0;
            //        break;
            //    default:
            //        hourMult = 0.0;
            //        break;
            //}
            ////Get time value
            //int dateVal = (int)dateValue.Value;
            DateTime dateval = dateTimePicker1.Value;

            ////Reset Axes
            //cartesianChart1.Series.Configuration = Mappers.Xy<DateTimePoint>()
            //    .X(value => (double)value.DateTime.Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks)
            //    .Y(value => value.Value);

            //cartesianChart1.AxisX.Clear();

            //cartesianChart1.AxisX.Add(new Axis
            //{
            //    MinValue = (double)DateTime.Now.AddHours(-1 * dateVal * hourMult).Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks,
            //    MaxValue = (double)DateTime.Now.Ticks / TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks,
            //    Separator = new Separator
            //    {
            //        Step = hourMult == 24.0 ? 12 : 30,
            //        IsEnabled = true
            //    },

            //    LabelFormatter = value => new DateTime((long)(value * TimeSpan.FromMinutes(hourMult == 24.0 ? 60 : 1).Ticks)).ToString("t")
            //});

            //cartesianChart1.AxisY[0].MaxValue = hourMult == 24.0 ? 3600 : 60;

            //Open DB connection
            SQLiteConnection database = new SQLiteConnection(Watcher.DBCONNECTION);
            database.Open();

            //Do query for times
            using (SQLiteCommand comm = new SQLiteCommand())
            {
                //Build command
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = string.Format(@"
                    WITH RECURSIVE times(hour,counter) AS (
                        SELECT DATETIME('{0}'),0
                        UNION ALL
                        SELECT DATETIME(hour,'+1 hour'),counter+1
                        FROM times
                        WHERE counter < 23
                    )
                    SELECT p.common_name, COALESCE(a.counter/60,0), t.hour
                    FROM times t
                    join (SELECT process_ndx, common_name FROM process_ref UNION SELECT 0,'Idle') p
                    ON 1=1
                    LEFT JOIN ( SELECT COALESCE(process_ndx,0) AS 'process_ndx', COUNT(*) AS 'counter', DATETIME(ROUND(JULIANDAY(time_stamp) * 24.0 - 0.5) / 24.0, 'localtime') AS 'time_stamp'
	                    FROM process_time_data
	                    WHERE time_stamp BETWEEN DATETIME('{0}') AND DATETIME('{0}','+1 day')
	                    GROUP BY process_ndx, ROUND(JULIANDAY(time_stamp) * 24.0 -0.5)) a
                    ON a.process_ndx = p.process_ndx
                    AND a.time_stamp = t.hour
                    ORDER BY t.hour", dateval.ToString("yyyy-MM-dd"));
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
                            string pname = data.GetString(0);
                            int count = data.GetInt32(1);
                            DateTime date = data.GetDateTime(2);

                            if (!seriesHold.ContainsKey(pname))
                            {
                                Series hold = new Series();
                                hold.ChartType = SeriesChartType.StackedArea;
                                hold.Legend = "Legend";
                                hold.ChartArea = "ChartArea";
                                hold.Name = pname;
                                hold.XValueType = ChartValueType.Time;
                                seriesHold[pname] = hold;
                                ignoreEmpty.Add(hold);
                            }

                            ////If index is not in dictionary
                            //if (!seriesHold.ContainsKey(index))
                            //{
                            //    StackedColumnSeries hold = new StackedColumnSeries();
                            //    hold.Values = new ChartValues<DateTimePoint>();
                            //    //hold.LineSmoothness = 0;
                            //    hold.ScalesXAt = 0;
                            //    hold.MaxColumnWidth = 100;
                            //    hold.ColumnPadding = 0;
                            //    hold.PointGeometry = null;
                            //    seriesHold.Add(index, hold);
                            //}

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

                            if (count > 0) ignoreEmpty.Remove(seriesHold[pname]);

                            //Add value to series data
                            //seriesHold[index].Values.Add(new DateTimePoint(date, data.GetInt32(1)));
                            seriesHold[pname].Points.AddXY(date, count);
                        }
                    }
                    //Close data reader
                    data.Close();
                }
            }

            ////Do query for processes
            //using (SQLiteCommand comm = new SQLiteCommand())
            //{
            //    //Build command
            //    comm.CommandType = System.Data.CommandType.Text;
            //    comm.CommandText = string.Format(@"
            //        SELECT COALESCE(p.process_ndx,0), p.common_name, p.icon, t.counter
            //        FROM (SELECT process_ndx, COUNT(*) AS 'counter'
            //             FROM process_time_data
            //             WHERE time_stamp BETWEEN DATETIME('NOW', '-{0} {1}') AND DATETIME('NOW')
            //             GROUP BY process_ndx) t
            //        LEFT JOIN process_ref p
            //        ON p.process_ndx = t.process_ndx
            //        WHERE t.counter > 0
            //        ORDER BY t.counter", dateVal, dateString);
            //    comm.Connection = database;

            //    //Run command and get data
            //    using (SQLiteDataReader data = comm.ExecuteReader())
            //    {
            //        //Data exists
            //        if (data.HasRows)
            //        {
            //            //While data still exists
            //            while (data.Read())
            //            {
            //                WProcess hold;
            //                if (data.GetInt32(0) == 0)
            //                {
            //                    hold = new WProcess(0, data.GetInt32(3).ToString(), "Idle Time", Properties.Resources.ZZZ);
            //                }
            //                else
            //                {
            //                    hold = new WProcess(data.GetInt32(0), data.GetInt32(3).ToString(), data.GetString(1), Utility.imageFromString(data.GetString(2)));
            //                }

            //                processes.Add(hold);
            //                processTotal += int.Parse(hold.processName);
            //            }
            //        }
            //        //Close data reader
            //        data.Close();
            //    }
            //}

            ////Clear label list
            //foreach (Control c in labelPanel.Controls)
            //{
            //    c.Dispose();
            //}
            //labelPanel.Controls.Clear();
            //labelPanel.Invalidate();

            //processes.Sort((x, y) => int.Parse(x.processName) > int.Parse(y.processName) ? -1 : 1);

            ////Repopulate Label list
            //foreach (WProcess p in processes)
            //{
            //    IconLabel hold = new IconLabel();
            //    hold.displayText = p.commonName;
            //    hold.Image = p.icon;
            //    hold.fillPercent = double.Parse(p.processName) / processTotal;
            //    hold.resizeText();
            //    hold.Location = new Point(0, processes.IndexOf(p) * 32);

            //    labelPanel.Controls.Add(hold);

            //    //Set matching series label for each process
            //    if (seriesHold.ContainsKey(p.ID))
            //    {
            //        seriesHold[p.ID].Title = p.commonName;
            //        seriesHold[p.ID].Values.Add(new DateTimePoint(((DateTimePoint)seriesHold[p.ID].Values[seriesHold[p.ID].Values.Count - 1]).DateTime.AddMinutes(1), 0));
            //    }
            //}

            ////Build chart
            //cartesianChart1.Series.Clear();

            //cartesianChart1.Series.AddRange(seriesHold.Values);

            //cartesianChart1.Refresh();
            chart1.Series.Clear();
            foreach (Series item in seriesHold.Values)
            {
                if(!ignoreEmpty.Contains(item))
                    chart1.Series.Add(item);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshData();
        }
    }
}
