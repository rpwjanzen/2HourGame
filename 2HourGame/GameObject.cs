using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class GameObject : DrawableGameComponent
    {
        public Color Color { get; private set; }
        public Vector2 Position
        {
            get { return Bounds.Center; }
            protected set { this.Bounds.Center = value; }
        }
        public float Rotation { get; protected set; }
        public BoundingCircle Bounds { get; private set; }
        private float boundsMultiplyer;

        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 origin;

        string contentName;

        public GameObject(Game game, Vector2 position, string contentName, float boundsMultiplyer)
            : base(game) {
            this.Color = Color.White;
            this.Bounds = new BoundingCircle();
            this.Position = position;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            texture = this.Game.Content.Load<Texture2D>(contentName);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.Bounds.Radius = Math.Max(origin.X, origin.Y) * boundsMultiplyer;
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
