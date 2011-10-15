using System.Collections.Generic;
using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    internal class ShipActionViewFactory : GameObjectFactory
    {
        public ShipActionViewFactory(World world, TextureManager tm, AnimationManager am) : base(world, tm, am)
        {
        }

        public IEnumerable<ShipActionsView> CreateShipActionsViews(IEnumerable<Player> players)
        {
            foreach (Player p in players)
            {
                yield return CreateShipActionsView(p);
            }
        }

        public ShipActionsView CreateShipActionsView(Player player)
        {
            return new ShipActionsView(World, player, TextureManager, AnimationManager);
        }
    }
}