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

        List<IGameObject> targets;
        float range;

        const float maxCannonRotationDegrees = 1f;

        // code so that we dont switch targets too often
        IGameObject currentTarget = null;
        Timer minTargetFocusTimer;

        public Cannon<Tower> cannon { get; private set; }

        public Tower(Game game, Vector2 position, List<IGameObject> targets, CannonBallManager cannonBallManager) 
            : base(game, position, "Tower", 0.5f, 0.5f)//, new Vector2(30, 30))
        {
            this.Velocity = Vector2.Zero;
            this.targets = targets;
            minTargetFocusTimer = new Timer(10f);
            range = 200;
            cannon = new Cannon<Tower>(game, this, cannonBallManager, CannonType.FrontCannon);
            game.Components.Add(cannon);
            base.Body.IsStatic = true;
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
                cannon.facingRadians = AIHelpers.GetFacingTowardsPointInRadians(currentTarget.Position, this.Position, cannon.facingRadians, maxCannonRotationDegrees);
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
