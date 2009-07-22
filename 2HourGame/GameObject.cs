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
        
        private Body body;
        protected Body Body { get { return this.body; } }
        

        private float boundsMultiplyer;

        SpriteBatch spriteBatch;
        Texture2D texture;
        Vector2 origin;

        string contentName;

        public GameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, PhysicsSimulator physicsSimulator)
            : base(game) {
            this.Color = Color.White;
            this.InitialPosition = initialPosition;
            this.contentName = contentName;
            this.boundsMultiplyer = boundsMultiplyer;
            this.physicsSimulator = physicsSimulator;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            texture = this.Game.Content.Load<Texture2D>(@"Content\" + contentName);
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            var radius = Math.Max(origin.X, origin.Y) * boundsMultiplyer;
            
            this.body = BodyFactory.Instance.CreateCircleBody(radius, 1.0f);
            this.body.Position = this.InitialPosition;
            body.RotationalDragCoefficient = 0.65f;
            body.LinearDragCoefficient = 0.95f;
            physicsSimulator.Add(this.body);

            this.geometry = GeomFactory.Instance.CreateCircleGeom(this.body, radius, 12);
            physicsSimulator.Add(geometry);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, Position, null, Color, Rotation, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
