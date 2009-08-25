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

namespace _2HourGame {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PlayerViewManager playerViewManager;
        Texture2D alphaMask;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            //graphics.PreferredBackBufferFormat = SurfaceFormat.Depth24Stencil8;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
        }

        protected override void Initialize()
        {
            alphaMask = Content.Load<Texture2D>(@"Content\16pxSquareAlphaMask");

            float width = graphics.PreferredBackBufferWidth;
            float height = graphics.PreferredBackBufferHeight;

			spriteBatch = new SpriteBatch(this.GraphicsDevice);
            PhysicsSimulator physicsSimulator = new PhysicsSimulator(Vector2.Zero);
            PhysicsComponent physicsComponent = new PhysicsComponent(this, physicsSimulator);
            physicsComponent.Debug = false;
            this.Components.Add(physicsComponent);

            EffectManager effectManager = new EffectManager(this, spriteBatch);
            TextureManager textureManager = new TextureManager(this);

			CannonBallManager cannonBallManager = new CannonBallManager(this, spriteBatch, physicsSimulator);
            this.Components.Add(cannonBallManager);

            //var worldBorder = new WorldBorder(new Rectangle(0, 0, (int)width, (int)height), physicsSimulator);

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
            //foreach (var v in islandBuildings) {
            //    this.Components.Add(v);
            //}

            IslandFactory islandFactory = new IslandFactory(this, spriteBatch, physicsSimulator);
            var playerIslands = islandFactory.CreatePlayerIslands(islandPositions, islandBuildings);
            //foreach (var v in playerIslands) {
            //    this.Components.Add(v);
            //}

            var goldIslands = new List<Island>();
            goldIslands.Add(islandFactory.CreateIsland(new Vector2(width / 2, height / 2), null, 16));
            //foreach (var v in goldIslands) {
            //    this.Components.Add(v);
            //}

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

            var ships = new ShipFactory(this, spriteBatch, physicsSimulator, cannonBallManager).CreatePlayerShips(playerColors, playerPositions, playerIslands, playerAngles);
            //foreach (var v in ships) {
            //    this.Components.Add(v);
            //}

            var shipGoldViewFactory = new ShipGoldViewFactory(this, spriteBatch, 100);
            var playerGoldViews = ships.Zip(new[] {
                ShipGoldView.GoldViewPosition.UpperLeft,
                ShipGoldView.GoldViewPosition.UpperRight, 
                ShipGoldView.GoldViewPosition.LowerLeft, 
                ShipGoldView.GoldViewPosition.LowerRight
            }, (s, p) => shipGoldViewFactory.CreateShipGoldView(s, p)).ToList();
            //foreach (var v in playerGoldViews) {
            //    this.Components.Add(v);
            //}

            var shipMovers = new ShipMoverFactory(this, playerIslands.Concat(goldIslands)).CreateShipMovers(ships, new[] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four });
            
            for(int i = 0; i < shipMovers.Count; i++) {
                this.Components.Add(shipMovers[i]);
            }

            var playerPositionEnums = new[] {
                PlayerViewManager.PlayerView.One,
                PlayerViewManager.PlayerView.Two,
                PlayerViewManager.PlayerView.Three,
                PlayerViewManager.PlayerView.Four
            }.ToList();

            playerViewManager = new PlayerViewManager(ships, playerPositionEnums);
 
            //var targetShip = ships[0];
            //var controlledShip = ships[3];
            //var islands = playerIslands.Concat(goldIslands);
            //AIController playerFourController = new AIController(this, controlledShip, controlledShip.HomeIsland, islands.Where(i => i != controlledShip.HomeIsland).ToList(), targetShip);
            //this.Components.Add(playerFourController);

            base.Initialize();
        }

        protected override void Draw(GameTime gameTime) {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            // setup for alpha masks
            GraphicsDevice.RenderState.StencilEnable = true;
            GraphicsDevice.RenderState.StencilPass = StencilOperation.Replace;
            GraphicsDevice.RenderState.ReferenceStencil = 1;

            playerViewManager.prepareToDraw();
            PlayerViewManager.PlayerView playerView = PlayerViewManager.PlayerView.One;
            //foreach (PlayerViewManager.PlayerView playerView in playerViewManager)
            //{
                // Draw Alpha Map
                // we use a different reference stencil for each alpha map so that they dont apply cumulatively
                GraphicsDevice.RenderState.ReferenceStencil++;
                // we turn off the color write channels so that the alpha map image is not actually drawn
                // GraphicsDevice.RenderState.ColorWriteChannels = ColorWriteChannels.None;
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None);
                GraphicsDevice.RenderState.StencilFunction = CompareFunction.Always;
                foreach (MapSquare mapSquare in playerViewManager.getPlayerMapSquares(playerView))
                {
                    spriteBatch.Draw(alphaMask, mapSquare.origin, Color.White);
                }
                spriteBatch.End();

                // Draw Graphics For A Single Player
                //GraphicsDevice.RenderState.ColorWriteChannels = ColorWriteChannels.All;
                spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.BackToFront, SaveStateMode.None, Matrix.CreateTranslation(new Vector3(playerViewManager.drawOffset(playerView), 0)));
                GraphicsDevice.RenderState.StencilFunction = CompareFunction.Equal;
                base.Draw(gameTime);
                spriteBatch.End();
            //}
        }
    }
}
