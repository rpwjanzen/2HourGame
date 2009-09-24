using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    static class AIHelpers
    {
        public static void getAngle(Vector2 heading, Vector2 positionToFace, double maxRotation) 
        {
            Vector2 normailzedHeading = (positionToFace - heading);
            normailzedHeading.Normalize();

            var dot = Vector2.Dot(heading, normailzedHeading);
            var cross = heading.X * normailzedHeading.X - heading.Y * normailzedHeading.Y;
            var angle = Math.Atan2(cross, dot);

            if (angle > maxRotation)
                angle = maxRotation;
            else if (angle < -maxRotation)
                angle = -maxRotation;

            var newHeading = Vector2.Transform(heading, Matrix.CreateRotationZ((float)angle));
        }
    }
}
