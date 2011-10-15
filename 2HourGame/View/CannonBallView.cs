using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;

namespace _2HourGame.View
{
    internal class CannonBallView : GameObjectView
    {
        public CannonBallView(CannonBall cannonBall, World world, TextureManager tm, AnimationManager am)
            : base(
                world, "cannonBall", Color.White, cannonBall,
                ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonBall), tm, am)
        {
        }
    }
}