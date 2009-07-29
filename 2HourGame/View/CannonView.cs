using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;

namespace _2HourGame.View
{
    class CannonView : GameObject
    {
        public enum CannonType { LeftCannon, RightCannon }

        public CannonType cannonType { get; private set; }

        public CannonView(Game game, Vector2 initialPosition, string contentName, float scale, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo, CannonType cannonType)
            : base(game, initialPosition, contentName, scale, color, spriteBatch, animatedTextureInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipCannon))
        {
            this.cannonType = cannonType;
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
            
            int frame;
            if (totalFrame >= animatedTextureInfo.totalFrames)
                frame = 0;
            else
                frame = totalFrame % animatedTextureInfo.totalFrames;

            Rectangle source = new Rectangle((int)animatedTextureInfo.imageSize.X * frame, 0, (int)animatedTextureInfo.imageSize.X, (int)animatedTextureInfo.imageSize.Y);
            spriteBatch.Draw(
                texture, 
                Position + (cannonType == CannonType.LeftCannon ? animatedTextureInfo.drawOffset(Rotation) : -animatedTextureInfo.drawOffset(Rotation)),
                source, 
                Color, 
                Rotation, 
                Origin, 
                animatedTextureInfo.scale, 
                SpriteEffects.None, 
                ZIndex
                );

            //base.Draw(gameTime);
        }

        public void PlayAnimation(GameTime gameTime) 
        {
            animationStartTime = gameTime.TotalGameTime;
        }

        public void UpdatePosition(Vector2 newPosition) 
        {
            base.Position = newPosition;
        }

        public void UpdateRotation(float rotation) 
        {
            base.Rotation = rotation;
        }
    }
}
