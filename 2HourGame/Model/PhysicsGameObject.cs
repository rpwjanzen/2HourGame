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
    class CollisionEventArgs : EventArgs
    {
        public object Self { get; private set; }
        public object Other { get; private set; }
        public ContactList ContactList { get; private set; }
        public GameTime CollisionTime { get; private set; }

        public CollisionEventArgs(object self, object other, ContactList contactList, GameTime collisionTime)
        {
            this.Self = self;
            this.Other = other;
            this.ContactList = contactList;
            this.CollisionTime = collisionTime;
        }
    }
    class PhysicsGameObject : GameObject
    {

        IPhysicsSimulatorService physicsSimulatorService;

        /// <summary>
        /// The PhysicsSimulator that this object is part of
        /// </summary>
        PhysicsSimulator PhysicsSimulator
        {
            get { return physicsSimulatorService.PhysicsSimulator; }
        }

        /// <summary>
        /// The GameObject's Geom
        /// </summary>
        Geom Geometry { get; set; }
        
        /// <summary>
        /// Give public access to see the CollisionGroup.
        /// </summary>
        public int CollisionGroup
        { 
            get
            {
                return Geometry.CollisionGroup;
            }
        }

        /// <summary>
        /// The GameObject's Body
        /// </summary>
        protected Body Body { get; private set; }

        /// <summary>
        /// The GameObject's linear velocity
        /// </summary>
        public float Speed {
            get { return this.Body.LinearVelocity.Length(); }
        }

        public override Vector2 Position { get { return Geometry.Position; } }
        public override float Rotation { get { return this.Geometry.Rotation; } }
        public Vector2 Velocity { get { return Body.LinearVelocity; } }

        public event EventHandler<CollisionEventArgs> OnCollision;

        public PhysicsGameObject(Game game, Vector2 initialPosition, float width, float height)
            : this(game, initialPosition, width, height, 0) { }

        public PhysicsGameObject(Game game, Vector2 initialPosition, float width, float height, int collisionGroup)
            : this(game, initialPosition, width, height, 0.0f, collisionGroup) { }

        public PhysicsGameObject(Game game, Vector2 initialPosition, float width, float height, float initialRotation)
            : this(game, initialPosition, width, height, initialRotation, 0) { }

        public PhysicsGameObject(Game game, Vector2 initialPosition, float width, float height, float initialRotation, int collisionGroup)
            : base(game, initialPosition, width, height)
        {
            this.physicsSimulatorService = (IPhysicsSimulatorService)Game.Services.GetService(typeof(IPhysicsSimulatorService));

            this.Body = BodyFactory.Instance.CreateEllipseBody(base.HalfWidth, base.HalfHeight, 1.0f);
            this.Body.Position = base.Position;
            this.Body.Rotation = initialRotation;
            this.Body.LinearDragCoefficient = 0.95f;
            this.Body.RotationalDragCoefficient = 10.0f;
            PhysicsSimulator.Add(this.Body);

            this.Geometry = GeomFactory.Instance.CreateEllipseGeom(this.Body, base.HalfWidth, base.HalfHeight, 12);
            this.Geometry.Tag = this;
            this.Geometry.CollisionGroup = collisionGroup;
            this.Geometry.CollisionCategories = ((CollisionCategoryManager)Game.Services.GetService(typeof(CollisionCategoryManager))).getCollisionCategory(this.GetType());
            this.Geometry.CollidesWith = ((CollisionCategoryManager)Game.Services.GetService(typeof(CollisionCategoryManager))).getCollidesWith(this.GetType());
            PhysicsSimulator.Add(Geometry);

            this.Geometry.OnCollision += RaiseCollisionEvent;

            base.GameObjectRemoved += RemoveFromPhysicsSimulator;
        }

        public override void Update(GameTime gameTime)
        {
            //XXX - Hack to prevent AngularVelocity from reaching NaN
            if (Body.AngularVelocity > 5000)
                Body.AngularVelocity = 5000;

            base.Update(gameTime);
        }

        public void RemoveFromPhysicsSimulator(object sender, EventArgs e) 
        {
            RemoveFromPhysicsSimulator();
        }

        public void RemoveFromPhysicsSimulator() {
            PhysicsSimulator.Remove(this.Body);
            PhysicsSimulator.Remove(this.Geometry);
        }

        protected void AddToPhysicsSimulator() 
        {
            PhysicsSimulator.Add(this.Body);
            PhysicsSimulator.Add(this.Geometry);
        }

        bool RaiseCollisionEvent(Geom geometry1, Geom geometry2, ContactList contactList)
        {
            if (OnCollision != null)
            {
                OnCollision(this, new CollisionEventArgs(geometry1.Tag, geometry2.Tag, contactList, physicsSimulatorService.CollisionTime));
            }

            return true;
        }
    }
}
