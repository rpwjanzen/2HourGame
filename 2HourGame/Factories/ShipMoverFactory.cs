using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class ShipMoverFactory : GameObjectFactory {
        IEnumerable<Island> Islands { get; set; }

        public ShipMoverFactory(Game game, IEnumerable<Island> islands)
            : base(game) {
            this.Islands = islands;
        }

        public List<ShipMover> CreateShipMovers(IEnumerable<Ship> ships, IEnumerable<PlayerIndex> playerIndices) {
            return ships.Zip(playerIndices, (s, p) => CreateShipMover(s, p)).ToList();
        }

        public ShipMover CreateShipMover(Ship ship, PlayerIndex playerIndex) {
            return new ShipMover(base.Game, ship, playerIndex, Islands);
        }
    }
}
