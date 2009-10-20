using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View.GameServices;
using System.Diagnostics;
using _2HourGame.Model.GameServices;

namespace _2HourGame.Model
{
    class ObjectDestroyedEventArgs : EventArgs
    {
        public GameTime DestroyedTime { get; private set; }
        public ObjectDestroyedEventArgs(GameTime destroyedTime)
        {
            this.DestroyedTime = destroyedTime;
        }
    }
    class ObjectRespawnedEventArgs : EventArgs
    {
        public GameTime RespawnTime { get; private set; }
        public ObjectRespawnedEventArgs(GameTime respawnTime)
        {
            this.RespawnTime = respawnTime;
        }
    }
    class ObjectDamagedEventArgs : EventArgs
    {
        public GameTime DamagedTime { get; private set; }
        public CannonBall DamagingObject { get; private set; }
        public ObjectDamagedEventArgs(GameTime damagedTime, CannonBall damagingObject)
        {
            this.DamagedTime = damagedTime;
            this.DamagingObject = damagingObject;
        }
    }
    class DamageablePhysicsGameObject : PhysicsGameObject, IDamageableGameObject
    {
        public event EventHandler<ObjectDestroyedEventArgs> ObjectDestroyed;
        public event EventHandler<ObjectRespawnedEventArgs> ObjectRespawned;
        public event EventHandler<ObjectDamagedEventArgs> ObjectDamaged;

        const double healthRepairAmount = 0.10;
        public bool IsAlive { get; protected set; }

        double Health { get; set; }
        readonly double maxHealth = 5;

        public bool IsDamaged
        {
            get { return Health < maxHealth; }
        }
        public double HealthPercentage
        {
            get { return Health / maxHealth; }
        }

        Vector2 spawnPoint;
        protected Timer respawnTimer = null;
        private bool respawnOnDeath = false;

        CannonBallManager cannonBallManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initialPosition"></param>
        /// <param name="cannonBallManager"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        /// <param name="respawnTime">Time in seconds before a destroyed object will respawn.</param>
        public DamageablePhysicsGameObject(Game game, Vector2 initialPosition, CannonBallManager cannonBallManager,
            float width, float height, float rotation, int respawnTime)
            : this(game, initialPosition, cannonBallManager, width, height, rotation)
        {
            respawnTimer = new Timer(respawnTime);
            respawnOnDeath = true;
        }

        /// <summary>
        /// If you do not pass in a respawn time then this object will delete itself when it is destroyed.
        /// </summary>
        /// <param name="game"></param>
        /// <param name="initialPosition"></param>
        /// <param name="cannonBallManager"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="rotation"></param>
        public DamageablePhysicsGameObject(Game game, Vector2 initialPosition, CannonBallManager cannonBallManager,
            float width, float height, float rotation)
            : base(game, initialPosition, width, height, rotation, ((CollisionGroupManager)game.Services.GetService(typeof(CollisionGroupManager))).getNextFreeCollisionGroup())
        {
            this.Health = 5;
            this.IsAlive = true;
            this.spawnPoint = initialPosition;

            this.OnCollision += ShipCollision;
            this.cannonBallManager = cannonBallManager;

            ObjectDestroyed += ObjectDestroyedEventHandler;
            ObjectRespawned += ObjectRespawnedEventHandler;
            ObjectDamaged += ObjectDamagedEventHandler;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Objects should be removed if they have no respawn timer.
            Debug.Assert(IsAlive || respawnTimer != null, "DamageablePhysicsGameObject is dead with no respawn timer and is trying to update (should be removed).");

            if (!IsAlive && respawnTimer.TimerHasElapsed(gameTime))
            {
                IsAlive = true;
                RaiseObjectRespawnedEvent(gameTime);
            }
        }

        /// <summary>
        /// Handles ship getting hit by a cannonball
        /// </summary>
        private void ShipCollision(object sender, CollisionEventArgs e)
        {
            var cannonBall = e.Other as CannonBall;
            if (cannonBall != null)
            {
                this.cannonBallManager.RemoveCannonBall(cannonBall);
                hitByCannonBall(cannonBall, e.CollisionTime);
            }
        }

        /// <summary>
        /// Object status reaction to being hit by a cannon ball.
        /// </summary>
        private void hitByCannonBall(CannonBall cannonBall, GameTime gameTime)
        {
            RaiseObjectDamagedEvent(gameTime, cannonBall);
        }

        protected void takeDamage(GameTime gameTime, double damage) 
        {
            // if a ship is too close when it fires then the cannon ball it can have a speed of 0.
            // We could consider puttin the cannon ball in a collision group with the ship
            // and then creating it closer to the ship.
            Health -= damage;

            if (Health <= 0)
            {
                RaiseObjectDestroyedEvent(gameTime);
            }
        }

        public void Repair()
        {
            var repairedHealth = Health + healthRepairAmount;
            Health = Math.Min(repairedHealth, maxHealth);
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideObject()
        {
            RemoveFromPhysicsSimulator();
            IsAlive = false;
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void unHideObject()
        {
            base.Body.Position = spawnPoint;
            Health = maxHealth;
            AddToPhysicsSimulator();
            IsAlive = true;
        }

        void ObjectDamagedEventHandler(object sernder, ObjectDamagedEventArgs e) 
        {
            takeDamage(e.DamagedTime, (e.DamagingObject.Speed != 0 ? e.DamagingObject.Speed : 120) / 75);
        }

        void ObjectDestroyedEventHandler(object sender, ObjectDestroyedEventArgs e)
        {
            if (respawnOnDeath)
            {
                this.respawnTimer.resetTimer(e.DestroyedTime.TotalGameTime);
                hideObject();
            }
            else
                base.Game.Components.Remove(this);
        }

        void ObjectRespawnedEventHandler(object sender, ObjectRespawnedEventArgs e)
        {
            unHideObject();
        }

        void RaiseObjectDamagedEvent(GameTime damagedTime, CannonBall damagingObject)
        {
            if (ObjectDamaged != null)
            {
                ObjectDamaged(this, new ObjectDamagedEventArgs(damagedTime, damagingObject));
            }
        }

        void RaiseObjectDestroyedEvent(GameTime destroyedTime)
        {
            if (ObjectDestroyed != null)
            {
                ObjectDestroyed(this, new ObjectDestroyedEventArgs(destroyedTime));
            }
        }

        void RaiseObjectRespawnedEvent(GameTime respawnTime)
        {
            if (ObjectRespawned != null)
            {
                ObjectRespawned(this, new ObjectRespawnedEventArgs(respawnTime));
            }
        }
    }
}
