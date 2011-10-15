using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class CannonBall : Projectile
    {
        public CannonBall(PhysicsWorld world, Vector2 position, Actor owner)
            : base(world, position, 5, 5, owner)
        {
            base.Body.LinearDragCoefficient = 0.20f;
            base.Body.Mass = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsAlive && Speed < 1f)
            {
                Die();
            }
            base.Update(gameTime);
        }
    }
}