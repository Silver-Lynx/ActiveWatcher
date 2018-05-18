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
    public partial class RuleAdd : Form
    {
        RuleList parent;

        public RuleAdd(RuleList rl)
        {
            InitializeComponent();
            parent = rl;

            #region Dropdown - Program list
            //Make set of key,value pairs to display in list
            KeyValuePair<string, string>[] programData = new KeyValuePair<string, string>[Watcher.instance.procManager.getProcessCount()+1];
            programData[0] = new KeyValuePair<string, string>("*", "Any Program");

            //Populate array with the process values
            int ptr = 1;
            foreach(WProcess process in Watcher.instance.procManager.processes.Values)
            {
                //Dont include this program, or idle processes
                if (process.processName == "ActiveWatcher" || process.processName == "Idle" || process.processName == Watcher.IDLENAME) continue;

                //Add the program root process as key and the display name as value
                programData[ptr++] = new KeyValuePair<string, string>(process.processName, process.commonName);
            }
            //Set properties on Dropdown
            ddProgram.DataSource = programData;
            ddProgram.ValueMember = "Key";
            ddProgram.DisplayMember = "Value";
            #endregion

            #region Dropdown - Other Choices
            //Time type
            ddTime.DataSource = new string[] { "sec", "min"};
            ddTime.SelectedIndex = 0;

            //Action Type
            KeyValuePair<Rule.RuleResult, string>[] actionData = new KeyValuePair<Rule.RuleResult, string>[] {
                new KeyValuePair<Rule.RuleResult, string>(Rule.RuleResult.Sound,"Play Sound"),
                new KeyValuePair<Rule.RuleResult, string>(Rule.RuleResult.MessageBox,"Show Popup Message"),
                new KeyValuePair<Rule.RuleResult, string>(Rule.RuleResult.Minimize,"Minimize program"),
                new KeyValuePair<Rule.RuleResult, string>(Rule.RuleResult.Kill,"Force kill the process (Risky!)") };
            ddAction.DataSource = actionData;
            ddAction.ValueMember = "Key";
            ddAction.DisplayMember = "Value";
            #endregion
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            Watcher.instance.addRule(new Rule(((KeyValuePair<string, string>)ddProgram.SelectedItem).Value,
                (string)ddProgram.SelectedValue,
                false,
                (int)numericUpDown1.Value * ((string)ddTime.SelectedValue == "sec" ? 1 : 60),
                (Rule.RuleResult)ddAction.SelectedValue));
            parent?.update();
            this.Close();
        }
    }
}
