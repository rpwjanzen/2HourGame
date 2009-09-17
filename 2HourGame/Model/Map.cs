using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model
{
    class Map
    {
        List<Island> islands;

        private Map(List<Island> islands) 
        {
            this.islands = islands;
        }

        /// <summary>
        /// Get the island that is closest to the ship and within range.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="range">Minimum range to look for islands.</param>
        /// <returns>The closest island if one exists, otherwise null.</returns>
        public Island GetClosestInRangeIsland(Ship ship, float range)
        {
            Island closestIsland = null;
            float closestIslandDistance = int.MaxValue;

            foreach (Island i in islands)
            {
                float distanceToIsland = (ship.Position - i.Position).Length();
                if (distanceToIsland < closestIslandDistance)
                {
                    closestIslandDistance = distanceToIsland;
                    closestIsland = i;
                }
            }

            if (closestIslandDistance <= range)
                return closestIsland;
            else
                return null;
        }
    }
}
