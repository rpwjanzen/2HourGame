using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    static class AIHelpers
    {
        public enum RotationDirection { None, Left, Right }

        public static RotationDirection GetRotationToPointInRadians(Vector2 targetPosition, Vector2 myPosition, float initialFacingRadians, float toleranceInRadians)
        {
            var delta = myPosition - targetPosition;
            // my rotation should match this angle (-180.0f - 180.0f)
            var desiredRotationInDegrees = MathHelper.ToDegrees((float)Math.Atan2(delta.Y, delta.X));

            // something is wrong in here, so we need to -90 to get it to aim properly
            desiredRotationInDegrees -= 90.0f;
            if (desiredRotationInDegrees > 180.0f)
                desiredRotationInDegrees -= 360.0f;
            else if (desiredRotationInDegrees < -180.0f)
                desiredRotationInDegrees += 360.0f;

            var myRotationInDegrees = MathHelper.ToDegrees(initialFacingRadians);
            // body rotations are 0 - 360.0f
            myRotationInDegrees -= 180.0f;

            var angleDifference = (myRotationInDegrees - desiredRotationInDegrees);

            var toleranceInDegrees = MathHelper.ToDegrees(toleranceInRadians);
            if (angleDifference > (180.0f - toleranceInDegrees))
            {
                return RotationDirection.Left;
            }
            else if (angleDifference > (0 + toleranceInDegrees))
            {
                return RotationDirection.Right;
            }
            else if (angleDifference > (-180.0f - -toleranceInDegrees))
            {
                return RotationDirection.Left;
            }
            else if (angleDifference < (-180.0f + -toleranceInDegrees))
            {
                return RotationDirection.Right;
            }
            else {
                return RotationDirection.None;
            }
        }
    }
}
