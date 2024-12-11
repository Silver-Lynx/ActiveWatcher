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
			this.Template = new System.Windows.Forms.Panel();
			this.Icon = new System.Windows.Forms.PictureBox();
			this.Title = new System.Windows.Forms.Label();
			this.Tags = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
			this.Table = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.Details.SuspendLayout();
			this.Template.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.Table.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// Details
			// 
			this.Details.Controls.Add(this.checkedListBox1);
			this.Details.Controls.Add(this.textBox1);
			this.Details.Controls.Add(this.pictureBox1);
			this.Details.Controls.Add(this.label4);
			this.Details.Controls.Add(this.label3);
			this.Details.Controls.Add(this.label2);
			this.Details.Dock = System.Windows.Forms.DockStyle.Right;
			this.Details.Location = new System.Drawing.Point(314, 0);
			this.Details.Name = "Details";
			this.Details.Size = new System.Drawing.Size(182, 450);
			this.Details.TabIndex = 1;
			// 
			// Template
			// 
			this.Template.BackColor = System.Drawing.SystemColors.Control;
			this.Template.Controls.Add(this.label1);
			this.Template.Controls.Add(this.Tags);
			this.Template.Controls.Add(this.Title);
			this.Template.Controls.Add(this.Icon);
			this.Template.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Template.Location = new System.Drawing.Point(1, 1);
			this.Template.Margin = new System.Windows.Forms.Padding(1);
			this.Template.Name = "Template";
			this.Template.Size = new System.Drawing.Size(312, 48);
			this.Template.TabIndex = 0;
			// 
			// Icon
			// 
			this.Icon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.Icon.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Icon.Location = new System.Drawing.Point(0, 0);
			this.Icon.Margin = new System.Windows.Forms.Padding(0);
			this.Icon.Name = "Icon";
			this.Icon.Size = new System.Drawing.Size(48, 48);
			this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Icon.TabIndex = 1;
			this.Icon.TabStop = false;
			// 
			// Title
			// 
			this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Title.Location = new System.Drawing.Point(54, 0);
			this.Title.Margin = new System.Windows.Forms.Padding(0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(128, 24);
			this.Title.TabIndex = 2;
			this.Title.Text = "Process Title";
			this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Tags
			// 
			this.Tags.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tags.Location = new System.Drawing.Point(54, 24);
			this.Tags.Margin = new System.Windows.Forms.Padding(0);
			this.Tags.Name = "Tags";
			this.Tags.Size = new System.Drawing.Size(253, 24);
			this.Tags.TabIndex = 3;
			this.Tags.Text = "No Tags";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 101);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Display Name";
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
			// label4
			// 
			this.label4.BackColor = System.Drawing.SystemColors.Control;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Location = new System.Drawing.Point(12, 63);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(158, 20);
			this.label4.TabIndex = 3;
			this.label4.Text = "ProcessNameHere";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.pictureBox1.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.pictureBox1.InitialImage = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.pictureBox1.Location = new System.Drawing.Point(15, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(15, 117);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(155, 20);
			this.textBox1.TabIndex = 5;
			// 
			// checkedListBox1
			// 
			this.checkedListBox1.FormattingEnabled = true;
			this.checkedListBox1.Location = new System.Drawing.Point(15, 172);
			this.checkedListBox1.Name = "checkedListBox1";
			this.checkedListBox1.Size = new System.Drawing.Size(155, 169);
			this.checkedListBox1.TabIndex = 6;
			// 
			// Table
			// 
			this.Table.BackColor = System.Drawing.SystemColors.ControlDark;
			this.Table.ColumnCount = 1;
			this.Table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.Table.Controls.Add(this.panel1, 0, 1);
			this.Table.Controls.Add(this.Template, 0, 0);
			this.Table.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Table.Location = new System.Drawing.Point(0, 0);
			this.Table.Name = "Table";
			this.Table.RowCount = 3;
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
			this.Table.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.Table.Size = new System.Drawing.Size(314, 450);
			this.Table.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label1.Location = new System.Drawing.Point(182, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 24);
			this.label1.TabIndex = 4;
			this.label1.Text = "12:34 PM Dec 11 2024";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.pictureBox2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(1, 51);
			this.panel1.Margin = new System.Windows.Forms.Padding(1);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(312, 48);
			this.panel1.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label5.Location = new System.Drawing.Point(182, 0);
			this.label5.Margin = new System.Windows.Forms.Padding(0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(131, 24);
			this.label5.TabIndex = 4;
			this.label5.Text = "12:34 PM Dec 11 2024";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label6.Location = new System.Drawing.Point(54, 24);
			this.label6.Margin = new System.Windows.Forms.Padding(0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(253, 24);
			this.label6.TabIndex = 3;
			this.label6.Text = "No Tags";
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(54, 0);
			this.label7.Margin = new System.Windows.Forms.Padding(0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(128, 24);
			this.label7.TabIndex = 2;
			this.label7.Text = "Process Title";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.pictureBox2.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.pictureBox2.Location = new System.Drawing.Point(0, 0);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(48, 48);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox2.TabIndex = 1;
			this.pictureBox2.TabStop = false;
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
			this.Template.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.Table.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel Details;
		private System.Windows.Forms.Panel Template;
		private System.Windows.Forms.PictureBox Icon;
		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.Label Tags;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckedListBox checkedListBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TableLayoutPanel Table;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.PictureBox pictureBox2;
	}
}