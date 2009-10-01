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
    public enum CannonType { LeftCannon, RightCannon, FrontCannon }

    class CannonView<T> : AnimationView where T : PhysicsGameObject, ICannonMountable
    {
        private const string cannonTextureName = "cannonAnimation";
        private readonly Vector2 cannonOrigin;
        private Cannon<T> cannon;

        public CannonView(Game game, Color color, SpriteBatch spriteBatch, Cannon<T> cannon)
            : base(game, cannonTextureName, Color.White, spriteBatch, ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo(cannonTextureName), null, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannon))
        {
            this.cannon = cannon;
            cannonOrigin = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager))).getTextureCentre(cannonTextureName, 1f);

            cannon.CannonFired += playAnimation;
        }

        public override void Draw(GameTime gameTime)
        {
            if (cannon.drawCannon())
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
                SpriteBatch.Draw(
                    Texture,
                    cannon.getCannonPosition() + (cannon.cannonType == CannonType.LeftCannon ? animatedTextureInfo.drawOffset(cannon.getCannonRotation()) : -animatedTextureInfo.drawOffset(cannon.getCannonRotation())),
                    source,
                    Color,
                    cannon.getCannonRotation(),
                    cannonOrigin,
                    animatedTextureInfo.scale,
                    SpriteEffects.None,
                    ZIndex
                );
            }
        }

        private void playAnimation(GameTime gameTime) 
        {
            animationStartTime = gameTime.TotalGameTime;
        }
    }
}
