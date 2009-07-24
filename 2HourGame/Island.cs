﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame {
    class Island : OldPhysicsGameObject
    {

		private OldGameObject islandBuilding;

        public int Gold { get; private set; }
        public bool HasGold {
            get { return this.Gold > 0; }
        }

        public Island(Game game, Vector2 position, OldGameObject islandBuilding, SpriteBatch spriteBatch, int initialGold, PhysicsSimulator physicsSimulator, AnimationManager effectManager, float zIndex)
            : base(game, position, "island", 1f, Color.White, spriteBatch, physicsSimulator, null, effectManager, zIndex) {
			this.islandBuilding = islandBuilding;
			this.Gold = initialGold;
        }

        protected override void LoadContent() {
            base.LoadContent();
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
