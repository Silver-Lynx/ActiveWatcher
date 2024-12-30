using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ActiveWatcher
{
	public partial class ProcessLabel : UserControl
	{
		public ProcessLabel()
		{
			InitializeComponent();
		}

		public void SetProcess(ProcessDetails process)
		{
			Title.Text = process.DisplayName;

			Icon.Image = process.Icon;

			if(process.TagIDs.Length == 0)
				Tags.Text = "No Tags Assigned";
			else
			{
				Tags.Text = "";
				foreach (DataManager.ProcessTag item in DataManager.TagList)
					if (process.TagIDs.Contains(item.ID))
						Tags.Text += item.TagName + ",";

				Tags.Text = Tags.Text.Substring(0, Tags.Text.Length - 1);
			}

			LastTime.Text = process.LastActive.ToString("h:mm tt MMM d yyyy");

			picColor.Image = new Bitmap(picColor.Width, picColor.Height);

			using (Graphics g = Graphics.FromImage(picColor.Image))
			{
				g.Clear(process.DisplayColor);
			}

			picColor.Refresh();
		}
	}
}
