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

    class ShipController : GameComponent {

        public event ControllerBehaviour ProcessControllerBehaviours;

        Player player;

        ShipRelativeMoveBehavior moveShipBehavior = new ShipRelativeMoveBehavior();

        GamePadState previousGamePadState;

        public ShipController(Game game, Player player)
            : base(game) {
            this.player = player;

            ProcessControllerBehaviours += ShipControllBehaviours.FireCannons;
            ProcessControllerBehaviours += ShipControllBehaviours.PickupGold;
            ProcessControllerBehaviours += ShipControllBehaviours.RepairShip;
        }

        public override void Update(GameTime gameTime) {

            GamePadState gs = player.GamePadState;

            if (gs.IsConnected)
            {
                ProcessControllerBehaviours(gs, previousGamePadState, player, gameTime);

                moveShipBehavior.MoveShip(gs, player.ship);
            }
            // Controller Not connected
            else
            {
            }

            previousGamePadState = gs;

            base.Update(gameTime);
        }
    }
}
