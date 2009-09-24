using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    public delegate void CannonFired(GameTime gameTime);

    class Cannon<T> where T : PhysicsGameObject, ICannonMountable
    {
        Game game;

        Timer firingTimer;

        public CannonType cannonType { get; private set; }

        public event CannonFired CannonFired;

        CannonBallManager cannonBallManager { get; set; }

        private T parentObject;

        public float Scale { 
            get 
            {
                return parentObject.Scale;   
            }
        }

        public Cannon(Game game, T parentObject, CannonBallManager cannonBallManager, CannonType cannonType) 
        {
            this.cannonType = cannonType;
            this.game = game;
            this.cannonBallManager = cannonBallManager;
            this.parentObject = parentObject;
            firingTimer = new Timer(3f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="now"></param>
        /// <returns>Vector2.Zero if the cannon was not fired, the firingvector otherwise.</returns>
        public Vector2 attemptFireCannon(GameTime now) 
        {
            if(firingTimer.TimerHasElapsed(now) && parentObject.drawCannon())
            {
                firingTimer.resetTimer(now.TotalGameTime);
                CannonFired(now);
                return fireCannon();
            }
            return Vector2.Zero;
        }

        public bool drawCannon()
        {
            return parentObject.drawCannon();
        }

        public float getCannonRotation()
        {
            if (cannonType == CannonType.LeftCannon)
                return 2f * (float)Math.PI + parentObject.Rotation;
            else
                return (float)Math.PI + parentObject.Rotation;
        }

        public Vector2 getCannonPosition()
        {
            if (cannonType == CannonType.LeftCannon)
                return new Vector2(parentObject.Body.GetBodyMatrix().Left.X, parentObject.Body.GetBodyMatrix().Left.Y) * (parentObject.XRadius - 8) + parentObject.Position;
            else
                return new Vector2(parentObject.Body.GetBodyMatrix().Right.X, parentObject.Body.GetBodyMatrix().Right.Y) * (parentObject.XRadius - 8) + parentObject.Position;
        }

        private Vector2 fireCannon() 
        {
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
    }
}
