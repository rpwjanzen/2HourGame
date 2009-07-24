using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame
{
    class CannonBall : PhysicsGameObject
    {
        public CannonBall(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, float zIndex)
            : base(game, position, "cannonBall", 1f, Color.White, spriteBatch, physicsSimulator, null, zIndex)
        {
        }

        protected override void LoadContent() {
            base.Body.LinearDragCoefficient = 1.0f;
            base.LoadContent();
        }
    }
}
