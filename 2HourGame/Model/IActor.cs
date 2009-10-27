using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.Model {
    interface IActor {
        void Draw(GameTime gameTime);
        void Update(GameTime gameTime);
        void LoadContent(ContentManager content);
    }
}
