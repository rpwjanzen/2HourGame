using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class OldGameObject : DrawableGameComponent
    {
        protected readonly Vector2 InitialPosition;

        public Color Color { get; private set; }
        public virtual Vector2 Position { get; private set; }
        public virtual float Rotation { get; private set; }

        public float Radius { get; private set; }
        
        protected float boundsMultiplyer;

        SpriteBatch spriteBatch;
        Texture2D texture;
        AnimatedTextureInfo animatedTextureInfo;
        TimeSpan animationStartTime;
        bool firstDraw;

        protected Vector2 origin;

        public float ZIndex { get; private set; }

        string contentName;

        protected AnimationManager effectManager;

        Game game;

        public OldGameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , AnimationManager effectManager)
            : this(game, initialPosition, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, effectManager, 0) {
        }

        public OldGameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , AnimationManager effectManager, float zIndex)
            : base(game)
        {
            this.game = game;
            this.Color = color;
            this.InitialPosition = initialPosition;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
            this.spriteBatch = spriteBatch;
            this.animatedTextureInfo = animatedTextureInfo;
            this.effectManager = effectManager;
            firstDraw = this.animatedTextureInfo != null ? true : false;
            this.ZIndex = zIndex;
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>(@"Content\" + contentName);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.Radius = Math.Max(origin.X, origin.Y) * boundsMultiplyer;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            // animated sprite
            if (animatedTextureInfo != null)
            {
                if (firstDraw)
                {
                    firstDraw = false;
                    animationStartTime = gameTime.TotalGameTime;
                }

                // get the frame to draw
                int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds) 
                    * animatedTextureInfo.FramesPerSecond));

                if (animatedTextureInfo.AnimateOnceOnly && totalFrame == animatedTextureInfo.TotalFrames)
                    animationDone();
                else
                {
                    int frame = totalFrame % animatedTextureInfo.TotalFrames;

                    Rectangle source = new Rectangle((int)animatedTextureInfo.ImageDimensions.X * frame, 0, (int)animatedTextureInfo.ImageDimensions.X, (int)animatedTextureInfo.ImageDimensions.Y);

                    spriteBatch.Draw(texture, Position, source, Color, Rotation, origin, animatedTextureInfo.Scale, SpriteEffects.None, ZIndex);
                }
            }
            else // regular sprite
            {
                spriteBatch.Draw(texture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, ZIndex);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// This animation is done so it should be removed from the list of components.
        /// </summary>
        private void animationDone() 
        {
            game.Components.Remove(this);
        }
    }
}
