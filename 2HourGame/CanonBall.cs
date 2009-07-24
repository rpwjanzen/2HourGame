using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;

namespace _2HourGame
{
    class CanonBall {
        Body Body;
        Geom Geometry;

        Vector2 Velocity {
            get { return this.Body.LinearVelocity; }
        }
        Vector2 Position {
            get { return this.Body.Position; }
        }

        Texture Texture;
        SpriteBatch SpriteBatch;

        public CanonBall() {
        }

        public void AddToPhysicsSimulator(PhysicsSimulator physicsSimulator) {
            physicsSimulator.Add(this.Body);
            physicsSimulator.Add(this.Geometry);
        }

        public void RemoveFromPhysicsSimulator(PhysicsSimulator physicsSimulator) {
            physicsSimulator.Remove(this.Body);
            physicsSimulator.Remove(this.Geometry);
        }
    }
}
