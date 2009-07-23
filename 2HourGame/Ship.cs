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
        public float Speed {
            get { return base.Body.LinearVelocity.Length(); }
        }
        
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

        public Ship(Game game, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, Island homeIsland, EffectManager effectManger)
            : base(game, position, "boat", 0.6f, Color.White, spriteBatch, physicsSimulator, null, effectManger)
        {
            this.GoldCapacity = 5;
            this.Gold = 0;
            this.HomeIsland = homeIsland;
        }

        public void Accelerate(Vector2 amount) {
            base.Body.ApplyImpulse(amount);
        }

        public void Accelerate(float amount) {
            // calculate vector along direction the object is facing
            var bodyRotation = base.Body.GetBodyRotationMatrix();
            var v = bodyRotation.Up * amount;

            base.Body.ApplyImpulse(new Vector2(v.X, v.Y));
            throw new NotImplementedException();
        }

        public void Rotate(float amount) {
            //base.Body.ApplyAngularImpulse(amount);
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
    }
}
