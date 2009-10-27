using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class PhysicsGameObjectFactory : GameObjectFactory {
        public PhysicsWorld PhysicsWorld { get; private set; }

        public PhysicsGameObjectFactory(PhysicsWorld world, TextureManager textureManager, AnimationManager am)
            : base(world, textureManager, am) {
            this.PhysicsWorld = world;
        }
    }
}
