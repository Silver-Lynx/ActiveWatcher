using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveWatcher
{
    class Rule
    {
        public bool active = false;
        string humanName;
        public string ProcessName { get { return humanName; } }
        internal string processName;
        internal int valLimit;
        bool limitPercent;
        public IRuleAction action;

        public Rule(string humanName, string process, bool limitPercent, int limit)
        {
            this.humanName = humanName;
            this.processName = process;
            this.limitPercent = limitPercent;
            this.valLimit = limit;
        }

        public bool checkApply(ProcessDetails p)
        {
            //If any program, or the matching program, apply is true
            return processName == "*" || processName == p.Descriptor;
        }

        internal RuleInstance getAlarm()
        {
            return new RuleInstance(valLimit, this);
        }
    }

    class RuleInstance
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        bool active = false;
        int idleTime = 0;
        int idleMax = 30;
        int limit;

        public Rule Parent { get; private set; }

        public RuleInstance(int limit, Rule parent)
        {
            this.limit = limit;
            this.Parent = parent;
            Watcher.instance.registerTick(idleTick);
        }

        public void evaluate(int t, ProcessDetails p)
        {
            //If already active, do nothing
            if (active) return;

            //If not met limit, do nothing
            if (t < limit) return;

            //Passed tests, Set active and Reset idle time
            active = true;
            idleTime = 0;

            Parent.action?.DoAction(p);
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
