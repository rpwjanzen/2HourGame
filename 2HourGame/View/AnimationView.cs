using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class AnimationView : DrawableGameComponent
    {
        protected AnimatedTextureInfo AnimatedTextureInfo;
        protected TimeSpan AnimationStartTime;
        protected bool FirstDraw;
        protected IGameObject GameObject;
        protected SpriteBatch SpriteBatch;

        protected Vector2 Scale;
        protected Texture2D Texture;
        protected Color Color;
        protected Vector2 Origin;
        protected float ZIndex;

        public event EventHandler AnimationFinished;

        Content content;

        public AnimationView(Game game, Content content, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo,
            IGameObject gameObject, float zIndex) : base(game)
        {
            this.content = content;
            this.Color = color;
            this.SpriteBatch = spriteBatch;
            this.AnimatedTextureInfo = animatedTextureInfo;
            this.GameObject = gameObject;
            this.ZIndex = zIndex;

            this.FirstDraw = this.AnimatedTextureInfo != null;
        }

        protected override void LoadContent()
        {
            this.Texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager)))[content];
            this.Scale = new Vector2(AnimatedTextureInfo.Scale, AnimatedTextureInfo.Scale);
            this.Origin = AnimatedTextureInfo.WindowCenter;            

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
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

                SpriteBatch.Draw(Texture, GameObject.Position + AnimatedTextureInfo.GetRotatedOffset(GameObject.Rotation), source, Color, GameObject.Rotation, Origin, Scale, SpriteEffects.None, ZIndex);
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
