using System;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class FiredEventArgs : EventArgs
    {
        public Vector2 SmokePosition { get; set; }
    }

    /// <summary>
    /// A cannon that is mounted to another object. It's position is fixed relative to the attached to object.
    /// </summary>
    internal class Cannon : PhysicsGameObject
    {
        private const float FiringSpeed = 65.0f;
        private const float CannonBallOffset = 15.0f;
        private const float SmokeOffset = 13.0f;
        private readonly AnimationManager animationManager;

        private readonly Timer firingTimer;
        private readonly GameObject owner;
        private readonly Vector2 positionalOffset;
        private readonly float rotationalOffset;
        private readonly TextureManager textureManager;

        /// <summary>
        /// Local rotation is restricted to being between 180 and -180.
        /// </summary>
        private float localRotation;

        public Cannon(PhysicsWorld world, PhysicsGameObject owner, Vector2 positionalOffset, float rotationalOffset,
                      TextureManager tm, AnimationManager am)
            : base(world, owner.Position, 12, 27, 0.0f)
        {
            this.owner = owner;
            this.positionalOffset = positionalOffset;
            this.rotationalOffset = rotationalOffset;

            textureManager = tm;
            animationManager = am;

            firingTimer = new Timer(3f);
        }

        public TimeSpan lastTimeFired
        {
            get { return firingTimer.timerStartTime; }
        }

        public override Vector2 Position
        {
            get { return RotatedOffset + owner.Position; }
            //protected set { throw new InvalidOperationException(); }
        }

        private Vector2 RotatedOffset
        {
            get
            {
                Matrix rotationMatrix = Matrix.CreateRotationZ(owner.Rotation);
                return Vector2.Transform(positionalOffset, rotationMatrix);
            }
        }

        public float LocalRotation
        {
            get { return localRotation; }
            set
            {
                localRotation = value;

                while (localRotation > Math.PI)
                    localRotation -= 2f*(float) Math.PI;
                while (localRotation < -Math.PI)
                    localRotation += 2f*(float) Math.PI;
            }
        }

        public override float Rotation
        {
            get { return LocalRotation + rotationalOffset + owner.Rotation; }
            protected set { LocalRotation = value - rotationalOffset - owner.Rotation; }
        }


        public event EventHandler<FiredEventArgs> Fired;

        /// <summary>
        /// Fire the cannon if the timer has elapsed and its ready to fire again.
        /// </summary>
        /// <param name="now"></param>
        /// <returns>The amount of thrust the cannon generates from firing.</returns>
        public Vector2? AttemptFireCannon(GameTime now)
        {
            if (firingTimer.TimerHasElapsed(now))
            {
                firingTimer.resetTimer(now.TotalGameTime);
                return FireCannon();
            }

            return null;
        }

        private Vector2 FireCannon()
        {
            //get the direction the cannon is facing
            Vector2 firingVector = Vector2.Transform(-Vector2.UnitY, Matrix.CreateRotationZ(Rotation));
            Vector2 thrust = firingVector*FiringSpeed;

            // take into account the owner's velocity
            thrust += owner.Velocity;

            Vector2 cannonBallPostion = firingVector*CannonBallOffset + Position;
            Vector2 smokePosition = firingVector*SmokeOffset + Position;

            if (Fired != null)
            {
                var ea = new FiredEventArgs();
                ea.SmokePosition = smokePosition;
                Fired(this, ea);
            }

            var cannonBall = new CannonBall(PhysicsWorld, cannonBallPostion, owner);
            var cannonBallView = new CannonBallView(cannonBall, PhysicsWorld, textureManager, animationManager);
            cannonBall.Spawn();
            cannonBall.Fire(thrust);

            return -thrust;
        }
    }
}