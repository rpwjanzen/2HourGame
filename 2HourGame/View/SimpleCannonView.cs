using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class SimpleCannonView : DrawableGameComponent
    {
        SimpleCannon cannon;
        Vector2 origin;

        Texture2D texture;
        SpriteBatch spriteBatch;

        SimpleCannonBallView cannonBallView;

        public SimpleCannonView(Game game, SimpleCannon cannon, SpriteBatch spriteBatch) : base(game) {
            this.cannon = cannon;
            this.spriteBatch = spriteBatch;
            cannonBallView = new SimpleCannonBallView(game, cannon.CannonBalls, spriteBatch);
            game.Components.Add(cannonBallView);
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(@"Content\simpleCannon");
            origin = CalculateCenter(texture);

            base.LoadContent();
        }

        Vector2 CalculateCenter(Texture2D texture)
        {
            return new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, cannon.Position, null, Color.White, cannon.Rotation, origin, 1.0f, SpriteEffects.None, 1);

            base.Draw(gameTime);
        }
    }
}
