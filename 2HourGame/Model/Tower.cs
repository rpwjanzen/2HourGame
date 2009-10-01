using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using _2HourGame.View;

namespace _2HourGame.Model
{
    class Tower : PhysicsGameObject, ICannonMountable
    {
        // to satisfy ICannonMountable
        public Vector2 Velocity { get; private set; }

        List<GameObject> targets;
        float range;

        const float maxCannonRotation = 0.1f;

        // code so that we dont switch targets too often
        GameObject currentTarget = null;
        Timer minTargetFocusTimer;

        Cannon<Tower> cannon;

        public Tower(Game game, Vector2 position, List<GameObject> targets, CannonBallManager cannonBallManager) 
            : base(game, position, "Tower", 80, 200)
        {
            this.Velocity = Vector2.Zero;
            this.targets = targets;
            minTargetFocusTimer = new Timer(10f);
            range = 200;
            cannon = new Cannon<Tower>(game, this, cannonBallManager, CannonType.FrontCannon);
            //base.Body.IsStatic = true;
        }

        public bool IsCannonVisible { get { return true; } }

        public override void Update(GameTime gameTime)
        {
            if (currentTarget == null || targetIsOutOfRange(currentTarget) || minTargetFocusTimer.TimerHasElapsed(gameTime))
            {
                currentTarget = getClosestToSelf(getInRangeTargets(targets));
                minTargetFocusTimer.resetTimer(gameTime.TotalGameTime);
            }

            if (currentTarget != null) 
            {
                // steer towards the target and potentially fire!
                cannon.facing = AIHelpers.GetFacingTowardsPoint(currentTarget.Position, this.Position, cannon.facing, maxCannonRotation);
            }

            base.Update(gameTime);
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
            return (first.Position - second.Position).Length();
        }
    }
}
