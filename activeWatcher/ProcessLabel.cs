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

			Tags.Text = "No Tags Assigned";

			LastTime.Text = process.LastActive.ToString("h:mm tt MMM d yyyy");
		}
	}
}
