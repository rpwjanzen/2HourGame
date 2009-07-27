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

        IEnumerable<Island> islands;
        Island goldIsland;
        float goldIslandMinimumRange;

        // we need to be able to add 

        public ShipMover(Game game, Ship ship, PlayerIndex playerIndex, IEnumerable<Island> islands)
            : base(game) {
            this.ship = ship;
            this.playerIndex = playerIndex;
            this.islands = islands;
            goldIsland = null;
            goldIslandMinimumRange = 100;
        }

        public override void Update(GameTime gameTime) {
            GamePadState gs = GamePad.GetState(playerIndex);
            if (gs.IsConnected) {
                // Gold Pickup Behaviour
                if (goldIsland != null && ship.Speed > 0.15) 
                    goldIsland = null;
                if (gs.Buttons.B == ButtonState.Pressed && ship.Speed <= 0.15) 
                {
                    Island closestInRangeIsland = GetClosestInRangeIsland();
                    if (closestInRangeIsland != null) {
                        if (closestInRangeIsland == ship.HomeIsland) {
                            ship.UnloadGoldToIsland(closestInRangeIsland);
                        } else {
                            if (goldIsland == null) {
                                if (closestInRangeIsland != null && closestInRangeIsland != ship.HomeIsland) {
                                    goldIsland = closestInRangeIsland;
                                }
                            } else if (goldIsland == GetClosestInRangeIsland()) {
                                ship.LoadGoldFromIsland(goldIsland, gameTime);
                            }
                        }
                    }
                }

                // Fire Cannons Behaviour
                if (gs.IsButtonDown(Buttons.LeftTrigger)) {
                    ship.FireCannon(gameTime, true);
                }
                if (gs.IsButtonDown(Buttons.RightTrigger))
                {
                    ship.FireCannon(gameTime, false);
                }
                // TODO Collision detection somewhere
                moveShipBehavior.MoveShip(gs, ship);
            }
                // Not connected
            else {
            }

            base.Update(gameTime);
        }

        private Island GetClosestInRangeIsland() 
        {
            Island closestIsland = null;
            float closestIslandDistance = int.MaxValue;

            foreach (Island i in islands) 
            {
                float distanceToIsland = (ship.Position - i.Position).Length();
                if (distanceToIsland < closestIslandDistance) 
                {
                    closestIslandDistance = distanceToIsland;
                    closestIsland = i;
                }
            }

            if (closestIslandDistance <= goldIslandMinimumRange)
                return closestIsland;
            else
                return null;
        }
    }
}
