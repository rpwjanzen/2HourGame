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
        List<CannonBall> cannonBalls;
        public IEnumerable<CannonBall> CannonBalls {
            get { return cannonBalls; }
        }
        float zIndex;
        float CannonBallLayerDepth {
            get { return this.zIndex; }
        }
        PhysicsSimulator PhysicsSimulator { get; set; }
        SpriteBatch spriteBatch;

        const float minimumCannonBallSpeed = 25f;

        Game game;

        public CannonBallManager(Game game, float zIndex, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator)
            : base(game) {
            cannonBalls = new List<CannonBall>();
            this.spriteBatch = spriteBatch;
            this.zIndex = zIndex;
            this.PhysicsSimulator = physicsSimulator;
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            this.RemoveStationaryCannonBalls();
            base.Update(gameTime);
        }

        //public void RemoveCannonBall(CannonBall cannonBall) {
        //    cannonBalls.Remove(cannonBall);
        //    base.Game.Components.Remove(cannonBall);
        //}

        public CannonBall CreateCannonBall(Vector2 position, Vector2 firingForce) {
            var cannonBall = new CannonBall(this.Game, position, this.spriteBatch, this.PhysicsSimulator, this.CannonBallLayerDepth);            
            base.Game.Components.Add(cannonBall);
            cannonBall.ApplyFiringForce(firingForce);
            cannonBalls.Add(cannonBall);

            ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).CannonSmokeEffect(cannonBall);

            return cannonBall;
        }

        void RemoveStationaryCannonBalls() {
            List<CannonBall> cannonBallsToRemove = cannonBalls.Where(x => x.Speed < minimumCannonBallSpeed).ToList<CannonBall>();

            foreach (CannonBall c in cannonBallsToRemove) 
            {
                ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).SplashEffect(c);
                c.RemoveFromPhysicsSimulator();
                game.Components.Remove(c);
                cannonBalls.Remove(c);
            }
        }
    }
}
