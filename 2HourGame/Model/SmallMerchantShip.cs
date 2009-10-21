﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class SmallMerchantShip : Ship
    {
        public SmallMerchantShip(Game game, Vector2 initialPosition, CannonBallManager cannonBallManager, float initialRotation)
            : base(game, initialPosition, cannonBallManager, initialRotation) 
        {
            this.GoldCapacity = 5;
            this.Gold = 0;

            var rotationMatrix = Matrix.Identity;

            //var leftCannonPosition = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y) * ((this.Width / 2.0f)) + leftCannonOffset;
            //var leftCannonRotation = MathHelper.ToRadians(-90);
            //LeftCannons.Add(new Cannon(game, this, cannonBallManager, leftCannonPosition, leftCannonRotation));

            //var rightCannonPosition = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y) * ((this.Width / 2.0f)) + rightCannonOffset;
            //var rightCannonRotation = MathHelper.ToRadians(90);
            //RightCannons.Add(new Cannon(game, this, cannonBallManager, rightCannonPosition, rightCannonRotation));
        }
    }
}
