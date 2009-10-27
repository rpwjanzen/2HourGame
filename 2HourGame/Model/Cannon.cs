using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class FiredEventArgs : EventArgs {
        public Vector2 SmokePosition { get; set; }
    }

    /// <summary>
    /// A cannon that is mounted to another object. It's position is fixed relative to the attached to object.
    /// </summary>
    class Cannon : PhysicsGameObject
    {
        const float FiringSpeed = 65.0f;
        const float CannonBallOffset = 15.0f;
        const float SmokeOffset = 13.0f;

        Timer firingTimer;
        public TimeSpan lastTimeFired
        {
            get { return firingTimer.timerStartTime; }
        }

        public override Vector2 Position
        {
            get { return RotatedOffset + owner.Position; }
            protected set { throw new InvalidOperationException(); }
        }

        Vector2 RotatedOffset
        {
            get
            {
                var rotationMatrix = Matrix.CreateRotationZ(owner.Rotation);
                return Vector2.Transform(positionalOffset, rotationMatrix);
            }
        }

        /// <summary>
        /// Local rotation is restricted to being between 180 and -180.
        /// </summary>
        private float localRotation;
        public float LocalRotation {
            get { return localRotation; }
            set 
            {
                localRotation = value;

                while (localRotation > Math.PI)
                    localRotation -= 2f * (float)Math.PI;
                while (localRotation < -Math.PI)
                    localRotation += 2f * (float)Math.PI;
            } 
        }

        public override float Rotation
        {
            get { return LocalRotation + rotationalOffset + owner.Rotation; }
            protected set { LocalRotation = value - rotationalOffset - owner.Rotation; }
        }

        GameObject owner;
        Vector2 positionalOffset;
        float rotationalOffset;


        public event EventHandler<FiredEventArgs> Fired;

        public Cannon(PhysicsWorld world, PhysicsGameObject owner, Vector2 positionalOffset, float rotationalOffset)
            : base(world, Vector2.Zero, 0, 0, 0.0f)
        {
            this.owner = owner;
            this.positionalOffset = positionalOffset;
            this.rotationalOffset = rotationalOffset;

            firingTimer = new Timer(3f);
        }

        /// <summary>
        /// Fire the cannon if the timer has elapsed and its ready to fire again.
        /// </summary>
        /// <param name="now"></param>
        /// <returns>The amount of thrust the cannon generates from firing.</returns>
        public Vector2? AttemptFireCannon(GameTime now) 
        {
            if(firingTimer.TimerHasElapsed(now))
            {
                firingTimer.resetTimer(now.TotalGameTime);
                return FireCannon();
            }

            return null;
        }

        private Vector2 FireCannon() 
        {
            //get the direction the cannon is facing
            Vector2 firingVector = Vector2.Transform(-Vector2.UnitY, Matrix.CreateRotationZ(this.Rotation));
            var thrust = firingVector * FiringSpeed;

            // take into account the owner's velocity
            thrust += owner.Velocity;

            var cannonBallPostion = firingVector * CannonBallOffset + this.Position;
            var smokePosition = firingVector * SmokeOffset + this.Position;

            if (Fired != null) {
                var ea = new FiredEventArgs();
                ea.SmokePosition = smokePosition;
                Fired(this, ea);
            }

            var cannonBall = new CannonBall(PhysicsWorld, cannonBallPostion, owner);
            cannonBall.Fire(thrust);


            return -thrust;
        }
    }
}
