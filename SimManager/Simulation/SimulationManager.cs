﻿using NGAPI;
using System;

namespace NGSim.Simulation
{
	// On the server-side, the simulation manager is reponsible for quite a lot, including updating the algorithms,
	// performing entity and game state logic updates, and dispatching state packets to all of the connected clients.
	public class SimulationManager
	{
		internal NGAPI.Simulation Simulation { get; private set; }
		private float timeInTurn = 1;
		private int gameResult = 0; //0 means game is running, 1 means Team1 won, 2  means Team2 won, 3 means a draw
		private int numberOfTurns = 0;
		private int maxTurns = 100000;

		public SimulationManager()
		{
			Simulation = new NGAPI.Simulation();
			Random rand = new Random();
			//Set a random Position for the Tank and UAV, between -19999 and 19999, field bounds are -20000 and 20000
			//int randX = rand.Next(1, 40000) - 20000;
			//int randY = rand.Next(1, 40000) - 20000;
			//Simulation.Team1.Tank.Position = new Position(randX, randY);
			//Simulation.Team1.UAV.Position = new Position(randX, randY);

			//randX = rand.Next(1, 40000) - 20000;
			//randY = rand.Next(1, 40000) - 20000;
			//Simulation.Team1.Tank.Position = new Position(randX, randY);
			//Simulation.Team1.UAV.Position = new Position(randX, randY);
			//for(int i=0; i<maxTurns && gameResult == 0; i++)
			//{
			//	Update();
			//}
		}

		public void Update()
		{

			//Check if any of the Tanks have been destroyed
			if (!Simulation.Team1.Tank.Alive && !Simulation.Team1.Tank.Alive) {
				gameResult = 3;
				return;
			}
			else if (!Simulation.Team1.Tank.Alive)
			{
				gameResult = 2;
				return;
			}
			else if (!Simulation.Team2.Tank.Alive)
			{
				gameResult = 1;
				return;
			}

			//change UAV and Tank Heading and Position
			Simulation.Team1.Tank.UpdateHeading();
			Simulation.Team1.UAV.UpdateHeading();

			//change UAV and Tank Speed
			//Simulation.Team1.Tank.UpdateSpeed();
			//Simulation.Team1.UAV.UpdateSpeed();

			//move tank and UAV
			//Simulation.Team1.Tank.Position.MoveWithSpeed((float)Simulation.Team1.Tank.CurrentHeading, Simulation.Team1.Tank.GetSpeedUnits(), timeInTurn);
			//Simulation.Team1.UAV.Position.MoveWithSpeed((float)Simulation.Team1.UAV.CurrentHeading, Simulation.Team1.UAV.GetSpeedUnits(), timeInTurn);

			//do the same thing as above for Team2
			//Simulation.Team2.Tank.UpdateHeading();
			//Simulation.Team2.UAV.UpdateHeading();

			//Simulation.Team2.Tank.UpdateSpeed();
			//Simulation.Team2.UAV.UpdateSpeed();

			//Simulation.Team2.Tank.Position.MoveWithSpeed((float)Simulation.Team2.Tank.CurrentHeading, Simulation.Team2.Tank.GetSpeedUnits(), timeInTurn);
			//Simulation.Team2.UAV.Position.MoveWithSpeed((float)Simulation.Team2.UAV.CurrentHeading, Simulation.Team2.UAV.GetSpeedUnits(), timeInTurn);

			++numberOfTurns;
		}
	}
}
