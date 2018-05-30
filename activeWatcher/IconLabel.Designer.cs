namespace ActiveWatcher
{
    partial class IconLabel
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
            this.picLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // picLabel
            // 
            this.picLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.picLabel.BackColor = System.Drawing.Color.Transparent;
            this.picLabel.Location = new System.Drawing.Point(0, 0);
            this.picLabel.Name = "picLabel";
            this.picLabel.Size = new System.Drawing.Size(32, 28);
            this.picLabel.TabIndex = 0;
            // 
            // textLabel
            // 
            this.textLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLabel.BackColor = System.Drawing.Color.Transparent;
            this.textLabel.Font = new System.Drawing.Font("Monospac821 BT", 10F);
            this.textLabel.ForeColor = System.Drawing.Color.White;
            this.textLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.textLabel.Location = new System.Drawing.Point(32, 0);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(118, 28);
            this.textLabel.TabIndex = 0;
            this.textLabel.Text = "Microsoft Visual Studio";
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IconLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.picLabel);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "IconLabel";
            this.Size = new System.Drawing.Size(150, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label picLabel;
        private System.Windows.Forms.Label textLabel;
    }
}
