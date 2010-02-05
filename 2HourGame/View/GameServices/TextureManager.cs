﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View.GameServices
{
    class TextureManager : GameComponent, ITextureManager
    {
        Dictionary<string, Texture2D> loadedTextures;

        public Texture2D this[string index] {
            get { return loadedTextures[index]; }
        }

        public TextureManager(Game game) : base(game)
        {
            loadedTextures = new Dictionary<string, Texture2D>();
            game.Services.AddService(typeof(ITextureManager), this);
        }

        public override void Initialize()
        {
            this.LoadTextures();
            base.Initialize();
        }

        /// <summary>
        /// If this list is incomplete the texture will automatically be loaded when it is requested, but the ones in the list are loaded when the game starts up.
        /// </summary>
        private void LoadTextures()
        {
            LoadTexture("ShipImages\\boatHitByCannonAnimation");
            LoadTexture("boundingCircle");
            LoadTexture("cannonAnimation");
            LoadTexture("cannonBall");
            LoadTexture("cannonSmokeAnimation");
            LoadTexture("ControllerImages\\xboxControllerButtonA");
            LoadTexture("ControllerImages\\xboxControllerButtonB");
            LoadTexture("dig");
            LoadTexture("floatingCrate");
            LoadTexture("gold");
            LoadTexture("goldGetAnimation");
            LoadTexture("goldLoseAnimation");
            LoadTexture("healthBar");
            LoadTexture("house");
            LoadTexture("island");
            LoadTexture("progressBar");
            LoadTexture("repair");
            LoadTexture("ShipImages\\shipGunwale");
            LoadTexture("ShipImages\\shipHull");
            LoadTexture("ShipImages\\shipRigging");
            LoadTexture("ShipImages\\shipRiggingSingleMast");
            LoadTexture("ShipImages\\shipSinking");
            LoadTexture("splashAnimation");
            LoadTexture("tower");
        }

        private void LoadTexture(string textureName) 
        {
            loadedTextures.Add(textureName, Game.Content.Load<Texture2D>(@"Content\" + textureName));
        }
    }
}
