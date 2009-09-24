using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.Model;

namespace _2HourGame.Controller
{
    class SimpleCannonFirer : GameComponent
    {
        SimpleCannon cannon;

        public SimpleCannonFirer(Game game, SimpleCannon cannon)
            : base(game)
        {
            this.cannon = cannon;
        }

        public override void Update(GameTime gameTime)
        {
            cannon.Rotation += MathHelper.ToRadians(5.0f);
            cannon.TryFire();

            base.Update(gameTime);
        }
    }
}
