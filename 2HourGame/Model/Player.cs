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

        public IShip ship { get; private set; }
        public Island ClosestInRangeIsland
        {
            get { return map.GetClosestInRangeIsland(ship, inRangeIslandMinimumRange); }
        }
        Island homeIsland;
        public Island HomeIsland
        {
            get { return homeIsland; }
        }
        public bool ShipIsMovingSlowly
        {
            get { return ship.Speed <= maxShipSpeedForIslandInteraction; }
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
            get { return homeIsland.Gold + ship.Gold; }
        }

        public Player(PlayerIndex playerIndex, IShip ship, Island homeIsland, Map map)
        {
            this.playerIndex = playerIndex;
            this.ship = ship;
            this.homeIsland = homeIsland;
            this.map = map;
        }

        #region Controller Actions
        public void FireCannon(GameTime gameTime, CannonType cannonType)
        {
            ship.FireCannon(gameTime, cannonType);
        }

        public void AttemptPickupGold()
        {
            if (!ship.IsAlive)
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
                ship.UnloadGoldToIsland(ClosestInRangeIsland);
                return;
            }

            if (numGoldButtonPresses >= numGoldButtonPressesRequired)
            {
                ship.LoadGoldFromIsland(ClosestInRangeIsland);
                numGoldButtonPresses = 0;
            }
            else
            {
                numGoldButtonPresses++;
            }
        }

        public void AttemptRepair()
        {
            if (!ship.IsAlive)
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
                ship.Repair();
            }
        }
        #endregion
    }
}
