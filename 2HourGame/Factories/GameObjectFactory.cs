using Microsoft.Xna.Framework;

namespace _2HourGame.Factories
{
    class GameObjectFactory {
        public Game Game { get; private set; }

        public GameObjectFactory(Game game) {
            this.Game = game;
        }
    }
}
