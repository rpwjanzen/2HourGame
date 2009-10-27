using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Controller
{
    delegate void ControllerBehaviour(GamePadState gs, GamePadState previousGamePadState, Player player, GameTime gameTime);

    class ShipController {

        public event ControllerBehaviour ProcessControllerBehaviours;

        Player player;

        ShipRelativeMoveBehavior moveShipBehavior = new ShipRelativeMoveBehavior();

        GamePadState previousGamePadState;

        public ShipController(Player player) {
            this.player = player;

            ProcessControllerBehaviours += ShipControlBehaviours.FireCannons;
            ProcessControllerBehaviours += ShipControlBehaviours.PickupGold;
            ProcessControllerBehaviours += ShipControlBehaviours.RepairShip;
        }

        public virtual void Update(GameTime gameTime) {

            GamePadState gs = player.GamePadState;

            if (gs.IsConnected)
            {
                ProcessControllerBehaviours(gs, previousGamePadState, player, gameTime);

                moveShipBehavior.MoveShip(gs, player.Ship);
            }

            previousGamePadState = gs;
        }
    }
}
