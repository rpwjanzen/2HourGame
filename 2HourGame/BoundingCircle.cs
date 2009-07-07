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

        public Vector2 DifferenceVector(BoundingCircle bc) {
            Vector2 differenceAngle = Center - bc.Center;

            float overlap = (Radius + bc.Radius) - differenceAngle.Length();
            differenceAngle.Normalize();

            return differenceAngle * overlap;
        }
    }
}
