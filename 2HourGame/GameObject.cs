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
        public virtual Vector2 Position { get; protected set; }
        public virtual float Rotation { get; protected set; } // in radians

        public float XRadius { get; private set; }
        public float YRadius { get; private set; }
        public float Width { get { return XRadius * 2; } }
        public float Height { get { return YRadius * 2; } }
        public bool IsCircle {
            get { return this.XRadius == this.YRadius; }
        }

        public float Scale { get; private set; }

        protected SpriteBatch spriteBatch;
        protected Texture2D texture;
        protected AnimatedTextureInfo animatedTextureInfo;
        protected TimeSpan animationStartTime;
        protected bool firstDraw;

        public Vector2 Origin { get; private set; }

        public float ZIndex { get; private set; }

        protected string contentName;

        public GameObject(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo)
            : this(game, position, contentName, scale, color, spriteBatch, animatedTextureInfo, 0) {
        }

        public GameObject(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , float zIndex)
            : base(game)
        {
            this.Color = color;
            this.Position = position;
            this.contentName = contentName;
            this.Scale = scale;
            this.spriteBatch = spriteBatch;
            this.animatedTextureInfo = animatedTextureInfo;
            this.firstDraw = this.animatedTextureInfo != null ? true : false;
            this.ZIndex = zIndex;
        }

        protected override void LoadContent()
        {
            this.texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);
            this.Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.XRadius = this.Origin.X * this.Scale;
            this.YRadius = this.Origin.Y * this.Scale;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            // animated sprite
            if (animatedTextureInfo != null)
            {
                if (firstDraw)
                {
                    firstDraw = false;
                    animationStartTime = gameTime.TotalGameTime;
                }

                // get the frame to draw
                int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds) 
                    * animatedTextureInfo.framesPerSecond));

                if (animatedTextureInfo.animateOnceOnly && totalFrame == animatedTextureInfo.totalFrames)
                    animationDone();
                else
                {
                    int frame = totalFrame % animatedTextureInfo.totalFrames;

                    Rectangle source = new Rectangle((int)animatedTextureInfo.imageSize.X * frame, 0, (int)animatedTextureInfo.imageSize.X, (int)animatedTextureInfo.imageSize.Y);

                    spriteBatch.Draw(texture, Position + animatedTextureInfo.textureDrawOffset, source, Color, Rotation, Origin, animatedTextureInfo.scale, SpriteEffects.None, ZIndex);
                }
            }
            else // regular sprite
            {
                spriteBatch.Draw(texture, Position, null, Color, Rotation, Origin, this.Scale, SpriteEffects.None, ZIndex);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// This animation is done so it should be removed from the list of components.
        /// </summary>
        private void animationDone() 
        {
            base.Game.Components.Remove(this);
        }
    }
}
