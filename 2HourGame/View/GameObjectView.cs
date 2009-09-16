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
    class GameObjectView : DrawableGameComponent
    {
        protected Color Color;

        protected SpriteBatch spriteBatch;
        protected Texture2D texture;

        protected float ZIndex;

        private string contentName;

        public GameObject gameObject;

        //public GameObjectView(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch)
        //    : this(game, position, contentName, scale, color, spriteBatch, 0) {
        //}

        //public GameObjectView(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch, float zIndex)
        //    : base(game)
        //{
        //    this.Color = color;
        //    this.Position = position;
        //    this.contentName = contentName;
        //    this.Scale = scale;
        //    this.spriteBatch = spriteBatch;
        //    this.ZIndex = zIndex;
        //}

        //public GameObjectView(Game game, string contentName, Color color, SpriteBatch spriteBatch, GameObject gameObject)
        //    : this(game, contentName, color, spriteBatch, gameObject, 0)
        //{
        //}

        public GameObjectView(Game game, string contentName, Color color, SpriteBatch spriteBatch, GameObject gameObject, float zIndex)
            : base(game)
        {
            this.Color = color;
            this.contentName = contentName;
            this.spriteBatch = spriteBatch;
            this.ZIndex = zIndex;
            this.gameObject = gameObject;

            this.texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);

            gameObject.GameObjectRemoved += gameObjectRemoved;
        }

        protected override void LoadContent()
        {

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, gameObject.Position, null, Color, gameObject.Rotation, gameObject.Origin, gameObject.Scale, SpriteEffects.None, ZIndex);

            base.Draw(gameTime);
        }

        private void gameObjectRemoved()
        {
            gameObject.game.Components.Remove(this);
            gameObject = null;
        }
    }
}
