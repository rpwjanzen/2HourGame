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

        public CannonView(World world, Cannon cannon, Color color, TextureManager textureManager, AnimationManager am)
            : base(cannon, world, textureManager, am)
        {
            this.color = color;
            this.cannon = cannon;
            this.cannon.Fired += cannon_Fired;
        }

        void cannon_Fired(object sender, FiredEventArgs e) {
            AnimationManager.PlayAnimation(Animation.CannonSmoke, e.SmokePosition);
        }

        public override void LoadContent(ContentManager content)
        {
            this.animatedTextureInfo = AnimationManager[Animation.CannonFired];
            this.texture = TextureManager[cannonTextureName];

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
