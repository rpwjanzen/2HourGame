using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace _2HourGame {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";

        }

        protected override void Initialize() {
            spriteBatch = new SpriteBatch(this.GraphicsDevice);

            CannonBallManager cannonBallManager = new CannonBallManager(this, 0.001f, spriteBatch);

            Ship playerOneShip = new Ship(this, new Vector2((1280 / 4), (720 / 4) + 100), spriteBatch);
            playerOneShip.zIndex = 0f;
            this.Components.Add(playerOneShip);

            GameObject playerOneHouse = new GameObject(this, new Vector2(1280 / 4 - 80, 720 / 4 + 20), "house", 1f, Color.Blue, spriteBatch);
            playerOneHouse.zIndex = 0.1f;
            this.Components.Add(playerOneHouse);

            Island playerOneIsland = new Island(this, new Vector2(1280 / 4 - 100, 720 / 4), playerOneHouse, spriteBatch, 0, playerOneShip);
            playerOneIsland.zIndex = 0.2f;
            this.Components.Add(playerOneIsland);

            Island goldIsland = new Island(this, new Vector2(1280 / 2, 720 / 2), null, spriteBatch, 11, null);
            goldIsland.zIndex = 0.2f;
            this.Components.Add(goldIsland);

            ShipMover playerOneShipMover = new ShipMover(this, playerOneShip, PlayerIndex.One, new[] { playerOneIsland, goldIsland });
            this.Components.Add(playerOneShipMover);

            CollisionDetector collisionDetector = new CollisionDetector(this, new[] { playerOneIsland, goldIsland }, new[] { playerOneShip });
            this.Components.Add(collisionDetector);

            GoldView goldView = new GoldView(this, new[] { playerOneIsland, goldIsland }, new[] { playerOneShip }, spriteBatch, 0.01f);
            this.Components.Add(goldView);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

    }
}
