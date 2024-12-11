using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveWatcher
{
	internal interface IRuleAction
	{
		void DoAction(ProcessTimer process);
	}

	class MessageAction : IRuleAction
	{
		string message = "Your time limit has been reached!";

		public MessageAction(string message)
		{
			this.message = message;
		}

		public void DoAction(ProcessTimer process)
		{
			System.Media.SystemSounds.Exclamation.Play();
			System.Windows.Forms.MessageBox.Show(
				message, 
				"Overseer Alarm", 
				System.Windows.Forms.MessageBoxButtons.OK, 
				System.Windows.Forms.MessageBoxIcon.Warning);
		}
	}

	class MinimizeAction : IRuleAction
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public MinimizeAction() { }

		public void DoAction(ProcessTimer process)
		{
			foreach (System.Diagnostics.Process p in process.process.getHooks())
				ShowWindow(p.MainWindowHandle, 11);
		}
	}

	class KillAction : IRuleAction
	{
		public KillAction() { }

		public void DoAction(ProcessTimer process)
		{
			foreach (System.Diagnostics.Process p in process.process.getHooks())
				p.Kill();

			System.Windows.Forms.MessageBox.Show(
				"Your time limit has been reached! Process Killed!", 
				"ActiveWatcher Alarm", 
				System.Windows.Forms.MessageBoxButtons.OK,
				System.Windows.Forms.MessageBoxIcon.Stop);
		}
	}
}
