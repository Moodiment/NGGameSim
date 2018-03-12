using System;
using Eto.Forms;
using Eto;

namespace NGSim
{
	public static class Program
	{
        [STAThread]
		public static void Main(string[] args)
		{
			var pf = Platform.Detect;
			
				Console.WriteLine("Using wpf...");
				pf.Add(typeof(StateInfoTextArea), () => new StateInfoTextAreaHandler());
				pf.Add(typeof(NetworkInfoTextArea), () => new NetworkInfoTextAreaHandler());
			
				Console.WriteLine("Platform not supported...");

			Application app = new Application();

			UpdateManager.Initialize();

			SimManagerWindow mainWindow = new SimManagerWindow();

			mainWindow.Shown += (sender, e) => { UpdateManager.LaunchThread(); };
			mainWindow.Closing += (sender, e) => { UpdateManager.CloseThread(); };

			app.Run(mainWindow);
		}
	}
}
