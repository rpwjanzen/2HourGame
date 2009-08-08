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
    public enum CannonType { LeftCannon, RightCannon }

    class CannonView : AnimationView
    {
        public CannonType cannonType { get; private set; }

        public bool isActive;

        //public CannonView(Game game, Vector2 initialPosition, float scale, Color color, SpriteBatch spriteBatch, CannonType cannonType)
        //    : base(game, initialPosition, "cannonAnimation", scale, color, spriteBatch, ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo("cannon"), ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipCannon))   
        public CannonView(Game game, Color color, SpriteBatch spriteBatch, CannonType cannonType, GameObject gameObject)
            : base(game, "cannonAnimation", color, spriteBatch, ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo("cannonAnimation"), gameObject, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipCannon))
        {
            this.cannonType = cannonType;
            isActive = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (isActive)
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
                    gameObject.Position + (cannonType == CannonType.LeftCannon ? animatedTextureInfo.drawOffset(gameObject.Rotation) : -animatedTextureInfo.drawOffset(gameObject.Rotation)),
                    source,
                    Color,
                    gameObject.Rotation,
                    gameObject.Origin,
                    animatedTextureInfo.scale,
                    SpriteEffects.None,
                    ZIndex
                    );
            }

        }

        public void PlayAnimation(GameTime gameTime) 
        {
            animationStartTime = gameTime.TotalGameTime;
        }

        public void UpdatePosition(Vector2 newPosition) 
        {
            gameObject.Position = newPosition;
        }

        public void UpdateRotation(float rotation) 
        {
            gameObject.Rotation = rotation;
        }
    }
}
