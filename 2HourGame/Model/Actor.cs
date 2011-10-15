using System;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class Actor
    {
        public Actor(World world)
        {
            World = world;
        }

        public bool IsAlive { get; protected set; }
        public virtual Vector2 Position { get; protected set; }
        public virtual Vector2 Velocity { get; protected set; }
        public virtual float Rotation { get; protected set; }
        public virtual Color Color { get; protected set; }
        protected World World { get; private set; }

        public event EventHandler Spawned;
        public event EventHandler Died;

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Spawn()
        {
            IsAlive = true;
            World.NewActors.Add(this);
            RaiseSpawnedEvent();
        }

        public virtual void Die()
        {
            IsAlive = false;
            World.GarbageActors.Add(this);
            RaiseDiedEvent();
        }

        private void RaiseDiedEvent()
        {
            if (Died != null)
            {
                Died(this, EventArgs.Empty);
            }
        }

        private void RaiseSpawnedEvent()
        {
            if (Spawned != null)
            {
                Spawned(this, EventArgs.Empty);
            }
        }
    }
}