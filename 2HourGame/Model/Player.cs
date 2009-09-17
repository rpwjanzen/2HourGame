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

        public Ship ship { get; private set; }

        Island homeIsland;

        Map map;

        // persistant state stuff for picking up gold
        Island inRangeIsland;
        const float inRangeIslandMinimumRange = 100;
        const float maxShipSpeedForIslandInteraction = 0.15f;
        int numGoldButtonPresses;
        const float numGoldButtonPressesRequired = 20;

        public int TotalGold
        {
            get { return homeIsland.Gold + ship.Gold; }
        }

        public Player(PlayerIndex playerIndex, Ship ship, Island homeIsland, Map map) 
        {
            this.playerIndex = playerIndex;
            this.ship = ship;
            this.homeIsland = homeIsland;
            this.map = map;

            inRangeIsland = null;
        }

        public GamePadState getGamePadState() 
        {
            return GamePad.GetState(playerIndex);
        }

        public bool ClosestInRangeIslandIsHomeAndShipTravelingSlowEnough() 
        {
            Island closestInRange = map.GetClosestInRangeIsland(ship, inRangeIslandMinimumRange);

            if (closestInRange == homeIsland && ship.Speed <= maxShipSpeedForIslandInteraction)
                return true;
            return false;
        }

        public bool ClosestInRangeIslandIsNotHomeAndHasGoldAndShipTravelingSlowEnough()
        {
            Island closestInRange = map.GetClosestInRangeIsland(ship, inRangeIslandMinimumRange);

            if (closestInRange != null && closestInRange != homeIsland && closestInRange.HasGold && ship.Speed <= maxShipSpeedForIslandInteraction)
                return true;
            return false;
        }

        #region Controller Actions
        public void FireCannon(GameTime gameTime, CannonType cannonType) 
        {
            ship.FireCannon(gameTime, cannonType);
        }

        public void AttemptPickupGold(GameTime gameTime) 
        {
            if (ship.isActive)
            {
                // Gold Pickup Behaviour
                if (inRangeIsland != null && ship.Speed > maxShipSpeedForIslandInteraction)
                {
                    inRangeIsland = null;
                    numGoldButtonPresses = 0;
                }
                else if (ship.Speed <= maxShipSpeedForIslandInteraction)
                {
                    Island closestInRangeIsland = map.GetClosestInRangeIsland(ship, inRangeIslandMinimumRange);
                    if (closestInRangeIsland != null)
                    {
                        if (closestInRangeIsland == homeIsland)
                        {
                            ship.UnloadGoldToIsland(closestInRangeIsland);
                        }
                        else
                        {
                            if (inRangeIsland == null)
                            {
                                if (closestInRangeIsland != null && closestInRangeIsland != homeIsland)
                                {
                                    inRangeIsland = closestInRangeIsland;
                                }
                            }
                            else if (inRangeIsland == closestInRangeIsland)
                            {
                                if (numGoldButtonPresses >= numGoldButtonPressesRequired)
                                {
                                    ship.LoadGoldFromIsland(inRangeIsland, gameTime);
                                    numGoldButtonPresses = 0;
                                }
                                else
                                    numGoldButtonPresses++;
                            }
                        }
                    }
                }
            }
        }

        public void AttemptRepair()
        {
            if (ship.isActive)
            {
                // Gold Pickup Behaviour
                if (inRangeIsland != null && ship.Speed > maxShipSpeedForIslandInteraction)
                    inRangeIsland = null;
                else if (ship.Speed <= maxShipSpeedForIslandInteraction)
                {
                    Island closestInRangeIsland = map.GetClosestInRangeIsland(ship, inRangeIslandMinimumRange);
                    if (closestInRangeIsland != null)
                    {
                        if (closestInRangeIsland == homeIsland)
                        {
                            ship.Repair();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
