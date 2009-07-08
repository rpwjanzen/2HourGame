using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class Island : GameObject {

        private GameObject islandBuilding;

        public int gold;

        public Island(Game game, Vector2 position, GameObject islandBuilding, SpriteBatch spriteBatch, int gold)
            : base(game, position, "island", 1f, Color.White, spriteBatch)
        {
            this.islandBuilding = islandBuilding;
            this.gold = gold;
        }
    }
}
