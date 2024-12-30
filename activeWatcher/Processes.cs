using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace ActiveWatcher
{
	public partial class Processes : UserControl
	{
		ProcessLabel activeLabel;
		ProcessDetails activeProcess;

		public Processes()
		{
			InitializeComponent();
		}

		public void LoadLists()
		{
			foreach (ProcessDetails item in Watcher.instance.procManager.processList)
			{
				ProcessLabel hold = new ProcessLabel();
				hold.SetProcess(item);

				Table.RowCount++;
				Table.Controls.Add(hold);
				hold.Dock = DockStyle.Fill;

				ProcessDetails link = item;
				EventHandler call = (s, e) => { Console.WriteLine("Show item"); activeLabel = hold; ShowProcess(link); };
				hold.Controls[0].Click += call;

				//Add the click function to EVERY CHILD because Microsoft are jackasses
				foreach (Control child in hold.Controls[0].Controls)
				{
					child.Click += call;
				}
			}

			TagList.Items.Clear();

			foreach (DataManager.ProcessTag item in DataManager.TagList)
			{
				TagList.Items.Add(item);
			}
		}

		public void ShowProcess(ProcessDetails process)
		{
			Bitmap showColor = new Bitmap(50,50);

			using (Graphics g = Graphics.FromImage(showColor))
			{
				g.Clear(process.DisplayColor);
			}

			PopupColor.Color = process.DisplayColor;

			Color.Image = showColor;
			Icon.Image = process.Icon;
			DisplayName.Text = process.DisplayName;
			ProcessName.Text = process.Descriptor;

			activeProcess = process;

			if (process.TagIDs == null)
			{
				process.TagIDs = new int[0];
			}

			for (int i = 0; i < TagList.Items.Count; i++)
			{
				if(TagList.Items[i] is DataManager.ProcessTag t)
					TagList.SetItemChecked(i, process.TagIDs.Contains(t));
			}
		}

		private void Color_Click(object sender, EventArgs e)
		{
			if (activeProcess == null) return;

			DialogResult r = PopupColor.ShowDialog();

			Bitmap showColor = new Bitmap(50, 50);

			using (Graphics g = Graphics.FromImage(showColor))
			{
				g.Clear(PopupColor.Color);
			}

			Color.Image = showColor;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if(activeProcess == null) return;

			activeProcess.DisplayName = DisplayName.Text;

			if(Color.Image != null)
				activeProcess.DisplayColor = ((Bitmap)Color.Image).GetPixel(0,0);

			Console.WriteLine(((Bitmap)Color.Image).GetPixel(0, 0));

			List<DataManager.ProcessTag> tags = new List<DataManager.ProcessTag>();

			foreach(object raw in TagList.CheckedItems)
			{
				if (raw is DataManager.ProcessTag t)
					tags.Add(t);
			}
			
			activeProcess.TagIDs = new int[tags.Count];
			for (int i = 0; i < tags.Count; i++)
			{
				activeProcess.TagIDs[i] = tags[i];
			}

			//Refresh all label fields
			if (activeLabel != null)
				activeLabel.SetProcess(activeProcess);

			DataManager.SaveProcesses(Watcher.instance.procManager.processList);
		}
	}
}
