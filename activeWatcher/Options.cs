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
            numIdle.Value = Watcher.idleMax;
            boxNumShow.Value = TimerHolder.instance.DisplayCount;
            
        }

        private void btnAddRule_Click(object sender, EventArgs e)
        {
            new RuleList().Show();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Watcher.idleMax = (int)numIdle.Value;
            TimerHolder.instance.DisplayCount = (int)boxNumShow.Value;
            Watcher.instance.saveConfig();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
