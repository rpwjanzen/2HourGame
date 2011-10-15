using System;
using System.Collections.Generic;
using _2HourGame.View.GameServices;
using FarseerGames.FarseerPhysics.Collisions;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class DamagedEventArgs : EventArgs
    {
        public Vector2 DamagePosition { get; set; }
    }

    internal class Ship : PhysicsGameObject
    {
        private readonly Vector2 leftCannonOffset = new Vector2(8, 4);
        private readonly Vector2 rightCannonOffset = new Vector2(-8, 4);
        private int repairAmount = 1;

        public Ship(PhysicsWorld world, Vector2 position, float rotation, TextureManager tm, AnimationManager am)
            : base(world, position, 34, 60, rotation)
        {
            // the direction to fire relative to the cannon
            FiringVelocity = Vector2.UnitY;

            Body.RotationalDragCoefficient = 2500.0f;

            base.Rotation = rotation;

            LeftCannons = new List<Cannon>();
            RightCannons = new List<Cannon>();

            Matrix rotationMatrix = Matrix.Identity;

            Vector2 leftCannonPosition = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y)*((Width/2.0f)) +
                                         leftCannonOffset;
            float leftCannonRotation = MathHelper.ToRadians(-90);
            LeftCannons.Add(new Cannon(PhysicsWorld, this, leftCannonPosition, leftCannonRotation, tm, am));

            Vector2 rightCannonPosition = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y)*((Width/2.0f)) +
                                          rightCannonOffset;
            float rightCannonRotation = MathHelper.ToRadians(90);
            RightCannons.Add(new Cannon(PhysicsWorld, this, rightCannonPosition, rightCannonRotation, tm, am));

            MaxHealth = 50;
            Health = 50;
        }

        public int GoldCapacity { get; protected set; }
        public int Gold { get; protected set; }

        public bool IsFull
        {
            get { return Gold >= GoldCapacity; }
        }

        public List<Cannon> LeftCannons { get; protected set; }
        public List<Cannon> RightCannons { get; protected set; }

        private Vector2 FiringVelocity { get; set; }

        // Offset to move the cannons so they look good on the ship.

        public event EventHandler GoldLoaded;
        public event EventHandler<DamagedEventArgs> Damaged;

        private void Thrust(float amount)
        {
            //get the forward vector
            var forward = new Vector2(-base.Body.GetBodyMatrix().Up.X, -base.Body.GetBodyMatrix().Up.Y);
            Vector2 thrust = forward*amount;
            base.Body.ApplyForceAtLocalPoint(thrust, Vector2.Zero);
        }

        public void Accelerate(float amount)
        {
            Thrust(amount);
        }

        public void Rotate(float amount)
        {
            base.Body.ApplyTorque(amount);
        }

        public void LoadGoldFromIsland(Island island)
        {
            if (island.HasGold && !IsFull)
            {
                island.RemoveGold();
                AddGold();
                if (GoldLoaded != null)
                {
                    GoldLoaded(this, EventArgs.Empty);
                }
            }
        }

        public void UnloadGoldToIsland(Island island)
        {
            island.AddGold(Gold);
            Gold = 0;
        }

        public void FireLeftCannons(GameTime now)
        {
            foreach (Cannon c in LeftCannons)
                FireCannon(now, c);
        }

        public void FireRightCannons(GameTime now)
        {
            foreach (Cannon c in RightCannons)
                FireCannon(now, c);
        }

        public override void Die()
        {
            Gold = 0;

            base.Die();
        }

        public override bool Touch(Actor other, Contact contact)
        {
            var cannonBall = other as CannonBall;
            if (cannonBall != null && cannonBall.Owner != this)
            {
                var damage = (int) (cannonBall.Speed/12f);
                TakeDamage(contact, damage);
                cannonBall.Die();
                return true;
            }

            return base.Touch(other, contact);
        }

        public void Repair()
        {
            Health += repairAmount;
            Health = Math.Min(Health, MaxHealth);
        }

        private void TakeDamage(Contact damagePoint, int amount)
        {
            if (Gold > 0)
            {
                Gold--;
                if (Damaged != null)
                {
                    var ea = new DamagedEventArgs();
                    ea.DamagePosition = damagePoint.Position;
                    Damaged(this, ea);
                }
            }

            Health -= amount;
            if (Health <= 0)
            {
                Die();
            }
        }

        private void FireCannon(GameTime now, Cannon cannonToFire)
        {
            if (IsAlive)
            {
                Vector2? kickback = cannonToFire.AttemptFireCannon(now);
                if (kickback.HasValue)
                {
                    base.Body.ApplyImpulse(kickback.Value/8);
                }
            }
        }

        private void AddGold()
        {
            Gold++;
        }
    }
}