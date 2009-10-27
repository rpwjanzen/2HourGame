using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;
using System;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    /// <summary>
    /// 
    /// </summary>
    class GameObjectView : ActorView
    {
        protected Color Color { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Texture2D Texture { get; set; }
        protected float ZIndex { get; set; }
        protected Vector2 Scale { get; set; }
        protected Vector2 Origin { get; set; }
        protected GameObject GameObject { get; set; }        

        /// <summary>
        /// Stores offset to adjust drawing for objects where the collision area is not based on the texture size.
        /// </summary>
        private Vector2 textureOriginOffset;

        string contentName;

        public GameObjectView(World world, string contentName, Color color, GameObject gameObject, float zIndex, TextureManager textureManager, AnimationManager am)
            : this(world, contentName, color, gameObject, zIndex, Vector2.Zero, textureManager, am)
        { }

        public GameObjectView(World world, string contentName, Color color, GameObject gameObject, float zIndex, Vector2 textureOriginOffset, TextureManager textureManager, AnimationManager am)
            : base(gameObject, world, textureManager, am)
        {
            this.textureOriginOffset = textureOriginOffset;
            this.Color = color;
            this.contentName = contentName;
            this.ZIndex = zIndex;
            this.GameObject = gameObject;

            gameObject.Died += new EventHandler(gameObject_Died);
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = TextureManager[contentName];
            this.Origin = GetTextureCenter(this.Texture);
            var scaleX = GameObject.Width / Texture.Width;
            var scaleY = GameObject.Height / Texture.Height;

            this.Scale = new Vector2(scaleX, scaleY);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, GameObject.Position + (textureOriginOffset * this.Scale), null, Color, GameObject.Rotation, this.Origin, this.Scale, SpriteEffects.None, ZIndex);
            
            base.Draw(gameTime, spriteBatch);
        }

        void gameObject_Died(object sender, EventArgs e) {
            World.GarbageActorViews.Add(this);
        }

        private static Vector2 GetTextureCenter(Texture2D texture)
        {
            return new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        }
    }
}
