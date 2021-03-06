﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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
        #region imports
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;
        #endregion

        public static TimerHolder instance;

        Watcher w;
        Dictionary<string,ProcessTimer> processes;
        Position pos;
        public int DisplayCount { get { return Watcher.DISPLAYCOUNT; } set { Watcher.DISPLAYCOUNT = value;} }
        IconLabel[] plabels;
        IconLabel total;

        public bool displaying = false;
        Timer ANIMTIMER;
        double targetOpacity;
        double opacitySpeed = 0.04;

        int labelHeight = 24;
        int labelWidth = 150;

        public TimerHolder()
        {
            InitializeComponent();

            instance = this;
            this.Opacity = 0.0;
            w = Watcher.instance;
            processes = w.getTimers();
            pos = Position.BOTTOM_RIGHT;
            plabels = new IconLabel[DisplayCount];

            ANIMTIMER = new Timer();
            ANIMTIMER.Interval = 16;
            ANIMTIMER.Tick += ANIMTIMER_Tick;
            targetOpacity = Watcher.HIDDENOPACITY;
            setPassThrough(Watcher.PASSTHROUGH);

            w.onResize += W_resize;
            w.onTick += W_onTick;

            //Needs to use Windows message handler instead of winforms events due to transparency
            MouseMoveMessageFilter m = new MouseMoveMessageFilter(this);
            m.onMouseEnter += TimerHolder_MouseEnter;
            m.onMouseLeave += TimerHolder_MouseLeave;
            Application.AddMessageFilter(m);
        }

        internal void setPassThrough(bool enable)
        {
            int style = GetWindowLong(this.Handle, GWL_EXSTYLE);
            //Make sure form is layered window and transparency
            style = (style | WS_EX_LAYERED) | WS_EX_TRANSPARENT;
            //If disabling, XOR the transparency to turn off
            if (!enable)
                style = (style | WS_EX_LAYERED) ^ WS_EX_TRANSPARENT;
            SetWindowLong(this.Handle, GWL_EXSTYLE, style);
        }

        public void display()
        {
            redraw();
            ANIMTIMER.Start();
            displaying = true;
        }

        private void ANIMTIMER_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < targetOpacity)
            {
                this.Opacity += opacitySpeed;
                if (this.Opacity >= targetOpacity)
                    ANIMTIMER.Stop();
            }
            else
            {
                this.Opacity -= opacitySpeed;
                if (this.Opacity <= targetOpacity)
                    ANIMTIMER.Stop();
            }
        }

        private void W_onTick()
        {
            this.Invoke(new MethodInvoker(delegate { updateLabels(); }));

            if (!displaying) this.Invoke(new MethodInvoker(delegate { display(); }));
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

        internal void redraw()
        {
            if (w == null) return;

            //Re-query Display Variables
            DisplayCount = Watcher.DISPLAYCOUNT;
            setPassThrough(Watcher.PASSTHROUGH);

            //Resize label array
            if (DisplayCount != plabels.Length)
            {
                IconLabel[] hold = new IconLabel[DisplayCount];
                for (int i = 0; i < plabels.Length; i++)
                    hold[i] = plabels[i];
                plabels = hold;
            }

            //Build total display if needed
            if (total == null && Watcher.SHOWTOTAL)
            {
                //Total time label
                total = makeLabel(0);
                this.Controls.Add(total);
                //Update label positions
                for (int i = 0; i < plabels.Length; i++)
                    if (plabels[i] != null)
                        plabels[i].Location = new System.Drawing.Point(0, (i + 1) * labelHeight);
            }

            //Remove total display if needed
            if (total != null && !Watcher.SHOWTOTAL)
            {
                this.Controls.Remove(total);
                //Update label positions
                for (int i = 0; i < plabels.Length; i++)
                    if (plabels[i] != null)
                        plabels[i].Location = new System.Drawing.Point(0, i * labelHeight);
            }

            int num = processes.Count > DisplayCount ? DisplayCount : processes.Count;
            this.ClientSize = new Size(labelWidth, labelHeight * (num + (Watcher.SHOWTOTAL ? 1 : 0)));
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
                IconLabel hold = makeLabel(processes.Count - (Watcher.SHOWTOTAL ? 0 : 1));
                this.Controls.Add(hold);
                this.plabels[processes.Count - 1] = hold;
            }

            if (total != null)
                total.displayText = string.Format("{0:D}:{1:D2}:{2:D2}/100%", ProcessTimer.total / 3600, (ProcessTimer.total % 3600) / 60, ProcessTimer.total % 60);

            //Sort processes by time active
            List<ProcessTimer> sorted = processes.Values.ToList();
            sorted.Sort((x , y) => x.secondsActive > y.secondsActive ? -1 : 1);

            //Display process details on labels
            for(int l = 0; l < plabels.Length; l++)
            {
                if(plabels[l] != null)
                {
                    plabels[l].displayText = sorted[l].ToString();
                    plabels[l].setToolTip(sorted[l].process.commonName);
                    plabels[l].Image = sorted[l].getIcon();
                    plabels[l].fillPercent = (double)sorted[l].secondsActive / ProcessTimer.total;
                    plabels[l].Refresh();
                }
            }
        }

        IconLabel makeLabel(int position)
        {
            /*
            Label hold = new Label();
            hold.Name = "Label" + position.ToString();
            hold.Text = "";
            hold.Font = new Font(FontFamily.GenericMonospace,10);
            hold.BackColor = System.Drawing.Color.Transparent;
            hold.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255, 255);
            hold.Location = new System.Drawing.Point(0, position * labelHeight);
            hold.Size = new System.Drawing.Size(labelWidth, labelHeight);
            hold.Margin = new System.Windows.Forms.Padding(0);
            hold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            hold.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            */
            IconLabel hold = new IconLabel();
            hold.Location = new System.Drawing.Point(0, position * labelHeight);
            hold.Size = new System.Drawing.Size(labelWidth, labelHeight);

            //hold.MouseEnter += TimerHolder_MouseEnter;
            //hold.MouseLeave += TimerHolder_MouseLeave;

            return hold;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Repositioning
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

        #endregion

        private void reset_Click(object sender, EventArgs e)
        {
            w.resetAll();
            processes = w.getTimers();
            foreach(IconLabel l in plabels)
            {
                if(l != null)
                    l.Dispose();
            }
            plabels = new IconLabel[DisplayCount];
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

        private void TimerHolder_MouseEnter(object sender, EventArgs e)
        {
            targetOpacity = 1.0;
            if (!ANIMTIMER.Enabled) ANIMTIMER.Start();
        }

        private void TimerHolder_MouseLeave(object sender, EventArgs e)
        {
            targetOpacity = Watcher.HIDDENOPACITY;
            if (!ANIMTIMER.Enabled) ANIMTIMER.Start();
        }

        private void viewTimelineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Timeline().Show();
            new Graphs().Show();
        }

        //Mouse Handler for Windows OS level messages to trigger events
        class MouseMoveMessageFilter : IMessageFilter
        {
            private const int WM_NCMOUSEMOVE = 0x00A0;
            private const int WM_MOUSEMOVE = 0x200;
            private const int WM_NCMOUSELEAVE = 0x02A2;
            private const int WM_MOUSELEAVE = 0x02A3;

            public event EventHandler onMouseEnter;
            public event EventHandler onMouseLeave;
            public TimerHolder TargetForm { get; set; }
            bool mouseInside = false;

            public MouseMoveMessageFilter(TimerHolder t)
            {
                TargetForm = t;
            }

            public bool PreFilterMessage(ref Message m)
            {
                if (TargetForm == null) return false;

                switch (m.Msg)
                {
                    case WM_MOUSEMOVE:
                    case WM_NCMOUSEMOVE:
                        CheckMouseBounds(true);
                        break;

                    case WM_NCMOUSELEAVE:
                    case WM_MOUSELEAVE:
                        CheckMouseBounds(false);
                        break;
                }

                return false;
            }

            //If in our form's bounds on the screen, trigger mouse hover
            void CheckMouseBounds(bool inside)
            {
                //Already inside and just moving, skip
                if (mouseInside && inside) return;

                //Check if still inside
                bool check = TargetForm.Bounds.Contains(Cursor.Position);

                if(mouseInside != check)
                {
                    mouseInside = check;

                    //Moved inside
                    if (mouseInside)
                        onMouseEnter?.Invoke(TargetForm,EventArgs.Empty);

                    //Moved outside
                    if (!mouseInside)
                        onMouseLeave?.Invoke(TargetForm, EventArgs.Empty);

                }
            }
        }
    }
}
