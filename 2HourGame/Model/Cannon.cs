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

    class Cannon<T> where T : PhysicsGameObject, ICannonMountable
    {
        Game game;
        Timer firingTimer;

        public CannonType cannonType { get; private set; }
        public event EventHandler<CannonFiredEventArgs> CannonFired;

        CannonBallManager cannonBallManager { get; set; }
        private T parentObject;
        public float facing { get; set; }
        public bool ShouldCannonDraw { get { return parentObject.IsCannonVisible; } }
        public float Rotation
        {
            get
            {
                if (cannonType == CannonType.LeftCannon)
                    return 2f * (float)Math.PI + parentObject.Rotation + facing;
                else
                    return (float)Math.PI + parentObject.Rotation + facing;
            }
        }

        public Vector2 Position
        {
            get
            {
                if (cannonType == CannonType.LeftCannon)
                    return new Vector2(parentObject.Body.GetBodyMatrix().Left.X, parentObject.Body.GetBodyMatrix().Left.Y) * (parentObject.XRadius - 8) + parentObject.Position;
                else
                    return new Vector2(parentObject.Body.GetBodyMatrix().Right.X, parentObject.Body.GetBodyMatrix().Right.Y) * (parentObject.XRadius - 8) + parentObject.Position;
            }
        }

        public Cannon(Game game, T parentObject, CannonBallManager cannonBallManager, CannonType cannonType) 
        {
            this.cannonType = cannonType;
            this.game = game;
            this.cannonBallManager = cannonBallManager;
            this.parentObject = parentObject;
            firingTimer = new Timer(3f);
            facing = 0f;
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

            //get the right vector
            Vector2 firingVector = cannonType == CannonType.LeftCannon
                ? new Vector2(parentObject.Body.GetBodyMatrix().Left.X, parentObject.Body.GetBodyMatrix().Left.Y)
                : new Vector2(parentObject.Body.GetBodyMatrix().Right.X, parentObject.Body.GetBodyMatrix().Right.Y);
            var thrust = firingVector * 65.0f;

            // take into account the ship's momentum
            thrust += parentObject.Velocity;

            var cannonBallPostion = (firingVector * (parentObject.XRadius + 5)) + parentObject.Position;
            var smokePosition = firingVector * (parentObject.XRadius - 2) + parentObject.Position;

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
