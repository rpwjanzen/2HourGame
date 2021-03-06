﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class CannonView : DrawableGameComponent
    {
        private const string cannonTextureName = "cannonAnimation";
        private Cannon cannon;

        AnimatedTextureInfo animatedTextureInfo;
        float zIndex;
        SpriteBatch spriteBatch;
        Color color;
        Texture2D texture;

        public CannonView(Game game, Color color, SpriteBatch spriteBatch, Cannon cannon)
            : base(game)
        {
            this.color = color;
            this.spriteBatch = spriteBatch;
            this.cannon = cannon;
        }

        protected override void LoadContent()
        {
            this.animatedTextureInfo = ((IEffectManager)Game.Services.GetService(typeof(IEffectManager)))[Animation.CannonFired];
            this.texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager)))[Content.CannonAnimation];

            this.zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannon);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
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
