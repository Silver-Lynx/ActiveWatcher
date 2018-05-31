using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ActiveWatcher
{
    public partial class IconLabel : UserControl
    {
        public string displayText { get { return textLabel.Text; } set { textLabel.Text = value; textLabel.Invalidate(); } }

        public string toolTipText { get; set; } = "";

        internal double fillPercent { get; set; } = 1.0;

        internal Image Image { get { return picLabel.Image; } set { picLabel.Image = value; picLabel.Invalidate(); } }

        public IconLabel()
        {
            InitializeComponent();
        }

        internal void resizeText()
        {
            //Make graphics object to check font size
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            SizeF extent = g.MeasureString(displayText, this.textLabel.Font);

            //Find scaling ratio
            float ratio = textLabel.Width / extent.Width;
            //Dont make bigger, only smaller
            if (ratio > 1.0) return;
            float newSize = textLabel.Font.Size * ratio;

            //Set font size
            textLabel.Font = new Font(textLabel.Font.FontFamily, newSize, textLabel.Font.Style);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this.Width > 0 && this.Height > 0 && (int)(fillPercent * (this.Width - 32)) > 0 && this.Height > 0)
            {
                int length = (int)(fillPercent * (this.Width-32));

                Point p1 = new Point(32, 5);
                Point p2 = new Point(32+length, 5);

                LinearGradientBrush b = new LinearGradientBrush(
                            p1, p2,
                            Color.FromArgb(0, 0, 0, 0),
                            Color.FromArgb(80, 80, 80));

                e.Graphics.FillRectangle(b, 32, 0, length, this.Height);
            }
        }
    }
}
