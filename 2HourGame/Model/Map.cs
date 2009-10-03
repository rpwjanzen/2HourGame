using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class Map
    {
        List<Island> islands;

        public Map(List<Island> islands) 
        {
            this.islands = islands;
        }

        /// <summary>
        /// Get the island that is closest to the ship and within range.
        /// </summary>
        /// <param name="ship"></param>
        /// <param name="range">Minimum range to look for islands.</param>
        /// <returns>The closest island if one exists, otherwise null.</returns>
        public Island GetClosestInRangeIsland(IShip ship, float range)
        {
            Island closestIsland = ClosestIslandToPoint(ship.Position);

            // We only want the closest island if it is in range
            if (closestIsland != null
                && DistanceToIsland(ship.Position, closestIsland) <= range)
            {
                return closestIsland;
            }
            else
            {
                return null;
            }
        }

        private Island ClosestIslandToPoint(Vector2 point)
        {
            Island closestIsland = null;
            float distanceToClosestIsland = int.MaxValue;

            foreach (Island i in islands)
            {
                float distanceToIsland = DistanceToIsland(point, i);
                if (distanceToIsland < distanceToClosestIsland)
                {
                    distanceToClosestIsland = distanceToIsland;
                    closestIsland = i;
                }
            }

            return closestIsland;
        }

        private float DistanceToIsland(Vector2 point, Island island)
        {
            return Vector2.Distance(point, island.Position);
        }
    }
}
