using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace _2HourGame.View
{
    class Border
    {
        Texture2D Texture { get; set; }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(@"Content\border");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, Color.White);
        }
    }
}
