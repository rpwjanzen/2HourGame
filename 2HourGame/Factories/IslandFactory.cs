﻿using System.Collections.Generic;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class IslandFactory : PhysicsGameObjectFactory {

        public IslandFactory(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch) { }

        public List<Island> CreatePlayerIslands(IEnumerable<Vector2> islandPositions) {
            return CreateIslands(islandPositions, new int[] { 0, 0, 0, 0 });
        }

        public List<Island> CreateIslands(IEnumerable<Vector2> islandPositions, IEnumerable<int> initialGolds) {
            return islandPositions.Zip(initialGolds, (p, g) => this.CreateIsland(p, g)).ToList();
        }

        public Island CreateIsland(Vector2 position, int goldAmount) {
            Island island = new Island(base.Game, position, goldAmount);
            GameObjectView islandView = new GameObjectView(base.Game, Content.Island, Color.White, SpriteBatch, island, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.island));
            base.Game.Components.Add(island);
            base.Game.Components.Add(islandView);
            return island;
        }
    }
}
