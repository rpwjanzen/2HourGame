using System;
using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    /// <summary>
    /// 
    /// </summary>
    internal class GameObjectView : ActorView
    {
        private readonly string contentName;

        /// <summary>
        /// Stores offset to adjust drawing for objects where the collision area is not based on the texture size.
        /// </summary>
        private readonly Vector2 textureOriginOffset;

        public GameObjectView(World world, string contentName, Color color, GameObject gameObject, float zIndex,
                              TextureManager textureManager, AnimationManager am)
            : this(world, contentName, color, gameObject, zIndex, Vector2.Zero, textureManager, am)
        {
        }

        public GameObjectView(World world, string contentName, Color color, GameObject gameObject, float zIndex,
                              Vector2 textureOriginOffset, TextureManager textureManager, AnimationManager am)
            : base(gameObject, world, textureManager, am)
        {
            this.textureOriginOffset = textureOriginOffset;
            Color = color;
            this.contentName = contentName;
            ZIndex = zIndex;
            GameObject = gameObject;

            gameObject.Died += gameObject_Died;
        }

        protected Color Color { get; set; }
        protected SpriteBatch SpriteBatch { get; set; }
        protected Texture2D Texture { get; set; }
        protected float ZIndex { get; set; }
        protected Vector2 Scale { get; set; }
        protected Vector2 Origin { get; set; }
        protected GameObject GameObject { get; set; }

        public override void LoadContent(ContentManager content)
        {
            Texture = TextureManager[contentName];
            Origin = GetTextureCenter(Texture);
            float scaleX = GameObject.Width/Texture.Width;
            float scaleY = GameObject.Height/Texture.Height;

            Scale = new Vector2(scaleX, scaleY);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, GameObject.Position + (textureOriginOffset*Scale), null, Color,
                             GameObject.Rotation, Origin, Scale, SpriteEffects.None, ZIndex);

            base.Draw(gameTime, spriteBatch);
        }

        private void gameObject_Died(object sender, EventArgs e)
        {
            World.GarbageActorViews.Add(this);
        }

        private static Vector2 GetTextureCenter(Texture2D texture)
        {
            return new Vector2(texture.Width/2.0f, texture.Height/2.0f);
        }
    }
}