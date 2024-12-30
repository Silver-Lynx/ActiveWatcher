namespace ActiveWatcher
{
	partial class RuleItem
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
			this.Condition = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Condition
			// 
			this.Condition.Location = new System.Drawing.Point(3, 25);
			this.Condition.Name = "Condition";
			this.Condition.Size = new System.Drawing.Size(394, 25);
			this.Condition.TabIndex = 0;
			this.Condition.Text = "Condition";
			this.Condition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Condition.Click += new System.EventHandler(this.label1_Click);
			// 
			// RuleItem
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Condition);
			this.Name = "RuleItem";
			this.Size = new System.Drawing.Size(400, 50);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label Condition;
	}
}
