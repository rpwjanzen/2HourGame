using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame {
    class ShipRelativeMoveBehavior {
        float maxRotation = 0.01f;
        float maxAcceleration = 0.01f;

        public void MoveShip(GamePadState gs, Ship ship) {
            // set the ships angle
            if (gs.ThumbSticks.Left.X != 0 || gs.ThumbSticks.Left.Y != 0) {
                ship.RotateRadians(gs.ThumbSticks.Left.X * maxRotation);
            }

            // set the acceleration / deacceleration
            float magnitude = gs.ThumbSticks.Left.Y * maxAcceleration;
            ship.Accelerate(magnitude);

            // move the ship
            // TODO, Use Elapsed Game Time to move the ship relative to game time
            ship.Offset((float)Math.Cos(ship.Rotation - (0.5 * Math.PI)) * ship.Speed, (float)Math.Sin(ship.Rotation - (0.5 * Math.PI)) * ship.Speed);
        }
    }
}
