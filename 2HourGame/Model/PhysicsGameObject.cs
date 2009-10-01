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

using _2HourGame.View;
using _2HourGame.Model.GameServices;

namespace _2HourGame.Model
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

        public PhysicsGameObject(Game game, Vector2 initialPosition, PhysicsSimulator physicsSimulator, string contentName, float scale, float initialRotation)
            : this(game, initialPosition, physicsSimulator, contentName, scale) 
        {
            this.Body.Rotation = initialRotation;
        }

        public PhysicsGameObject(Game game, Vector2 initialPosition, PhysicsSimulator physicsSimulator, string contentName, float scale, Vector2 origin) 
            :base(game, initialPosition, contentName, scale, origin)
        {
            setup(physicsSimulator);
        }


        public PhysicsGameObject(Game game, Vector2 initialPosition, PhysicsSimulator physicsSimulator, string contentName, float scale)
            : base(game, initialPosition, contentName, scale)
        {
            setup(physicsSimulator);
        }

        public void setup(PhysicsSimulator physicsSimulator) 
        {
            this.physicsSimulator = physicsSimulator;

            this.Body = BodyFactory.Instance.CreateEllipseBody(base.XRadius, base.YRadius, 1.0f);
            this.Body.Position = base.Position;
            this.Body.LinearDragCoefficient = 0.95f;
            this.Body.RotationalDragCoefficient = 10.0f;
            physicsSimulator.Add(this.Body);

            this.Geometry = GeomFactory.Instance.CreateEllipseGeom(this.Body, base.XRadius, base.YRadius, 12);
            this.Geometry.Tag = this;
            this.Geometry.CollisionCategories = ((CollisionCategoryManager)Game.Services.GetService(typeof(CollisionCategoryManager))).getCollisionCategory(this.GetType());
            this.Geometry.CollidesWith = ((CollisionCategoryManager)Game.Services.GetService(typeof(CollisionCategoryManager))).getCollidesWith(this.GetType());
            physicsSimulator.Add(Geometry);
        }

        public void RemoveFromPhysicsSimulator() {
            physicsSimulator.Remove(this.Body);
            physicsSimulator.Remove(this.Geometry);
        }

        protected void AddToPhysicsSimulator() 
        {
            physicsSimulator.Add(this.Body);
            physicsSimulator.Add(this.Geometry);
        }
    }
}
