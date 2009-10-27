using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.Model
{
    class AnimationView : ActorView
    {
        AnimatedTextureInfo AnimatedTextureInfo;
        TimeSpan AnimationStartTime;
        bool FirstDraw;
        GameObject GameObject;

        Vector2 Scale;
        Texture2D Texture;
        Color Color;
        Vector2 Origin;
        float ZIndex;

        public event EventHandler AnimationFinished;

        string contentName;

        public AnimationView(World world, string contentName, Color color, AnimatedTextureInfo animatedTextureInfo, GameObject gameObject, float zIndex)
            : base(gameObject, world)
        {
            this.contentName = contentName;
            this.Color = color;
            this.AnimatedTextureInfo = animatedTextureInfo;
            this.GameObject = gameObject;
            this.ZIndex = zIndex;

            this.FirstDraw = this.AnimatedTextureInfo != null;
        }

        public override void LoadContent(ContentManager content)
        {
            this.Texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager)))[contentName];
            this.Scale = new Vector2(AnimatedTextureInfo.Scale, AnimatedTextureInfo.Scale);
            this.Origin = AnimatedTextureInfo.WindowCenter;   

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
            int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - AnimationStartTime.TotalSeconds)
                * AnimatedTextureInfo.FramesPerSecond));

            if (totalFrame == AnimatedTextureInfo.TotalFrames * AnimatedTextureInfo.NumAnimationIterations)
            {
                RaiseAnimationFinishedEvent();
            } 
            else
            {
                int frame = totalFrame % AnimatedTextureInfo.TotalFrames;
                int dx = (int)AnimatedTextureInfo.WindowSize.X * frame;
                int width = (int)AnimatedTextureInfo.WindowSize.X;
                int height = (int)AnimatedTextureInfo.WindowSize.Y;

                Rectangle source = new Rectangle(dx, 0, width, height);

                spriteBatch.Draw(Texture, GameObject.Position + AnimatedTextureInfo.GetRotatedOffset(GameObject.Rotation), source, Color, GameObject.Rotation, Origin, Scale, SpriteEffects.None, ZIndex);
            }
        }

        void RaiseAnimationFinishedEvent()
        {
            if (AnimationFinished != null)
            {
                AnimationFinished(this, EventArgs.Empty);
            }
        }
    }
}
