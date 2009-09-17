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
        PlayerIndex playerIndex { get; set; }

        public Ship ship { get; private set; }

        Island homeIsland { get; set; }

        public int TotalGold
        {
            get { return homeIsland.Gold + ship.Gold; }
        }

        public Player(PlayerIndex playerIndex, Ship ship, Island homeIsland) 
        {
            this.playerIndex = playerIndex;
            this.ship = ship;
            this.homeIsland = homeIsland;
        }

        public GamePadState getGamePadState() 
        {
            return GamePad.GetState(playerIndex);
        }

        #region Controller Actions
        public void FireCannon(GameTime now, CannonType cannonType) 
        {
            ship.FireCannon(now, cannonType);
        }
        #endregion
    }
}
