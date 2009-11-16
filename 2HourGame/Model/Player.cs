using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class Player
    {
        PlayerIndex playerIndex;

        public Ship Ship { get; private set; }
        public Island ClosestInRangeIsland
        {
            get { return map.GetClosestInRangeIsland(Ship, inRangeIslandMinimumRange); }
        }
        Island homeIsland;
        public Island HomeIsland
        {
            get { return homeIsland; }
        }
        public bool ShipIsMovingSlowly
        {
            get { return Ship.Speed <= maxShipSpeedForIslandInteraction; }
        }
        public GamePadState GamePadState
        {
            get { return GamePad.GetState(playerIndex); }
        }
        Map map;

        // persistant state stuff for picking up gold
        const float inRangeIslandMinimumRange = 100;
        const float maxShipSpeedForIslandInteraction = 0.15f;

        public int numGoldButtonPresses { get; private set; }
        public const float numGoldButtonPressesRequired = 20;

        public int TotalGold
        {
            get { return homeIsland.Gold + Ship.Gold; }
        }

        public Player(PlayerIndex playerIndex, Ship ship, Island homeIsland, Map map)
        {
            this.playerIndex = playerIndex;
            this.Ship = ship;
            this.homeIsland = homeIsland;
            this.map = map;
        }

        #region Controller Actions
        public void FireLeftCannons(GameTime gameTime)
        {
            Ship.FireLeftCannons(gameTime);
        }

        public void FireRightCannons(GameTime gameTime)
        {
            Ship.FireRightCannons(gameTime);
        }

        public void AttemptPickupGold()
        {
            if (!Ship.IsAlive)
            {
                return;
            }

            if (!ShipIsMovingSlowly)
            {
                return;
            }

            if (ClosestInRangeIsland == null)
            {
                numGoldButtonPresses = 0;
                return;
            }

            if (ClosestInRangeIsland == homeIsland)
            {
                Ship.UnloadGoldToIsland(ClosestInRangeIsland);
                return;
            }

            if (numGoldButtonPresses >= numGoldButtonPressesRequired)
            {
                Ship.LoadGoldFromIsland(ClosestInRangeIsland);
                numGoldButtonPresses = 0;
            }
            else
            {
                numGoldButtonPresses++;
            }
        }

        public void AttemptRepair()
        {
            if (!Ship.IsAlive)
            {
                return;
            }

            if (!ShipIsMovingSlowly)
            {
                return;
            }

            if (ClosestInRangeIsland == null)
            {
                return;
            }

            if (ClosestInRangeIsland == homeIsland)
            {
                Ship.Repair();
            }
        }
        #endregion
    }
}
