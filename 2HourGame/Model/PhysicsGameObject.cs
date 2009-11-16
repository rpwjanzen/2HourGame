using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Factories;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class PhysicsGameObject : GameObject
    {
        protected PhysicsWorld PhysicsWorld { get; private set; }

        protected Geom Geometry { get; private set; }
        protected Body Body { get; private set; }

        public override Vector2 Position {
            get { return Body.Position; }
            //protected set { Body.Position = value; }
        }
        public override Vector2 Velocity {
            get { return Body.LinearVelocity; }
            protected set { Body.LinearVelocity = value; }
        }
        public override float Rotation {
            get { return this.Body.Rotation; }
            protected set { Body.Rotation = value; }
        }

        public float Speed { get { return this.Body.LinearVelocity.Length(); } }

        public PhysicsGameObject(PhysicsWorld world, Vector2 initialPosition, float width, float height)
            : this(world, initialPosition, width, height, 0.0f) { }

        public PhysicsGameObject(PhysicsWorld world, Vector2 initialPosition, float width, float height, float initialRotation)
            : base(world, initialPosition, width, height)
        {
            this.PhysicsWorld = world;

            this.Body = BodyFactory.Instance.CreateEllipseBody(base.HalfWidth, base.HalfHeight, 1.0f);
            this.Body.Tag = this;
            this.Body.Position = base.Position;
            this.Body.Rotation = initialRotation;
            this.Body.LinearDragCoefficient = 0.95f;
            this.Body.RotationalDragCoefficient = 10.0f;

            this.Geometry = GeomFactory.Instance.CreateEllipseGeom(this.Body, base.HalfWidth, base.HalfHeight, 12);
            this.Geometry.Tag = this;
            
            this.Geometry.OnCollision += HandleCollisionEvent;
        }

        public override void Spawn() {
            PhysicsWorld.PhysicsSimulator.Add(Body);
            PhysicsWorld.PhysicsSimulator.Add(Geometry);

            base.Spawn();
        }

        public override void Die() {
            PhysicsWorld.PhysicsSimulator.Remove(this.Body);
            PhysicsWorld.PhysicsSimulator.Remove(this.Geometry);

            base.Die();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns>True, if the collision should happen</returns>
        public virtual bool Touch(Actor other, Contact contactPoint) {
            return true;
        }

        bool HandleCollisionEvent(Geom geometry1, Geom geometry2, ContactList contactList)
        {
            var actor0 = geometry1.Tag as Actor;
            var actor1 = geometry2.Tag as Actor;
            if (actor0 == this && actor1 != null) {
                return Touch(actor1, contactList[0]);
            } else if (actor1 == this && actor0 != null) {
                return Touch(actor0, contactList[0]);
            }

            return true;
        }
    }
}
