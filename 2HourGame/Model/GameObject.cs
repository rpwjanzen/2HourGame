using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class GameObject : DrawableGameComponent
    {
        public Color Color { get; private set; }
        public virtual Vector2 Position { get; protected set; }
        public virtual float Rotation { get; protected set; } // in radians

        public float XRadius { get; private set; }
        public float YRadius { get; private set; }
        public float Width { get { return XRadius * 2; } }
        public float Height { get { return YRadius * 2; } }
        public bool IsCircle {
            get { return this.XRadius == this.YRadius; }
        }

        protected SpriteBatch spriteBatch;
        protected Texture2D texture;

        public float Scale { get; private set; }

        public Vector2 Origin { get; protected set; }

        public float ZIndex { get; private set; }

        protected string contentName;

        public GameObject(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch)
            : this(game, position, contentName, scale, color, spriteBatch, 0) {
        }

        public GameObject(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch, float zIndex)
            : base(game)
        {
            this.Color = color;
            this.Position = position;
            this.contentName = contentName;
            this.Scale = scale;
            this.spriteBatch = spriteBatch;
            this.ZIndex = zIndex;
        }

        protected override void LoadContent()
        {
            this.texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);

            setOrigin();

            this.XRadius = this.Origin.X * this.Scale;
            this.YRadius = this.Origin.Y * this.Scale;

            base.LoadContent();
        }

        protected virtual void setOrigin()
        {
            this.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, Position, null, Color, Rotation, Origin, this.Scale, SpriteEffects.None, ZIndex);

            base.Draw(gameTime);
        }
    }
}
