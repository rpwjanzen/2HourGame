using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _2HourGame.Model;
using _2HourGame.View;

namespace _2HourGame.Controller
{
    static class ShipControllBehaviours
    {
        public static void FireCannons(GamePadState gs, Player player, GameTime gameTime) 
        {
            if (gs.IsButtonDown(Buttons.LeftTrigger))
            {
                player.FireCannon(gameTime, CannonType.LeftCannon);
            }
            if (gs.IsButtonDown(Buttons.RightTrigger))
            {
                player.FireCannon(gameTime, CannonType.RightCannon);
            }
        }

        public static void PickupGold(GamePadState gs, Player player, GameTime gameTime) 
        {
            if (gs.IsButtonDown(Buttons.A)) 
            {
                player.AttemptPickupGold(gameTime);
            }
        }
    }
}
