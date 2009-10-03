using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System;

namespace _2HourGame.Model {
    class AIController : GameComponent {
        Island HomeIsland { get; set; }
        IEnumerable<Island> OtherIslands { get; set; }
        IShip ControlledShip { get; set; }
        float ProximityRadius { get; set; }
        IShip TargetShip { get; set; }

        /// <summary>
        /// An AIController should have access to all information a user of the system has.
        /// </summary>
        /// <param name="game">The game</param>
        /// <param name="controlledShip">The ship that the AI is controlling</param>
        /// <param name="homeIsland">The ship's home island</param>
        /// <param name="islands">All islands in the game(excluding the ships home island)</param>
        public AIController(Game game, IShip controlledShip, Island homeIsland, List<Island> otherIslands, IShip targetShip)
            : base(game) {
            this.ControlledShip = controlledShip;
            this.HomeIsland = homeIsland;
            this.OtherIslands = otherIslands;
            this.ProximityRadius = 50.0f;
            this.TargetShip = targetShip;
        }

        public override void Update(GameTime gameTime) {
            this.GoToPoint(this.TargetShip.Position, gameTime);
            base.Update(gameTime);
        }

        void GoToPoint(Vector2 point, GameTime gt) {
            var delta = this.ControlledShip.Position - point;
            // my rotation should match this angle (-180.0f - 180.0f)
            var desiredRotation = MathHelper.ToDegrees((float)Math.Atan2(delta.Y, delta.X));

            // drawing is off by 90.0
            desiredRotation -= 90.0f;
            if (desiredRotation > 180.0f) {
                desiredRotation -= 360.0f;
            } else if (desiredRotation < -180.0f) {
                desiredRotation += 360.0f;
            } else { }

            var myRotation = MathHelper.ToDegrees(this.ControlledShip.Rotation);
            // body rotations are 0 - 360.0f
            myRotation -= 180.0f;

            var angleDifference = (myRotation - desiredRotation);
            if (angleDifference > 180.0f) {
                this.TurnLeft();
            } else if (angleDifference > 0) {
                this.TurnRight();
            } else if (angleDifference > -180.0f) {
                this.TurnLeft();
            } else if (angleDifference < -180.0f) {
                this.TurnRight();
            } else { }
        }

        void TurnLeft() {
            this.ControlledShip.Rotate(-1.0f * 15.0f);
        }

        void TurnRight() {
            this.ControlledShip.Rotate(1.0f * 15.0f);
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
