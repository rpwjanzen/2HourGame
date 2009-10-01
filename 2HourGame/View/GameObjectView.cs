using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    /// <summary>
    /// 
    /// </summary>
    class GameObjectView : DrawableGameComponent
    {
        protected Color Color { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Texture2D Texture { get; set; }
        protected float ZIndex { get; set; }
        protected Vector2 Scale { get; set; }
        protected Vector2 Origin { get; set; }
        protected IGameObject GameObject { get; set; }
        /// <summary>
        /// Stores offset to adjust drawing for objects where the collision area is not based on the texture size.
        /// </summary>
        private Vector2 textureOriginOffset;

        string contentName;

        /// <summary>
        /// The view is automatically removed when it's GameObject is
        /// </summary>
        /// <param name="game"></param>
        /// <param name="contentName"></param>
        /// <param name="color"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameObject">The GameObject that this view draws</param>
        /// <param name="zIndex"></param>
        public GameObjectView(Game game, string contentName, Color color, SpriteBatch spriteBatch, IGameObject gameObject, float zIndex)
            : this(game, contentName, color, spriteBatch, gameObject, zIndex, Vector2.Zero)
        { }

        public GameObjectView(Game game, string contentName, Color color, SpriteBatch spriteBatch, IGameObject gameObject, float zIndex, Vector2 textureOriginOffset)
            : base(game)
        {
            this.textureOriginOffset = textureOriginOffset;
            this.Color = color;
            this.contentName = contentName;
            this.SpriteBatch = spriteBatch;
            this.ZIndex = zIndex;
            this.GameObject = gameObject;

            this.Texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);
            this.Origin = GetTextureCenter(this.Texture);
            var scaleX = gameObject.Width / Texture.Width;
            var scaleY = gameObject.Height / Texture.Height;

            this.Scale = new Vector2(scaleX, scaleY);

            gameObject.GameObjectRemoved += GameObjectRemoved;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, GameObject.Position + (textureOriginOffset * GameObject.Scale), null, Color, GameObject.Rotation, this.Origin, this.Scale, SpriteEffects.None, ZIndex);

            base.Draw(gameTime);
        }

        private void GameObjectRemoved(object sender, EventArgs e)
        {
            Game.Components.Remove(this);
        }

        private static Vector2 GetTextureCenter(Texture2D texture)
        {
            return new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        }
    }
}
