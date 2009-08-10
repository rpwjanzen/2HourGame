using System.Collections.Generic;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

using _2HourGame.Model;
using _2HourGame.View;

namespace _2HourGame.Factories
{
    class IslandFactory : PhysicsGameObjectFactory {

        public IslandFactory(Game game, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator) : base(game, spriteBatch, physicsSimulator) { }

        public List<Island> CreatePlayerIslands(IEnumerable<Vector2> islandPositions, IEnumerable<GameObject> islandBuildings) {
            return CreateIslands(islandPositions, islandBuildings, new int[] { 0, 0, 0, 0 });
        }

        public List<Island> CreateIslands(IEnumerable<Vector2> islandPositions, IEnumerable<GameObject> islandBuildings, IEnumerable<int> initialGolds) {
            return islandPositions.Zip3(islandBuildings, initialGolds, (p, b, g) => this.CreateIsland(p, b, g)).ToList();
        }

        public Island CreateIsland(Vector2 position, GameObject building, int goldAmount) {
            Island island = new Island(base.Game, position, base.PhysicsSimulator, goldAmount, building, "island");
            GameObjectView islandView = new GameObjectView(base.Game, "island", Color.White, SpriteBatch, island, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.island));
            base.Game.Components.Add(island);
            base.Game.Components.Add(islandView);
            return island;
        }
    }
}
