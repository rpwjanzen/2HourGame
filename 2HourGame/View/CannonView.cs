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

    class CannonView<T> : DrawableGameComponent where T : PhysicsGameObject, ICannonMountable
    {
        private const string cannonTextureName = "cannonAnimation";
        private Cannon<T> cannon;

        bool firstDraw;
        TimeSpan animationStartTime;
        AnimatedTextureInfo animatedTextureInfo;
        float zIndex;
        SpriteBatch spriteBatch;
        Color color;
        Texture2D texture;

        public CannonView(Game game, Color color, SpriteBatch spriteBatch, CannonType cannonType, Cannon<T> cannon) : base(game)
        {
            this.color = color;
            this.spriteBatch = spriteBatch;
            this.cannon = cannon;
            cannon.CannonFired += HandleCannonFiredEvent;
            
            this.animatedTextureInfo = ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo(cannonTextureName);
            this.texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager))).getTexture(cannonTextureName);

            this.zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannon);

            this.firstDraw = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (cannon.ShouldCannonDraw)
            {
                if (firstDraw)
                {
                    firstDraw = false;
                    animationStartTime = gameTime.TotalGameTime;
                }

                // get the frame to draw
                int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds)
                    * animatedTextureInfo.FramesPerSecond));

                int frame;
                if (totalFrame >= animatedTextureInfo.TotalFrames)
                    frame = 0;
                else
                    frame = totalFrame % animatedTextureInfo.TotalFrames;

                int dx = (int)animatedTextureInfo.WindowSize.X * frame;
                int width = (int)animatedTextureInfo.WindowSize.X;
                int height = (int)animatedTextureInfo.WindowSize.Y;
                Rectangle source = new Rectangle(dx, 0, width, height);
                var cannonImageOffset = (cannon.cannonType == CannonType.LeftCannon ? animatedTextureInfo.GetRotatedOffset(cannon.Rotation) : -animatedTextureInfo.GetRotatedOffset(cannon.Rotation));
                spriteBatch.Draw(
                    texture,
                    cannon.Position + cannonImageOffset,
                    source,
                    color,
                    cannon.Rotation,
                    animatedTextureInfo.Origin,
                    animatedTextureInfo.Scale,
                    SpriteEffects.None,
                    zIndex
                );
            }
        }

        void HandleCannonFiredEvent(object sender, CannonFiredEventArgs e)
        {
            playAnimation(e.FiredTime);
        }

        private void playAnimation(GameTime gameTime) 
        {
            animationStartTime = gameTime.TotalGameTime;
        }
    }
}
