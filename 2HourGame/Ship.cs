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

namespace _2HourGame {
    class Ship : PhysicsGameObject
    {
        private Color shipColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        public int GoldCapacity { get; private set; }
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

        public Ship(Game game, Color playerColor, Vector2 position, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, Island homeIsland, CannonBallManager cannonBallManager)
            : base(game, position, "shipHull", 0.6f, Color.White, spriteBatch, physicsSimulator, null, (float)ZIndexManager.drawnItemOrders.shipHull / 100)
        {
            this.GoldCapacity = 5;
            this.Gold = 0;
            this.HomeIsland = homeIsland;
            this.CannonBallManager = cannonBallManager;
            this.FiringVelocity = Vector2.UnitY;
            this.CannonCooldownTime = 2.0f;
            this.LastFireTimeLeft = new TimeSpan();
            this.LastFireTimeRight = new TimeSpan();
            this.shipColor = playerColor;
        }

        private bool ShipCollision(Geom geom1, Geom geom2, ContactList contactList)
        {
            if (!(geom1.Tag != null || geom2.Tag == null)) {
                if (geom1.Tag.GetType() == typeof(CannonBall) || geom2.Tag.GetType() == typeof(CannonBall)) {
                    takeDamage();

                    if (geom1.Tag.GetType() == typeof(CannonBall))
                        this.CannonBallManager.RemoveCannonBall((CannonBall)geom1.Tag);
                    else
                        this.CannonBallManager.RemoveCannonBall((CannonBall)geom2.Tag);
                }
            }
            return true;
        }

        private void takeDamage() 
        {
            if (Gold > 0)
                Gold--;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Geometry.OnCollision += ShipCollision;
            this.Body.RotationalDragCoefficient = 2500.0f;
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipGunwale");
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipRigging");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            base.spriteBatch.Draw(gunwale, Position, null, shipColor, Rotation, base.Origin, this.Scale, SpriteEffects.None, (float)ZIndexManager.drawnItemOrders.shipGunwale / 100);
            base.spriteBatch.Draw(rigging, Position, null, Color.White, Rotation, base.Origin, this.Scale, SpriteEffects.None, (float)ZIndexManager.drawnItemOrders.shipRigging / 100);
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

        public void FireCannon(GameTime now, bool isLeftCannon) {
            if ((isLeftCannon && LeftCannonHasCooledDown(now)) || (!isLeftCannon && RightCannonHasCooledDown(now))) {
                //get the right vector
                Vector2 firingVector = isLeftCannon ? new Vector2(base.Body.GetBodyMatrix().Left.X, base.Body.GetBodyMatrix().Left.Y) : new Vector2(base.Body.GetBodyMatrix().Right.X, base.Body.GetBodyMatrix().Right.Y);
                var thrust = firingVector * 75.0f;
                
                // take into account the ship's momentum
                thrust += this.Velocity;

                var cannonBallPostion = (firingVector * (this.XRadius + 10)) + this.Position;
                
                var cannonBall = this.CannonBallManager.CreateCannonBall(cannonBallPostion, thrust);

                base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y)/8);

                if (isLeftCannon)
                    this.LastFireTimeLeft = now.TotalGameTime;
                else
                    this.LastFireTimeRight = now.TotalGameTime;
            }
        }
    }
}
