using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class SimpleCannonBallView : DrawableGameComponent
    {
        List<SimpleCannonBall> cannonBalls;
        Vector2 origin;

        Texture2D texture;
        SpriteBatch spriteBatch;
        float scale;


        public SimpleCannonBallView(Game game, List<SimpleCannonBall> cannonBalls, SpriteBatch spriteBatch) : base(game) {
            this.cannonBalls = cannonBalls;
            this.spriteBatch = spriteBatch;            
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(@"Content\cannonBall");
            origin = CalculateCenter(texture);
            scale = SimpleCannonBall.Radius / ((float)texture.Width);

            base.LoadContent();
        }

        Vector2 CalculateCenter(Texture2D texture)
        {
            return new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var cannonBall in cannonBalls)
            {
                spriteBatch.Draw(texture, cannonBall.Position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 1);
            }

            base.Draw(gameTime);
        }
    }
}
