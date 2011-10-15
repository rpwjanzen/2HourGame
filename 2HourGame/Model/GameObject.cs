using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class GameObject : Actor
    {
        protected int Health;
        protected int MaxHealth;

        public GameObject(World world, Vector2 position, float width, float height)
            : base(world)
        {
            Position = position;

            Width = width;
            Height = height;
            Health = 0;
            MaxHealth = 0;
        }

        public float Width { get; protected set; }
        public float Height { get; protected set; }

        public float HalfWidth
        {
            get { return Width/2.0f; }
        }

        public float HalfHeight
        {
            get { return Height/2.0f; }
        }

        public bool IsDamaged
        {
            get { return Health < MaxHealth; }
        }

        public float HealthPercentage
        {
            get { return Health/(float) MaxHealth; }
        }

        public override void Spawn()
        {
            Health = MaxHealth;
            base.Spawn();
        }

        public override void Die()
        {
            Health = 0;
            base.Die();
        }
    }
}