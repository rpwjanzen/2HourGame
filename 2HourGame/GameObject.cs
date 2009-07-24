using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

namespace _2HourGame
{
    class GameObject : DrawableGameComponent
    {
        protected readonly Vector2 InitialPosition;

        public Color Color { get; private set; }
        public Vector2 Position { get { return geometry.Position; } }
        float Rotation { get { return this.geometry.Rotation; } }

        protected Geom geometry;
        protected Body body;
        public Body Body { get { return this.body; } }
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

        protected EffectManager effectManager;

        Game game;

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , EffectManager effectManager)
            : this(game, initialPosition, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, effectManager, 0) {
        }

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, AnimatedTextureInfo animatedTextureInfo
            , EffectManager effectManager, float zIndex)
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
            
            this.body = BodyFactory.Instance.CreateCircleBody(this.Radius, 1.0f);
            this.body.Position = this.InitialPosition;
            this.body.LinearDragCoefficient = 0.95f;
            this.body.RotationalDragCoefficient = 10000.0f;

            this.geometry = GeomFactory.Instance.CreateCircleGeom(this.Body, this.Radius, 12);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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
