using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    internal class ShipGoldViewFactory : GameObjectFactory
    {
        public ShipGoldViewFactory(World world, float displayWidth, TextureManager textureManager, AnimationManager am)
            : base(world, textureManager, am)
        {
            DisplayWidth = displayWidth;
        }

        private float DisplayWidth { get; set; }

        public ShipGoldView CreateShipGoldView(Ship ship, ShipGoldView.GoldViewPosition position)
        {
            return new ShipGoldView(World, ship, position, DisplayWidth, TextureManager, AnimationManager);
        }
    }
}