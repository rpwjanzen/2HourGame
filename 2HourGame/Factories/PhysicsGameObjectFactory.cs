using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    internal class PhysicsGameObjectFactory : GameObjectFactory
    {
        public PhysicsGameObjectFactory(PhysicsWorld world, TextureManager textureManager, AnimationManager am)
            : base(world, textureManager, am)
        {
            PhysicsWorld = world;
        }

        public PhysicsWorld PhysicsWorld { get; private set; }
    }
}