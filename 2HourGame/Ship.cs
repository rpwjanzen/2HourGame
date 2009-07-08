using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class Ship : GameObject {
        public float Speed { get; private set; }
        float maxSpeed = 1f;
        int maxGold = 5;

        public Ship(Game game, Vector2 position, SpriteBatch spriteBatch)
            : base(game, position, "boat", 0.6f, Color.White, spriteBatch) {
        }

        public void Accelerate(float amount) {
            this.Speed += amount;
            this.Speed = Math.Min(this.Speed, maxSpeed);
            this.Speed = this.Speed < 0 ? 0 : this.Speed;
        }

        public void Offset(float dx, float dy) {
            this.Offset(new Vector2(dx, dy));
        }

        public void Offset(Vector2 o) {
            this.Position += o;
            this.EnsureInBounds();
        }

        public void RotateRadians(float radians) {
            this.Rotation += radians;
            this.Rotation %= MathHelper.TwoPi;
        }

        public void RotateDegrees(float degrees) {
            this.RotateRadians(MathHelper.ToRadians(degrees));
        }

        void EnsureInBounds() {
            float x = this.Position.X;
            float y = this.Position.Y;
            x = Math.Max(x, 0 + this.Bounds.Radius);
            x = Math.Min(x, 1280 - this.Bounds.Radius);
            y = Math.Max(y, 0 + this.Bounds.Radius);
            y = Math.Min(y, 720 - this.Bounds.Radius);
            this.Position = new Vector2(x, y);
        }
    }
}
