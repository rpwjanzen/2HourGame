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
        string aButtonTextureName = "ControllerImages\\xboxControllerButtonA";
        float aButtonScale = 0.2f;
        Vector2 aButtonOffset = new Vector2(-18, -30);
            
        Texture2D bButtonTexture;
        string bButtonTextureName = "ControllerImages\\xboxControllerButtonB";
        float bButtonScale = 0.2f;
        Vector2 bButtonOffset = new Vector2(10, -30);

        Texture2D digTexture;
        string digTextureName = "dig";
        float digScale = 0.35f;
        Vector2 digOffset = new Vector2(-5, -35);

        Texture2D repairTexture;
        string repairTextureName = "repair";
        float repairScale = 0.35f;
        Vector2 repairOffset = new Vector2(25, -30);

        SpriteBatch spriteBatch;

        GoldPickupProgressView GoldPickupProgressView { get; set; }

        public ShipActionsView(Game game, Player player, SpriteBatch spriteBatch) 
            :base(game)
        {
            this.player = player;
            this.spriteBatch = spriteBatch;

            aButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(aButtonTextureName);
            bButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(bButtonTextureName);
            digTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(digTextureName);
            repairTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(repairTextureName);
            GoldPickupProgressView = new GoldPickupProgressView(game, spriteBatch, player);
        }

        protected override void LoadContent()
        {
            GoldPickupProgressView.LoadContent();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            GoldPickupProgressView.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GamePadState gamePadState = player.GamePadState;

            // dig icon display
            if (player.ClosestInRangeIsland != player.HomeIsland
                && player.ClosestInRangeIsland != null
                && player.ClosestInRangeIsland.HasGold
                && player.ShipIsMovingSlowly
                && !player.ship.IsFull)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.A) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(aButtonTexture, player.ship.Position + aButtonOffset, null, drawColor, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(aButtonTextureName), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonA));
                spriteBatch.Draw(digTexture, player.ship.Position + digOffset, null, Color.White, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(digTextureName), digScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.dig));
            }

            // gold pickup progress
            if (player.ClosestInRangeIsland != null
                && player.ClosestInRangeIsland != player.HomeIsland)
            {
                GoldPickupProgressView.Draw(gameTime);
            }

            // repair icon display
            if (player.ClosestInRangeIsland == player.HomeIsland
                && player.ShipIsMovingSlowly
                && player.ship.IsDamaged)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.B) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(bButtonTexture, player.ship.Position + bButtonOffset, null, drawColor, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(bButtonTextureName), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonB));
                spriteBatch.Draw(repairTexture, player.ship.Position + repairOffset, null, Color.White, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(repairTextureName), repairScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.repair));
            }
            base.Draw(gameTime);
        }
    }
}
