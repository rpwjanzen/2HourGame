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

using _2HourGame.Factories;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using _2HourGame.Model;
using _2HourGame.Model.GameServices;

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
            float width = graphics.PreferredBackBufferWidth;
            float height = graphics.PreferredBackBufferHeight;

			spriteBatch = new SpriteBatch(this.GraphicsDevice);
            PhysicsComponent physicsComponent = new PhysicsComponent(this, new PhysicsSimulator(Vector2.Zero));
            this.Components.Add(physicsComponent);

            EffectManager effectManager = new EffectManager(this, spriteBatch);
            this.Components.Add(effectManager);

            TextureManager textureManager = new TextureManager(this);
            this.Components.Add(textureManager);
            CollisionCategoryManager collisionCategoryManager = new CollisionCategoryManager(this); 

			CannonBallManager cannonBallManager = new CannonBallManager(this, spriteBatch);
            this.Components.Add(cannonBallManager);

            var worldBorder = new WorldBorder(this, new Rectangle(0, 0, (int)width, (int)height), physicsComponent.PhysicsSimulator);

            var playerColors = new[] {
                Color.Blue,
                Color.Red,
                Color.Green,
                Color.Yellow
            }.ToList();

            var islandPositions = new[] {
                new Vector2(width / 4 - 100, height / 4 - 50),
                new Vector2((width / 4) * 3 + 100, (height / 4) - 50),
                new Vector2((width / 4) - 100, (height / 4) * 3 + 50),
                new Vector2((width / 4) * 3 + 100, (height / 4) * 3 + 50)
            }.ToList();
            
            // maybe it's being re-evaluated each time and we are getting a bunch of extra objects
            var islandBuildingOffset = new Vector2(20, 20);
            var islandBuildings = new HouseFactory(this, spriteBatch).CreateHouses(playerColors, islandPositions.Select(i => i + islandBuildingOffset).ToList());

            IslandFactory islandFactory = new IslandFactory(this, spriteBatch);
            var playerIslands = islandFactory.CreatePlayerIslands(islandPositions);

            var goldIslands = new List<Island>();
            goldIslands.Add(islandFactory.CreateIsland(new Vector2(width / 2, height / 2), 16));

            List<Island> allIslands = new List<Island>(playerIslands.ToArray());
            allIslands.AddRange(goldIslands);
            IslandsGoldView islandGoldView = new IslandsGoldView(this, allIslands, spriteBatch);
            this.Components.Add(islandGoldView);

            var playerPositions = new[] {
                new Vector2((width / 4), (height / 4) + 50),
                new Vector2((width / 4) * 3, (height / 4) + 50),
                new Vector2((width / 4), (height / 4) * 3 - 50),
                new Vector2((width / 4) * 3, (height / 4) * 3 - 50)
            }.ToList();

            var playerAngles = new[] {
                (float)(Math.PI * 0.75),
                (float)(Math.PI * 1.25),
                (float)(Math.PI * 0.25),
                (float)(Math.PI * 1.75)
            }.ToList();

            var ships = new ShipFactory(this, spriteBatch, cannonBallManager)
                .CreatePlayerShips(playerColors, playerPositions, playerIslands, playerAngles);

            var shipGoldViewFactory = new ShipGoldViewFactory(this, spriteBatch, 100);
            var playerGoldViews = ships.Zip(new[] {
                ShipGoldView.GoldViewPosition.UpperLeft,
                ShipGoldView.GoldViewPosition.UpperRight, 
                ShipGoldView.GoldViewPosition.LowerLeft, 
                ShipGoldView.GoldViewPosition.LowerRight
            }, (s, p) => shipGoldViewFactory.CreateShipGoldView(s, p)).ToList();
            foreach (var v in playerGoldViews) {
                this.Components.Add(v);
            }

            var tower = new TowerFactory(this, cannonBallManager, spriteBatch).CreateTower(new Vector2(width / 2, height / 2), ships.Cast<IGameObject>().ToList<IGameObject>());

            var map = new Map(allIslands);

            var players = new PlayerFactory(map).CreatePlayers(new[] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four }, ships, playerIslands);

            var shipActionViews = new ShipActionViewFactory(this, spriteBatch).CreateShipActionsViews(players).ToList();
            this.Components.AddRange(shipActionViews);

            var shipControllers = new ShipControllerFactory(this).CreateShipControllers(players).ToList();
            this.Components.AddRange(shipControllers);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
