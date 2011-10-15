using System.Collections.Generic;
using _2HourGame.Controller;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    internal class ShipControllerFactory : GameObjectFactory
    {
        public ShipControllerFactory(World world, TextureManager textureManager, AnimationManager am)
            : base(world, textureManager, am)
        {
        }

        public IEnumerable<ShipController> CreateShipControllers(IEnumerable<Player> players)
        {
            foreach (Player p in players)
            {
                yield return CreateShipController(p);
            }
        }

        public ShipController CreateShipController(Player player)
        {
            return new ShipController(player);
        }
    }
}