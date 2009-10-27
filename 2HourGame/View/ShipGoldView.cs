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
    /// <summary>
    /// Draws gold on the corner of the screen so that we know how much gold is on the ship.
    /// TODO: update when ship gold capacity changes.
    /// </summary>
    class ShipGoldView : ActorView
    {
        public enum GoldViewPosition { UpperLeft, UpperRight, LowerLeft, LowerRight }
        Texture2D texture;

        float displayWidth;

        float scale;

        Ship ship;

        // where to display the gold
        List<Vector2> GoldPositions { get; set; }

        // where is this ships goldview
        GoldViewPosition DisplayCorner { get; set; }

        public ShipGoldView(World world, Ship ship, GoldViewPosition position, float displayWidth, TextureManager textureManager, AnimationManager am)
            : base(ship, world, textureManager, am)
        {
            this.ship = ship;
            this.displayWidth = displayWidth;
            scale = 1f;
            this.DisplayCorner = position;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = TextureManager["gold"];
            this.GoldPositions = CalculateGoldCoinPositions(displayWidth, this.DisplayCorner);
            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (ship.Gold != GoldPositions.Count) {
                this.GoldPositions = CalculateGoldCoinPositions(displayWidth, this.DisplayCorner);
            }

            for (int i = 0; i < GoldPositions.Count; i++)
            {
                Color drawColor = i < ship.Gold ? Color.White : Color.DarkGray;

                spriteBatch.Draw(texture, GoldPositions[i], null, drawColor, 0, Vector2.Zero, scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGoldView) + (0.001f * i));
            }

            base.Draw(gameTime, spriteBatch);
        }

        private List<Vector2> CalculateGoldCoinPositions(float displayWidth, GoldViewPosition displayCorner)
        {
            Vector2 position;

            switch (displayCorner) {
                case GoldViewPosition.UpperLeft:
                    position = new Vector2(
                        10,
                        10);
                    break;
                case GoldViewPosition.UpperRight:
                    position = new Vector2(
                        1280 - displayWidth - 10,
                        10);
                    break;
                case GoldViewPosition.LowerLeft:
                    position = new Vector2(
                        10,
                        720 - texture.Height - 10);
                    break;
                case GoldViewPosition.LowerRight:
                    position = new Vector2(
                        1280 - displayWidth - 10,
                        720 - texture.Height - 10);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("displayCorner", "Unknown gold position");
            }

            float increment = displayWidth / ship.GoldCapacity;

            var goldPositions = new List<Vector2>();
            for (int i = 0; i < ship.GoldCapacity; i++)
            {
                var offset = new Vector2(i * increment, 0);
                goldPositions.Add(position + offset);
            }

            return goldPositions;
        }
    }
}
