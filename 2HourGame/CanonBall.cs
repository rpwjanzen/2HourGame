using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame
{
    class CanonBall : GameObject
    {
        public CanonBall(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game, position, "cannonBall", 1f, Color.White, spriteBatch, physicsSimulator) {
        }

        protected override void LoadContent() {
            base.Body.LinearDragCoefficient = 1.0f;
            base.LoadContent();
        }

        public bool IsOutOfBounds {
            get { return false; }
        }
        public float Speed {
            get { return base.Body.LinearVelocity.Length(); }
        }
    }
}
