using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using _2HourGame.View;
using _2HourGame.Model.GameServices;

namespace _2HourGame.Model
{
    class Tower : PhysicsGameObject
    {
        List<IGameObject> targets;
        float range;

        // XXX: sometimes ship cannot lock on properly (moves lefe<->right continuously)
        const float maxCannonRotationDegrees = 0.5f;
        const float toleranceInDegrees = 9.0f;

        // code so that we dont switch targets too often
        IGameObject currentTarget = null;
        Timer minTargetFocusTimer;

        public Cannon Cannon { get; private set; }

        public Tower(Game game, Vector2 position, List<IGameObject> targets, CannonBallManager cannonBallManager)
            : base(game, position, 40, 100, ((CollisionGroupManager)game.Services.GetService(typeof(CollisionGroupManager))).getNextFreeCollisionGroup())
        {
            this.targets = targets;
            minTargetFocusTimer = new Timer(10f);
            range = 300;
            Cannon = new Cannon(game, this, cannonBallManager, new Vector2(0, -35), 0.0f);
            base.Body.IsStatic = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (currentTarget == null || targetIsOutOfRange(currentTarget))// || minTargetFocusTimer.TimerHasElapsed(gameTime))
            {
                currentTarget = getClosestToSelf(getInRangeTargets(targets));
                minTargetFocusTimer.resetTimer(gameTime.TotalGameTime);
            }

            // XXX: Dirty hack!
            var ship = currentTarget as Ship;

            if (currentTarget != null && ship != null && ship.IsAlive) 
            {
                // steer towards the target and potentially fire!
                // get the change to make to point directly at the target.
                var differenceToTarget = AIHelpers.GetRotationToPointInDegrees(currentTarget.Position, Cannon.Position, Cannon.Rotation);

                float angleChangeInDegrees = differenceToTarget;

                // we need to cap the angle to change by the max rotation
                if (Math.Abs(differenceToTarget) > maxCannonRotationDegrees) 
                {
                    angleChangeInDegrees = maxCannonRotationDegrees;

                    if (differenceToTarget < 0)
                        angleChangeInDegrees *= -1;
                }

                Cannon.LocalRotation += MathHelper.ToRadians(angleChangeInDegrees);

                if(Math.Abs(differenceToTarget) <= toleranceInDegrees)
                    Cannon.attemptFireCannon(gameTime);

                //if(direction == AIHelpers.RotationDirection.Left) {
                //    RotateCannonLeft();
                //}else if(direction == AIHelpers.RotationDirection.Right) {
                //    RotateCannonRight();
                //} else if(direction == AIHelpers.RotationDirection.None) {
                //    Cannon.attemptFireCannon(gameTime);
                //}
            }

            base.Update(gameTime);
        }

        private List<IGameObject> getInRangeTargets(List<IGameObject> allTargets) 
        {
            List<IGameObject> inRangeTargets = new List<IGameObject>();

            foreach (IGameObject g in allTargets) 
            {
                if (getDistanceBetween(g, this) <= range)
                    inRangeTargets.Add(g);
            }

            return inRangeTargets;
        }

        private IGameObject getClosestToSelf(List<IGameObject> inRangeTargets) 
        {
            IGameObject closest = null;
            float closestDistance = float.MaxValue;

            float distanceBetween;

            foreach (IGameObject t in inRangeTargets) 
            {
                distanceBetween = getDistanceBetween(t, this);

                if(distanceBetween < closestDistance)
                {
                    closest = t;
                    closestDistance = distanceBetween;
                }
            }

            return closest;
        }

        private bool targetIsOutOfRange(IGameObject target) 
        {
            return getDistanceBetween(this, target) > range;
        }

        private float getDistanceBetween(IGameObject first, IGameObject second) 
        {
            return Vector2.Distance(first.Position, second.Position);
        }
    }
}
