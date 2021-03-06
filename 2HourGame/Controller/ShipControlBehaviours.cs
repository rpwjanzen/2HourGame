﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _2HourGame.Model;
using _2HourGame.View;

namespace _2HourGame.Controller
{
    static class ShipControlBehaviours
    {
        public enum Action { FireLeftCannon, FireRightCannon, PickupGold, RepairShip };

        static Dictionary<Action, Buttons> actionKeys;

        static ShipControlBehaviours()
        {
            actionKeys = new Dictionary<Action, Buttons>();

            actionKeys.Add(Action.FireLeftCannon, Buttons.LeftTrigger);
            actionKeys.Add(Action.FireRightCannon, Buttons.RightTrigger);
            actionKeys.Add(Action.PickupGold, Buttons.A);
            actionKeys.Add(Action.RepairShip, Buttons.B);
        }


        public static void FireCannons(GamePadState gs, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            if (gs.IsButtonDown(actionKeys[Action.FireLeftCannon]))
            {
                player.FireLeftCannons(gameTime);
            }
            if (gs.IsButtonDown(actionKeys[Action.FireRightCannon]))
            {
                player.FireRightCannons(gameTime);
            }
        }

        public static void PickupGold(GamePadState gs, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            var pickupGoldButton = actionKeys[Action.PickupGold];
            if (gs.IsButtonDown(pickupGoldButton) && previousGamePadState.IsButtonUp(pickupGoldButton))
            {
                player.AttemptPickupGold();
            }
        }

        public static void RepairShip(GamePadState gs, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            var repairButton = actionKeys[Action.RepairShip];
            if (gs.IsButtonDown(repairButton) && previousGamePadState.IsButtonUp(repairButton))
                player.AttemptRepair();
        }
    }
}
