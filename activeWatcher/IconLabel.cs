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

        internal double fillPercent { get; set; } = 1.0;

        internal Image Image { get { return picLabel.Image; } set { picLabel.Image = value; picLabel.Invalidate(); } }

        public IconLabel()
        {
            InitializeComponent();
        }

        internal void setToolTip(string text)
        {
            toolTip1.SetToolTip(picLabel, text);
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

            //Try to find spaces to make two lines
            string[] split = displayText.Split(' ');
            string before = split[0];
            string after = "";
            int pnt = 1;

            //Make sure second line is at least 5 characters
            while (pnt < split.Length && after.Length < 5)
            {
                after = split[split.Length - pnt] + (after.Length == 0 ? "" : " " + after);
                ++pnt;
            }

            //New pointer to fill in before string
            int pnt2 = 1;
            while (pnt2 <= split.Length - pnt)
            {
                before = before + " " + split[pnt2];
                ++pnt2;
            }


            //Refine scaling process using new strings (Only use two lines if there were results)
            extent = g.MeasureString(before + (after == "" ? "" : "\n" + after), this.textLabel.Font);

            //Find scaling ratios
            float wratio = textLabel.Width / extent.Width;
            float hratio = textLabel.Height / extent.Height;

            //If double line isn't 25% better, use single line
            if (hratio < ratio * 1.25f)
            {
                before = before + " " + after;
                after = "";
            }
            //Else, finish double line
            else
                //Take smaller value
                ratio = wratio < hratio ? wratio : hratio;

            if (ratio > 1.0f) ratio = 1.0f;

            float newSize = textLabel.Font.Size * ratio * 0.85f;

            //Set font size
            textLabel.Font = new Font(textLabel.Font.FontFamily, newSize, textLabel.Font.Style);
            textLabel.Text = before + (after == "" ? "" : "\n" + after);
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
