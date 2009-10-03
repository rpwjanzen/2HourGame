using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class ShipGoldViewFactory : DrawableGameObjectFactory {

        float DisplayWidth { get; set; }

        public ShipGoldViewFactory(Game game, SpriteBatch spriteBatch, float displayWidth) : base(game, spriteBatch) {
            this.DisplayWidth = displayWidth;
        }

        public ShipGoldView CreateShipGoldView(IShip ship, ShipGoldView.GoldViewPosition position) {
            return new ShipGoldView(base.Game, ship, position, base.SpriteBatch, this.DisplayWidth);
        }
    }
}
