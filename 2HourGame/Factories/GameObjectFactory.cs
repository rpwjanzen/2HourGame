using Microsoft.Xna.Framework;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class GameObjectFactory {
        public World World { get; private set; }
        public TextureManager TextureManager { get; private set; }
        public AnimationManager AnimationManager { get; private set; }

        public GameObjectFactory(World world, TextureManager textureManager, AnimationManager am) {
            this.World = world;
            this.TextureManager = textureManager;
            this.AnimationManager = am;
        }
    }
}
