using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class SmallPlayerShip : Ship
    {
        // Offset to move the cannons so they look good on the ship.
        readonly Vector2 leftCannonOffset = new Vector2(8, 4);
        readonly Vector2 rightCannonOffset = new Vector2(-8, 4);

        public SmallPlayerShip(PhysicsWorld world, Vector2 initialPosition, float initialRotation)
            : base(world, initialPosition, initialRotation) 
        {
            this.GoldCapacity = 3;
            this.Gold = 0;

            var rotationMatrix = Matrix.Identity;

            var leftCannonPosition = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y) * ((this.Width / 2.0f)) + leftCannonOffset;
            var leftCannonRotation = MathHelper.ToRadians(-90);
            LeftCannons.Add(new Cannon(PhysicsWorld, this, leftCannonPosition, leftCannonRotation));

            var rightCannonPosition = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y) * ((this.Width / 2.0f)) + rightCannonOffset;
            var rightCannonRotation = MathHelper.ToRadians(90);
            RightCannons.Add(new Cannon(PhysicsWorld, this, rightCannonPosition, rightCannonRotation));
        }
    }
}
