using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveWatcher
{
    class Rule
    {
        public enum RuleResult
        {
            Sound,
            MessageBox,
            Minimize,
            Swap,
            Kill
        }
        public bool active = false;
        string humanName;
        public string ProcessName { get { return humanName; } }
        internal string processName;
        internal int valLimit;
        bool limitPercent;
        internal RuleResult result;
        public string ActionTaken
        {
            get
            {
                switch (result)
                {
                    case RuleResult.Sound:
                        return "Play Sound";
                    case RuleResult.MessageBox:
                        return "Show Popup Message";
                    case RuleResult.Minimize:
                        return "Minimize program";
                    case RuleResult.Swap:
                        return "Swap to focus program";
                    case RuleResult.Kill:
                        return "Force kill the process";
                    default:
                        return "Invalid Action";
                }
            }
        }

        public Rule(string humanName, string process, bool limitPercent, int limit, RuleResult result)
        {
            this.humanName = humanName;
            this.processName = process;
            this.limitPercent = limitPercent;
            this.valLimit = limit;
            this.result = result;
        }

        public bool checkApply(ProcessTimer t)
        {
            //If any program, or the matching program, apply is true
            return processName == "*" || processName == t.process.processName;
        }

        internal RuleAlarm getAlarm()
        {
            return new RuleAlarm(valLimit, result, this);
        }
    }

    class RuleAlarm
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        bool active = false;
        int idleTime = 0;
        int idleMax = 30;
        int limit;
        Rule.RuleResult action;

        public Rule Parent { get; private set; }

        public RuleAlarm(int limit, Rule.RuleResult action, Rule parent)
        {
            this.limit = limit;
            this.action = action;
            this.Parent = parent;
            Watcher.instance.registerTick(idleTick);
        }

        public void evaluate(int t, ProcessTimer pt)
        {
            //If already active, do nothing
            if (active) return;

            //If not met limit, do nothing
            if (t < limit) return;

            //Passed tests, Set active and Reset idle time
            active = true;
            idleTime = 0;

            //Do alarm result
            switch (action)
            {
                case Rule.RuleResult.Sound:
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
                case Rule.RuleResult.MessageBox:
                    System.Media.SystemSounds.Exclamation.Play();
                    System.Windows.Forms.MessageBox.Show("Your time limit has been reached!","ActiveWatcher Alarm",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
                case Rule.RuleResult.Minimize:
                    foreach(System.Diagnostics.Process p in pt.process.getHooks())
                        ShowWindow(p.MainWindowHandle, 2);
                    break;
                case Rule.RuleResult.Swap:
                    foreach (System.Diagnostics.Process p in pt.process.getHooks())
                        ShowWindow(p.MainWindowHandle, 2);
                    break;
                case Rule.RuleResult.Kill:
                    foreach (System.Diagnostics.Process p in pt.process.getHooks())
                        p.Kill();
                    System.Windows.Forms.MessageBox.Show("Your time limit has been reached! Process Killed!", "ActiveWatcher Alarm", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                    break;
                default:
                    break;
            }

        }

        public void idleTick(object sender, EventArgs e)
        {
            //If snooze timeout is passed, reset active
            if (++idleTime >= idleMax) active = false;
        }

        internal void delete()
        {
            Watcher.instance.unregisterTick(idleTick);
        }
    }
}
