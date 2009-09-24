using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class SimpleCannon : GameComponent
    {
        public Vector2 Position { get; set; }
        Timer timer;
        bool CanFire
        {
            get { return timer.Expired; }
        }

        float firingForce;
        public float Rotation { get; set; }
        public List<SimpleCannonBall> CannonBalls { get; private set; }

        Vector2 muzzleExitPoint;

        public SimpleCannon(Game game) : base(game) {
            timer = new Timer(game);
            firingForce = 10.0f;
            Rotation = 0.0f;

            CannonBalls = new List<SimpleCannonBall>();
            muzzleExitPoint = Vector2.UnitY * 5.0f;
        }

        public override void Update(GameTime gameTime)
        {
            timer.Update(gameTime);

            // remove cannonballs that are going too slow
            for (int i = CannonBalls.Count - 1; i >= 0; i--)
            {
                SimpleCannonBall cb = CannonBalls[i];
                cb.Update(gameTime);

                if (cb.Expired)
                {
                    CannonBalls.RemoveAt(i);
                    cb.RemoveFromPhysicsEngine();
                }
            }

            base.Update(gameTime);
        }

        public void TryFire()
        {
            if (CanFire)
            {
                Fire();
                timer.Reset();
            }
        }

        void Fire()
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(Rotation);

            Vector2 exitPoint = Vector2.Transform(muzzleExitPoint, rotationMatrix);
            SimpleCannonBall cannonBall = new SimpleCannonBall(Game, Position + exitPoint);            

            Vector2 firingVector = Vector2.UnitY * firingForce;
            cannonBall.Fire(Vector2.Transform(firingVector, rotationMatrix));

            CannonBalls.Add(cannonBall);
        }

    }
}
