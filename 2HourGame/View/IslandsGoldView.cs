using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View.GameServices;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class IslandsGoldView : DrawableGameComponent
    {
        IEnumerable<Island> islands;
        IEnumerable<Ship> ships;

        SpriteBatch spriteBatch;
        Texture2D texture;

        Vector2 goldIslandOffset;

        Vector2 origin;

        float scale;

        public IslandsGoldView(Game game, IEnumerable<Island> islands, IEnumerable<Ship> ships, SpriteBatch spriteBatch)
            : base(game)
        {
            this.islands = islands;
            this.ships = ships;
            this.spriteBatch = spriteBatch;
            scale = 0.3f;
            goldIslandOffset = new Vector2(17, 17);
        }

        protected override void LoadContent()
        {
            texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager))).getTexture("gold");
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Island island in islands) 
            {
                for (int i = 0; i < island.Gold && i < 10; i++) 
                {
                    spriteBatch.Draw(texture, island.Position + goldIslandOffset + goldLocation(i), null, Color.White, 0, origin, scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.islandGoldView));
                }
            }
            
            //spriteBatch.Draw(texture, Position, null, Color.White, 0, origin, 1.0f, SpriteEffects.None, zIndex);
            
            base.Draw(gameTime);
        }

        private Vector2 goldLocation(int goldPeiceNumber) 
        {
            switch (goldPeiceNumber)
            {
                case 0:
                    return new Vector2(0, -6);
                case 1:
                    return new Vector2(-4, -13);
                case 2:
                    return new Vector2(5, -4);
                case 3:
                    return new Vector2(8,-11);
                case 4:
                    return new Vector2(7, 7);
                case 5:
                    return new Vector2(-8, -5);
                case 6:
                    return new Vector2(-10, 6);
                case 7:
                    return new Vector2(13, 0);
                case 8:
                    return new Vector2(-9, -7);
                case 9:
                    return new Vector2(-2, 4);
                default:
                    return Vector2.Zero;
            }
        }
    }
}
