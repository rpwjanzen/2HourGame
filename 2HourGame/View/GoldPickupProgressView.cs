using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class GoldPickupProgressView : ActorView
    {
        public GoldPickupProgressView(Player player, World world, TextureManager textureManager, AnimationManager am)
            : base(player.Ship, world, textureManager, am)
        {
            Player = player;
            Offset = new Vector2(0, -55);

            ProgressBar = new ProgressBar();
            ProgressBar.FillColor = Color.Gold;
            ProgressBar.EmptyColor = Color.Gray;
        }

        private Vector2 Offset { get; set; }

        private Player Player { get; set; }
        private ProgressBar ProgressBar { get; set; }

        public override void LoadContent(ContentManager content)
        {
            ProgressBar.LoadContent(TextureManager);
            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ProgressBar.Position = Player.Ship.Position + Offset;
            ProgressBar.Progress = (Player.numGoldButtonPresses)/Player.numGoldButtonPressesRequired;

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