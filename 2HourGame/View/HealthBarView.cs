using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class HealthBarView : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        IShip ship;
        Vector2 offset;
        ProgressBar progressBar;

        public HealthBarView(Game game, SpriteBatch spriteBatch, IShip ship)
            :base(game)
        {
            this.ship = ship;
            this.spriteBatch = spriteBatch;
            offset = new Vector2(0, 35);

            progressBar = new ProgressBar();
            progressBar.FillColor = Color.Green;
            progressBar.EmptyColor = Color.Red;
        }

        public new void LoadContent()
        {
            progressBar.LoadContent((ITextureManager)Game.Services.GetService(typeof(ITextureManager)));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            progressBar.Progress = (float)ship.HealthPercentage;
            progressBar.Position = ship.Position + offset;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (ship.IsDamaged)
            {
                progressBar.Draw(spriteBatch);
            }
        }

    }
}
