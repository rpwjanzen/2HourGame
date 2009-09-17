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
    public delegate void Notifiaction();
    public delegate void CannonFired(CannonType cannonType, GameTime gameTime);

    class Ship : PhysicsGameObject
    {
        public event Notifiaction ShipSank;
        public event Notifiaction ShipSpawned;
        public event CannonFired CannonHasBeenFired;

        public readonly double maxHealth = 5;
        public double health { get; private set; }
        public bool isActive { get; private set; }

        public int GoldCapacity { get; private set; }
        public int Gold { get; private set; }

        private bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }

        readonly int MinimumSecondsBetweenLoadingGold = 2;
        TimeSpan LastGoldLoadTime = new TimeSpan();
        private bool LoadCooldownHasExpired(GameTime now) {
            return now.TotalGameTime.TotalSeconds - LastGoldLoadTime.TotalSeconds > MinimumSecondsBetweenLoadingGold;
        }

        CannonBallManager CannonBallManager { get; set; }
        Vector2 Velocity {
            get { return base.Body.LinearVelocity; }
        }
        Vector2 FiringVelocity { get; set; }

        TimeSpan LastFireTimeLeft { get; set; }
        TimeSpan LastFireTimeRight { get; set; }
        // in seconds
        float CannonCooldownTime { get; set; }
        bool LeftCannonHasCooledDown(GameTime now)
        {
            return now.TotalGameTime.TotalSeconds - LastFireTimeLeft.TotalSeconds > this.CannonCooldownTime;
        }
        bool RightCannonHasCooledDown(GameTime now)
        {
            return now.TotalGameTime.TotalSeconds - LastFireTimeRight.TotalSeconds > this.CannonCooldownTime;
        }

        Vector2 spawnPoint;
        TimeSpan timeOfDeath;
        bool setTimeOfDeathTimespan;
        double respawnTimeSeconds = 10;
        bool respawnTimeIsOver(GameTime now) 
        {
            return now.TotalGameTime.TotalSeconds - timeOfDeath.TotalSeconds > respawnTimeSeconds;
        }

        public Ship(Game game, Vector2 position, PhysicsSimulator physicsSimulator, CannonBallManager cannonBallManager, string contentName, float rotation)
            : base(game, position, physicsSimulator, contentName, 0.6f, rotation)
        {
            this.GoldCapacity = 3;
            this.Gold = 0;
            this.CannonBallManager = cannonBallManager;
            this.FiringVelocity = Vector2.UnitY;
            this.CannonCooldownTime = 2.0f;
            this.LastFireTimeLeft = new TimeSpan();
            this.LastFireTimeRight = new TimeSpan();
            this.health = 5;
            this.isActive = true;
            this.spawnPoint = position;
            this.setTimeOfDeathTimespan = false;

            Geometry.OnCollision += ShipCollision;
            this.Body.RotationalDragCoefficient = 2500.0f;
            base.Rotation = rotation;

            ShipSank += hideShip;
            ShipSpawned += unHideShip;
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
                ShipSpawned();
            }
        }

        private bool ShipCollision(Geom geom1, Geom geom2, ContactList contactList)
        {
            if (geom1.Tag != null && geom2.Tag != null) {
                if (geom1.Tag.GetType() == typeof(CannonBall) || geom2.Tag.GetType() == typeof(CannonBall)) {

                    if (geom1.Tag.GetType() == typeof(CannonBall))
                    {
                        this.CannonBallManager.RemoveCannonBall((CannonBall)geom1.Tag);
                        hitByCannonBall((CannonBall)geom1.Tag);
                    }
                    else
                    {
                        this.CannonBallManager.RemoveCannonBall((CannonBall)geom2.Tag);
                        hitByCannonBall((CannonBall)geom2.Tag);
                    }
                }
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
            health -= (cannonBall.Speed != 0 ? cannonBall.Speed : 120)/75;

            if (health <= 0)
            {
                ShipSank();
                Gold = 0;
            }
        }
        
        public void Thrust(float amount) {
            //get the forward vector
            Vector2 forward = new Vector2(-base.Body.GetBodyMatrix().Up.X, -base.Body.GetBodyMatrix().Up.Y);
            var thrust = forward * amount;
            base.Body.ApplyForceAtLocalPoint(thrust, Vector2.Zero);
        }

        public void Accelerate(float amount) {
            this.Thrust(amount);
        }

        public void Rotate(float amount) {
            base.Body.ApplyTorque(amount);
        }

        public void LoadGoldFromIsland(Island island, GameTime now) {
            if (island.HasGold && !this.IsFull && this.LoadCooldownHasExpired(now)) {
                island.RemoveGold();
                this.AddGold();
                this.LastGoldLoadTime = now.TotalGameTime;
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).GoldPickupEffect(this.Position);
            }
        }

        public void UnloadGoldToIsland(Island island) {
            island.AddGold(this.Gold);
            this.Gold = 0;
        }

        private void AddGold() {
            this.Gold++;
        }

        public void FireCannon(GameTime now, CannonType cannonType) {
            if ((cannonType == CannonType.LeftCannon && LeftCannonHasCooledDown(now)) ||
                (cannonType == CannonType.RightCannon && RightCannonHasCooledDown(now)))
            {
                CannonHasBeenFired(cannonType, now);
                
                //get the right vector
                Vector2 firingVector = cannonType == CannonType.LeftCannon ? new Vector2(base.Body.GetBodyMatrix().Left.X, base.Body.GetBodyMatrix().Left.Y) : new Vector2(base.Body.GetBodyMatrix().Right.X, base.Body.GetBodyMatrix().Right.Y);
                var thrust = firingVector * 65.0f;
                
                // take into account the ship's momentum
                thrust += this.Velocity;

                var cannonBallPostion = (firingVector * (this.XRadius + 5)) + this.Position;
                var smokePosition = firingVector * (this.XRadius - 2) + this.Position;

                ((IEffectManager)Game.Services.GetService(typeof(IEffectManager))).CannonSmokeEffect(smokePosition);
                var cannonBall = this.CannonBallManager.CreateCannonBall(cannonBallPostion, thrust);

                base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y)/8);

                if (cannonType == CannonType.LeftCannon)
                    this.LastFireTimeLeft = now.TotalGameTime;
                else
                    this.LastFireTimeRight = now.TotalGameTime;
            }
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
            health = maxHealth;
            AddToPhysicsSimulator();
            isActive = true;
        }
    }
}
