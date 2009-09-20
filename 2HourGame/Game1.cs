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

namespace _2HourGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Ship> ships;
        Viewport[] viewports;
        Viewport screenViewport;
        List<ShipGoldView> shipGoldViews;
        Border border;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            int screenWidth = graphics.PreferredBackBufferWidth;
            int screenHeight = graphics.PreferredBackBufferHeight;

            spriteBatch = new SpriteBatch(this.GraphicsDevice);
            CreateGameObjects(screenWidth, screenHeight);
            InitializeViewports();
            shipGoldViews = CreateShipGoldViews();
            border = new Border();

            base.Initialize();
        }

        private void InitializeViewports()
        {
            int screenWidth = graphics.PreferredBackBufferWidth;
            int screenHeight = graphics.PreferredBackBufferHeight;
            screenViewport = GraphicsDevice.Viewport;

            viewports = new Viewport[4];
            var viewportWidth = screenWidth / 2;
            var viewportHeight = screenHeight / 2;
            for (int i = 0; i < viewports.Length; i++)
            {
                viewports[i] = GraphicsDevice.Viewport;
                viewports[i].Width = viewportWidth;
                viewports[i].Height = viewportHeight;
            }
            viewports[1].X = viewportWidth + 1;
            viewports[2].Y = viewportHeight + 1;
            viewports[3].X = viewportWidth + 1;
            viewports[3].Y = viewportHeight + 1;
        }

        protected override void LoadContent()
        {
            LoadShipGoldViewsContent();
            border.LoadContent(this.Content);

            base.LoadContent();
        }

        private void LoadShipGoldViewsContent()
        {
            foreach (var gv in shipGoldViews)
            {
                gv.LoadContent();
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Viewport = screenViewport;
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            for (int i = 0; i < viewports.Length; i++)
            {
                GraphicsDevice.Viewport = viewports[i];
                this.GraphicsDevice.Clear(Color.CornflowerBlue);

                var ship = ships[i];
                // center on ship
                var translateVector = new Vector3(-ship.Position + new Vector2(viewports[i].Width / 2, viewports[i].Height / 2), 0);
                var playerTransformMatrix = Matrix.CreateTranslation(translateVector) * Matrix.CreateScale(0.5f);
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, playerTransformMatrix);
                base.Draw(gameTime);
                spriteBatch.End();

                spriteBatch.Begin();
                border.Draw(spriteBatch);
                spriteBatch.End();
            }

            GraphicsDevice.Viewport = screenViewport;
            spriteBatch.Begin();
            DrawShipGoldViews(gameTime);
            spriteBatch.End();
        }

        void CreateGameObjects(int width, int height)
        {
            PhysicsSimulator physicsSimulator = new PhysicsSimulator(Vector2.Zero);
            PhysicsComponent physicsComponent = new PhysicsComponent(this, physicsSimulator);
            physicsComponent.Debug = false;
            this.Components.Add(physicsComponent);

            EffectManager effectManager = new EffectManager(this, spriteBatch);
            TextureManager textureManager = new TextureManager(this);

            CannonBallManager cannonBallManager = new CannonBallManager(this, spriteBatch, physicsSimulator);
            this.Components.Add(cannonBallManager);

            WorldBorder.AddWorldBorder(new Rectangle(0, 0, (int)width, (int)height), physicsSimulator);

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

            IslandFactory islandFactory = new IslandFactory(this, spriteBatch, physicsSimulator);
            var playerIslands = islandFactory.CreatePlayerIslands(islandPositions, islandBuildings);

            var goldIslands = new List<Island>();
            goldIslands.Add(islandFactory.CreateIsland(new Vector2(width / 2, height / 2), null, 16));

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

            ships = new ShipFactory(this, spriteBatch, physicsSimulator, cannonBallManager).CreatePlayerShips(playerColors, playerPositions, playerIslands, playerAngles);

            var map = new Map(allIslands);

            var players = new PlayerFactory(map).CreatePlayers(new[] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four }, ships, playerIslands);

            var shipActionViews = new ShipActionViewFactory(this, spriteBatch).CreateShipActionsViews(players).ToList();
            for (int i = 0; i < shipActionViews.Count; i++)
            {
                this.Components.Add(shipActionViews[i]);
            }

            var shipControllers = new ShipControllerFactory(this).CreateShipControllers(players).ToList();
            for (int i = 0; i < shipControllers.Count; i++)
            {
                this.Components.Add(shipControllers[i]);
            }

        }

        private List<ShipGoldView> CreateShipGoldViews()
        {
            var shipGoldViewFactory = new ShipGoldViewFactory(this, spriteBatch, 100);
            var playerGoldViews = ships.Zip(new[] {
                ShipGoldView.GoldViewPosition.UpperLeft,
                ShipGoldView.GoldViewPosition.UpperRight, 
                ShipGoldView.GoldViewPosition.LowerLeft, 
                ShipGoldView.GoldViewPosition.LowerRight
            }, (s, p) => shipGoldViewFactory.CreateShipGoldView(s, p)).ToList();
            return playerGoldViews;
        }

        void DrawShipGoldViews(GameTime gameTime)
        {
            foreach (var gv in shipGoldViews)
            {
                gv.Draw(gameTime);
            }
        }

        void DrawGameObjects()
        {
        }

        void UpdateGameObjects()
        {
        }
    }
}
