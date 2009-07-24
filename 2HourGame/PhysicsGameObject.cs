using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;

namespace _2HourGame {
    class PhysicsGameObject : GameObject {
        public Body Body { get; private set; }
        public Geom Geometry { get; private set; }
        
        public override Vector2 Position {
            get { return Body.Position; }
        }
        public override float Rotation {
            get { return Body.Rotation; }
        }

        public PhysicsGameObject(Body body, Geom geometry) : base(body.Position, body.Rotation) {
            this.Body = body;
            this.Geometry = geometry;
        }
    }
}
