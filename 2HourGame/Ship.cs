using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame {
    class Ship : GameObject {        
		float maxSpeed = 1f;
		public int maxGold = 5;
		public int gold = 0;

        public float Speed {
            get { return base.Body.LinearVelocity.Length(); }
        }

        public Ship(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game, position, "boat", 0.6f, Color.White, spriteBatch, physicsSimulator) {
        }

        public void Accelerate(Vector2 amount) {
            base.Body.ApplyImpulse(amount);
        }

        public void Accelerate(float amount) {
            // calculate vector along direction the object is facing
            var bodyRotation = base.Body.GetBodyRotationMatrix();
            var v = bodyRotation.Up * amount;

            base.Body.ApplyImpulse(new Vector2(v.X, v.Y));
        }

        public void Rotate(float amount) {
            //base.Body.ApplyAngularImpulse(amount);
        }
    }
}
