using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class ShipGoldViewFactory : DrawableGameObjectFactory {

        float DisplayWidth { get; set; }

        public ShipGoldViewFactory(Game game, SpriteBatch spriteBatch, float displayWidth) : base(game, spriteBatch) {
            this.DisplayWidth = displayWidth;
        }

        public ShipGoldView CreateShipGoldView(Ship ship, ShipGoldView.GoldViewPosition position) {
            return new ShipGoldView(base.Game, ship, position, base.SpriteBatch, this.DisplayWidth);
        }
    }
}
