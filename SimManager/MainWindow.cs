﻿using System;
using Eto.Forms;
using Eto.Drawing;
using NLog;

namespace NGSim
{
	public class MainWindow : Form
	{
        private static Logger logger = LogManager.GetCurrentClassLogger();
		public MainWindow()
		{
            logger.Info("Sample Log Message");
			ClientSize = new Size(400, 300);
			Title = "SimManager";
		}
	}
}