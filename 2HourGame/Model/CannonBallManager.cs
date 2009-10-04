using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Model
{
    class CannonBallManager : GameComponent
    {
        List<CannonBall> cannonBalls;
        public IEnumerable<CannonBall> CannonBalls {
            get { return cannonBalls; }
        }
        PhysicsSimulator PhysicsSimulator { get; set; }
        SpriteBatch spriteBatch;

        const float minimumCannonBallSpeed = 25f;

        public CannonBallManager(Game game, SpriteBatch spriteBatch)
            : base(game) {
            cannonBalls = new List<CannonBall>();
            this.spriteBatch = spriteBatch;
            this.PhysicsSimulator = ((IPhysicsSimulatorService)Game.Services.GetService(typeof(IPhysicsSimulatorService))).PhysicsSimulator;
        }

        public override void Update(GameTime gameTime)
        {
            this.RemoveStationaryCannonBalls();
            base.Update(gameTime);
        }

        public CannonBall CreateCannonBall(Vector2 position, Vector2 firingForce) {
            var cannonBall = new CannonBall(this.Game, position);
            var cannonBallView = new GameObjectView(base.Game, "cannonBall", Color.White, spriteBatch, cannonBall, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonBall));
            base.Game.Components.Add(cannonBall);
            base.Game.Components.Add(cannonBallView);
            cannonBall.ApplyFiringForce(firingForce);
            cannonBalls.Add(cannonBall);

            return cannonBall;
        }

        public void RemoveStationaryCannonBalls() {
            List<CannonBall> cannonBallsToRemove = cannonBalls.Where(x => x.Speed < minimumCannonBallSpeed).ToList<CannonBall>();

            foreach (CannonBall c in cannonBallsToRemove) 
            {
                ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.Splash, c.Position);
                RemoveCannonBall(c);
            }
        }

        public void RemoveCannonBall(CannonBall cannonBall) 
        {
            cannonBall.RemoveFromPhysicsSimulator();
            base.Game.Components.Remove(cannonBall);
            cannonBalls.Remove(cannonBall);
        }
    }
}
