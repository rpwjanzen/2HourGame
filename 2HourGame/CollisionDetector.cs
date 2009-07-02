using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame {
    class CollisionDetector : DrawableGameComponent {
        IEnumerable<Island> islands;
        IEnumerable<Ship> ships;

        SpriteBatch spriteBatch;
        Texture2D boundingTexture;
        Vector2 origin;

        public CollisionDetector(Game game, IEnumerable<Island> islands, IEnumerable<Ship> ships) : base(game) {
            this.islands = islands;
            this.ships = ships;            
        }

        public bool Collides(Island island, Ship ship) {
            return island.Bounds.Intersects(ship.Bounds);
        }

        public bool Collides(Ship a, Ship b) {
            return a.Bounds.Intersects(b.Bounds);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            boundingTexture = this.Game.Content.Load<Texture2D>("boundingCircle");
            origin = new Vector2(boundingTexture.Width / 2, boundingTexture.Height / 2);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            foreach (Island i in islands) {
                float scale = i.Bounds.Radius / (boundingTexture.Width / 2);
                spriteBatch.Draw(boundingTexture, i.Position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0);
            }

            foreach (Ship s in ships) {
                float scale = s.Bounds.Radius / (boundingTexture.Width / 2);
                spriteBatch.Draw(boundingTexture, s.Position, null, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
