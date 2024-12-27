using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ActiveWatcher
{
	public partial class Processes : Form
	{
		public Processes()
		{
			InitializeComponent();

			foreach (ProcessDetails item in Watcher.instance.procManager.processList)
			{
				ProcessLabel hold = new ProcessLabel();
				hold.SetProcess(item);

				Table.RowCount++;
				Table.Controls.Add(hold);

				ProcessDetails link = item;
				EventHandler call = (s, e) => { Console.WriteLine("Show item"); ShowProcess(link); };
				hold.Controls[0].Click += call;

				//Add the function to EVERY CHILD because Microsoft are jackasses
				foreach (Control child in hold.Controls[0].Controls)
				{
					child.Click += call;
				}
			}
		}

		public void ShowProcess(ProcessDetails process)
		{
			Bitmap showColor = new Bitmap(50,50);

			using (Graphics g = Graphics.FromImage(showColor))
			{
				g.Clear(process.Color);
			}

			Color.Image = showColor;
			Icon.Image = process.Icon;
			DisplayName.Text = process.DisplayName;
			ProcessName.Text = process.Descriptor;
		}
	}
}
