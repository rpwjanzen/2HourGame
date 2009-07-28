using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame {
    class ShipRelativeMoveBehavior {
        public void MoveShip(GamePadState gs, Ship ship) {
            ship.Accelerate(gs.ThumbSticks.Left.Y * 15);
            ship.Rotate(gs.ThumbSticks.Left.X * 25);
        }
    }
}
