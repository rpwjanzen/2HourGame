using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.Model;
using _2HourGame.Controller;

namespace _2HourGame.Factories
{
    class ShipControllerFactory : GameObjectFactory {
        public ShipControllerFactory(World world)
            : base(world) {
        }

        public IEnumerable<ShipController> CreateShipControllers(IEnumerable<Player> players) {
            foreach (Player p in players)
            {
                yield return CreateShipController(p);
            }
        }

        public ShipController CreateShipController(Player player) {
            return new ShipController(player);
        }
    }
}
