﻿using System;

namespace NGAPI
{
    public static class API
    {
		// This gets populated my SimulationManager in SimManager
        internal static Simulation Simulation = null;
        internal static UAV FriendlyUAV = Simulation.Team1.UAV;
        internal static Tank FriendlyTank = Simulation.Team1.Tank;
        internal static Tank EnemyTank = Simulation.Team2.Tank;

        public static void SetUAVHeading(int targetHeading)
        {
            if (targetHeading < 0 || targetHeading > 360)
            {
                throw new Exception("Invalid Heading");
            }
            FriendlyUAV.TargetHeading = targetHeading;
        }

        public static void SetUAVSpeed(Speed targetSpeed)
        {
            if(!Enum.IsDefined(typeof(Speed),targetSpeed))
            {
                throw new Exception("InvalidSpeed");
            }
            FriendlyUAV.TargetSpeed = targetSpeed;
        }

        //Returns True if Enemy Tank is within the UAVs view radius
        public static bool UAVScan()
        {
            int viewRadius = Simulation.Team1.UAV.ViewRadius;
            float distance = FriendlyUAV.Position.DistanceTo(EnemyTank.Position);

            if(distance < viewRadius) { return true; }
            else { return false; }
        }

        public static void TankSetHeading(int targetHeading)
        {
            if (targetHeading < 0 || targetHeading > 360)
            {
                throw new Exception("Invalid Heading");
            }
            FriendlyTank.TargetHeading = targetHeading;
        }

        public static void TankSetSpeed(Speed targetSpeed)
        {
            if (!Enum.IsDefined(typeof(Speed), targetSpeed))
            {
                throw new Exception("Invalid Speed");
            }
            FriendlyTank.TargetSpeed = targetSpeed;
        }

        //Return True on a Hit on the Enemy Tank
        //Return False on a Miss or a failure to fire
        public static bool Fire(Position Target)
        {
            FriendlyTank.MisslesLeft--;

            //Out of Missiles (failure to fire)
            if(FriendlyTank.MisslesLeft == 0)
            {
                //TODO: Besides an obvious miss, what do we do if we're out of missiles
                return false;
            }

            //Out of Range (Failure to Fire)
            //4000 is just a number and can be changed, but it is the real firing range of the M1 Abrams
            if(FriendlyTank.Position.DistanceTo(Target) > 4000)
            {
                return false;
            }

            //Good Hit Good Kill (Rifle Rifle Splash)
            //22 is effective blast radius of 120mm cannon on M1 Abrams
            if(EnemyTank.Position.DistanceTo(Target) < 22)
            {
                return true;
            }
            //Return False
            else
            {
                return false;
            }
        }

        //Set a Target Heading and Speed while turning in a given direction
        //Check to see if the move will move the UAV out of the play area.
        public static void SetUAVVector(int targetHeading, Speed targetSpeed, Direction direction)
        {
            float unitsMoved;
            int degreesTurned;

            if (targetHeading < 0 || targetHeading > 360) { throw new Exception("Invalid Heading"); }
            else if (!Enum.IsDefined(typeof(Speed), targetSpeed)) { throw new Exception("Invalid Speed"); }

            unitsMoved = EntityUtility.SpeedToUnits(targetSpeed, typeof(UAV));
            degreesTurned = EntityUtility.SpeedToDegrees(targetSpeed, direction, FriendlyUAV.CurrentHeading, targetHeading, typeof(UAV));

            //TODO: Predict updated position of UAV and throw exception if out of bounds. Incorporate Direction
            
            FriendlyUAV.TargetHeading = targetHeading;
            FriendlyUAV.TargetSpeed = targetSpeed;
        }

        //Set a Target Heading and Speed while turning in a given direction
        //Check to see if the move will move the UAV out of the play area.
        public static void SetTankVector(int targetHeading, Speed targetSpeed, Direction direction)
        {
            float unitsMoved;
            int degreesTurned;

            if (targetHeading < 0 || targetHeading > 360) { throw new Exception("Invalid Heading"); }
            else if (!Enum.IsDefined(typeof(Speed), targetSpeed)) { throw new Exception("Invalid Speed"); }

            unitsMoved = EntityUtility.SpeedToUnits(targetSpeed, typeof(UAV));
            degreesTurned = EntityUtility.SpeedToDegrees(targetSpeed, direction, FriendlyUAV.CurrentHeading, targetHeading, typeof(UAV));
            //TODO: Predict updated position of Tank and throw exception if out of bounds

            FriendlyUAV.TargetHeading = targetHeading;
            FriendlyUAV.TargetSpeed = targetSpeed;
        }

        public static bool stillAlive() { return FriendlyTank.Alive; }
    }
}
