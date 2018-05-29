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
        public string displayText { get; set; } = "No Label";

        public string toolTipText { get; set; } = "";

        internal double fillPercent { get; set; } = 1.0;


        public IconLabel()
        {
            InitializeComponent();
        }

        void resizeText()
        {
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));

            SizeF extent = g.MeasureString(displayText, this.textLabel.Font);

            float ratio = textLabel.Width / extent.Width;

            float newSize = textLabel.Font.Size * ratio;

            textLabel.Font = new Font(textLabel.Font.FontFamily, newSize, textLabel.Font.Style);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            if (this.Width > 0 && this.Height > 0)
            {
                int length = (int)(fillPercent * this.Width);

                Point p1 = new Point(0, 5);
                Point p2 = new Point(length, 5);

                LinearGradientBrush b = new LinearGradientBrush(
                            p1, p2,
                            Color.FromArgb(0, 0, 0, 0),
                            Color.FromArgb(30, 30, 30));

                e.Graphics.FillRectangle(b, 0, 0, length, this.Height);
            }
        }
    }
}
