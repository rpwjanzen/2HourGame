using Microsoft.Xna.Framework;

namespace _2HourGame {
    class GameObject {
        public virtual Vector2 Position { get; private set; }
        public virtual float Rotation { get; private set; }

        public GameObject(Vector2 position, float rotation) {
            this.Position = position;
            this.Rotation = rotation;
        }

        public GameObject(Vector2 position) : this(position, 0.0f) { }
    }
}
