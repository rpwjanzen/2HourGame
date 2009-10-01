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
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class Ship : PhysicsGameObject, ICannonMountable
    {
        public event EventHandler ShipSank;
        public event EventHandler ShipSpawned;

        readonly double maxHealth = 5;
        double Health { get; set; }
        public bool IsDamaged {
            get { return Health < maxHealth; }
        }
        public double HealthPercentage
        {
            get { return Health / maxHealth; }
        }
        const double healthRepairAmount = 0.10;
        public bool isActive { get; private set; }

        public int GoldCapacity { get; private set; }
        public int Gold { get; private set; }

        public bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }

        public Cannon<Ship> leftCannon;
        public Cannon<Ship> rightCannon;

        CannonBallManager cannonBallManager;

        public Vector2 Velocity {
            get { return base.Body.LinearVelocity; }
        }
        Vector2 FiringVelocity { get; set; }

        Vector2 spawnPoint;
        TimeSpan timeOfDeath;
        bool setTimeOfDeathTimespan;
        double respawnTimeSeconds = 10;
        bool respawnTimeIsOver(GameTime now) 
        {
            return now.TotalGameTime.TotalSeconds - timeOfDeath.TotalSeconds > respawnTimeSeconds;
        }
        public bool IsCannonVisible { get { return isActive; } }

        public Ship(Game game, Vector2 position, CannonBallManager cannonBallManager, string contentName, float rotation)
            : base(game, position, contentName, 34, 60, rotation)
        {
            this.GoldCapacity = 3;
            this.Gold = 0;
            this.FiringVelocity = Vector2.UnitY;
            this.Health = 5;
            this.isActive = true;
            this.spawnPoint = position;
            this.setTimeOfDeathTimespan = false;

            Geometry.OnCollision += ShipCollision;
            this.Body.RotationalDragCoefficient = 2500.0f;
            base.Rotation = rotation;
            this.cannonBallManager = cannonBallManager;

            leftCannon = new Cannon<Ship>(game, this, cannonBallManager, CannonType.LeftCannon);
            rightCannon = new Cannon<Ship>(game, this, cannonBallManager, CannonType.RightCannon);

            ShipSank += ShipSankEventHandler;
            ShipSpawned += ShipSpawnedEventHandler;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (setTimeOfDeathTimespan)
            {
                setTimeOfDeathTimespan = false;
                timeOfDeath = gameTime.TotalGameTime;
            }

            if (!isActive && respawnTimeIsOver(gameTime))
            {
                isActive = true;
                ShipSpawned(this, EventArgs.Empty);
            }
        }

        public void Repair()
        {
            var repairedHealth = Health + healthRepairAmount;
            Health = Math.Min(repairedHealth, maxHealth);
        }

        public void Thrust(float amount)
        {
            //get the forward vector
            Vector2 forward = new Vector2(-base.Body.GetBodyMatrix().Up.X, -base.Body.GetBodyMatrix().Up.Y);
            var thrust = forward * amount;
            base.Body.ApplyForceAtLocalPoint(thrust, Vector2.Zero);
        }

        public void Accelerate(float amount)
        {
            this.Thrust(amount);
        }

        public void Rotate(float amount)
        {
            base.Body.ApplyTorque(amount);
        }

        public void LoadGoldFromIsland(Island island, GameTime now)
        {
            if (island.HasGold && !this.IsFull)
            {
                island.RemoveGold();
                this.AddGold();
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).GoldPickupEffect(this.Position);
            }
        }

        public void UnloadGoldToIsland(Island island)
        {
            island.AddGold(this.Gold);
            this.Gold = 0;
        }

        public void FireCannon(GameTime now, CannonType cannonType) 
        {
            Cannon<Ship> cannon;
            if (cannonType == CannonType.LeftCannon)
                cannon = leftCannon;
            else
                cannon = rightCannon;

            Vector2 thrust = cannon.attemptFireCannon(now);
            base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y) / 8);
        }

        /// <summary>
        /// Handles ship getting hit by a cannonball
        /// </summary>
        /// <param name="myGeom">Source Geom</param>
        /// <param name="otherGeom">Colliding Geom</param>
        /// <param name="contactList">Contact points of collision</param>
        /// <returns>True, if the event should be cancelled.</returns>
        private bool ShipCollision(Geom myGeom, Geom otherGeom, ContactList contactList)
        {
            var cannonBall = otherGeom.Tag as CannonBall;
            if (cannonBall != null)
            {
                this.cannonBallManager.RemoveCannonBall(cannonBall);
                hitByCannonBall(cannonBall);
            }
            return true;
        }

        /// <summary>
        /// Ships status reaction to being hit by a cannon ball.
        /// </summary>
        private void hitByCannonBall(CannonBall cannonBall) 
        {
            if (Gold > 0)
            {
                Gold--;
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).BoatHitByCannonEffect(cannonBall.Position);
            }

            // if a ship is too close when it fires then the cannon ball it can have a speed of 0.
            // We could consider puttin the cannon ball in a collision group with the ship
            // and then creating it closer to the ship.
            Health -= (cannonBall.Speed != 0 ? cannonBall.Speed : 120)/75;

            if (Health <= 0)
            {
                ShipSank(this, EventArgs.Empty);
                Gold = 0;
            }
        }

        private void AddGold() {
            this.Gold++;
        }

        void ShipSankEventHandler(object sender, EventArgs e)
        {
            hideShip();
        }


        void ShipSpawnedEventHandler(object sender, EventArgs e)
        {
            unHideShip();
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideShip()
        {
            setTimeOfDeathTimespan = true;
            RemoveFromPhysicsSimulator();
            isActive = false;
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void unHideShip()
        {
            base.Body.Position = spawnPoint;
            Health = maxHealth;
            AddToPhysicsSimulator();
            isActive = true;
        }
    }
}
