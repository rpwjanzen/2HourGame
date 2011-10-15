using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class ProgressBar
    {
        private float progress;

        public ProgressBar()
        {
            FillColor = Color.Green;
            EmptyColor = Color.Yellow;
            Scale = Vector2.One;
        }

        private Texture2D Texture { get; set; }
        private Vector2 Origin { get; set; }
        private float ZIndex { get; set; }

        public Color FillColor { get; set; }
        public Color EmptyColor { get; set; }
        public Vector2 Position { get; set; }

        /// <summary>
        /// Clamped to [0.0f - 1.0f]
        /// </summary>
        public float Progress
        {
            get { return progress; }
            set { progress = MathHelper.Clamp(value, 0.0f, 1.0f); }
        }

        private Vector2 Scale { get; set; }

        public float Width
        {
            get { return Scale.X*Texture.Width; }
            set { Scale = new Vector2(Scale.X*value/Texture.Width, Scale.Y); }
        }

        public float Height
        {
            get { return Scale.Y*Texture.Height; }
            set { Scale = new Vector2(Scale.X, Scale.Y*value/Texture.Height); }
        }

        public void LoadContent(TextureManager textureManager)
        {
            Texture = textureManager["progressBar"];
            Origin = Texture.Center(Scale);
            ZIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.healthBar);
        }

        // reference http://www.xnadevelopment.com/tutorials/notsohealthy/NotSoHealthy.shtml
        public void Draw(SpriteBatch spriteBatch)
        {
            var progressSoFarRectangle = new Rectangle(0, 0, (int) (Texture.Width*Progress), Texture.Height);
            spriteBatch.Draw(Texture, Position, null, EmptyColor, 0f, Origin, Scale, SpriteEffects.None, ZIndex + 0.001f);
            spriteBatch.Draw(Texture, Position, progressSoFarRectangle, FillColor, 0f, Origin, Scale, SpriteEffects.None,
                             ZIndex);
        }
    }
}