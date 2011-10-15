using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class PhysicsWorld : World
    {
        public PhysicsWorld(PhysicsSimulator physicsSimulator)
        {
            PhysicsSimulator = physicsSimulator;
        }

        public PhysicsSimulator PhysicsSimulator { get; protected set; }
        public GameTime CollisionTime { get; protected set; }

        public override void Update(GameTime gameTime)
        {
            CollisionTime = gameTime;
            PhysicsSimulator.Update((gameTime.ElapsedGameTime.Milliseconds)/100.0f);

            base.Update(gameTime);
        }
    }
}