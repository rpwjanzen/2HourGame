using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.View
{
    /// <summary>
    /// The order of this enum is the order that Items will draw in the game.
    /// Divide the enum position by 100 to get the zIndex.
    /// </summary>
    static class ZIndexManager
    {
        public enum drawnItemOrders
        {
            shipGoldView = 0,
            goldAnimation,
            cannonBall,
            cannonSmokeAnimation,
            shipRigging,
            shipCannon,
            goldLoseAnimation,
            boatHitByCannonAnimation,
            healthBar,
            shipGunwale,
            shipHull,
            floatingCrate,
            shipSinking,
            islandGoldView,
            house,
            island,
            splashAnimation
        }

        public static float getZIndex(drawnItemOrders drawnItem) 
        {
            return ((int)drawnItem) / 100f;
        }
    }
}
