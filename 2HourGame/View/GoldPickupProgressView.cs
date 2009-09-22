using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class GoldPickupProgressView : DrawableGameComponent
    {
        SpriteBatch SpriteBatch { get; set; }
        Vector2 Offset { get; set; }

        Player Player { get; set; }
        ProgressBar ProgressBar { get; set; }

        public GoldPickupProgressView(Game game, SpriteBatch spriteBatch, Player player)
            : base(game)
        {
            this.SpriteBatch = spriteBatch;
            Player = player;            
            Offset = new Vector2(0, -55);

            ProgressBar = new ProgressBar();
            ProgressBar.FillColor = Color.Gold;
            ProgressBar.EmptyColor = Color.Gray;
            ProgressBar.Scale = 1.0f;
        }

        public new void LoadContent()
        {
            ProgressBar.LoadContent((ITextureManager)Game.Services.GetService(typeof(ITextureManager)));
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ProgressBar.Position = Player.ship.Position + Offset;
            ProgressBar.Progress = ((float)Player.numGoldButtonPresses) / Player.numGoldButtonPressesRequired;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Player.ClosestInRangeIsland != null
                && Player.ClosestInRangeIsland != Player.HomeIsland
                && Player.ClosestInRangeIsland.HasGold
                && Player.ShipIsMovingSlowly
                && !Player.ship.IsFull)
            {
                ProgressBar.Draw(SpriteBatch);
            }
        }
    }
}
