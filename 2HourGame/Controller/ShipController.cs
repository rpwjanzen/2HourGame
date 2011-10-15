using _2HourGame.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.Controller
{
    /// <summary>
    /// Defines some behavior that will happen based on the current game state, previous game state, player and the current game time.
    /// </summary>
    internal delegate void ControllerBehaviour(GamePadState gamePadState, GamePadState previousGamePadState, Player player, GameTime gameTime);

    internal sealed class ShipController {

        private readonly ShipRelativeMoveBehavior _moveShipBehavior = new ShipRelativeMoveBehavior();
        private event ControllerBehaviour ProcessControllerBehaviours;

        private readonly Player _player;
        public Player Player { get { return _player;  } }

        public ShipController(Player player)
        {
            _player = player;

            ProcessControllerBehaviours += ShipControlBehaviours.FireCannons;
            ProcessControllerBehaviours += ShipControlBehaviours.PickupGold;
            ProcessControllerBehaviours += ShipControlBehaviours.RepairShip;
        }

        public void Update(GameTime gameTime, GamePadState gamePadState, GamePadState previousGamePadState)
        {
            if (gamePadState.IsConnected)
            {
                ProcessControllerBehaviours(gamePadState, previousGamePadState, _player, gameTime);
                _moveShipBehavior.MoveShip(gamePadState, _player.Ship);
            }
        }
    }
}
