using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2HourGame.View
{
    internal class ShipActionsView : ActorView
    {
        private readonly Vector2 aButtonOffset = new Vector2(-18, -30);
        private readonly Vector2 bButtonOffset = new Vector2(10, -30);
        private readonly Vector2 digOffset = new Vector2(-5, -35);
        private readonly Player player;
        private readonly Vector2 repairOffset = new Vector2(25, -30);
        private float aButtonScale = 0.2f;

        private Texture2D aButtonTexture;
        private string aButtonTextureName = "ControllerImages\\xboxControllerButtonA";
        private float bButtonScale = 0.2f;

        private Texture2D bButtonTexture;
        private string bButtonTextureName = "ControllerImages\\xboxControllerButtonB";
        private float digScale = 0.35f;

        private Texture2D digTexture;
        private string digTextureName = "dig";
        private GoldPickupProgressView goldPickupProgressView;
        private float repairScale = 0.35f;

        private Texture2D repairTexture;
        private string repairTextureName = "repair";

        public ShipActionsView(World world, Player player, TextureManager textureManager, AnimationManager am)
            : base(player.Ship, world, textureManager, am)
        {
            this.player = player;
            goldPickupProgressView = new GoldPickupProgressView(player, world, textureManager, am);
        }

        public override void LoadContent(ContentManager content)
        {
            aButtonTexture = TextureManager[aButtonTextureName];
            bButtonTexture = TextureManager[bButtonTextureName];
            digTexture = TextureManager[digTextureName];
            repairTexture = TextureManager[repairTextureName];

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

                spriteBatch.Draw(aButtonTexture, player.Ship.Position + aButtonOffset, null, drawColor, 0,
                                 aButtonTexture.Center(aButtonScale), aButtonScale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonA));
                spriteBatch.Draw(digTexture, player.Ship.Position + digOffset, null, Color.White, 0,
                                 digTexture.Center(digScale), digScale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.dig));
            }

            // repair icon display
            if (player.ClosestInRangeIsland == player.HomeIsland
                && player.ShipIsMovingSlowly
                && player.Ship.IsDamaged)
            {
                Color drawColor = gamePadState.IsButtonUp(Buttons.B) ? Color.White : Color.DarkGray;

                spriteBatch.Draw(bButtonTexture, player.Ship.Position + bButtonOffset, null, drawColor, 0,
                                 bButtonTexture.Center(bButtonScale), aButtonScale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.xboxControllerButtonB));
                spriteBatch.Draw(repairTexture, player.Ship.Position + repairOffset, null, Color.White, 0,
                                 repairTexture.Center(repairScale), repairScale, SpriteEffects.None,
                                 ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.repair));
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}