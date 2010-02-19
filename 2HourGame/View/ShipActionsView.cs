using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class ShipActionsView : DrawableGameComponent
    {
        Player player;

        Texture2D aButtonTexture;
        float aButtonScale = 0.2f;
        Vector2 aButtonOffset = new Vector2(-18, -30);
            
        Texture2D bButtonTexture;
        float bButtonScale = 0.2f;
        Vector2 bButtonOffset = new Vector2(10, -30);

        Texture2D digTexture;
        float digScale = 0.35f;
        Vector2 digOffset = new Vector2(-5, -35);

        Texture2D repairTexture;
        float repairScale = 0.35f;
        Vector2 repairOffset = new Vector2(25, -30);

        SpriteBatch spriteBatch;

        GoldPickupProgressView GoldPickupProgressView { get; set; }

        public ShipActionsView(Game game, Player player, SpriteBatch spriteBatch) 
            :base(game)
        {
            this.player = player;
            this.spriteBatch = spriteBatch;

            GoldPickupProgressView = new GoldPickupProgressView(game, spriteBatch, player);
            Game.Components.Add(GoldPickupProgressView);
        }

        protected override void LoadContent()
        {
            aButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.XboxControllerButtonA];
            bButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.XboxControllerButtonB];
            digTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.Dig];
            repairTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[Content.Repair];

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            // gold pickup progress
            GoldPickupProgressView.Enabled = player.ClosestInRangeIsland != null
                && player.ClosestInRangeIsland != player.HomeIsland
                && player.ClosestInRangeIsland.HasGold
                && !player.ship.IsFull;

            GamePadState gamePadState = player.GamePadState;

            // dig icon display
            if (player.ClosestInRangeIsland != player.HomeIsland
                && player.ClosestInRangeIsland != null
                && player.ClosestInRangeIsland.HasGold
                && player.ShipIsMovingSlowly
                && !player.ship.IsFull)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.A) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(aButtonTexture, player.ship.Position + aButtonOffset, null, drawColor, 0, aButtonTexture.Center(aButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonA));
                spriteBatch.Draw(digTexture, player.ship.Position + digOffset, null, Color.White, 0, digTexture.Center(digScale), digScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.dig));
            }

            // repair icon display
            if (player.ClosestInRangeIsland == player.HomeIsland
                && player.ShipIsMovingSlowly
                && player.ship.IsDamaged)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.B) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(bButtonTexture, player.ship.Position + bButtonOffset, null, drawColor, 0, bButtonTexture.Center(bButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonB));
                spriteBatch.Draw(repairTexture, player.ship.Position + repairOffset, null, Color.White, 0, repairTexture.Center(repairScale), repairScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.repair));
            }
            base.Draw(gameTime);
        }
    }
}
