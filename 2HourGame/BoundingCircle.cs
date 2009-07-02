using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame {
    class BoundingCircle {
        public Vector2 Center { get; set; }
        public float Radius { get; set; }

        public bool Intersects(BoundingCircle bc) {
            return (bc.Center - this.Center).Length() < bc.Radius + this.Radius;
        }
    }
}
