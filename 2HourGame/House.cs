using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class House : DrawableGameComponent {
        public Color Color { get; private set; }
        public Vector2 Position {
            get { return Bounds.Center; }
            private set { Bounds.Center = value; }
        }
        public float Rotation { get; private set; }
        public BoundingCircle Bounds { get; private set; }

        Texture2D islandTexture;
        SpriteBatch spriteBatch;
        Vector2 origin;

        public House(Game game, Vector2 position)
            : base(game) {
            this.Color = Color.White;
            this.Bounds = new BoundingCircle();
            this.Position = position;
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            islandTexture = this.Game.Content.Load<Texture2D>("house");
            origin = new Vector2(islandTexture.Width / 2, islandTexture.Height / 2);
            this.Bounds.Radius = Math.Max(origin.X, origin.Y);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            spriteBatch.Draw(islandTexture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
