using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    /// <summary>
    /// Draws gold on the corner of the screen so that we know how much gold is on the ship.
    /// TODO: update when ship gold capacity changes.
    /// </summary>
    class ShipGoldView : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D texture;

        float displayWidth;

        float scale;

        public float zIndex;

        Ship ship;

        // where to display the gold
        List<Vector2> goldPositions;

        // where is this ships goldview
        bool top;
        bool left;

        public ShipGoldView(Game game, Ship ship, bool top, bool left, SpriteBatch spriteBatch, float zIndex, float displayWidth)
            : base(game)
        {
            this.ship = ship;
            this.spriteBatch = spriteBatch;
            this.zIndex = zIndex;
            this.top = top;
            this.left = left;
            this.displayWidth = displayWidth;
            scale = 1f;

            //setPositions(top, left, displayWidth);
        }

        protected override void LoadContent()
        {
            texture = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager))).getTexture("gold");
            //origin = new Vector2(texture.Width / 2, texture.Height / 2);
            setPositions(displayWidth);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            if(ship.CarriedGold != goldPositions.Count)
                setPositions(displayWidth);

            for (int i = 0; i < goldPositions.Count; i++)
            {
                Color drawColor = i < ship.CarriedGold ? Color.White : Color.DarkGray;

                spriteBatch.Draw(texture, goldPositions[i], null, drawColor, 0, Vector2.Zero, scale, SpriteEffects.None, zIndex + (0.001f * i));
            }

            base.Draw(gameTime);
        }

        private void setPositions(float displayWidth)
        {
            float leftmost = left ? 0 : 1280 - displayWidth;
            float topPosition = top ? 0 : 720 - texture.Height;
            float length = displayWidth;

            float increment = length / ship.GoldCapacity;

            goldPositions = new List<Vector2>();

            for (int i = 0; i < ship.GoldCapacity; i++)
            {
                goldPositions.Add(new Vector2(leftmost + (i * increment), topPosition));
            }
        }
    }
}
