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
    delegate void ControllerBehaviour(GamePadState gs, Player player, GameTime gameTime);

    class ShipController : GameComponent {

        public event ControllerBehaviour ProcessControllerBehaviours;

        Player player;

        ShipRelativeMoveBehavior moveShipBehavior = new ShipRelativeMoveBehavior();

        Island goldIsland;
        float goldIslandMinimumRange;

        // we need to be able to add 

        public ShipController(Game game, Player player)
            : base(game) {
            this.player = player;
            goldIsland = null;
            goldIslandMinimumRange = 100;

            ProcessControllerBehaviours += ShipControllBehaviours.FireCannons;
        }

        public override void Update(GameTime gameTime) {

            GamePadState gs = player.getGamePadState();

            if (gs.IsConnected)
            {
                ProcessControllerBehaviours(gs, player, gameTime);

                //// Gold Pickup Behaviour
                //if (goldIsland != null && ship.Speed > 0.15)
                //    goldIsland = null;
                //if (gs.Buttons.A == ButtonState.Pressed && ship.Speed <= 0.15)
                //{
                //    Island closestInRangeIsland = GetClosestInRangeIsland();
                //    if (closestInRangeIsland != null)
                //    {
                //        if (closestInRangeIsland == ship.HomeIsland)
                //        {
                //            ship.UnloadGoldToIsland(closestInRangeIsland);
                //        }
                //        else
                //        {
                //            if (goldIsland == null)
                //            {
                //                if (closestInRangeIsland != null && closestInRangeIsland != ship.HomeIsland)
                //                {
                //                    goldIsland = closestInRangeIsland;
                //                }
                //            }
                //            else if (goldIsland == GetClosestInRangeIsland())
                //            {
                //                ship.LoadGoldFromIsland(goldIsland, gameTime);
                //            }
                //        }
                //    }
                //}

                //// Fire Cannons Behaviour
                //if (gs.IsButtonDown(Buttons.LeftTrigger))
                //{
                //    ship.FireCannon(gameTime, CannonType.LeftCannon);
                //}
                //if (gs.IsButtonDown(Buttons.RightTrigger))
                //{
                //    ship.FireCannon(gameTime, CannonType.RightCannon);
                //}

                moveShipBehavior.MoveShip(gs, player.ship);
            }
            // Controller Not connected
            else
            {
            }
            base.Update(gameTime);
        }
    }
}
