using System;
using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class CannonView : ActorView
    {
        private const string cannonTextureName = "cannonAnimation";
        private readonly Cannon cannon;

        private readonly Color color;
        private AnimatedTextureInfo animatedTextureInfo;
        private Texture2D texture;
        private float zIndex;

        public CannonView(World world, Cannon cannon, Color color, TextureManager textureManager, AnimationManager am)
            : base(cannon, world, textureManager, am)
        {
            this.color = color;
            this.cannon = cannon;
            this.cannon.Fired += cannon_Fired;
        }

        private void cannon_Fired(object sender, FiredEventArgs e)
        {
            AnimationManager.PlayAnimation(Animation.CannonSmoke, e.SmokePosition);
        }

        public override void LoadContent(ContentManager content)
        {
            animatedTextureInfo = AnimationManager[Animation.CannonFired];
            texture = TextureManager[cannonTextureName];

            zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannon);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // get the frame to draw
            var totalFrame = (int) Math.Round(((gameTime.TotalGameTime.TotalSeconds - cannon.lastTimeFired.TotalSeconds)
                                               *animatedTextureInfo.FramesPerSecond));

            int frame;
            if (totalFrame >= animatedTextureInfo.TotalFrames)
                frame = 0;
            else
                frame = totalFrame%animatedTextureInfo.TotalFrames;

            int dx = (int) animatedTextureInfo.WindowSize.X*frame;
            var width = (int) animatedTextureInfo.WindowSize.X;
            var height = (int) animatedTextureInfo.WindowSize.Y;
            var source = new Rectangle(dx, 0, width, height);
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