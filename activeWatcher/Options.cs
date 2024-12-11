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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            numIdle.Value = Watcher.settings.IDLEMAX;
            boxNumShow.Value = TimerHolder.instance.DisplayCount;
            numOpacity.Value = (decimal)(Watcher.settings.HIDDENOPACITY*100.0);
            CBIgnoreMouse.Checked = Watcher.settings.PASSTHROUGH;
            CBShowTotal.Checked = Watcher.settings.SHOWTOTAL;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //Set Variables
            Watcher.settings.IDLEMAX = (int)numIdle.Value;
            Watcher.settings.DISPLAYCOUNT = (int)boxNumShow.Value;
            Watcher.settings.HIDDENOPACITY = (double)numOpacity.Value / 100.0;
            Watcher.settings.PASSTHROUGH = CBIgnoreMouse.Checked;
            Watcher.settings.SHOWTOTAL = CBShowTotal.Checked;

            TimerHolder.instance.redraw();

            //Save to init file
            DataManager.SaveConfig(Watcher.settings);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void btnRules_Click(object sender, EventArgs e)
		{
			new RuleList().Show();
		}

		private void btnPrograms_Click(object sender, EventArgs e)
		{
            new Processes().Show();
		}
	}
}
