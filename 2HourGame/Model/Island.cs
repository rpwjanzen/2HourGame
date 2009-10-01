using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

using _2HourGame.View;

namespace _2HourGame.Model
{
    class Island : PhysicsGameObject
    {
		private GameObject islandBuilding;

        public int Gold { get; private set; }
        public bool HasGold {
            get { return this.Gold > 0; }
        }

        public Island(Game game, Vector2 position, int initialGold, GameObject islandBuilding, string contentName)
            : base(game, position, contentName, 1f)
        {
			this.islandBuilding = islandBuilding;
            this.Gold = initialGold;
            base.Body.IsStatic = true;
        }

        /// <summary>
        /// Removes one gold from the island.
        /// </summary>
        public void RemoveGold() {
            this.Gold--;
        }

        public void AddGold(int amount) {
            this.Gold += amount;
        }
    }
}
