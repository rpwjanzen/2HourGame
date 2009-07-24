using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame {
    class Ship : PhysicsGameObject
    {        
        int GoldCapacity { get; set; }
        int Gold { get; set; }

        
        readonly float MaximumGoldTransferSpeed = 0.15f;
        bool CanTransferGold {
            get { return this.Speed < MaximumGoldTransferSpeed; }
        }

        readonly int MinimumSecondsBetweenLoadingGold = 2;
        TimeSpan LastGoldLoadTime = new TimeSpan();

        public Island HomeIsland { get; private set; }
        
        public int TotalGold {
            get { return HomeIsland.Gold + this.Gold; }
        }
        public int CarriedGold {
            get { return this.Gold; }
        }
        private bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }
        private bool LoadCooldownHasExpired(GameTime now) {
            return now.TotalGameTime.TotalSeconds - LastGoldLoadTime.TotalSeconds > MinimumSecondsBetweenLoadingGold;
        }

        CannonBallManager CannonBallManager { get; set; }
        Vector2 Velocity {
            get { return base.Body.LinearVelocity; }
        }
        Vector2 FiringVelocity { get; set; }

        TimeSpan LastFireTime { get; set; }
        // in seconds
        float CannonCooldownTime { get; set; }
        bool CannonHasCooledDown(GameTime now) {
            return now.TotalGameTime.TotalSeconds - LastFireTime.TotalSeconds > this.CannonCooldownTime;
        }

        public Ship(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, Island homeIsland, EffectManager effectManger, float zIndex, CannonBallManager cannonBallManager)
            : base(game, position, "boat", 0.6f, Color.White, spriteBatch, physicsSimulator, null, effectManger, zIndex)
        {
            this.GoldCapacity = 5;
            this.Gold = 0;
            this.HomeIsland = homeIsland;
            this.CannonBallManager = cannonBallManager;
            this.FiringVelocity = Vector2.UnitY;
            this.CannonCooldownTime = 2.0f;
            this.LastFireTime = new TimeSpan();
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
                effectManager.GoldPickupEffect(this);
            }
        }

        public void UnloadGoldToIsland(Island island) {
            island.AddGold(this.Gold);
            this.Gold = 0;
        }

        private void AddGold() {
            this.Gold++;
        }

        public void FireCannon(GameTime now) {
            if (CannonHasCooledDown(now)) {
                //get the left vector
                Vector2 left = new Vector2(-base.Body.GetBodyMatrix().Left.X, -base.Body.GetBodyMatrix().Left.Y);
                var thrust = left * 150.0f;
                
                // take into account the ship's momentum
                thrust += this.Velocity;

                var cannonBallPostion = (left * (this.Radius + 10)) + this.Position;
                
                var cannonBall = this.CannonBallManager.CreateCannonBall(cannonBallPostion, thrust);
                
                this.LastFireTime = now.TotalGameTime;
            }
        }
    }
}
