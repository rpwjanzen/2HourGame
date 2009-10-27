using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class ShipGoldViewFactory : GameObjectFactory {

        float DisplayWidth { get; set; }

        public ShipGoldViewFactory(World world, float displayWidth) : base(world) {
            this.DisplayWidth = displayWidth;
        }

        public ShipGoldView CreateShipGoldView(Ship ship, ShipGoldView.GoldViewPosition position) {
            return new ShipGoldView(this.World, ship, position, this.DisplayWidth);
        }
    }
}
