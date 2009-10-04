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
    class ShipSankEventArgs : EventArgs
    {
        public GameTime SinkTime { get; private set; }
        public ShipSankEventArgs(GameTime sinkTime)
        {
            this.SinkTime = sinkTime;
        }
    }
    class ShipSpawnedEventArgs : EventArgs
    {
        public GameTime SpawnTime { get; private set; }
        public ShipSpawnedEventArgs(GameTime spawnTime)
        {
            this.SpawnTime = spawnTime;
        }
    }
    class Ship : PhysicsGameObject, IShip
    {
        const float CannonBuffer = -8.0f;
        public event EventHandler<ShipSankEventArgs> ShipSank;
        public event EventHandler<ShipSpawnedEventArgs> ShipSpawned;

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
        public bool IsAlive { get; private set; }

        public int GoldCapacity { get; private set; }
        public int Gold { get; private set; }

        public bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }

        public Cannon<IShip> LeftCannon { get; private set; }
        public Cannon<IShip> RightCannon { get; private set; }

        CannonBallManager cannonBallManager;

        public Vector2 Velocity {
            get { return base.Body.LinearVelocity; }
        }
        
        Vector2 FiringVelocity { get; set; }
        Vector2 spawnPoint;
        TimeSpan timeOfDeath;
        double respawnTimeSeconds = 10;
        bool respawnTimeIsOver(GameTime now) 
        {
            return now.TotalGameTime.TotalSeconds - timeOfDeath.TotalSeconds > respawnTimeSeconds;
        }
        public bool IsCannonVisible { get { return IsAlive; } }

        public Ship(Game game, Vector2 position, CannonBallManager cannonBallManager, float rotation)
            : base(game, position, 34, 60, rotation)
        {
            this.GoldCapacity = 3;
            this.Gold = 0;
            this.FiringVelocity = Vector2.UnitY;
            this.Health = 5;
            this.IsAlive = true;
            this.spawnPoint = position;

            this.OnCollision += ShipCollision;
            this.Body.RotationalDragCoefficient = 2500.0f;
            this.cannonBallManager = cannonBallManager;

            var rotationMatrix = Matrix.Identity;

            var leftCannonOffset = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y) * ((this.Width / 2.0f) + CannonBuffer);
            var leftCannonRotation = MathHelper.ToRadians(-90);
            LeftCannon = new Cannon<IShip>(game, this, cannonBallManager, leftCannonOffset, leftCannonRotation);

            var rightCannonOffset = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y) * ((this.Width / 2.0f) + CannonBuffer);
            var rightCannonRotation = MathHelper.ToRadians(90);
            RightCannon = new Cannon<IShip>(game, this, cannonBallManager, rightCannonOffset, rightCannonRotation);

            ShipSank += ShipSankEventHandler;
            ShipSpawned += ShipSpawnedEventHandler;

            base.Rotation = rotation;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsAlive && respawnTimeIsOver(gameTime))
            {
                IsAlive = true;
                RaiseShipSpawnedEvent(gameTime);
            }
        }

        public void Repair()
        {
            var repairedHealth = Health + healthRepairAmount;
            Health = Math.Min(repairedHealth, maxHealth);
        }

        private void Thrust(float amount)
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

        public void LoadGoldFromIsland(Island island)
        {
            if (island.HasGold && !this.IsFull)
            {
                island.RemoveGold();
                this.AddGold();
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.GetGold, this.Position);
            }
        }

        public void UnloadGoldToIsland(Island island)
        {
            island.AddGold(this.Gold);
            this.Gold = 0;
        }

        public void FireCannon(GameTime now, CannonType cannonType) 
        {
            Cannon<IShip> cannon;
            if (cannonType == CannonType.LeftCannon)
                cannon = LeftCannon;
            else
                cannon = RightCannon;

            Vector2 thrust = cannon.attemptFireCannon(now);
            base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y) / 8);
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
        /// Ships status reaction to being hit by a cannon ball.
        /// </summary>
        private void hitByCannonBall(CannonBall cannonBall, GameTime gameTime) 
        {
            if (Gold > 0)
            {
                Gold--;
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.BoatHitByCannon, cannonBall.Position);
            }

            // if a ship is too close when it fires then the cannon ball it can have a speed of 0.
            // We could consider puttin the cannon ball in a collision group with the ship
            // and then creating it closer to the ship.
            Health -= (cannonBall.Speed != 0 ? cannonBall.Speed : 120)/75;

            if (Health <= 0)
            {
                RaiseShipSankEvent(gameTime);
                Gold = 0;
            }
        }

        private void AddGold() {
            this.Gold++;
        }

        void ShipSankEventHandler(object sender, ShipSankEventArgs e)
        {
            this.timeOfDeath = e.SinkTime.TotalGameTime;
            hideShip();
        }


        void ShipSpawnedEventHandler(object sender, ShipSpawnedEventArgs e)
        {
            unHideShip();
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideShip()
        {
            RemoveFromPhysicsSimulator();
            IsAlive = false;
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void unHideShip()
        {
            base.Body.Position = spawnPoint;
            Health = maxHealth;
            AddToPhysicsSimulator();
            IsAlive = true;
        }

        void RaiseShipSankEvent(GameTime sinkTime)
        {
            if (ShipSank != null)
            {
                ShipSank(this, new ShipSankEventArgs(sinkTime));
            }
        }

        void RaiseShipSpawnedEvent(GameTime spawnTime)
        {
            if (ShipSpawned != null)
            {
                ShipSpawned(this, new ShipSpawnedEventArgs(spawnTime));
            }
        }
    }
}
