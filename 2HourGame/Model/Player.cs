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
        Island goldIsland;
        float goldIslandMinimumRange;

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

            goldIsland = null;
            goldIslandMinimumRange = 100;
        }

        public GamePadState getGamePadState() 
        {
            return GamePad.GetState(playerIndex);
        }

        #region Controller Actions
        public void FireCannon(GameTime gameTime, CannonType cannonType) 
        {
            ship.FireCannon(gameTime, cannonType);
        }

        public void AttemptPickupGold(GameTime gameTime) 
        {
            // Gold Pickup Behaviour
            if (goldIsland != null && ship.Speed > 0.15)
                goldIsland = null;
            else if (ship.Speed <= 0.15)
            {
                Island closestInRangeIsland = map.GetClosestInRangeIsland(ship, goldIslandMinimumRange);
                if (closestInRangeIsland != null)
                {
                    if (closestInRangeIsland == homeIsland)
                    {
                        ship.UnloadGoldToIsland(closestInRangeIsland);
                    }
                    else
                    {
                        if (goldIsland == null)
                        {
                            if (closestInRangeIsland != null && closestInRangeIsland != homeIsland)
                            {
                                goldIsland = closestInRangeIsland;
                            }
                        }
                        else if (goldIsland == closestInRangeIsland)
                        {
                            ship.LoadGoldFromIsland(goldIsland, gameTime);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
