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
        Texture2D texture;
        Ship ship;
        Vector2 offset;
        Vector2 origin;
        float zIndex;

        public HealthBarView(Game game, SpriteBatch spriteBatch, Ship ship)
            :base(game)
        {
            this.ship = ship;
            this.spriteBatch = spriteBatch;
            offset = new Vector2(0, 35);
            origin = ((ITextureManager)game.Services.GetService(typeof(ITextureManager))).getTextureOrigin("healthBar", ship.Scale); 
            texture = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("healthBar");
            zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.healthBar);
        }

        public override void Draw(GameTime gameTime)
        {
            // reference http://www.xnadevelopment.com/tutorials/notsohealthy/NotSoHealthy.shtml
            //Draw the negative space for the health bar
            //spriteBatch.Draw(texture, new Rectangle(base.game.Window.ClientBounds.Width / 2 - texture.Width / 2, 30, texture.Width, 44), new Rectangle(0, 45, texture.Width, 44), Color.Red);

            spriteBatch.Draw(texture, ship.Position + offset, null, Color.Red, 0f, origin, ship.Scale, SpriteEffects.None, zIndex + 0.001f);

            spriteBatch.Draw(texture, ship.Position + offset, new Rectangle(0, 0, (int)(texture.Width * (ship.health / ship.maxHealth)), texture.Height), Color.Green, 0f, origin, ship.Scale, SpriteEffects.None, zIndex);

            //Draw the current health level based on the current Health
            //spriteBatch.Draw(texture, new Rectangle(this.Window.ClientBounds.Width / 2 - texture.Width / 2, 30, (int)(texture.Width * ((double)mCurrentHealth / 100)), 44),
                 //new Rectangle(0, 45, texture.Width, 44), Color.Green);
        }
    }
}
