using System;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Model
{
    internal class AnimationView : ActorView
    {
        private readonly AnimatedTextureInfo AnimatedTextureInfo;
        private readonly Color Color;
        private readonly GameObject GameObject;
        private readonly float ZIndex;
        private readonly string contentName;
        private TimeSpan AnimationStartTime;
        private bool FirstDraw;
        private Vector2 Origin;

        private Vector2 Scale;
        private Texture2D Texture;

        public AnimationView(World world, string contentName, Color color, AnimatedTextureInfo animatedTextureInfo,
                             GameObject gameObject, float zIndex, TextureManager textureManager, AnimationManager am)
            : base(gameObject, world, textureManager, am)
        {
            this.contentName = contentName;
            Color = color;
            AnimatedTextureInfo = animatedTextureInfo;
            GameObject = gameObject;
            ZIndex = zIndex;

            FirstDraw = AnimatedTextureInfo != null;
        }

        public event EventHandler AnimationFinished;

        public override void LoadContent(ContentManager content)
        {
            Texture = TextureManager[contentName];
            Scale = new Vector2(AnimatedTextureInfo.Scale, AnimatedTextureInfo.Scale);
            Origin = AnimatedTextureInfo.WindowCenter;

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (FirstDraw)
            {
                FirstDraw = false;
                AnimationStartTime = gameTime.TotalGameTime;
            }

            // get the frame to draw
            var totalFrame = (int) Math.Round(((gameTime.TotalGameTime.TotalSeconds - AnimationStartTime.TotalSeconds)
                                               *AnimatedTextureInfo.FramesPerSecond));

            if (totalFrame == AnimatedTextureInfo.TotalFrames*AnimatedTextureInfo.NumAnimationIterations)
            {
                RaiseAnimationFinishedEvent();
            }
            else
            {
                int frame = totalFrame%AnimatedTextureInfo.TotalFrames;
                int dx = (int) AnimatedTextureInfo.WindowSize.X*frame;
                var width = (int) AnimatedTextureInfo.WindowSize.X;
                var height = (int) AnimatedTextureInfo.WindowSize.Y;

                var source = new Rectangle(dx, 0, width, height);

                spriteBatch.Draw(Texture,
                                 GameObject.Position + AnimatedTextureInfo.GetRotatedOffset(GameObject.Rotation), source,
                                 Color, GameObject.Rotation, Origin, Scale, SpriteEffects.None, ZIndex);
            }
        }

        private void RaiseAnimationFinishedEvent()
        {
            if (AnimationFinished != null)
            {
                AnimationFinished(this, EventArgs.Empty);
            }
        }
    }
}