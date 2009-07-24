using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame
{
    class CanonBall : PhysicsGameObject
    {
        public CanonBall(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, EffectManager effectManger, float zIndex)
            : base(game, position, "cannonBall", 1f, Color.White, spriteBatch, physicsSimulator, null, effectManger, zIndex)
        {
        }

        protected override void LoadContent() {
            base.LoadContent();
            //base.Body.LinearDragCoefficient = 1.0f;            
        }

        public void ApplyFiringForce(Vector2 firingForce) {
            base.Body.ApplyImpulse(firingForce);
        }
    }
}
