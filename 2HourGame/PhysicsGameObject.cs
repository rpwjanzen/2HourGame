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
    class PhysicsGameObject : GameObject
    {
        PhysicsSimulator physicsSimulator;
        protected Geom Geometry { get; set; }
        public Body Body { get; private set; }
        public float Speed {
            get { return this.Body.LinearVelocity.Length(); }
        }

        public override Vector2 Position { get { return Geometry.Position; } }
        public override float Rotation { get { return this.Geometry.Rotation; } }

        public PhysicsGameObject(Game game, Vector2 initialPosition, string contentName, float boundsMultiplyer, Color color, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, AnimatedTextureInfo animatedTextureInfo
            , float zIndex)
            : base(game, initialPosition, contentName, boundsMultiplyer, color, spriteBatch, animatedTextureInfo, zIndex)
        {
            this.physicsSimulator = physicsSimulator;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.Body = BodyFactory.Instance.CreateEllipseBody(base.XRadius, base.YRadius, 1.0f);
            this.Body.Position = base.Position;
            this.Body.LinearDragCoefficient = 0.95f;
            this.Body.RotationalDragCoefficient = 10.0f;
            physicsSimulator.Add(this.Body);

            this.Geometry = GeomFactory.Instance.CreateEllipseGeom(this.Body, base.XRadius, base.YRadius, 12);
            this.Geometry.Tag = this;
            physicsSimulator.Add(Geometry);
        }

        public void RemoveFromPhysicsSimulator() {
            physicsSimulator.Remove(this.Body);
            physicsSimulator.Remove(this.Geometry);
        }
    }
}
