using Microsoft.Xna.Framework;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.Controller
{
    class KeyboardShipController : GameComponent
    {
        Player player;
        
        const Keys Left = Keys.Left;
        const Keys Right = Keys.Right;
        const Keys Forward = Keys.Up;
        const Keys FireLeft = Keys.Space;
        const Keys FireRight = Keys.Space;
        const Keys Action = Keys.F;

        KeyboardState previousKeyboardState;

        public KeyboardShipController(Game game, Player player) : base(game)
        {
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            var ship = player.ship;

            var ks = Keyboard.GetState();
            if (ks.IsKeyDown(Forward))
            {
                ship.Accelerate(5.0f);
            }

            if (ks.IsKeyDown(Right))
            {
                ship.Rotate(5.0f);
            }

            if (ks.IsKeyDown(Left))
            {
                ship.Rotate(-5.0f);
            }

            if (ks.IsKeyDown(FireLeft))
            {
                ship.FireLeftCannons(gameTime);
            }

            if (ks.IsKeyDown(FireRight))
            {
                ship.FireRightCannons(gameTime);
            }

            if (ks.IsKeyDown(Action) && previousKeyboardState.IsKeyUp(Action))
            {
                player.AttemptPickupGold();
                player.AttemptRepair();
            }
            
            previousKeyboardState = ks;

            base.Update(gameTime);
        }
    }
}
