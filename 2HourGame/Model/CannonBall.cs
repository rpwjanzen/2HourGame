using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class CannonBall : Projectile
    {
       public CannonBall(PhysicsWorld world, Vector2 position, Actor owner)
            : base(world, position, 5, 5, owner)
        {
            base.Body.LinearDragCoefficient = 0.20f;
            base.Body.Mass = 0.5f;
        }
    }
}
