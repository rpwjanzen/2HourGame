using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame {
    class ShipRelativeMoveBehavior {
        float maxRotationForce = 1.0f;
        float maxAccelerationForce = 1.0f;

        public void MoveShip(GamePadState gs, Ship ship) {
          // accelerate / decelerate
            //float magnitude = gs.ThumbSticks.Left.Y * maxAccelerationForce;
            //ship.Accelerate(magnitude);
            var leftStick = gs.ThumbSticks.Left;
            var v = new Vector2(leftStick.X, leftStick.Y * -1.0f); 
            ship.Accelerate(v);
        }
    }
}
