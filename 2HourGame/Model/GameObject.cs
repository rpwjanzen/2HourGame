using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class GameObject : Actor
    {
        public float Width { get; protected set; }
        public float Height { get; protected set; }

        public float HalfWidth { get { return Width / 2.0f; } }
        public float HalfHeight { get { return Height / 2.0f; } }

        protected int MaxHealth;
        protected int Health;
        public bool IsDamaged { get { return Health < MaxHealth; } }
        public float HealthPercentage { get { return (float)Health / (float)MaxHealth; } }

        public GameObject(World world, Vector2 position, float width, float height)
            : base(world)
        {
            this.Position = position;

            Width = width;
            Height = height;
        }

        public override void Spawn() {
            this.Health = MaxHealth;
            base.Spawn();
        }

        public override void Die() {
            this.Health = 0;
            base.Die();
        }
    }
}
