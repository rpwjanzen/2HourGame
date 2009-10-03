using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class CannonFiredEventArgs : EventArgs
    {
        public GameTime FiredTime { get; private set; }
        public CannonFiredEventArgs(GameTime gameTime)
        {
            this.FiredTime = gameTime;
        }
    }

    class Cannon<T> : GameComponent where T : IPhysicsGameObject, ICannonMountable
    {
        const float FiringSpeed = 65.0f;

        Game game;
        Timer firingTimer;

        public CannonType cannonType { get; private set; }
        public event EventHandler<CannonFiredEventArgs> CannonFired;

        CannonBallManager cannonBallManager { get; set; }
        private T parentObject;
        public float facingRadians { get; set; }
        public bool ShouldCannonDraw { get { return parentObject.IsCannonVisible; } }
        public float Rotation
        {
			get
			{
	            if (cannonType == CannonType.LeftCannon)
	                return 2f * (float)Math.PI + parentObject.Rotation + facingRadians;
	            else if (cannonType == CannonType.RightCannon)
	                return (float)Math.PI + parentObject.Rotation + facingRadians;
	            else
	                return facingRadians;
			}
        }

        public Vector2 Position
        {
			get
			{
                if (cannonType == CannonType.LeftCannon)
                {
                    return Left(parentObject) * ((parentObject.Width / 2.0f) - 8) + parentObject.Position;
                }
                else if (cannonType == CannonType.RightCannon)
                {
                    return Right(parentObject) * ((parentObject.Width / 2.0f) - 8) + parentObject.Position;
                }
                else
                {
                    return new Vector2(0, -53) + parentObject.Position;
                }
			}
        }

        Vector2 Left(IGameObject gameObject)
        {
            var rotationMatrix = Matrix.CreateRotationZ(parentObject.Rotation);
            var left = new Vector2(rotationMatrix.Left.X, rotationMatrix.Left.Y);
            return left;
        }

        Vector2 Right(IGameObject gameObject)
        {
            var rotationMatrix = Matrix.CreateRotationZ(parentObject.Rotation);
            var right = new Vector2(rotationMatrix.Right.X, rotationMatrix.Right.Y);
            return right;
        }


        public Cannon(Game game, T parentObject, CannonBallManager cannonBallManager, CannonType cannonType)
            : base(game)
        {
            this.cannonType = cannonType;
            this.game = game;
            this.cannonBallManager = cannonBallManager;
            this.parentObject = parentObject;
            firingTimer = new Timer(3f);
            facingRadians = 0f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="now"></param>
        /// <returns>Vector2.Zero if the cannon was not fired, the firingvector otherwise.</returns>
        public Vector2 attemptFireCannon(GameTime now) 
        {
            if(firingTimer.TimerHasElapsed(now) && parentObject.IsCannonVisible)
            {
                firingTimer.resetTimer(now.TotalGameTime);
                RaiseCannonFiredEvent(now);
                return fireCannon();
            }
            return Vector2.Zero;
        }

        private Vector2 fireCannon() 
        {
            // TODO, ADD FACING TO firingVector

            //get the direction the cannon is facing vector
            Vector2 firingVector = cannonType == CannonType.LeftCannon
                ? Left(parentObject)
                : Right(parentObject);
            var thrust = firingVector * FiringSpeed;

            // take into account the ship's momentum
            thrust += parentObject.Velocity;

            var cannonBallPostion = (firingVector * ((parentObject.Width / 2.0f) + 5)) + parentObject.Position;
            var smokePosition = firingVector * ((parentObject.Width / 2.0f) - 2) + parentObject.Position;

            ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).CannonSmokeEffect(smokePosition);
            var cannonBall = this.cannonBallManager.CreateCannonBall(cannonBallPostion, thrust);

            return thrust;
        }

        void RaiseCannonFiredEvent(GameTime gameTime)
        {
            if (CannonFired != null)
            {
                CannonFired(this, new CannonFiredEventArgs(gameTime));
            }
        }
    }
}
