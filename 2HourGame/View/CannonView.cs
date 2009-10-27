using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class CannonView : ActorView
    {
        private const string cannonTextureName = "cannonAnimation";
        private Cannon cannon;

        AnimatedTextureInfo animatedTextureInfo;
        float zIndex;
        Color color;
        Texture2D texture;

        public CannonView(World world, Cannon cannon, Color color)
            : base(cannon, world)
        {
            this.color = color;
            this.cannon = cannon;
        }

        public override void LoadContent(ContentManager content)
        {
            this.animatedTextureInfo = ((IAnimationManager)Game.Services.GetService(typeof(IAnimationManager)))[Animation.CannonFired];
            this.texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager)))[cannonTextureName];

            this.zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannon);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // get the frame to draw
            int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - cannon.lastTimeFired.TotalSeconds)
                * this.animatedTextureInfo.FramesPerSecond));

            int frame;
            if (totalFrame >= animatedTextureInfo.TotalFrames)
                frame = 0;
            else
                frame = totalFrame % animatedTextureInfo.TotalFrames;

            int dx = (int)animatedTextureInfo.WindowSize.X * frame;
            int width = (int)animatedTextureInfo.WindowSize.X;
            int height = (int)animatedTextureInfo.WindowSize.Y;
            Rectangle source = new Rectangle(dx, 0, width, height);
            spriteBatch.Draw(
                texture,
                cannon.Position,
                source,
                color,
                cannon.Rotation,
                animatedTextureInfo.WindowCenter,
                animatedTextureInfo.Scale,
                SpriteEffects.None,
                zIndex
            );
        }
    }
}
