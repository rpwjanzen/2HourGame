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
using _2HourGame.Model.GameServices;

namespace _2HourGame.Model
{
    class Ship : DamageablePhysicsGameObject, IShip
    {
        public int GoldCapacity { get; protected set; }
        public int Gold { get; protected set; }

        public bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }

        public List<Cannon> LeftCannons { get; protected set; }
        public List<Cannon> RightCannons { get; protected set; }
        
        Vector2 FiringVelocity { get; set; }

        private bool IsCannonVisible { get { return IsAlive; } }


        // Offset to move the cannons so they look good on the ship.
        readonly Vector2 leftCannonOffset = new Vector2(8, 4);
        readonly Vector2 rightCannonOffset = new Vector2(-8, 4);

        public Ship(Game game, Vector2 position, CannonBallManager cannonBallManager, float rotation)
            : base(game, position, cannonBallManager, 34, 60, rotation, 10)
        {
            // the direction to fire relative to the cannon
            this.FiringVelocity = Vector2.UnitY;

            this.Body.RotationalDragCoefficient = 2500.0f;

            base.Rotation = rotation;

            ObjectDestroyed += ShipSankEventHandler;
            ObjectDamaged += ShipDamagedEventHandler;

            LeftCannons = new List<Cannon>();
            RightCannons = new List<Cannon>();



            var rotationMatrix = Matrix.Identity;

            var leftCannonPosition = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y) * ((this.Width / 2.0f)) + leftCannonOffset;
            var leftCannonRotation = MathHelper.ToRadians(-90);
            LeftCannons.Add(new Cannon(game, this, cannonBallManager, leftCannonPosition, leftCannonRotation));

            var rightCannonPosition = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y) * ((this.Width / 2.0f)) + rightCannonOffset;
            var rightCannonRotation = MathHelper.ToRadians(90);
            RightCannons.Add(new Cannon(game, this, cannonBallManager, rightCannonPosition, rightCannonRotation));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
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

        public void FireLeftCannons(GameTime now) 
        {
            foreach(Cannon c in LeftCannons)
                FireCannon(now, c);
        }

        public void FireRightCannons(GameTime now) 
        {
            foreach(Cannon c in RightCannons)
                FireCannon(now, c);
        }

        private void FireCannon(GameTime now, Cannon cannonToFire) 
        {
            if (IsCannonVisible)
            {
                Vector2 thrust = cannonToFire.attemptFireCannon(now);
                base.Body.ApplyImpulse(new Vector2(-thrust.X, -thrust.Y) / 8);
            }
        }

        private void AddGold() {
            this.Gold++;
        }

        private void ShipDamagedEventHandler(object sender, ObjectDamagedEventArgs e) 
        {
            if (Gold > 0)
            {
                Gold--;
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.BoatHitByCannon, e.DamagingObject.Position);
            }
        }

        private void ShipSankEventHandler(object sender, ObjectDestroyedEventArgs e)
        {
            Gold = 0;
        }
    }
}
