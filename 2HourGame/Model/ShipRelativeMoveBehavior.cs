using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.Model
{
    internal class ShipRelativeMoveBehavior
    {
        public void MoveShip(GamePadState gs, Ship ship)
        {
            if (gs.ThumbSticks.Left != Vector2.Zero)
            {
                if (gs.ThumbSticks.Left.Y > 0)
                {
                    ship.Accelerate(gs.ThumbSticks.Left.Y*10);
                }
                ship.Rotate(gs.ThumbSticks.Left.X*20);
            }
        }
    }
}