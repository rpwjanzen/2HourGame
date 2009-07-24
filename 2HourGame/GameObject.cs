using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class GameObject : DrawableGameComponent
    {
        // is this something that the PhysicsGameObject should handle???
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

        protected Game game;

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo)
            : this(game, initialPosition, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, 0) {
        }

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , float zIndex)
            : base(game)
        {
            this.game = game;
            this.Color = color;
            this.InitialPosition = initialPosition;
            Position = initialPosition;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
            this.spriteBatch = spriteBatch;
            this.animatedTextureInfo = animatedTextureInfo;
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
                    * animatedTextureInfo.framesPerSecond));

                if (animatedTextureInfo.animateOnceOnly && totalFrame == animatedTextureInfo.totalFrames)
                    animationDone();
                else
                {
                    int frame = totalFrame % animatedTextureInfo.totalFrames;

                    Rectangle source = new Rectangle((int)animatedTextureInfo.imageSize.X * frame, 0, (int)animatedTextureInfo.imageSize.X, (int)animatedTextureInfo.imageSize.Y);

                    spriteBatch.Draw(texture, Position, source, Color, Rotation, origin, animatedTextureInfo.scale, SpriteEffects.None, ZIndex);
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
