using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class CannonBallManager : GameComponent
    {
        IEnumerable<CannonBall> cannonBalls;
        const float cannonBallDeaccelerationRate = -0.05f;

        float zIndex;
        SpriteBatch spriteBatch;

        public CannonBallManager(Game game, float zIndex, SpriteBatch spriteBatch)
            : base(game) {
            this.spriteBatch = spriteBatch;
            this.zIndex = zIndex;
        }

        public override void Update(GameTime gameTime)
        {
            // TODO explicitly remove all cannonBalls that are no longer moving
            cannonBalls = cannonBalls.Where(x => x.Speed != 0);

            base.Update(gameTime);
        }
    }
}
