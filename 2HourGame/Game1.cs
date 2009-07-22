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
using FarseerGames.FarseerPhysics;

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
        }

        protected override void Initialize() {
            PhysicsSimulator physicsSimulator = new PhysicsSimulator(Vector2.Zero);

            PhysicsComponent physicsComponent = new PhysicsComponent(this, physicsSimulator);
            physicsComponent.Debug = true;

            var worldBorder = new WorldBorder(new Rectangle(0, 0, 1280, 720), physicsSimulator);

            Ship playerOneShip = new Ship(this, new Vector2((1280 / 4), (720 / 4) + 100), physicsSimulator);
            this.Components.Add(playerOneShip);

            Island playerOneIsland = new Island(this, new Vector2(1280 / 4 - 100, 720 / 4), physicsSimulator);
            this.Components.Add(playerOneIsland);

            ShipMover playerOneShipMover = new ShipMover(this, playerOneShip, PlayerIndex.One);
            this.Components.Add(playerOneShipMover);

            this.Components.Add(physicsComponent);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
