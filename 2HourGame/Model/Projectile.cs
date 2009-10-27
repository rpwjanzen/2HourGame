using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model {
    class Projectile : PhysicsGameObject {
        public Actor Owner { get; private set; }

        public Projectile(PhysicsWorld world, Vector2 position, float width, float height, Actor owner) : base(world, position, width, height) {
            this.Owner = owner;
        }

        public virtual void Fire(Vector2 impulse) {
            this.Body.ApplyImpulse(impulse);
        }
    }
}
