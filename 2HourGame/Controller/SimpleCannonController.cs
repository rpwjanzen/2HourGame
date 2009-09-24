using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.Controller
{
    class SimpleCannonController : GameComponent
    {
        SimpleCannon cannon;

        public SimpleCannonController(Game game, SimpleCannon cannon) : base(game)
        {
            this.cannon = cannon;
        }

        public override void Update(GameTime gameTime)
        {
            var gs = GamePad.GetState(PlayerIndex.One);
            if (gs.IsConnected)
            {
                if (gs.IsButtonDown(Buttons.X))
                {
                    cannon.TryFire();
                }
                if (gs.IsButtonDown(Buttons.DPadLeft))
                {
                    cannon.Rotation += MathHelper.ToRadians(5.0f);
                }
                if (gs.IsButtonDown(Buttons.DPadRight))
                {
                    cannon.Rotation += MathHelper.ToRadians(-5.0f);
                }
            }
            base.Update(gameTime);
        }
    }
}
