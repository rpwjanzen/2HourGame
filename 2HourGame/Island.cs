using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class Island : GameObject {

        public Island(Game game, Vector2 position)
            : base(game, position, "island", 1f) {
        }
    }
}
