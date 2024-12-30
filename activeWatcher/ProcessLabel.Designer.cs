namespace ActiveWatcher
{
	partial class ProcessLabel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Template = new System.Windows.Forms.Panel();
			this.picColor = new System.Windows.Forms.PictureBox();
			this.LastTime = new System.Windows.Forms.Label();
			this.Tags = new System.Windows.Forms.Label();
			this.Title = new System.Windows.Forms.Label();
			this.Icon = new System.Windows.Forms.PictureBox();
			this.Template.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// Template
			// 
			this.Template.BackColor = System.Drawing.SystemColors.Control;
			this.Template.Controls.Add(this.picColor);
			this.Template.Controls.Add(this.LastTime);
			this.Template.Controls.Add(this.Tags);
			this.Template.Controls.Add(this.Title);
			this.Template.Controls.Add(this.Icon);
			this.Template.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Template.Location = new System.Drawing.Point(0, 0);
			this.Template.Margin = new System.Windows.Forms.Padding(1);
			this.Template.Name = "Template";
			this.Template.Size = new System.Drawing.Size(321, 50);
			this.Template.TabIndex = 1;
			// 
			// picColor
			// 
			this.picColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.picColor.BackColor = System.Drawing.Color.Black;
			this.picColor.InitialImage = null;
			this.picColor.Location = new System.Drawing.Point(0, 0);
			this.picColor.Margin = new System.Windows.Forms.Padding(0);
			this.picColor.Name = "picColor";
			this.picColor.Size = new System.Drawing.Size(16, 50);
			this.picColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picColor.TabIndex = 5;
			this.picColor.TabStop = false;
			// 
			// LastTime
			// 
			this.LastTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LastTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LastTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.LastTime.Location = new System.Drawing.Point(202, 0);
			this.LastTime.Margin = new System.Windows.Forms.Padding(0);
			this.LastTime.Name = "LastTime";
			this.LastTime.Size = new System.Drawing.Size(120, 24);
			this.LastTime.TabIndex = 4;
			this.LastTime.Text = "12:34 PM Dec 11 2024";
			this.LastTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// Tags
			// 
			this.Tags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Tags.Location = new System.Drawing.Point(66, 26);
			this.Tags.Margin = new System.Windows.Forms.Padding(0);
			this.Tags.Name = "Tags";
			this.Tags.Size = new System.Drawing.Size(255, 24);
			this.Tags.TabIndex = 3;
			this.Tags.Text = "No Tags";
			// 
			// Title
			// 
			this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Title.AutoEllipsis = true;
			this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Title.Location = new System.Drawing.Point(66, 0);
			this.Title.Margin = new System.Windows.Forms.Padding(0);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(136, 24);
			this.Title.TabIndex = 2;
			this.Title.Text = "Process Title";
			this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Icon
			// 
			this.Icon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.Icon.Image = global::ActiveWatcher.Properties.Resources.ZZZ;
			this.Icon.Location = new System.Drawing.Point(16, 0);
			this.Icon.Margin = new System.Windows.Forms.Padding(0);
			this.Icon.Name = "Icon";
			this.Icon.Size = new System.Drawing.Size(50, 50);
			this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Icon.TabIndex = 1;
			this.Icon.TabStop = false;
			// 
			// ProcessLabel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.Template);
			this.Name = "ProcessLabel";
			this.Size = new System.Drawing.Size(321, 50);
			this.Template.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel Template;
		private System.Windows.Forms.Label LastTime;
		private System.Windows.Forms.Label Tags;
		private System.Windows.Forms.Label Title;
		private System.Windows.Forms.PictureBox Icon;
		private System.Windows.Forms.PictureBox picColor;
	}
}
