using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class ProgressBar
    {
        Texture2D Texture { get; set; }
        Vector2 Origin { get; set; }
        float ZIndex { get; set; }

        public Color FillColor { get; set; }
        public Color EmptyColor { get; set; }
        public float Scale { get; set; }
        public Vector2 Position { get; set; }

        /// <summary>
        /// [0.0f - 1.0f]
        /// </summary>
        public float Progress { get; set; }

        public ProgressBar() {
            FillColor = Color.Green;
            EmptyColor = Color.Yellow;
            Scale = 1.0f;
        }

        public void LoadContent(ITextureManager textureManager)
        {
            Origin = textureManager.getTextureOrigin("progressBar");
            Texture = textureManager.getTexture("progressBar");
            ZIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.healthBar);
        }

        // reference http://www.xnadevelopment.com/tutorials/notsohealthy/NotSoHealthy.shtml
        public void Draw(SpriteBatch spriteBatch)
        {
            var progressSoFarRectangle = new Rectangle(0, 0, (int)(Texture.Width * Progress), Texture.Height);
            spriteBatch.Draw(Texture, Position, null, EmptyColor, 0f, Origin, Scale, SpriteEffects.None, ZIndex + 0.001f);
            spriteBatch.Draw(Texture, Position, progressSoFarRectangle, FillColor, 0f, Origin, Scale, SpriteEffects.None, ZIndex);
        }
    }
}
