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
        protected IGameObject GameObject { get; set; }

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
            : base(game)
        {
            this.Color = color;
            this.contentName = contentName;
            this.SpriteBatch = spriteBatch;
            this.ZIndex = zIndex;
            this.GameObject = gameObject;

            this.Texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);
            if (gameObject != null)
            {
                gameObject.GameObjectRemoved += GameObjectRemoved;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, GameObject.Position, null, Color, GameObject.Rotation, GameObject.Origin, GameObject.Scale, SpriteEffects.None, ZIndex);

            base.Draw(gameTime);
        }

        private void GameObjectRemoved(object sender, EventArgs e)
        {
            Game.Components.Remove(this);
        }
    }
}
