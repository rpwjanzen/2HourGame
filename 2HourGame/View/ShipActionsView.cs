using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class ShipActionsView : DrawableGameComponent
    {
        Ship ship;

        public ShipActionsView(Game game, Ship ship) 
            :base(game)
        {
            this.ship = ship;
        }



    }
}
