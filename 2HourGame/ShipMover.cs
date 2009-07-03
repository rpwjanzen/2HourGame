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

        ShipRelativeMoveBehavior moveShipBehavior = new ShipRelativeMoveBehavior();

        public ShipMover(Game game, Ship ship, PlayerIndex playerIndex)
            : base(game) {
            this.ship = ship;
            this.playerIndex = playerIndex;
        }

        public override void Update(GameTime gameTime) {
            GamePadState gs = GamePad.GetState(playerIndex);
            if (gs.IsConnected) {
                // TODO Collision detection somewhere

                moveShipBehavior.MoveShip(gs, ship);
            }
                // Not connected
            else { }

            base.Update(gameTime);
        }
    }
}
