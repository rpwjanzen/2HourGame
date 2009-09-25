using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    static class AIHelpers
    {
        public static float GetFacingTowardsPoint(Vector2 point, Vector2 selfLocation, float initialFacing, float maxRotation)
        {
            var delta = selfLocation - point;
            // my rotation should match this angle (-180.0f - 180.0f)
            var desiredRotation = MathHelper.ToDegrees((float)Math.Atan2(delta.Y, delta.X));

            // drawing is off by 90.0
            desiredRotation -= 90.0f;
            if (desiredRotation > 180.0f)
                desiredRotation -= 360.0f;
            else if (desiredRotation < -180.0f)
                desiredRotation += 360.0f;

            var myRotation = MathHelper.ToDegrees(initialFacing);
            // body rotations are 0 - 360.0f
            myRotation -= 180.0f;

            var angleDifference = (myRotation - desiredRotation);

            if (Math.Abs(angleDifference) <= maxRotation)
                return desiredRotation;

            float newRotation;
            if (angleDifference > 0)
                newRotation = desiredRotation + maxRotation;
            else
                newRotation = desiredRotation - maxRotation;

            return newRotation;
        }
    }
}
