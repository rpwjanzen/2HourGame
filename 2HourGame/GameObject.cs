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
        PhysicsSimulator physicsSimulator;
        readonly Vector2 InitialPosition;
        Geom geometry;

        public Color Color { get; private set; }
        public Vector2 Position { get { return geometry.Position; } }
        float Rotation { get { return this.geometry.Rotation; } }

        public Body Body { get; private set; }
        public float Radius { get; private set; }

        private float boundsMultiplyer;

        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 origin;

        public float zIndex;

        string contentName;

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game) {
            this.Color = color;
            this.InitialPosition = initialPosition;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
			this.spriteBatch = spriteBatch;
			zIndex = 0;
            this.physicsSimulator = physicsSimulator;
        }

        protected override void LoadContent()
        {
            texture = this.Game.Content.Load<Texture2D>(@"Content\" + contentName);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.Radius = Math.Max(origin.X, origin.Y) * boundsMultiplyer;
            
            this.Body = BodyFactory.Instance.CreateCircleBody(this.Radius, 1.0f);
            this.Body.Position = this.InitialPosition;
            this.Body.LinearDragCoefficient = 0.95f;
            this.Body.RotationalDragCoefficient = 10000.0f;
            physicsSimulator.Add(this.Body);

            this.geometry = GeomFactory.Instance.CreateCircleGeom(this.Body, this.Radius, 12);
            physicsSimulator.Add(geometry);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            //spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, zIndex);
            //spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
