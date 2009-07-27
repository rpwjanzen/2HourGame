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
        public virtual Vector2 Position { get; private set; }
        public virtual float Rotation { get; private set; }

        public float XAxis { get; private set; }
        public float YAxis { get; private set; }
        public bool IsCircle {
            get { return this.XAxis == this.YAxis; }
        }

        float boundsMultiplyer;

        SpriteBatch spriteBatch;
        Texture2D texture;
        AnimatedTextureInfo animatedTextureInfo;
        TimeSpan animationStartTime;
        bool firstDraw;

        Vector2 Origin { get; set; }

        float ZIndex { get; set; }

        string contentName;

        public GameObject(Game game, Vector2 position, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo)
            : this(game, position, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, 0) {
        }

        public GameObject(Game game, Vector2 position, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , float zIndex)
            : base(game)
        {
            this.Color = color;
            this.Position = position;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
            this.spriteBatch = spriteBatch;
            this.animatedTextureInfo = animatedTextureInfo;
            firstDraw = this.animatedTextureInfo != null ? true : false;
            this.ZIndex = zIndex;
        }

        protected override void LoadContent()
        {
            texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(contentName);
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.XAxis = Origin.X * boundsMultiplyer;
            this.YAxis = Origin.Y * boundsMultiplyer;

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
                spriteBatch.Draw(texture, Position, null, Color, Rotation, Origin, 1.0f, SpriteEffects.None, ZIndex);
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
