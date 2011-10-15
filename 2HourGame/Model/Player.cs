using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.Model
{
    internal class Player
    {
        private const float inRangeIslandMinimumRange = 100;
        private const float maxShipSpeedForIslandInteraction = 0.15f;
        public const float numGoldButtonPressesRequired = 20;
        private readonly PlayerIndex _playerIndex;
        private readonly Island homeIsland;
        private readonly Map map;

        public Player(PlayerIndex playerIndex, Ship ship, Island homeIsland, Map map)
        {
            _playerIndex = playerIndex;
            Ship = ship;
            this.homeIsland = homeIsland;
            this.map = map;
        }

        #region Controller Actions

        public void FireLeftCannons(GameTime gameTime)
        {
            Ship.FireLeftCannons(gameTime);
        }

        public void FireRightCannons(GameTime gameTime)
        {
            Ship.FireRightCannons(gameTime);
        }

        public void AttemptPickupGold()
        {
            if (!Ship.IsAlive)
            {
                return;
            }

            if (!ShipIsMovingSlowly)
            {
                return;
            }

            if (ClosestInRangeIsland == null)
            {
                numGoldButtonPresses = 0;
                return;
            }

            if (ClosestInRangeIsland == homeIsland)
            {
                Ship.UnloadGoldToIsland(ClosestInRangeIsland);
                return;
            }

            if (numGoldButtonPresses >= numGoldButtonPressesRequired)
            {
                Ship.LoadGoldFromIsland(ClosestInRangeIsland);
                numGoldButtonPresses = 0;
            }
            else
            {
                numGoldButtonPresses++;
            }
        }

        public void AttemptRepair()
        {
            if (!Ship.IsAlive)
            {
                return;
            }

            if (!ShipIsMovingSlowly)
            {
                return;
            }

            if (ClosestInRangeIsland == null)
            {
                return;
            }

            if (ClosestInRangeIsland == homeIsland)
            {
                Ship.Repair();
            }
        }

        #endregion

        public Ship Ship { get; private set; }

        public Island ClosestInRangeIsland
        {
            get { return map.GetClosestInRangeIsland(Ship, inRangeIslandMinimumRange); }
        }

        public Island HomeIsland
        {
            get { return homeIsland; }
        }

        public bool ShipIsMovingSlowly
        {
            get { return Ship.Speed <= maxShipSpeedForIslandInteraction; }
        }

        public GamePadState GamePadState
        {
            get { return GamePad.GetState(_playerIndex); }
        }

        public PlayerIndex PlayerIndex
        {
            get { return _playerIndex; }
        }

        public int numGoldButtonPresses { get; private set; }

        public int TotalGold
        {
            get { return homeIsland.Gold + Ship.Gold; }
        }
    }
}