﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveWatcher
{
    enum Position
    {
        TOP_LEFT,
        TOP_CENTER,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_CENTER,
        BOTTOM_RIGHT
    }
    public partial class TimerHolder : Form
    {
        public static TimerHolder instance;

        Watcher w;
        Dictionary<string,ProcessTimer> processes;
        Position pos;
        public int DisplayCount { get { return Watcher.displayCount; } set { Watcher.displayCount = value; redraw(); } }
        Label[] plabels;

        public TimerHolder()
        {
            InitializeComponent();
            instance = this;
            w = Watcher.instance;
            processes = w.getProcesses();
            pos = Position.BOTTOM_RIGHT;
            plabels = new Label[DisplayCount];

            w.onResize += W_resize;
            w.onTick += W_onTick;
        }

        private void W_onTick()
        {
            this.Invoke(new MethodInvoker(delegate { updateLabels(); }));
        }

        private void W_resize(int processCount)
        {
            //this.processes = w.getProcesses();
            this.Invoke(new MethodInvoker(delegate { this.redraw(); }));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Watcher.instance.saveTimes();
        }

        private void redraw()
        {
            //Unload previous labels
            foreach (Label l in plabels)
            {
                if (l == null) continue;
                l.Dispose();
            }

            //Add labels back
            plabels = new Label[DisplayCount];

            //Loop through and rebuild labels
            int p = 0;
            while (p < processes.Count && p < plabels.Length)
            {
                Label hold = new Label();
                hold.Name = "Label" + p.ToString();
                hold.Text = "";
                hold.BackColor = System.Drawing.Color.Transparent;
                hold.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                hold.Location = new System.Drawing.Point(0, p * 20);
                hold.Size = new System.Drawing.Size(150, 20);
                hold.Margin = new System.Windows.Forms.Padding(0);
                hold.TabIndex = 0;
                hold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                hold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                this.Controls.Add(hold);
                this.plabels[p] = hold;
                p++;
            }


            int num = processes.Count > DisplayCount ? DisplayCount : processes.Count;
            this.ClientSize = new Size(150, 20 * (num < 1 ? 1 : num));
            switch (pos)
            {
                case Position.TOP_LEFT:
                    this.Location = new Point(0, 0);
                    break;
                case Position.TOP_CENTER:
                    this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2), 0);
                    break;
                case Position.TOP_RIGHT:
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width-this.Width, 0);
                    break;
                case Position.BOTTOM_LEFT:
                    this.Location = new Point(0, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                    break;
                case Position.BOTTOM_CENTER:
                    this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2), Screen.PrimaryScreen.WorkingArea.Height-this.Height);
                    break;
                case Position.BOTTOM_RIGHT:
                    this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                    break;
            }
            updateLabels();
            this.Visible = true;
        }

        private void updateLabels()
        {
            //Add new label if needed
            if(processes.Count > 0 && processes.Count <= plabels.Length && plabels[processes.Count-1] == null)
            {
                // 
                // TestLabel
                // 
                Label hold = new Label();
                hold.Name = "Label" + ((processes.Count - 1) * 20).ToString();
                hold.Text = "";
                hold.BackColor = System.Drawing.Color.Transparent;
                hold.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
                hold.Location = new System.Drawing.Point(0, (processes.Count-1)*20);
                hold.Size = new System.Drawing.Size(150, 20);
                hold.Margin = new System.Windows.Forms.Padding(0);
                hold.TabIndex = 0;
                hold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
                hold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

                this.Controls.Add(hold);
                this.plabels[processes.Count - 1] = hold;
            }

            //Sort processes by time active
            List<ProcessTimer> sorted = processes.Values.ToList();
            sorted.Sort((x , y) => x.secondsActive > y.secondsActive ? -1 : 1);

            //Display process details on labels
            for(int l = 0; l < plabels.Length; l++)
            {
                if (plabels[l] != null)
                {
                    plabels[l].Text = sorted[l].ToString();
                    plabels[l].Image = sorted[l].getIcon();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void posBR_Click(object sender, EventArgs e)
        {
            pos = Position.BOTTOM_RIGHT;
            this.redraw();
        }

        private void posBL_Click(object sender, EventArgs e)
        {
            pos = Position.BOTTOM_LEFT;
            this.redraw();
        }

        private void bottomCenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pos = Position.BOTTOM_CENTER;
            this.redraw();
        }

        private void posTR_Click(object sender, EventArgs e)
        {
            pos = Position.TOP_RIGHT;
            this.redraw();
        }

        private void posTL_Click(object sender, EventArgs e)
        {
            pos = Position.TOP_LEFT;
            this.redraw();
        }

        private void posTC_Click(object sender, EventArgs e)
        {
            pos = Position.TOP_CENTER;
            this.redraw();
        }

        private void TimerHolder_Deactivate(object sender, EventArgs e)
        {
            w.resetIdle();
        }

        private void reset_Click(object sender, EventArgs e)
        {
            w.resetAll();
            processes = w.getProcesses();
            foreach(Label l in plabels)
            {
                if(l != null)
                    l.Dispose();
            }
            plabels = new Label[DisplayCount];
            redraw();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().Show();
        }

        private void Notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            new Options().Show();
        }
    }
}