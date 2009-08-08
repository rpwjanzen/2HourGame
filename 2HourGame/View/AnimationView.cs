using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Model
{
    class AnimationView : GameObjectView
    {
        protected AnimatedTextureInfo animatedTextureInfo;
        protected TimeSpan animationStartTime;
        protected bool firstDraw;

        public AnimationView(Game game, string contentName, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo,
            GameObject gameObject, float zIndex)
            : base(game, contentName, color, spriteBatch, gameObject, zIndex)
        {
            this.animatedTextureInfo = animatedTextureInfo;
            this.firstDraw = this.animatedTextureInfo != null ? true : false;
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

                spriteBatch.Draw(base.texture, gameObject.Position + animatedTextureInfo.drawOffset(gameObject.Rotation), source, base.Color, gameObject.Rotation, gameObject.Origin, gameObject.Scale, SpriteEffects.None, base.ZIndex);
            }
        }

        /// <summary>
        /// This animation is done so it should be removed from the list of components.
        /// </summary>
        private void animationDone()
        {
            base.Game.Components.Remove(gameObject);
            base.Game.Components.Remove(this);
        }
    }
}
