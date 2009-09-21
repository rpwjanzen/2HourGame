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
        Texture2D Texture { get; set; }
        Vector2 Offset { get; set; }
        Vector2 Origin { get; set; }
        float ZIndex { get; set; }

        public Color FillColor { get; set; }
        public Color EmptyColor { get; set; }
        public float Progress {
            get { return ((float)Player.numGoldButtonPresses) / Player.numGoldButtonPressesRequired; }
        }
        public Vector2 DrawPosition {
            get { return Player.ship.Position + Offset; }
        }
        public float Scale { get; set; }

        Player Player { get; set; }

        public GoldPickupProgressView(Game game, SpriteBatch spriteBatch, Player player)
            :base(game)
        {
            FillColor = Color.Gold;
            EmptyColor = Color.Gray;
            Scale = 1.0f;
            Player = player;            

            this.SpriteBatch = spriteBatch;
            Offset = new Vector2(0, -25);
            Origin = ((ITextureManager)game.Services.GetService(typeof(ITextureManager))).getTextureOrigin("healthBar", Scale); 
            Texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("healthBar");
            ZIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.healthBar);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Player.ClosestInRangeIsland != null
                && Player.ClosestInRangeIsland != Player.HomeIsland
                && Player.ShipIsMovingSlowly
                && !Player.ship.IsFull)
            {
                SpriteBatch.Draw(
                    Texture,
                    DrawPosition + Offset, null, EmptyColor, 0f, Origin, Scale, SpriteEffects.None, ZIndex + 0.001f);
                SpriteBatch.Draw(
                    Texture,
                    DrawPosition + Offset,
                    new Rectangle(0, 0, (int)(Texture.Width * Progress), Texture.Height),
                    FillColor, 0f, Origin, Scale, SpriteEffects.None, ZIndex);
            }
        }
    }
}
