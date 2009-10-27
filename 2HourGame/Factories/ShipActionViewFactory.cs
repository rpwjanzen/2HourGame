using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class ShipActionViewFactory : GameObjectFactory {

        public ShipActionViewFactory(World world, TextureManager tm, AnimationManager am) : base(world, tm, am) { }

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
