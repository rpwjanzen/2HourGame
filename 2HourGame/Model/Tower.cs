using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class Tower : PhysicsGameObject
    {
        List<GameObject> targets;
        float range;

        const float maxCannonRotationDegrees = 0.5f;
        const float toleranceInDegrees = 9.0f;

        // code so that we dont switch targets too often
        GameObject currentTarget = null;
        Timer minTargetFocusTimer;

        public Cannon Cannon { get; private set; }

        public Tower(PhysicsWorld world, Vector2 position, List<GameObject> targets, TextureManager tm, AnimationManager am)
            : base(world, position, 40, 100, 0)
        {
            this.targets = targets;

            this.minTargetFocusTimer = new Timer(10f);
            this.range = 300;
            Cannon = new Cannon(world, this, new Vector2(0, -35), 0.0f, tm, am);
            base.Body.IsStatic = true;
        }

        public override bool Touch(Actor other, FarseerGames.FarseerPhysics.Collisions.Contact contactPoint) {
            var cannonBall = other as CannonBall;
            if (cannonBall != null) {
                cannonBall.Die();
            }
            return base.Touch(other, contactPoint);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.IsAlive)
                doCannonBehaviour(gameTime);

            base.Update(gameTime);
        }

        private void doCannonBehaviour(GameTime gameTime) 
        {
            if (currentTarget == null || targetIsOutOfRange(currentTarget))
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

                if (Math.Abs(differenceToTarget) <= toleranceInDegrees)
                    Cannon.AttemptFireCannon(gameTime);
            }
        }

        private List<GameObject> getInRangeTargets(List<GameObject> allTargets) 
        {
            List<GameObject> inRangeTargets = new List<GameObject>();

            foreach (GameObject g in allTargets) 
            {
                if (getDistanceBetween(g, this) <= range)
                    inRangeTargets.Add(g);
            }

            return inRangeTargets;
        }

        private GameObject getClosestToSelf(List<GameObject> inRangeTargets) 
        {
            GameObject closest = null;
            float closestDistance = float.MaxValue;

            float distanceBetween;

            foreach (GameObject t in inRangeTargets) 
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

        private bool targetIsOutOfRange(GameObject target) 
        {
            return getDistanceBetween(this, target) > range;
        }

        private float getDistanceBetween(GameObject first, GameObject second) 
        {
            return Vector2.Distance(first.Position, second.Position);
        }
    }
}
