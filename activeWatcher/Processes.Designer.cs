namespace ActiveWatcher
{
	partial class Processes
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
			this.Details = new System.Windows.Forms.Panel();
			this.TagList = new System.Windows.Forms.CheckedListBox();
			this.DisplayName = new System.Windows.Forms.TextBox();
			this.Icon = new System.Windows.Forms.PictureBox();
			this.ProcessName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Table = new System.Windows.Forms.TableLayoutPanel();
			this.Color = new System.Windows.Forms.PictureBox();
			this.Details.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Color)).BeginInit();
			this.SuspendLayout();
			// 
			// Details
			// 
			this.Details.Controls.Add(this.Color);
			this.Details.Controls.Add(this.TagList);
			this.Details.Controls.Add(this.DisplayName);
			this.Details.Controls.Add(this.Icon);
			this.Details.Controls.Add(this.ProcessName);
			this.Details.Controls.Add(this.label3);
			this.Details.Controls.Add(this.label2);
			this.Details.Dock = System.Windows.Forms.DockStyle.Right;
			this.Details.Location = new System.Drawing.Point(314, 0);
			this.Details.Name = "Details";
			this.Details.Size = new System.Drawing.Size(182, 450);
			this.Details.TabIndex = 1;
			// 
			// TagList
			// 
			this.TagList.FormattingEnabled = true;
			this.TagList.Location = new System.Drawing.Point(15, 172);
			this.TagList.Name = "TagList";
			this.TagList.Size = new System.Drawing.Size(155, 169);
			this.TagList.TabIndex = 6;
			// 
			// DisplayName
			// 
			this.DisplayName.Location = new System.Drawing.Point(15, 111);
			this.DisplayName.Name = "DisplayName";
			this.DisplayName.Size = new System.Drawing.Size(155, 20);
			this.DisplayName.TabIndex = 5;
			// 
			// Icon
			// 
			this.Icon.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Icon.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Icon.InitialImage = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Icon.Location = new System.Drawing.Point(15, 12);
			this.Icon.Name = "Icon";
			this.Icon.Size = new System.Drawing.Size(48, 48);
			this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Icon.TabIndex = 4;
			this.Icon.TabStop = false;
			// 
			// ProcessName
			// 
			this.ProcessName.BackColor = System.Drawing.SystemColors.Control;
			this.ProcessName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ProcessName.Location = new System.Drawing.Point(12, 63);
			this.ProcessName.Name = "ProcessName";
			this.ProcessName.Size = new System.Drawing.Size(158, 20);
			this.ProcessName.TabIndex = 3;
			this.ProcessName.Text = "ProcessNameHere";
			this.ProcessName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 156);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(31, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Tags";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 95);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Display Name";
			// 
			// Table
			// 
			this.Table.AutoScroll = true;
			this.Table.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Table.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.Table.ColumnCount = 1;
			this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Table.Location = new System.Drawing.Point(0, 0);
			this.Table.Name = "Table";
			this.Table.RowCount = 2;
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.Table.Size = new System.Drawing.Size(314, 450);
			this.Table.TabIndex = 1;
			// 
			// Color
			// 
			this.Color.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Color.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Color.InitialImage = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Color.Location = new System.Drawing.Point(69, 12);
			this.Color.Name = "Color";
			this.Color.Size = new System.Drawing.Size(48, 48);
			this.Color.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Color.TabIndex = 7;
			this.Color.TabStop = false;
			// 
			// Processes
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(496, 450);
			this.Controls.Add(this.Table);
			this.Controls.Add(this.Details);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Processes";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Known Processes";
			this.Details.ResumeLayout(false);
			this.Details.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Color)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel Details;
		private System.Windows.Forms.Label ProcessName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox Icon;
		private System.Windows.Forms.CheckedListBox TagList;
		private System.Windows.Forms.TextBox DisplayName;
		private System.Windows.Forms.TableLayoutPanel Table;
		private System.Windows.Forms.PictureBox Color;
	}
}