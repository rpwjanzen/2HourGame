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
        SpriteBatch spriteBatch;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize() {

            List<Island> islands = new List<Island>();
            List<Ship> ships = new List<Ship>();

			spriteBatch = new SpriteBatch(this.GraphicsDevice);
            PhysicsSimulator physicsSimulator = new PhysicsSimulator(Vector2.Zero);

            EffectManager effectManager = new EffectManager(this, spriteBatch);
            TextureManager textureManager = new TextureManager(this);

			CannonBallManager cannonBallManager = new CannonBallManager(this, spriteBatch, physicsSimulator);
            this.Components.Add(cannonBallManager);
            //CannonBallManagerView cannonBallManagerView = new CannonBallManagerView(this, cannonBallManager, spriteBatch, 0.001f);
            //this.Components.Add(cannonBallManagerView);

            PhysicsComponent physicsComponent = new PhysicsComponent(this, physicsSimulator);
            physicsComponent.Debug = true;

            var worldBorder = new WorldBorder(new Rectangle(0, 0, 1280, 720), physicsSimulator);

            addPlayer(PlayerIndex.One, Color.Blue, new Vector2(1280 / 4 - 100, 720 / 4), new Vector2((1280 / 4), (720 / 4) + 100), islands, ships, physicsSimulator, cannonBallManager, true, true);
            addPlayer(PlayerIndex.Two, Color.Red, new Vector2((1280 / 4) * 3 + 100, (720 / 4)), new Vector2((1280 / 4) * 3, (720 / 4) + 100), islands, ships, physicsSimulator, cannonBallManager, true, false);
            addPlayer(PlayerIndex.Three, Color.Green, new Vector2((1280 / 4) - 100, (720 / 4) * 3), new Vector2((1280 / 4), (720 / 4) * 3 - 100), islands, ships, physicsSimulator, cannonBallManager, false, true);
            addPlayer(PlayerIndex.Four, Color.Yellow, new Vector2((1280 / 4) * 3 + 100, (720 / 4) * 3), new Vector2((1280 / 4) * 3, (720 / 4) * 3 - 100), islands, ships, physicsSimulator, cannonBallManager, false, false);

            Island goldIsland = new Island(this, new Vector2(1280 / 2, 720 / 2), null, spriteBatch, 11, physicsSimulator);
            this.Components.Add(goldIsland);
            islands.Add(goldIsland);

            this.Components.Add(physicsComponent);

            IslandsGoldView goldView = new IslandsGoldView(this, islands, ships, spriteBatch);
            this.Components.Add(goldView);

            base.Initialize();
        }

        private void addPlayer(PlayerIndex playerIndex, Color color, Vector2 islandLocation, Vector2 shipLocation, List<Island> islands, List<Ship> ships, PhysicsSimulator physicsSimulator, CannonBallManager cannonBallManager, bool top, bool left) 
        {
            GameObject playerOneHouse = new GameObject(this, islandLocation + new Vector2(20, 20), "house", 1f, color, spriteBatch, null, (float)ZIndexManager.drawnItemOrders.house / 100);
            this.Components.Add(playerOneHouse);

            Island playerOneIsland = new Island(this, islandLocation, playerOneHouse, spriteBatch, 0, physicsSimulator);
            this.Components.Add(playerOneIsland);
            islands.Add(playerOneIsland);

            Ship playerOneShip = new Ship(this, color, shipLocation, spriteBatch, physicsSimulator, playerOneIsland, cannonBallManager);
            this.Components.Add(playerOneShip);
            ships.Add(playerOneShip);

            ShipGoldView playerOneShipGoldView = new ShipGoldView(this, playerOneShip, top, left, spriteBatch, 100);
            this.Components.Add(playerOneShipGoldView);

            ShipMover playerOneShipMover = new ShipMover(this, playerOneShip, playerIndex, islands);
            this.Components.Add(playerOneShipMover);
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
