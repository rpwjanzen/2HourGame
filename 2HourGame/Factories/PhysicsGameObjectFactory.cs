using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class PhysicsGameObjectFactory : DrawableGameObjectFactory {
        public PhysicsSimulator PhysicsSimulator { get; private set; }

        public PhysicsGameObjectFactory(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch) {
                this.PhysicsSimulator = ((IPhysicsSimulatorService)Game.Services.GetService(typeof(IPhysicsSimulatorService))).PhysicsSimulator;
        }
    }
}
