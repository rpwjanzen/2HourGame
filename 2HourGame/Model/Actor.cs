using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.Model {
    class Actor {
        
        public bool IsAlive { get; protected set; }
        public virtual Vector2 Position { get; protected set; }
        public virtual Vector2 Velocity { get; protected set; }
        public virtual float Rotation { get; protected set; }
        public virtual Color Color { get; protected set; }

        public event EventHandler Spawned;
        public event EventHandler Died;        

        protected World World { get; private set; }

        public Actor(World world) {
            this.World = world;
        }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Spawn() {
            this.IsAlive = true;
            this.World.Actors.Add(this);
            RaiseSpawnedEvent();
        }

        public virtual void Die() {
            this.IsAlive = false;
            this.World.GarbageActors.Add(this);
            RaiseDiedEvent();
        }

        private void RaiseDiedEvent() {
            if (Died != null) {
                Died(this, EventArgs.Empty);
            }
        }

        private void RaiseSpawnedEvent() {
            if (Spawned != null) {
                Spawned(this, EventArgs.Empty);
            }
        }
    }
}
