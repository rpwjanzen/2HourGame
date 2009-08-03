﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class CannonBall : PhysicsGameObject
    {
        public CannonBall(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game, position, "cannonBall", 0.5f, Color.White, spriteBatch, physicsSimulator, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonBall))
        {
        }

        protected override void LoadContent() {
            base.LoadContent();
            base.Body.LinearDragCoefficient = 0.20f;
            base.Body.Mass = 0.5f;
            // prevent collisions with the border
            base.Geometry.CollisionCategories = CollisionCategory.Cat2;
            base.Geometry.CollidesWith = CollisionCategory.All & ~CollisionCategory.Cat1;
        }

        public void ApplyFiringForce(Vector2 firingForce) {
            base.Body.ApplyImpulse(firingForce);
        }
    }
}
