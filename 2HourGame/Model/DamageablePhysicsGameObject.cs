using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
    class DamageablePhysicsGameObject : PhysicsGameObject, IDamageableGameObject
    {
        public event EventHandler<ObjectDestroyedEventArgs> ObjectDestroyed;
        public event EventHandler<ObjectRespawnedEventArgs> ObjectRespawned;

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
        protected Timer respawnTimer = new Timer(10);

        public DamageablePhysicsGameObject(Game game, Vector2 initialPosition, float width, float height, float rotation, int collisionGroup)
            : base(game, initialPosition, width, height, rotation, collisionGroup)
        {
            this.Health = 5;
            this.IsAlive = true;
            this.spawnPoint = initialPosition;

            ObjectDestroyed += ObjectDestroyedEventHandler;
            ObjectRespawned += ObjectRespawnedEventHandler;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsAlive && respawnTimer.TimerHasElapsed(gameTime))
            {
                IsAlive = true;
                RaiseObjectRespawnedEvent(gameTime);
            }
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

        void ObjectDestroyedEventHandler(object sender, ObjectDestroyedEventArgs e)
        {
            this.respawnTimer.resetTimer(e.DestroyedTime.TotalGameTime);
            hideObject();
        }

        void ObjectRespawnedEventHandler(object sender, ObjectRespawnedEventArgs e)
        {
            unHideObject();
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
