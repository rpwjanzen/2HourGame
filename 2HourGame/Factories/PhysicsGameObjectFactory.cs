using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class PhysicsGameObjectFactory : GameObjectFactory {
        public PhysicsWorld PhysicsWorld { get; private set; }

        public PhysicsGameObjectFactory(PhysicsWorld world)
            : base(world) {
            this.PhysicsWorld = world;
        }
    }
}
