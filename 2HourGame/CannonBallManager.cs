using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;

namespace _2HourGame
{
    class CannonBallManager : GameComponent
    {
        List<CanonBall> cannonBalls;
        public IEnumerable<CanonBall> CannonBalls {
            get { return cannonBalls; }
        }
        float zIndex;
        float CannonBallLayerDepth {
            get { return this.zIndex; }
        }
        PhysicsSimulator PhysicsSimulator { get; set; }
        EffectManager EffectManager { get; set; }
        SpriteBatch spriteBatch;

        public CannonBallManager(Game game, float zIndex, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, EffectManager effectManager)
            : base(game) {
            cannonBalls = new List<CanonBall>();
            this.spriteBatch = spriteBatch;
            this.zIndex = zIndex;
            this.PhysicsSimulator = physicsSimulator;
            this.EffectManager = effectManager;
        }

        public override void Update(GameTime gameTime)
        {
            this.RemoveStationaryCannonBalls();
            base.Update(gameTime);
        }

        public void RemoveCannonBall(CanonBall cannonBall) {
            cannonBalls.Remove(cannonBall);
            base.Game.Components.Remove(cannonBall);
        }

        public CanonBall CreateCannonBall(Vector2 position, Vector2 firingForce) {
            var cannonBall = new CanonBall(this.Game, position, this.spriteBatch, this.PhysicsSimulator, this.EffectManager, this.CannonBallLayerDepth);            
            base.Game.Components.Add(cannonBall);
            cannonBall.ApplyFiringForce(firingForce);
            cannonBalls.Add(cannonBall);

            return cannonBall;
        }

        void RemoveStationaryCannonBalls() {
            cannonBalls = cannonBalls.Where(x => x.Speed != 0).ToList<CanonBall>();
        }
    }
}
