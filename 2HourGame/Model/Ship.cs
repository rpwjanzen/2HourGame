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
    class Ship : PhysicsGameObject
    {
        private Color shipColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        private const float ShipScale = 0.6f;

        private const double maxHealth = 5;
        public double health { get; private set; }
        public bool isActive { get; private set; }

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

        CannonView LeftCannonView;
        CannonView RightCannonView;

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
            : base(game, position, "shipHull", ShipScale, Color.White, spriteBatch, physicsSimulator, null, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull))
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
            this.health = 5;
            this.isActive = true;
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
        /// Ship reaction to being hit by a cannon ball.
        /// </summary>
        private void hitByCannonBall(CannonBall cannonBall) 
        {
            if (Gold > 0)
            {
                Gold--;
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).BoatHitByCannonEffect(cannonBall.Position);
            }

            // if a ship is too close when it fires then the cannon ball as a speed of 0.
            // We could consider puttin the cannon ball in a collision group with the ship
            // and then creating it closer to the ship.
            health -= (cannonBall.Speed != 0 ? cannonBall.Speed : 120)/50;

            if (health <= 0)
                hideShip();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            Geometry.OnCollision += ShipCollision;
            this.Body.RotationalDragCoefficient = 2500.0f;
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipGunwale");
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipRigging");

            LeftCannonView = initializeCannonView(CannonView.CannonType.LeftCannon);
            RightCannonView = initializeCannonView(CannonView.CannonType.RightCannon);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateCannonView(LeftCannonView);
            UpdateCannonView(RightCannonView);
        }

        public override void Draw(GameTime gameTime)
        {
            if (isActive)
            {
                base.Draw(gameTime);
                base.spriteBatch.Draw(gunwale, Position, null, shipColor, Rotation, base.Origin, this.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGunwale));
                base.spriteBatch.Draw(rigging, Position, null, Color.White, Rotation, base.Origin, this.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipRigging));
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

        public void FireCannon(GameTime now, bool isLeftCannon) {
            if ((isLeftCannon && LeftCannonHasCooledDown(now)) || (!isLeftCannon && RightCannonHasCooledDown(now))) {
                // start the firing animation
                if(isLeftCannon)
                    LeftCannonView.PlayAnimation(now);
                else
                    RightCannonView.PlayAnimation(now);
                
                //get the right vector
                Vector2 firingVector = isLeftCannon ? new Vector2(base.Body.GetBodyMatrix().Left.X, base.Body.GetBodyMatrix().Left.Y) : new Vector2(base.Body.GetBodyMatrix().Right.X, base.Body.GetBodyMatrix().Right.Y);
                var thrust = firingVector * 65.0f;
                
                // take into account the ship's momentum
                thrust += this.Velocity;

                var cannonBallPostion = (firingVector * (this.XRadius + 5)) + this.Position;
                var smokePosition = firingVector * (this.XRadius - 2) + this.Position;

                ((IEffectManager)Game.Services.GetService(typeof(IEffectManager))).CannonSmokeEffect(smokePosition);
                var cannonBall = this.CannonBallManager.CreateCannonBall(cannonBallPostion, thrust);

                base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y)/8);

                if (isLeftCannon)
                    this.LastFireTimeLeft = now.TotalGameTime;
                else
                    this.LastFireTimeRight = now.TotalGameTime;
            }
        }


        private CannonView initializeCannonView(CannonView.CannonType cannonType)
        {
            CannonView newCannonView = new CannonView(
                Game,
                getCannonPosition(cannonType),
                "cannonAnimation",
                ShipScale,
                Color.White,
                spriteBatch,
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo("cannon"),
                cannonType
                );

            newCannonView.UpdateRotation(getCannonRotation(cannonType));

            Game.Components.Add(newCannonView);
            return newCannonView;
        }

        private void UpdateCannonView(CannonView cannonView) 
        {
            cannonView.UpdateRotation(getCannonRotation(cannonView.cannonType));
            cannonView.UpdatePosition(getCannonPosition(cannonView.cannonType));
        }

        private float getCannonRotation(CannonView.CannonType cannonType) 
        {
            if (cannonType == CannonView.CannonType.LeftCannon)
                return 2f * (float)Math.PI + this.Rotation;
            else
                return (float)Math.PI + this.Rotation;
        }

        private Vector2 getCannonPosition(CannonView.CannonType cannonType) 
        {
            if (cannonType == CannonView.CannonType.LeftCannon)
                return new Vector2(base.Body.GetBodyMatrix().Left.X, base.Body.GetBodyMatrix().Left.Y) * (this.XRadius - 8) + this.Position;
            else
                return new Vector2(base.Body.GetBodyMatrix().Right.X, base.Body.GetBodyMatrix().Right.Y) * (this.XRadius - 8) + this.Position;
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideShip()
        {
            RemoveFromPhysicsSimulator();
            LeftCannonView.isActive = false;
            RightCannonView.isActive = false;
            isActive = false;
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).ShipSinking(this.Position);
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).FloatingCrate(this.Position);
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void displayShip() 
        {
            AddToPhysicsSimulator();
            isActive = true;
            LeftCannonView.isActive = true;
            RightCannonView.isActive = true;
        }
    }
}
