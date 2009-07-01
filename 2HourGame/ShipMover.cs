using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame {
  class ShipMover : GameComponent {
    Ship ship;
    PlayerIndex playerIndex;

    float maxRotation = 0.01f;
    float maxAcceleration = 0.01f;
    float maxSpeed = 1f;

    public ShipMover(Game game, Ship ship, PlayerIndex playerIndex)
      : base(game) {
      this.ship = ship;
      this.playerIndex = playerIndex;
    }

    public override void Update(GameTime gameTime) {
      GamePadState gs = GamePad.GetState(playerIndex);
      if (gs.IsConnected) {
        // TODO Collision detection somewhere

          moveShip(gs);
      }
        // Not connected
      else { }

      base.Update(gameTime);
    }

    private void moveShip(GamePadState gs)
    {
        // set the ships angle
        if (gs.ThumbSticks.Left.X != 0 || gs.ThumbSticks.Left.Y != 0)
        {
            ship.Rotation += gs.ThumbSticks.Left.X * maxRotation;
        }

        // set the acceleration / deacceleration
        float magnitude = gs.ThumbSticks.Left.Y * maxAcceleration;
        ship.Speed += magnitude;
        if (ship.Speed > maxSpeed)
            ship.Speed = maxSpeed;
        if (ship.Speed < 0)
            ship.Speed = 0;

        // move the ship
        ship.Offset((float)Math.Cos(ship.Rotation - (0.5 * Math.PI)) * ship.Speed, (float)Math.Sin(ship.Rotation - (0.5 * Math.PI)) * ship.Speed);
    }

      //private void moveShip(GamePadState gs)
      //{
      //          // set the ships angle
      //  if (gs.ThumbSticks.Left.X != 0 || gs.ThumbSticks.Left.Y != 0)
      //  {
      //      float desiredRotation = (float)Math.Atan2(gs.ThumbSticks.Left.X, gs.ThumbSticks.Left.Y);

      //      float desiredRotationDifference = ship.Rotation - desiredRotation;

      //      if (desiredRotationDifference < 0)
      //          desiredRotationDifference += (float)(2 * Math.PI);

      //      if (Math.Abs(desiredRotationDifference) <= maxRotation || (2 * Math.PI) - Math.Abs(desiredRotationDifference) <= maxRotation)
      //      {
      //          ship.Rotation = desiredRotation;
      //      }
      //      else
      //      {
      //          if (desiredRotationDifference < -Math.PI ||
      //              (desiredRotationDifference > 0 && desiredRotationDifference < Math.PI))
      //              ship.Rotation -= maxRotation;
      //          else
      //              ship.Rotation += maxRotation;
      //      }
      //  }

      //  // move the ship
      //  float magnitude = (float)Math.Sqrt(Math.Pow(gs.ThumbSticks.Left.X, 2) + Math.Pow(gs.ThumbSticks.Left.Y, 2));
      //  ship.Offset((float)Math.Cos(ship.Rotation - (0.5 * Math.PI)) * magnitude, (float)Math.Sin(ship.Rotation - (0.5 * Math.PI)) * magnitude);
      //}

    //private Vector2 invertYAxis(Vector2 vector) 
    //{
    //    return new Vector2(vector.X, -vector.Y);
    //}
  }
}
