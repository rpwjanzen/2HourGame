using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2HourGame {
    class AnimatedGameObject : DrawableGameObject {
        bool firstDraw;
        TimeSpan animationStartTime;
        AnimatedTextureInfo AnimatedTextureInfo;

        public AnimatedGameObject(Vector2 position, float rotation, Rectangle sourceRectangle, Texture2D texture, Color color, Vector2 origin, float scale, SpriteEffects spriteEffect, float layerDepth, AnimatedTextureInfo animatedTextureInfo)
            : base(position, rotation, sourceRectangle, texture, color, origin, scale, spriteEffect, layerDepth) {
            this.AnimatedTextureInfo = animatedTextureInfo;
        }

        public override void DrawWith(SpriteBatch spriteBatch) {
            base.DrawWith(spriteBatch);
        }

        public void DrawWith(SpriteBatch spriteBatch, GameTime gameTime) {
                if (firstDraw) {
                    firstDraw = false;
                    animationStartTime = gameTime.TotalGameTime;
                }

                // get the theoretical total number of frames elapsed
                int totalFrame = CalculateTotalFrames(gameTime);

                if (AnimationCompleted(totalFrame)) {
                    animationDone();
                }  else {
                    // figure out which frame we should draw
                    int currentFrame = totalFrame % AnimatedTextureInfo.TotalFrames;
                    Rectangle source = GetFrameRectangle(currentFrame);
                    spriteBatch.Draw(base.Texture, base.Position, source, base.Color, base.Rotation, base.Origin, base.Scale, base.SpriteEffect, base.LayerDepth);
                }
        }

        Rectangle GetFrameRectangle(int frameNumber) {
            return new Rectangle(
                        AnimatedTextureInfo.ImageWidth * frameNumber,
                        0,
                        AnimatedTextureInfo.ImageWidth,
                        AnimatedTextureInfo.ImageHeight);
        }

        bool AnimationCompleted(int currentFrame) {
            return AnimatedTextureInfo.AnimateOnceOnly && currentFrame >= AnimatedTextureInfo.TotalFrames;
        }
        void animationDone() { }

        int CalculateTotalFrames(GameTime gameTime) {
            return (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds) * AnimatedTextureInfo.FramesPerSecond));
        }
    }
}
