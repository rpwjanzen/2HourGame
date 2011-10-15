using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class Projectile : PhysicsGameObject
    {
        public Projectile(PhysicsWorld world, Vector2 position, float width, float height, Actor owner)
            : base(world, position, width, height)
        {
            Owner = owner;
        }

        public Actor Owner { get; private set; }

        public virtual void Fire(Vector2 impulse)
        {
            Body.ApplyImpulse(impulse);
        }
    }
}