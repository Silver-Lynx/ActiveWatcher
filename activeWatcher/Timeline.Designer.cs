namespace ActiveWatcher
{
    partial class Timeline
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
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.dateType = new System.Windows.Forms.ComboBox();
            this.dateValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.labelPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dateValue)).BeginInit();
            this.SuspendLayout();
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cartesianChart1.ForeColor = System.Drawing.Color.White;
            this.cartesianChart1.Location = new System.Drawing.Point(12, 41);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(473, 434);
            this.cartesianChart1.TabIndex = 0;
            this.cartesianChart1.Text = "timeChart";
            // 
            // dateType
            // 
            this.dateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dateType.FormattingEnabled = true;
            this.dateType.Items.AddRange(new object[] {
            "Hours",
            "Days",
            "Months",
            "Years"});
            this.dateType.Location = new System.Drawing.Point(165, 14);
            this.dateType.Name = "dateType";
            this.dateType.Size = new System.Drawing.Size(80, 21);
            this.dateType.TabIndex = 1;
            // 
            // dateValue
            // 
            this.dateValue.Location = new System.Drawing.Point(109, 14);
            this.dateValue.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.dateValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dateValue.Name = "dateValue";
            this.dateValue.Size = new System.Drawing.Size(50, 20);
            this.dateValue.TabIndex = 2;
            this.dateValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Display last";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(251, 12);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 25);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // labelPanel
            // 
            this.labelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPanel.Location = new System.Drawing.Point(491, 41);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(150, 416);
            this.labelPanel.TabIndex = 7;
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(652, 487);
            this.Controls.Add(this.labelPanel);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateValue);
            this.Controls.Add(this.dateType);
            this.Controls.Add(this.cartesianChart1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = global::ActiveWatcher.Properties.Resources.ActiveWatcherIcon;
            this.Name = "Timeline";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Computer Usage Timeline";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dateValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.ComboBox dateType;
        private System.Windows.Forms.NumericUpDown dateValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Panel labelPanel;
    }
}