using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Factories;

namespace _2HourGame.Model
{
    class SimpleCannonBall : GameComponent
    {
        Timer timer;

        public bool Expired
        {
            get { return timer.Expired; }
        }

        Geom geometry;
        Body body;
        public const float Radius = 5.0f;
        float mass;

        /// <summary>
        /// If false, then the cannonball has been removed from the physics engine and is not longer active. True otherwise.
        /// </summary>
        public bool IsActive { get; private set; }

        public Vector2 Position
        {
            get { return body.Position; }
        }

        public SimpleCannonBall(Game game, Vector2 position) : base(game)
        {
            mass = 1.0f;
            timer = new Timer(game);
            timer.Interval = 1500.0f;

            var physicsService = (IPhysicsService)game.Services.GetService(typeof(IPhysicsService));
            body = BodyFactory.Instance.CreateCircleBody(physicsService.PhysicsSimulator, Radius, mass);
            geometry = GeomFactory.Instance.CreateCircleGeom(physicsService.PhysicsSimulator, body, Radius, 10);
            body.Position = position;
            IsActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// Removes the cannonball from its physics engine.
        /// </summary>
        public void RemoveFromPhysicsEngine()
        {
            var physicsService = (IPhysicsService)Game.Services.GetService(typeof(IPhysicsService));
            physicsService.PhysicsSimulator.Remove(geometry);
            physicsService.PhysicsSimulator.Remove(body);
            IsActive = false;
        }

        public void Fire(Vector2 force)
        {
            body.ApplyImpulse(force);
        }
    }
}
