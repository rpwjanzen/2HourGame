using Microsoft.Xna.Framework;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class GameObjectFactory {
        public World World { get; private set; }

        public GameObjectFactory(World world) {
            this.World = world;
        }
    }
}
