using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Factories
{
    class PhysicsGameObjectFactory : DrawableGameObjectFactory {
        public PhysicsSimulator PhysicsSimulator { get; private set; }

        public PhysicsGameObjectFactory(Game game, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game, spriteBatch) {
            this.PhysicsSimulator = physicsSimulator;
        }
    }
}
