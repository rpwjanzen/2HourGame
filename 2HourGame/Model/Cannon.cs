﻿using System;
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

    /// <summary>
    /// A cannon that is mounted to another object. It's position is fixed relative to the attached to object.
    /// </summary>
    /// <typeparam name="T">The object the cannon is attached to</typeparam>
    class Cannon<T> : GameComponent where T : IGameObject, ICannonMountable
    {
        Game game;

        const float FiringSpeed = 65.0f;
        
        /// <summary>
        /// Length of the cannon
        /// </summary>
        const float CannonBallOffset = 15.0f;

        const float SmokeOffset = 13.0f;
        Timer firingTimer;

        private T attachedToObject;
        Vector2 positionalOffset;
        float rotationalOffset;

        CannonBallManager cannonBallManager;

        public event EventHandler<CannonFiredEventArgs> CannonFired;

        public Vector2 Position
        {
            get
            {
                return RotatedOffset + attachedToObject.Position;
            }
        }

        Vector2 RotatedOffset
        {
            get
            {
                var rotationMatrix = Matrix.CreateRotationZ(attachedToObject.Rotation);
                return Vector2.Transform(positionalOffset, rotationMatrix);
            }
        }

        public float LocalRotation { get; set; }
        public float Rotation
        {
            get
            {
                return LocalRotation + rotationalOffset + attachedToObject.Rotation;
            }
            set
            {
                LocalRotation = value - rotationalOffset - attachedToObject.Rotation;
            }
        }

        public Cannon(Game game, T parentObject, CannonBallManager cannonBallManager, Vector2 positionalOffset, float rotationalOffset)
            : base(game)
        {
            this.game = game;
            this.attachedToObject = parentObject;
            this.cannonBallManager = cannonBallManager;
            this.positionalOffset = positionalOffset;
            this.rotationalOffset = rotationalOffset;

            firingTimer = new Timer(3f);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="now"></param>
        /// <returns>Vector2.Zero if the cannon was not fired, the firingvector otherwise.</returns>
        public Vector2 attemptFireCannon(GameTime now) 
        {
            if(firingTimer.TimerHasElapsed(now) && attachedToObject.IsCannonVisible)
            {
                firingTimer.resetTimer(now.TotalGameTime);
                RaiseCannonFiredEvent(now);
                return fireCannon();
            }
            return Vector2.Zero;
        }

        private Vector2 fireCannon() 
        {
            //get the direction the cannon is facing vector
            Vector2 firingVector = Vector2.Transform(-Vector2.UnitY, Matrix.CreateRotationZ(this.Rotation));
            var thrust = firingVector * FiringSpeed;

            // take into account the ship's momentum
            thrust += attachedToObject.Velocity;

            var cannonBallPostion = firingVector * CannonBallOffset + this.Position;
            var smokePosition = firingVector * SmokeOffset + this.Position;

            ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.CannonSmoke, smokePosition);
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
