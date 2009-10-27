using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Graphics;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class GoldPickupProgressView : ActorView
    {
        Vector2 Offset { get; set; }

        Player Player { get; set; }
        ProgressBar ProgressBar { get; set; }

        public GoldPickupProgressView(Player player, World world, TextureManager textureManager, AnimationManager am)
            : base(player.Ship, world, textureManager, am)
        {
            Player = player;            
            Offset = new Vector2(0, -55);

            ProgressBar = new ProgressBar();
            ProgressBar.FillColor = Color.Gold;
            ProgressBar.EmptyColor = Color.Gray;
        }

        public override void LoadContent(ContentManager content)
        {
            ProgressBar.LoadContent(TextureManager);
            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ProgressBar.Position = Player.Ship.Position + Offset;
            ProgressBar.Progress = ((float)Player.numGoldButtonPresses) / Player.numGoldButtonPressesRequired;

            if (Player.Ship.IsAlive
                && Player.ClosestInRangeIsland != null
                && Player.ClosestInRangeIsland != Player.HomeIsland
                && Player.ClosestInRangeIsland.HasGold
                && Player.ShipIsMovingSlowly
                && !Player.Ship.IsFull)
            {
                ProgressBar.Draw(spriteBatch);
            }
        }
    }
}
