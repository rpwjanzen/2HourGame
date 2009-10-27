using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View.GameServices;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class IslandGoldView : ActorView
    {
        Island island;
        Texture2D texture;

        Vector2 goldIslandOffset;

        Vector2 origin;

        float scale;

        public IslandGoldView(World world, Island island, TextureManager tm, AnimationManager am)
            : base(island, world, tm, am)
        {
            this.island = island;
            scale = 0.3f;
            goldIslandOffset = new Vector2(17, 17);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = TextureManager["gold"];
            origin = new Vector2(texture.Width / 2, texture.Height / 2);

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < island.Gold && i < 10; i++) 
            {
                spriteBatch.Draw(texture, island.Position + goldIslandOffset + goldLocation(i), null, Color.White, 0, origin, scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.islandGoldView));
            }
            
            base.Draw(gameTime, spriteBatch);
        }

        private Vector2 goldLocation(int goldPieceNumber) 
        {
            switch (goldPieceNumber)
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
