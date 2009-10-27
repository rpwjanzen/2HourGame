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
    class DamagedEventArgs : EventArgs {
        public Vector2 DamagePosition { get; set; }
    }

    class Ship : PhysicsGameObject
    {
        public int GoldCapacity { get; protected set; }
        public int Gold { get; protected set; }

        public bool IsFull {
            get { return this.Gold >= this.GoldCapacity; }
        }

        public List<Cannon> LeftCannons { get; protected set; }
        public List<Cannon> RightCannons { get; protected set; }
        
        Vector2 FiringVelocity { get; set; }

        // Offset to move the cannons so they look good on the ship.
        readonly Vector2 leftCannonOffset = new Vector2(8, 4);
        readonly Vector2 rightCannonOffset = new Vector2(-8, 4);

        public event EventHandler GoldLoaded;
        public event EventHandler<DamagedEventArgs> Damaged;

        public Ship(PhysicsWorld world, Vector2 position, float rotation)
            : base(world, position, 34, 60, rotation)
        {
            // the direction to fire relative to the cannon
            this.FiringVelocity = Vector2.UnitY;

            this.Body.RotationalDragCoefficient = 2500.0f;

            base.Rotation = rotation;

            LeftCannons = new List<Cannon>();
            RightCannons = new List<Cannon>();



            var rotationMatrix = Matrix.Identity;

            var leftCannonPosition = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y) * ((this.Width / 2.0f)) + leftCannonOffset;
            var leftCannonRotation = MathHelper.ToRadians(-90);
            LeftCannons.Add(new Cannon(PhysicsWorld, this, leftCannonPosition, leftCannonRotation));

            var rightCannonPosition = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y) * ((this.Width / 2.0f)) + rightCannonOffset;
            var rightCannonRotation = MathHelper.ToRadians(90);
            RightCannons.Add(new Cannon(PhysicsWorld, this, rightCannonPosition, rightCannonRotation));
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
                if (GoldLoaded != null) {
                    GoldLoaded(this, EventArgs.Empty);
                }                
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

        public override void Die() {
            this.Gold = 0;

            base.Die();
        }

        public override bool Touch(Actor other, Contact contact) {
            var cannonBall = other as CannonBall;
            if (cannonBall != null && cannonBall.Owner != this) {
                TakeDamage(contact);
                World.GarbageActors.Add(cannonBall);
                return true;
            }

            return base.Touch(other, contact);
        }

        private void TakeDamage(Contact damagePoint)
        {
            if (Gold > 0)
            {
                Gold--;
                if (Damaged != null) {
                    var ea = new DamagedEventArgs();
                    ea.DamagePosition = damagePoint.Position;
                    Damaged(this, ea);
                }
            }

            Health -= 1;
            if (Health == 0) {
                Die();
            }
        }

        private void FireCannon(GameTime now, Cannon cannonToFire) {
            if (IsAlive) {
                Vector2? kickback = cannonToFire.AttemptFireCannon(now);
                if (kickback.HasValue) {
                    base.Body.ApplyImpulse(kickback.Value / 8);
                }
            }
        }

        private void AddGold() {
            this.Gold++;
        }
    }
}
