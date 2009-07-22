using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame {
    class Island : GameObject {

        public Island(Game game, Vector2 position, PhysicsSimulator physicsSimulator)
            : base(game, position, "island", 1f, physicsSimulator) {
        }

        protected override void LoadContent() {
            base.LoadContent();
            this.Body.IsStatic = true;
        }
    }
}
