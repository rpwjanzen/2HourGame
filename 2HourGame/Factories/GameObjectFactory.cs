using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    internal class GameObjectFactory
    {
        public GameObjectFactory(World world, TextureManager textureManager, AnimationManager am)
        {
            World = world;
            TextureManager = textureManager;
            AnimationManager = am;
        }

        public World World { get; private set; }
        public TextureManager TextureManager { get; private set; }
        public AnimationManager AnimationManager { get; private set; }
    }
}