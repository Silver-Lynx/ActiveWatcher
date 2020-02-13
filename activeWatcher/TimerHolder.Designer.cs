namespace ActiveWatcher
{
    partial class TimerHolder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                w.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimerHolder));
            this.Notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewTimelineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reset = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posBR = new System.Windows.Forms.ToolStripMenuItem();
            this.posBL = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.posTR = new System.Windows.Forms.ToolStripMenuItem();
            this.posTL = new System.Windows.Forms.ToolStripMenuItem();
            this.posTC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Notify
            // 
            this.Notify.ContextMenuStrip = this.contextMenuStrip1;
            this.Notify.Icon = ((System.Drawing.Icon)(resources.GetObject("Notify.Icon")));
            this.Notify.Text = "ActiveWatch";
            this.Notify.Visible = true;
            this.Notify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Notify_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewTimelineToolStripMenuItem,
            this.toolStripSeparator2,
            this.reset,
            this.optionsToolStripMenuItem,
            this.positionToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 126);
            // 
            // viewTimelineToolStripMenuItem
            // 
            this.viewTimelineToolStripMenuItem.Name = "viewTimelineToolStripMenuItem";
            this.viewTimelineToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.viewTimelineToolStripMenuItem.Text = "View Timeline";
            this.viewTimelineToolStripMenuItem.Click += new System.EventHandler(this.viewTimelineToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
            // 
            // reset
            // 
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(147, 22);
            this.reset.Text = "Reset Timers";
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // positionToolStripMenuItem
            // 
            this.positionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.posBR,
            this.posBL,
            this.bottomCenterToolStripMenuItem,
            this.posTR,
            this.posTL,
            this.posTC});
            this.positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            this.positionToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.positionToolStripMenuItem.Text = "Position";
            // 
            // posBR
            // 
            this.posBR.Name = "posBR";
            this.posBR.Size = new System.Drawing.Size(152, 22);
            this.posBR.Text = "Bottom Right";
            this.posBR.Click += new System.EventHandler(this.posBR_Click);
            // 
            // posBL
            // 
            this.posBL.Name = "posBL";
            this.posBL.Size = new System.Drawing.Size(152, 22);
            this.posBL.Text = "Bottom Left";
            this.posBL.Click += new System.EventHandler(this.posBL_Click);
            // 
            // bottomCenterToolStripMenuItem
            // 
            this.bottomCenterToolStripMenuItem.Name = "bottomCenterToolStripMenuItem";
            this.bottomCenterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bottomCenterToolStripMenuItem.Text = "Bottom Center";
            this.bottomCenterToolStripMenuItem.Click += new System.EventHandler(this.bottomCenterToolStripMenuItem_Click);
            // 
            // posTR
            // 
            this.posTR.Name = "posTR";
            this.posTR.Size = new System.Drawing.Size(152, 22);
            this.posTR.Text = "Top Right";
            this.posTR.Click += new System.EventHandler(this.posTR_Click);
            // 
            // posTL
            // 
            this.posTL.Name = "posTL";
            this.posTL.Size = new System.Drawing.Size(152, 22);
            this.posTL.Text = "Top Left";
            this.posTL.Click += new System.EventHandler(this.posTL_Click);
            // 
            // posTC
            // 
            this.posTC.Name = "posTC";
            this.posTC.Size = new System.Drawing.Size(152, 22);
            this.posTC.Text = "Top Center";
            this.posTC.Click += new System.EventHandler(this.posTC_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // TimerHolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(120, 25);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = global::ActiveWatcher.Properties.Resources.ActiveWatcherIcon;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimerHolder";
            this.ShowInTaskbar = false;
            this.Text = "ActiveWatch";
            this.TopMost = true;
            this.MouseEnter += new System.EventHandler(this.TimerHolder_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.TimerHolder_MouseLeave);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.NotifyIcon Notify;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem positionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posBR;
        private System.Windows.Forms.ToolStripMenuItem posBL;
        private System.Windows.Forms.ToolStripMenuItem posTR;
        private System.Windows.Forms.ToolStripMenuItem posTL;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem posTC;
        private System.Windows.Forms.ToolStripMenuItem bottomCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem reset;
        private System.Windows.Forms.ToolStripMenuItem viewTimelineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

