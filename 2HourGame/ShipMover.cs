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
        TimeSpan goldTimer;
        Island goldIsland;
        float goldIslandMinimumRange;
        float goldPickupWaitSeconds;

        public ShipMover(Game game, Ship ship, PlayerIndex playerIndex, IEnumerable<Island> islands)
            : base(game) {
            this.ship = ship;
            this.playerIndex = playerIndex;
            this.islands = islands;
            goldIsland = null;
            goldIslandMinimumRange = 10;
            goldPickupWaitSeconds = 2;
        }

        public override void Update(GameTime gameTime) {
            GamePadState gs = GamePad.GetState(playerIndex);
            if (gs.IsConnected) {
                // Gold Pickup Behaviour
                if (goldIsland != null && ship.Speed > 0.15) 
                    goldIsland = null;
                if (gs.Buttons.B == ButtonState.Pressed && ship.Speed <= 0.15) 
                {
                    Island closestInRangeIsland = getClosestInRangeIsland();

                    if (closestInRangeIsland.shipThatOwnedThisIsland == ship)
                    {
                        closestInRangeIsland.gold += ship.gold;
                        ship.gold = 0;
                    }
                    else
                    {
                        if (goldIsland == null)
                        {
                            if (closestInRangeIsland != null && closestInRangeIsland.shipThatOwnedThisIsland != ship)
                            {
                                goldTimer = gameTime.TotalGameTime;
                                goldIsland = closestInRangeIsland;
                            }
                        }
                        else if (gameTime.TotalGameTime.TotalSeconds - goldTimer.TotalSeconds > 2 &&
                            goldIsland == getClosestInRangeIsland() &&
                            goldIsland.gold > 0 &&
                            ship.gold < ship.maxGold)
                        {
                            goldIsland.gold--;
                            ship.gold++;
                            goldTimer = gameTime.TotalGameTime;
                        }
                    }
                }

                // Fire Cannons Behaviour

                // TODO Collision detection somewhere
                moveShipBehavior.MoveShip(gs, ship);
            }
                // Not connected
            else { }

            base.Update(gameTime);
        }

        private Island getClosestInRangeIsland() 
        {
            Island closestIsland = null;
            float closestDistance = 99999999;

            foreach (Island i in islands) 
            {
                float distance = ship.Bounds.DifferenceVector(i.Bounds).Length() - (ship.Bounds.Radius + i.Bounds.Radius);
                if (distance < closestDistance) 
                {
                    closestDistance = distance;
                    closestIsland = i;
                }
            }

            if (closestDistance <= goldIslandMinimumRange)
                return closestIsland;
            else
                return null;
        }
    }
}
