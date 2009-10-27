using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class SmallMerchantShip : Ship
    {
        public SmallMerchantShip(PhysicsWorld world, Vector2 initialPosition, float initialRotation)
            : base(world, initialPosition, initialRotation) 
        {
            this.GoldCapacity = 5;
            this.Gold = 0;

            var rotationMatrix = Matrix.Identity;
        }
    }
}
