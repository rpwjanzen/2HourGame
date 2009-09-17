using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class ShipActionViewFactory : GameObjectFactory {
        SpriteBatch spriteBatch;

        public ShipActionViewFactory(Game game, SpriteBatch spriteBatch)
            : base(game) {
                this.spriteBatch = spriteBatch;
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
            return new ShipActionsView(base.Game, player, spriteBatch);
        }
    }
}
