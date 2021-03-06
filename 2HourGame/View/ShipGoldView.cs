﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View.GameServices;
using _2HourGame.Model;

namespace _2HourGame.View
{
    /// <summary>
    /// Draws gold on the corner of the screen so that we know how much gold is on the ship.
    /// TODO: update when ship gold capacity changes.
    /// </summary>
    class ShipGoldView : DrawableGameComponent
    {
        public enum GoldViewPosition { UpperLeft, UpperRight, LowerLeft, LowerRight }
        SpriteBatch spriteBatch;
        Texture2D texture;

        float displayWidth;

        float scale;

        IShip ship;

        // where to display the gold
        List<Vector2> GoldPositions { get; set; }

        // where is this ships goldview
        GoldViewPosition DisplayCorner { get; set; }

        public ShipGoldView(Game game, IShip ship, GoldViewPosition position , SpriteBatch spriteBatch, float displayWidth)
            : base(game)
        {
            this.ship = ship;
            this.spriteBatch = spriteBatch;
            this.displayWidth = displayWidth;
            scale = 1f;
            this.DisplayCorner = position;
        }

        protected override void LoadContent()
        {
            texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager)))[Content.Gold];
            this.GoldPositions = CalculateGoldCoinPositions(displayWidth, this.DisplayCorner);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if (ship.GoldCapacity != GoldPositions.Count) {
                this.GoldPositions = CalculateGoldCoinPositions(displayWidth, this.DisplayCorner);
            }

            for (int i = 0; i < GoldPositions.Count; i++)
            {
                Color drawColor = i < ship.Gold ? Color.White : Color.DarkGray;

                spriteBatch.Draw(texture, GoldPositions[i], null, drawColor, 0, Vector2.Zero, scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGoldView) + (0.001f * i));
            }

            base.Draw(gameTime);
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
