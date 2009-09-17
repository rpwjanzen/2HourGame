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

        public ShipActionsView(Game game, Player player, SpriteBatch spriteBatch) 
            :base(game)
        {
            this.player = player;
            this.spriteBatch = spriteBatch;

            aButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(aButtonTextureName);
            bButtonTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(bButtonTextureName);
            digTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(digTextureName);
            repairTexture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture(repairTextureName);
        }

        public override void Draw(GameTime gameTime)
        {
            GamePadState gamePadState = player.getGamePadState();

            if ((player.ClosestInRangeIslandIsNotHomeAndHasGoldAndShipTravelingSlowEnough() && player.ship.Gold < player.ship.GoldCapacity))
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.A) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(aButtonTexture, player.ship.Position + aButtonOffset, null, drawColor, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(aButtonTextureName, aButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonA));
                spriteBatch.Draw(digTexture, player.ship.Position + digOffset, null, Color.White, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(digTextureName, digScale), digScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.dig));
            }

            if ((player.ClosestInRangeIslandIsHomeAndShipTravelingSlowEnough() && player.ship.health < player.ship.maxHealth))
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.B) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(bButtonTexture, player.ship.Position + bButtonOffset, null, drawColor, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(bButtonTextureName, bButtonScale), aButtonScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonB));
                spriteBatch.Draw(repairTexture, player.ship.Position + repairOffset, null, Color.White, 0, ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(repairTextureName, repairScale), repairScale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.repair));
            }
            base.Draw(gameTime);
        }
    }
}
