using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ActiveWatcher
{
    public partial class RuleList : Form
    {
        BindingList<Rule> binds;

        public RuleList()
        {
            InitializeComponent();
            binds = new BindingList<Rule>(Watcher.instance.Rules);
            dataViewer.DataSource = binds;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new RuleAdd(this).Show();
        }

        private void dataViewer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
        }

        internal void update()
        {
            binds.ResetBindings();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataViewer.SelectedRows)
            {
                Watcher.instance.removeRule((Rule)r.DataBoundItem);
            }
            update();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement rules = doc.CreateElement("Rules");

            foreach(Rule r in Watcher.instance.Rules)
            {
                //Build Rule section
                XmlElement rule = doc.CreateElement("Rule");

                XmlElement val = doc.CreateElement("Label");
                val.InnerText = r.ProcessName;
                rule.AppendChild(val);

                val = doc.CreateElement("Process");
                val.InnerText = r.processName;
                rule.AppendChild(val);

                val = doc.CreateElement("Limit");
                val.InnerText = r.valLimit.ToString();
                rule.AppendChild(val);

                val = doc.CreateElement("Action");
                val.InnerText = r.result.ToString();
                rule.AppendChild(val);

                //Append to rule list
                rules.AppendChild(rule);
            }
            //Add Rules to main document
            doc.AppendChild(rules);

            //Save to File
            doc.Save("Rules.xml");

            MessageBox.Show("Saved to file.","Save Rules");
        }
    }
}
