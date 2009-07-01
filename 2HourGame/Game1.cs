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

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";

        }

        protected override void Initialize() {
            Ship playerOneShip = new Ship(this);
            playerOneShip.Position = new Vector2((1280 / 4), (720 / 4) + 100);
            this.Components.Add(playerOneShip);

            Island playerOneIsland = new Island(this);
            playerOneIsland.Position = new Vector2(1280 / 4 - 100, 720 / 4);
            this.Components.Add(playerOneIsland);

            ShipMover playerOneShipMover = new ShipMover(this, playerOneShip, PlayerIndex.One);
            this.Components.Add(playerOneShipMover);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

    }
}
