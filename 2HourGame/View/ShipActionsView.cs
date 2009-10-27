using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class ShipActionsView : ActorView
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

        GoldPickupProgressView goldPickupProgressView;

        public ShipActionsView(World world, Player player) 
            :base(player.Ship, world)
        {
            this.player = player;
            goldPickupProgressView = new GoldPickupProgressView(player, world);
        }

        public override void LoadContent(ContentManager content)
        {
            aButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[aButtonTextureName];
            bButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[bButtonTextureName];
            digTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[digTextureName];
            repairTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))[repairTextureName];

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GamePadState gamePadState = player.GamePadState;

            // dig icon display
            if (player.ClosestInRangeIsland != player.HomeIsland
                && player.ClosestInRangeIsland != null
                && player.ClosestInRangeIsland.HasGold
                && player.ShipIsMovingSlowly
                && !player.Ship.IsFull)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.A) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(aButtonTexture, player.Ship.Position + aButtonOffset, null, drawColor, 0, aButtonTexture.Center(aButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonA));
                spriteBatch.Draw(digTexture, player.Ship.Position + digOffset, null, Color.White, 0, digTexture.Center(digScale), digScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.dig));
            }

            // repair icon display
            if (player.ClosestInRangeIsland == player.HomeIsland
                && player.ShipIsMovingSlowly
                && player.Ship.IsDamaged)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.B) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(bButtonTexture, player.Ship.Position + bButtonOffset, null, drawColor, 0, bButtonTexture.Center(bButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonB));
                spriteBatch.Draw(repairTexture, player.Ship.Position + repairOffset, null, Color.White, 0, repairTexture.Center(repairScale), repairScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.repair));
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
