using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class AIController : GameComponent
    {
        /// <summary>
        /// An AIController should have access to all information a user of the system has.
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="controlledShip">The ship that the AI is controlling</param>
        /// <param name="homeIsland">The ship's home island</param>
        /// <param name="islands">All islands in the game(excluding the ships home island)</param>
        public AIController(Game game, Ship controlledShip, Island homeIsland, List<Island> otherIslands,
                            Ship targetShip)
            : base(game)
        {
            ControlledShip = controlledShip;
            HomeIsland = homeIsland;
            OtherIslands = otherIslands;
            ProximityRadius = 50.0f;
            TargetShip = targetShip;
        }

        private Island HomeIsland { get; set; }
        private IEnumerable<Island> OtherIslands { get; set; }
        private Ship ControlledShip { get; set; }
        private float ProximityRadius { get; set; }
        private Ship TargetShip { get; set; }

        public override void Update(GameTime gameTime)
        {
            GoToPoint(TargetShip.Position, gameTime);
            base.Update(gameTime);
        }

        private void GoToPoint(Vector2 point, GameTime gt)
        {
            Vector2 delta = ControlledShip.Position - point;
            // my rotation should match this angle (-180.0f - 180.0f)
            float desiredRotation = MathHelper.ToDegrees((float) Math.Atan2(delta.Y, delta.X));

            // drawing is off by 90.0
            desiredRotation -= 90.0f;
            if (desiredRotation > 180.0f)
            {
                desiredRotation -= 360.0f;
            }
            else if (desiredRotation < -180.0f)
            {
                desiredRotation += 360.0f;
            }
            else
            {
            }

            float myRotation = MathHelper.ToDegrees(ControlledShip.Rotation);
            // body rotations are 0 - 360.0f
            myRotation -= 180.0f;

            float angleDifference = (myRotation - desiredRotation);
            if (angleDifference > 180.0f)
            {
                TurnLeft();
            }
            else if (angleDifference > 0)
            {
                TurnRight();
            }
            else if (angleDifference > -180.0f)
            {
                TurnLeft();
            }
            else if (angleDifference < -180.0f)
            {
                TurnRight();
            }
            else
            {
            }
        }

        private void TurnLeft()
        {
            ControlledShip.Rotate(-1.0f*15.0f);
        }

        private void TurnRight()
        {
            ControlledShip.Rotate(1.0f*15.0f);
        }
    }
}

//IEnumerable<Island> IslandsWithGold {
//    get { return this.OtherIslands.Where(island => island.Gold > 0).OrderBy(island => this.DistanceTo(island)); }
//}

//float DistanceTo(Island i) {
//    return this.DistanceTo(i.Position);
//}

//float DistanceTo(Vector2 point) {
//    return (point - this.ControlledShip.Position).Length();
//}

//Island ClosestIslandWithGold {
//    get { return this.IslandsWithGold.Any() ? this.IslandsWithGold.First() : null; }
//}

//bool IsNear(Island island) {
//    return this.DistanceTo(island) < this.ProximityRadius;
//}

//bool IsPracticallyOnTopOf(Vector2 point) {
//    return this.DistanceTo(point) < 5;
//}

//void GoToHomeIsland() {
//    if (!this.IsNear(this.HomeIsland)) {
//        this.GoToPoint(this.HomeIsland.Position, new GameTime());
//    }
//}


//    double AngleTo(Vector2 point) {
//        var delta = this.ControlledShip.Position - point;
//        var angle = Math.Atan2(delta.Y, delta.X);
//        // range from -PI/2 to PI/2
//        return  (angle - this.ControlledShip.Body.Rotation) % Math.PI;
//    }

//    bool IsFacingPoint(Vector2 point) {
//        return this.AngleTo(point) < Math.PI / 32.0;
//    }

//    bool IsLeft(Vector2 point) {
//        return this.AngleTo(point) > 0;
//    }

//    bool IsRight(Vector2 point) {
//        return this.AngleTo(point) < 0;
//    }

//    void TurnToward(Vector2 point) {
//        if (this.IsLeft(point)) {
//            this.TurnLeft();
//        } else if (this.IsRight(point)) {
//            this.TurnRight();
//        } else { }
//    }

//    void MoveForward() {
//        this.ControlledShip.Thrust(1 * 25);
//    }

//    void MoveBackward() {
//        this.ControlledShip.Thrust(-1 * 25);
//    }
//}