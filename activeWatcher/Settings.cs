using Microsoft.Win32;
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
	public partial class Settings : Form
	{
		public Settings()
		{
			InitializeComponent();
			numIdle.Value = Watcher.settings.IDLEMAX;
			boxNumShow.Value = TimerList.instance.DisplayCount;
			numOpacity.Value = (decimal)(Watcher.settings.HIDDENOPACITY * 100.0);
			CBIgnoreMouse.Checked = Watcher.settings.PASSTHROUGH;
			CBShowTotal.Checked = Watcher.settings.SHOWTOTAL;

			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
			{
				if (key == null)
					return;

				CBstartup.Checked = key.GetValue("ActiveWatcher") != null;
			}
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			//Set Variables
			Watcher.settings.IDLEMAX = (int)numIdle.Value;
			Watcher.settings.DISPLAYCOUNT = (int)boxNumShow.Value;
			Watcher.settings.HIDDENOPACITY = (double)numOpacity.Value / 100.0;
			Watcher.settings.PASSTHROUGH = CBIgnoreMouse.Checked;
			Watcher.settings.SHOWTOTAL = CBShowTotal.Checked;

			TimerList.instance.Redraw();

			//Save to init file
			DataManager.SaveConfig(Watcher.settings);
		}

		private void btnRules_Click(object sender, EventArgs e)
		{
			new RuleList().Show();
		}

		private void btnPrograms_Click(object sender, EventArgs e)
		{
			new Processes().Show();
		}

		private void CBstartup_CheckedChanged(object sender, EventArgs e)
		{
			using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
			{
				if (key == null)
				{
					MessageBox.Show("Could not find the startup registry.\nCannot add to startup.");
					return;
				}

				if (CBstartup.Checked)
					key.SetValue("ActiveWatcher", Application.ExecutablePath);

				else if (key.GetValue("ActiveWatcher") != null)
					key.DeleteValue("ActiveWatcher");
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void processes1_Load(object sender, EventArgs e)
		{
			processes1.LoadLists();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}

