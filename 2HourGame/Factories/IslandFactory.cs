using System.Collections.Generic;
using System.Linq;
using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;

namespace _2HourGame.Factories
{
    internal class IslandFactory : PhysicsGameObjectFactory
    {
        public IslandFactory(PhysicsWorld world, TextureManager tm, AnimationManager am) : base(world, tm, am)
        {
        }

        public List<Island> CreatePlayerIslands(IEnumerable<Vector2> islandPositions)
        {
            return CreateIslands(islandPositions, new[] {0, 0, 0, 0});
        }

        public List<Island> CreateIslands(IEnumerable<Vector2> islandPositions, IEnumerable<int> initialGolds)
        {
            return islandPositions.Zip(initialGolds, (p, g) => CreateIsland(p, g)).ToList();
        }

        public Island CreateIsland(Vector2 position, int goldAmount)
        {
            var island = new Island(PhysicsWorld, position, goldAmount);
            var islandView = new GameObjectView(World, "island", Color.White, island,
                                                ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.island),
                                                TextureManager, AnimationManager);
            island.Spawn();

            return island;
        }
    }
}