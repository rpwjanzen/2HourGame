using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class AnimationObject : GameObject
    {
        protected AnimatedTextureInfo animatedTextureInfo;
        protected TimeSpan animationStartTime;
        protected bool firstDraw;

        public AnimationObject(Game game, Vector2 position, string contentName, float scale, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , float zIndex)
            : base(game, position, contentName, scale, color, spriteBatch, zIndex)
        {
            this.animatedTextureInfo = animatedTextureInfo;
            this.firstDraw = this.animatedTextureInfo != null ? true : false;
        }

        protected override void setOrigin()
        {
            this.Origin = animatedTextureInfo.textureOrigin;
        }

        public override void Draw(GameTime gameTime)
        {
            if (firstDraw)
            {
                firstDraw = false;
                animationStartTime = gameTime.TotalGameTime;
            }

            // get the frame to draw
            int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds)
                * animatedTextureInfo.framesPerSecond));

            if (totalFrame == animatedTextureInfo.totalFrames * animatedTextureInfo.numAnimationIterations)
                animationDone();
            else
            {
                int frame = totalFrame % animatedTextureInfo.totalFrames;

                Rectangle source = new Rectangle((int)animatedTextureInfo.imageSize.X * frame, 0, (int)animatedTextureInfo.imageSize.X, (int)animatedTextureInfo.imageSize.Y);

                spriteBatch.Draw(texture, Position + animatedTextureInfo.drawOffset(Rotation), source, Color, Rotation, Origin, this.Scale, SpriteEffects.None, ZIndex);
            }
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
