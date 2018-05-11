namespace ActiveWatcher
{
    partial class RuleAdd
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
            this.ddProgram = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ddTime = new System.Windows.Forms.ComboBox();
            this.ddCompare = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ddAction = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // ddProgram
            // 
            this.ddProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddProgram.FormattingEnabled = true;
            this.ddProgram.Location = new System.Drawing.Point(121, 16);
            this.ddProgram.Name = "ddProgram";
            this.ddProgram.Size = new System.Drawing.Size(293, 21);
            this.ddProgram.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "When this program:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Has been active for:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(121, 55);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown1.TabIndex = 3;
            // 
            // ddTime
            // 
            this.ddTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddTime.Items.AddRange(new object[] {
            "sec",
            "min",
            "hr",
            "%"});
            this.ddTime.Location = new System.Drawing.Point(193, 54);
            this.ddTime.Name = "ddTime";
            this.ddTime.Size = new System.Drawing.Size(51, 21);
            this.ddTime.TabIndex = 4;
            // 
            // ddCompare
            // 
            this.ddCompare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddCompare.FormattingEnabled = true;
            this.ddCompare.Location = new System.Drawing.Point(250, 54);
            this.ddCompare.Name = "ddCompare";
            this.ddCompare.Size = new System.Drawing.Size(164, 21);
            this.ddCompare.TabIndex = 5;
            this.ddCompare.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Perform this action:";
            // 
            // ddAction
            // 
            this.ddAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddAction.FormattingEnabled = true;
            this.ddAction.Location = new System.Drawing.Point(121, 92);
            this.ddAction.Name = "ddAction";
            this.ddAction.Size = new System.Drawing.Size(293, 21);
            this.ddAction.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(339, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(258, 132);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 9;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.apply_Click);
            // 
            // RuleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 167);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.ddAction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ddCompare);
            this.Controls.Add(this.ddTime);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ddProgram);
            this.Icon = global::ActiveWatcher.Properties.Resources.ActiveWatcherIcon;
            this.Name = "RuleAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Rule";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox ddTime;
        private System.Windows.Forms.ComboBox ddCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ddAction;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnApply;
    }
}