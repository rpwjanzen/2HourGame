using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    static class AIHelpers
    {
        public static float GetFacingTowardsPointInRadians(Vector2 point, Vector2 selfLocation, float initialFacing, float maxRotationDegrees)
        {
            var delta = selfLocation - point;
            // my rotation should match this angle (-180.0f - 180.0f)
            var desiredRotation = MathHelper.ToDegrees((float)Math.Atan2(delta.Y, delta.X));

            // drawing is off by 90.0
            //desiredRotation -= 90.0f;
            if (desiredRotation > 180.0f)
                desiredRotation -= 360.0f;
            else if (desiredRotation < -180.0f)
                desiredRotation += 360.0f;

            var myRotation = MathHelper.ToDegrees(initialFacing);
            // body rotations are 0 - 360.0f
            myRotation -= 180.0f;

            var angleDifference = (myRotation - desiredRotation);

            if (Math.Abs(angleDifference) <= maxRotationDegrees)
                return desiredRotation;

            float changeInRotation;
            if (angleDifference > 0)
                changeInRotation = maxRotationDegrees;
            else
                changeInRotation = -maxRotationDegrees;

            float resultInRadians = initialFacing + MathHelper.ToRadians(changeInRotation);
            if (resultInRadians < 0)
                resultInRadians += 2f * (float)Math.PI;
            else if (resultInRadians > Math.PI)
                resultInRadians -= 2f * (float)Math.PI;

            return resultInRadians;
        }
    }
}
