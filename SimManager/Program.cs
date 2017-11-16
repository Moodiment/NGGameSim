﻿using System;
using Eto.Forms;
using System.Threading;
using NGSim.Network;

namespace NGSim
{
	public static class Program
	{
		private static Server server;

        [STAThread]
		public static void Main(string[] args)
		{
			Thread thread = new Thread(testServer);

			Application app = new Application();
			app.Initialized += (sender, e) => { thread.Start(); };
			app.Terminating += (sender, e) => { server.CloseServer(); };
			app.Run(new MainWindow());
		}

		private static void testServer()
		{
            server = new Server();
            server.WaitForMessage();
        }
	}
}
