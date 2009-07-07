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
        List<Object> collidingObjects = new List<Object>();

        SpriteBatch spriteBatch;
        Texture2D boundingTexture;
        Vector2 origin;

        public CollisionDetector(Game game, IEnumerable<Island> islands, IEnumerable<Ship> ships)
            : base(game)
        {
            this.islands = islands;
            this.ships = ships;
        }

        bool Collides(Island island, Ship ship) {
            return island.Bounds.Intersects(ship.Bounds);
        }

        bool Collides(Ship a, Ship b) {
            return a.Bounds.Intersects(b.Bounds);
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            boundingTexture = this.Game.Content.Load<Texture2D>("boundingCircle");
            origin = new Vector2(boundingTexture.Width / 2, boundingTexture.Height / 2);
            base.LoadContent();
        }
        /// <summary>
        /// Figure out what all the colliding objects are and make a list of them.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            collidingObjects.Clear();

            //// ship <-> ship collisions
            //foreach (Ship a in ships) {
            //    foreach (Ship b in ships) {
            //        if (a != b) {
            //            if (this.Collides(a, b)) {
            //                if (!collidingObjects.Contains(a)) {
            //                    collidingObjects.Add(a);
            //                }
            //                if (!collidingObjects.Contains(b)) {
            //                    collidingObjects.Add(b);
            //                }
            //            }
            //        }
            //    }
            //}

            // ship <-> ship collisions
            // this will prevent from checking the same collision twice
            for (int i = 0; i < ships.Count() - 1; i++) 
            {
                for (int j = i + 1; j < ships.Count(); j++) 
                {
                    Ship a = ships.ElementAt(i);
                    Ship b = ships.ElementAt(j);
                    if (this.Collides(a, b)) 
                    {
                        if (!collidingObjects.Contains(a))
                        {
                            collidingObjects.Add(a);
                        }
                        if (!collidingObjects.Contains(b))
                        {
                            collidingObjects.Add(b);
                        }
                    }
                }
            }

            // island <-> ship collisions
            foreach (Island i in islands)
            {
                foreach (Ship s in ships)
                {
                    if (this.Collides(i, s))
                    {
                        if (!collidingObjects.Contains(i))
                        {
                            collidingObjects.Add(i);
                        }
                        if (!collidingObjects.Contains(s))
                        {
                            collidingObjects.Add(s);
                        }

                        // I threw this in here for now, could be refactored
                        handleShipIslandCollision(s, i);
                    }
                }
            }

            base.Update(gameTime);
        }

        private void handleShipIslandCollision(Ship s, Island i)
        {
            s.Offset(s.Bounds.DifferenceVector(i.Bounds));
        }

        private void handleShipShipCollision(Ship s, Ship s2)
        {
            Vector2 sOffset = s.Bounds.DifferenceVector(s2.Bounds) / 2;
            Vector2 s2Offset = sOffset;
            s2Offset.X = -s2Offset.X;
            s2Offset.Y = -s2Offset.Y;
        }

        /// <summary>
        /// Draw the collision spheres around the game objects. Red if they are colliding.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin();
            foreach (Island i in islands) {
                float scale = i.Bounds.Radius / (boundingTexture.Width / 2);
                Color color = (collidingObjects.Contains(i)) ? Color.Red : Color.White;
                spriteBatch.Draw(boundingTexture, i.Position, null, color, 0.0f, origin, scale, SpriteEffects.None, 0);
            }

            foreach (Ship s in ships) {
                float scale = s.Bounds.Radius / (boundingTexture.Width / 2);
                Color color = (collidingObjects.Contains(s)) ? Color.Red : Color.White;
                spriteBatch.Draw(boundingTexture, s.Position, null, color, 0.0f, origin, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
