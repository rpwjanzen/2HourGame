using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame {
    class Island : GameObject {

		private GameObject islandBuilding;

        private int gold;
        public int Gold {
            get { return gold; }
            set { this.gold = value; }
        }

		public Ship shipThatOwnedThisIsland;

        public Island(Game game, Vector2 position, GameObject islandBuilding, SpriteBatch spriteBatch, int gold, Ship shipThatOwnedThisIsland, PhysicsSimulator physicsSimulator)
            : base(game, position, "island", 1f, Color.White, spriteBatch, physicsSimulator) {
			this.islandBuilding = islandBuilding;
			this.Gold = gold;
			this.shipThatOwnedThisIsland = shipThatOwnedThisIsland;
        }

        protected override void LoadContent() {
            base.LoadContent();
            this.Body.IsStatic = true;
        }
    }
}
