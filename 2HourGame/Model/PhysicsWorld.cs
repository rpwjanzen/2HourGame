using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;

namespace _2HourGame.Model {
    class PhysicsWorld : World {
        public PhysicsSimulator PhysicsSimulator { get; protected set; }
        public GameTime CollisionTime { get; protected set; }

        public PhysicsWorld(PhysicsSimulator physicsSimulator) {
            this.PhysicsSimulator = physicsSimulator;
        }

        public override void Update(GameTime gameTime) {
            CollisionTime = gameTime;
            PhysicsSimulator.Update(((float)gameTime.ElapsedGameTime.Milliseconds) / 100.0f);

            base.Update(gameTime);
        }
    }
}
