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
    /// <summary>
    /// Actions a player can perform with a ship
    /// </summary>
    public enum PlayerAction { FireLeftCannon, FireRightCannon, PickupGold, RepairShip };

    static class ShipControlBehaviours
    {
        /// <summary>
        /// Mapping of ship actions to buttons on the controller
        /// </summary>
        static readonly Dictionary<PlayerAction, Buttons> _actionButtons;

        static ShipControlBehaviours()
        {
            _actionButtons = new Dictionary<PlayerAction, Buttons>();

            _actionButtons.Add(PlayerAction.FireLeftCannon, Buttons.LeftTrigger);
            _actionButtons.Add(PlayerAction.FireRightCannon, Buttons.RightTrigger);
            _actionButtons.Add(PlayerAction.PickupGold, Buttons.A);
            _actionButtons.Add(PlayerAction.RepairShip, Buttons.B);
        }


        public static void FireCannons(GamePadState gamePadState, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            if (gamePadState.IsButtonDown(_actionButtons[PlayerAction.FireLeftCannon]))
                player.FireLeftCannons(gameTime);
            
            if (gamePadState.IsButtonDown(_actionButtons[PlayerAction.FireRightCannon]))
                player.FireRightCannons(gameTime);
        }

        public static void PickupGold(GamePadState gamePadState, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            var pickupGoldButton = _actionButtons[PlayerAction.PickupGold];
            if (gamePadState.IsButtonDown(pickupGoldButton) && previousGamePadState.IsButtonUp(pickupGoldButton))
                player.AttemptPickupGold();
        }

        public static void RepairShip(GamePadState gamePadState, GamePadState previousGamePadState, Player player, GameTime gameTime)
        {
            var repairButton = _actionButtons[PlayerAction.RepairShip];
            if (gamePadState.IsButtonDown(repairButton) && previousGamePadState.IsButtonUp(repairButton))
                player.AttemptRepair();
        }
    }
}
