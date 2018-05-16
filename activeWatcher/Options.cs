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
            numIdle.Value = Watcher.IDLEMAX;
            boxNumShow.Value = TimerHolder.instance.DisplayCount;
            
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            new RuleList().Show();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Watcher.IDLEMAX = (int)numIdle.Value;
            Watcher.DISPLAYCOUNT = (int)boxNumShow.Value;
            Watcher.HIDDENOPACITY = (double)numOpacity.Value / 100.0;
            Watcher.instance.saveConfig();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
