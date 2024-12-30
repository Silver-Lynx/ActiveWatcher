namespace ActiveWatcher
{
    partial class Settings
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
			this.LmaxProcs = new System.Windows.Forms.Label();
			this.boxNumShow = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.numIdle = new System.Windows.Forms.NumericUpDown();
			this.btnApply = new System.Windows.Forms.Button();
			this.numOpacity = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.CBIgnoreMouse = new System.Windows.Forms.CheckBox();
			this.CBShowTotal = new System.Windows.Forms.CheckBox();
			this.CBstartup = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabSettings = new System.Windows.Forms.TabPage();
			this.tabCurrent = new System.Windows.Forms.TabPage();
			this.btnClose = new System.Windows.Forms.Button();
			this.tabProg = new System.Windows.Forms.TabPage();
			this.tabRules = new System.Windows.Forms.TabPage();
			this.processes1 = new ActiveWatcher.Processes();
			((System.ComponentModel.ISupportInitialize)(this.boxNumShow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numIdle)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numOpacity)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabSettings.SuspendLayout();
			this.tabProg.SuspendLayout();
			this.SuspendLayout();
			// 
			// LmaxProcs
			// 
			this.LmaxProcs.Location = new System.Drawing.Point(208, 119);
			this.LmaxProcs.Name = "LmaxProcs";
			this.LmaxProcs.Size = new System.Drawing.Size(83, 13);
			this.LmaxProcs.TabIndex = 0;
			this.LmaxProcs.Text = "Timers Shown:";
			// 
			// boxNumShow
			// 
			this.boxNumShow.Location = new System.Drawing.Point(297, 117);
			this.boxNumShow.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.boxNumShow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.boxNumShow.Name = "boxNumShow";
			this.boxNumShow.Size = new System.Drawing.Size(59, 20);
			this.boxNumShow.TabIndex = 1;
			this.boxNumShow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.boxNumShow.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(208, 151);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Idle Time (Sec):";
			// 
			// numIdle
			// 
			this.numIdle.Location = new System.Drawing.Point(297, 149);
			this.numIdle.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
			this.numIdle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numIdle.Name = "numIdle";
			this.numIdle.Size = new System.Drawing.Size(59, 20);
			this.numIdle.TabIndex = 7;
			this.numIdle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numIdle.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(242, 314);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 9;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// numOpacity
			// 
			this.numOpacity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numOpacity.Location = new System.Drawing.Point(297, 181);
			this.numOpacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.numOpacity.Name = "numOpacity";
			this.numOpacity.Size = new System.Drawing.Size(59, 20);
			this.numOpacity.TabIndex = 11;
			this.numOpacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numOpacity.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(208, 183);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Timer Opactiy";
			// 
			// CBIgnoreMouse
			// 
			this.CBIgnoreMouse.AutoSize = true;
			this.CBIgnoreMouse.Location = new System.Drawing.Point(218, 225);
			this.CBIgnoreMouse.Name = "CBIgnoreMouse";
			this.CBIgnoreMouse.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.CBIgnoreMouse.Size = new System.Drawing.Size(123, 17);
			this.CBIgnoreMouse.TabIndex = 12;
			this.CBIgnoreMouse.Text = "Ignore Mouse Hover";
			this.CBIgnoreMouse.UseVisualStyleBackColor = true;
			// 
			// CBShowTotal
			// 
			this.CBShowTotal.AutoSize = true;
			this.CBShowTotal.Location = new System.Drawing.Point(222, 248);
			this.CBShowTotal.Name = "CBShowTotal";
			this.CBShowTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.CBShowTotal.Size = new System.Drawing.Size(119, 17);
			this.CBShowTotal.TabIndex = 13;
			this.CBShowTotal.Text = "Show Session Time";
			this.CBShowTotal.UseVisualStyleBackColor = true;
			// 
			// CBstartup
			// 
			this.CBstartup.AutoSize = true;
			this.CBstartup.Location = new System.Drawing.Point(237, 271);
			this.CBstartup.Name = "CBstartup";
			this.CBstartup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.CBstartup.Size = new System.Drawing.Size(104, 17);
			this.CBstartup.TabIndex = 15;
			this.CBstartup.Text = "Open on Startup";
			this.CBstartup.UseVisualStyleBackColor = true;
			this.CBstartup.CheckedChanged += new System.EventHandler(this.CBstartup_CheckedChanged);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabCurrent);
			this.tabControl1.Controls.Add(this.tabProg);
			this.tabControl1.Controls.Add(this.tabRules);
			this.tabControl1.Controls.Add(this.tabSettings);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(560, 508);
			this.tabControl1.TabIndex = 17;
			// 
			// tabSettings
			// 
			this.tabSettings.Controls.Add(this.boxNumShow);
			this.tabSettings.Controls.Add(this.LmaxProcs);
			this.tabSettings.Controls.Add(this.CBstartup);
			this.tabSettings.Controls.Add(this.label1);
			this.tabSettings.Controls.Add(this.CBShowTotal);
			this.tabSettings.Controls.Add(this.numIdle);
			this.tabSettings.Controls.Add(this.CBIgnoreMouse);
			this.tabSettings.Controls.Add(this.btnApply);
			this.tabSettings.Controls.Add(this.numOpacity);
			this.tabSettings.Controls.Add(this.label2);
			this.tabSettings.Location = new System.Drawing.Point(4, 22);
			this.tabSettings.Name = "tabSettings";
			this.tabSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabSettings.Size = new System.Drawing.Size(552, 482);
			this.tabSettings.TabIndex = 0;
			this.tabSettings.Text = "Settings";
			this.tabSettings.UseVisualStyleBackColor = true;
			// 
			// tabCurrent
			// 
			this.tabCurrent.Location = new System.Drawing.Point(4, 22);
			this.tabCurrent.Name = "tabCurrent";
			this.tabCurrent.Padding = new System.Windows.Forms.Padding(3);
			this.tabCurrent.Size = new System.Drawing.Size(552, 482);
			this.tabCurrent.TabIndex = 1;
			this.tabCurrent.Text = "Current";
			this.tabCurrent.UseVisualStyleBackColor = true;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(497, 526);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 18;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// tabProg
			// 
			this.tabProg.Controls.Add(this.processes1);
			this.tabProg.Location = new System.Drawing.Point(4, 22);
			this.tabProg.Name = "tabProg";
			this.tabProg.Size = new System.Drawing.Size(552, 482);
			this.tabProg.TabIndex = 2;
			this.tabProg.Text = "Programs";
			this.tabProg.UseVisualStyleBackColor = true;
			// 
			// tabRules
			// 
			this.tabRules.Location = new System.Drawing.Point(4, 22);
			this.tabRules.Name = "tabRules";
			this.tabRules.Size = new System.Drawing.Size(552, 482);
			this.tabRules.TabIndex = 3;
			this.tabRules.Text = "Rules";
			this.tabRules.UseVisualStyleBackColor = true;
			// 
			// processes1
			// 
			this.processes1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.processes1.Location = new System.Drawing.Point(0, 0);
			this.processes1.Name = "processes1";
			this.processes1.Size = new System.Drawing.Size(552, 482);
			this.processes1.TabIndex = 0;
			this.processes1.Load += new System.EventHandler(this.processes1_Load);
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(584, 561);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tabControl1);
			this.Icon = global::ActiveWatcher.Properties.Resources.ActiveWatcherIcon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Options";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ActiveWatcher";
			((System.ComponentModel.ISupportInitialize)(this.boxNumShow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numIdle)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numOpacity)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabSettings.ResumeLayout(false);
			this.tabSettings.PerformLayout();
			this.tabProg.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LmaxProcs;
        private System.Windows.Forms.NumericUpDown boxNumShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numIdle;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.NumericUpDown numOpacity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CBIgnoreMouse;
        private System.Windows.Forms.CheckBox CBShowTotal;
		private System.Windows.Forms.CheckBox CBstartup;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabSettings;
		private System.Windows.Forms.TabPage tabCurrent;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TabPage tabProg;
		private System.Windows.Forms.TabPage tabRules;
		private Processes processes1;
	}
}