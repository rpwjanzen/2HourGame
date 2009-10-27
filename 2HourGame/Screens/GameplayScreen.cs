#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

using _2HourGame.Model;
using FarseerGames.FarseerPhysics;
using _2HourGame;
using _2HourGame.View.GameServices;
using _2HourGame.Factories;
using System.Collections.Generic;
using _2HourGame.View;
using _2HourGame.Controller;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;
        PhysicsWorld world;
        AnimationManager animationManager;
        TextureManager textureManager;
        List<ShipController> shipControllers;
        // XXX : What a hack!
        GameTime updateTime;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(5.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            gameFont = content.Load<SpriteFont>(@"Fonts/gamefont");

            CreateNewGame();

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if (IsActive)
            {
                world.Update(gameTime);
                updateTime = gameTime;
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                foreach (var sc in shipControllers) {
                    // XXX : What a hack!
                    sc.Update(updateTime);
                }
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            spriteBatch.Begin();
            world.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        #endregion

        void CreateNewGame() {
            float width = ScreenManager.GraphicsDevice.Viewport.Width;
            float height = ScreenManager.GraphicsDevice.Viewport.Height;

            PhysicsSimulator physicsSimulator = new PhysicsSimulator(Vector2.Zero);
            world = new PhysicsWorld(physicsSimulator);

            textureManager = new TextureManager();
            animationManager = new AnimationManager(world, textureManager);

            var worldBorder = new WorldBorder(new Rectangle(0, 0, (int)width, (int)height), physicsSimulator);

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
            var islandBuildings = new HouseFactory(world, textureManager, animationManager).CreateHouses(playerColors, islandPositions.Select(i => i + islandBuildingOffset).ToList());

            var islandFactory = new IslandFactory(world, textureManager, animationManager);
            var playerIslands = islandFactory.CreatePlayerIslands(islandPositions);

            var goldIslands = new List<Island>();
            goldIslands.Add(islandFactory.CreateIsland(new Vector2(width / 2, height / 2), 16));

            var allIslands = new List<Island>(playerIslands.ToArray());
            allIslands.AddRange(goldIslands);
            foreach (var island in allIslands) {
                var islandGoldView = new IslandGoldView(world, island, textureManager, animationManager);
                world.ActorViews.Add(islandGoldView);
            }

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

            var ships = new ShipFactory(world, textureManager, animationManager)
                .CreatePlayerShips(playerColors, playerPositions, playerIslands, playerAngles);

            var shipGoldViewFactory = new ShipGoldViewFactory(world, 100, textureManager, animationManager);
            var playerGoldViews = ships.Zip(new[] {
                ShipGoldView.GoldViewPosition.UpperLeft,
                ShipGoldView.GoldViewPosition.UpperRight,
                ShipGoldView.GoldViewPosition.LowerLeft,
                ShipGoldView.GoldViewPosition.LowerRight
            }, (s, p) => shipGoldViewFactory.CreateShipGoldView(s, p)).ToList();
            foreach (var v in playerGoldViews) {
                world.ActorViews.Add(v);
            }

            var tower = new TowerFactory(world, textureManager, animationManager).CreateTower(new Vector2(width / 2, height / 2), ships.Cast<GameObject>().ToList<GameObject>());

            var map = new Map(allIslands);

            var players = new PlayerFactory(map).CreatePlayers(new[] { PlayerIndex.One, PlayerIndex.Two, PlayerIndex.Three, PlayerIndex.Four }, ships, playerIslands);

            var shipActionViews = new ShipActionViewFactory(world, textureManager, animationManager).CreateShipActionsViews(players).ToList();
            foreach(var av in shipActionViews) {
                world.ActorViews.Add(av);
            }

            shipControllers = new ShipControllerFactory(world, textureManager, animationManager).CreateShipControllers(players).ToList();
        }
    }
}
