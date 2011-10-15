using System;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal static class AIHelpers
    {
        //public enum RotationDirection { None, Left, Right }

        /// <summary>
        /// The ammount to rotate in order to face the target n radians.
        /// </summary>
        public static float GetRotationToPointInDegrees(Vector2 targetPosition, Vector2 myPosition,
                                                        float initialFacingRadians)
        {
            Vector2 delta = myPosition - targetPosition;

            // my rotation should match this angle (-180.0f to 180.0f)
            float desiredRotationInDegrees = MathHelper.ToDegrees((float) Math.Atan2(delta.Y, delta.X));
            // rotation from Atan2 is in a different coordinate plane that is rotated by 90 degrees so
            desiredRotationInDegrees -= 90;
            // but now our desired rotation could be < -180 so
            if (desiredRotationInDegrees < -180)
                desiredRotationInDegrees += 360;

            float myRotationInDegrees = MathHelper.ToDegrees(initialFacingRadians);

            // angle difference range is from -360 to 360
            float angleDifference = (myRotationInDegrees - desiredRotationInDegrees);

            // we push angle difference into the range -180 to 180
            if (angleDifference < -180)
                angleDifference += 360;
            else if (angleDifference > 180)
                angleDifference -= 360;

            // so if you wanted to face the object you would change your angle by the negative of the angle difference.
            return -angleDifference;

            //if (angleDifference < 180)
            //    return RotationDirection.Left;
            //else if (angleDifference >= 180)
            //    return RotationDirection.Right;
            //else
            //    return RotationDirection.None;
        }
    }
}