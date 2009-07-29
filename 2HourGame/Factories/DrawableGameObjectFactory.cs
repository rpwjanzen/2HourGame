using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Factories {
    class DrawableGameObjectFactory : GameObjectFactory {

        public SpriteBatch SpriteBatch { get; private set; }

        public DrawableGameObjectFactory(Game game, SpriteBatch spriteBatch)
            : base(game) {
            this.SpriteBatch = spriteBatch;
        }
    }
}
