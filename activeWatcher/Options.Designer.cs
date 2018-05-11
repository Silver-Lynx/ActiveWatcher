namespace ActiveWatcher
{
    partial class Options
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
            this.btnAddRule = new System.Windows.Forms.Button();
            this.numIdle = new System.Windows.Forms.NumericUpDown();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.boxNumShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdle)).BeginInit();
            this.SuspendLayout();
            this.Icon = ActiveWatcher.Properties.Resources.ActiveWatcherIcon;
            // 
            // LmaxProcs
            // 
            this.LmaxProcs.Location = new System.Drawing.Point(12, 20);
            this.LmaxProcs.Name = "LmaxProcs";
            this.LmaxProcs.Size = new System.Drawing.Size(83, 13);
            this.LmaxProcs.TabIndex = 0;
            this.LmaxProcs.Text = "Number Shown:";
            // 
            // boxNumShow
            // 
            this.boxNumShow.Location = new System.Drawing.Point(101, 16);
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
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Idle Time (Sec):";
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(12, 94);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(83, 23);
            this.btnAddRule.TabIndex = 4;
            this.btnAddRule.Text = "Rules List...";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // numIdle
            // 
            this.numIdle.Location = new System.Drawing.Point(101, 53);
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
            this.btnApply.Location = new System.Drawing.Point(150, 94);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 9;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 129);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.numIdle);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.boxNumShow);
            this.Controls.Add(this.LmaxProcs);
            this.Name = "Options";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.boxNumShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LmaxProcs;
        private System.Windows.Forms.NumericUpDown boxNumShow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.NumericUpDown numIdle;
        private System.Windows.Forms.Button btnApply;
    }
}