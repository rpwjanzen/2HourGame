using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class CanonBall : GameObject
    {
        public float Speed { get; private set; }
        float maxSpeed = 1f;

        public CanonBall(Game game, Vector2 position, SpriteBatch spriteBatch)
            : base(game, position, "cannonBall", 1f, Color.White, spriteBatch) {
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
        }

        public bool OutOfBounds() {
            float x = this.Position.X;
            float y = this.Position.Y;
            if (x < 0 - this.Bounds.Radius ||
                x > 1280 + this.Bounds.Radius ||
                y < 0 - this.Bounds.Radius ||
                y > 1280 + this.Bounds.Radius)
                return true;
            return false;
        }
    }
}
