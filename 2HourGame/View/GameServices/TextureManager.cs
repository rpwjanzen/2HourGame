using System;
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
        bool initialized;

        public Texture2D this[string index] {
            get { return loadedTextures[index]; }
        }

        public TextureManager(Game game) : base(game)
        {
            initialized = false;
            loadedTextures = new Dictionary<string, Texture2D>();
            game.Services.AddService(typeof(ITextureManager), this);
        }

        public override void Initialize()
        {
            if (Game.GraphicsDevice != null)
            {
                this.LoadTextures();
                initialized = true;
            }
            base.Initialize();
        }

        private void LoadTextures()
        {
            LoadTexture("boatHitByCannonAnimation");
            LoadTexture("boundingCircle");
            LoadTexture("cannonAnimation");
            LoadTexture("cannonBall");
            LoadTexture("cannonSmokeAnimation");
            LoadTexture("ControllerImages\\xboxControllerButtonA");
            LoadTexture("ControllerImages\\xboxControllerButtonB");
            LoadTexture("dig");
            LoadTexture("repair");
            LoadTexture("floatingCrate");
            LoadTexture("gold");
            LoadTexture("goldGetAnimation");
            LoadTexture("goldLoseAnimation");
            LoadTexture("healthBar");
            LoadTexture("house");
            LoadTexture("island");
            LoadTexture("shipGunwale");
            LoadTexture("shipHull");
            LoadTexture("shipRigging");
            LoadTexture("shipSinking");
            LoadTexture("splashAnimation");
            LoadTexture("tower");
            LoadTexture("progressBar");
        }

        private void LoadTexture(string textureName) 
        {
            loadedTextures.Add(textureName, Game.Content.Load<Texture2D>(@"Content\" + textureName));
        }
    }
}
