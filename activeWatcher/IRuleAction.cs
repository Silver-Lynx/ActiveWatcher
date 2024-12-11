using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActiveWatcher
{
	internal interface IRuleAction
	{
		void DoAction(ProcessDetails process);
	}

	class MessageAction : IRuleAction
	{
		string message = "Your time limit has been reached!";

		public MessageAction(string message)
		{
			this.message = message;
		}

		public void DoAction(ProcessDetails process)
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

		public void DoAction(ProcessDetails process)
		{
			foreach (int id in process.getHooks())
			{
				Process p = Process.GetProcessById(id);
				if (p != null)
					ShowWindow(p.MainWindowHandle, 11);
			}
		}
	}

	class KillAction : IRuleAction
	{
		public KillAction() { }

		public void DoAction(ProcessDetails process)
		{
			foreach (int id in process.getHooks())
				Process.GetProcessById(id)?.Kill();

			process.clearHooks();

			System.Windows.Forms.MessageBox.Show(
				"Your time limit has been reached! Process Killed!", 
				"ActiveWatcher Alarm", 
				System.Windows.Forms.MessageBoxButtons.OK,
				System.Windows.Forms.MessageBoxIcon.Stop);
		}
	}
}
