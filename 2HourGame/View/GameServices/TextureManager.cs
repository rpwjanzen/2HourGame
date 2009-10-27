using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace _2HourGame.View.GameServices
{
    class TextureManager
    {
        Dictionary<string, Texture2D> loadedTextures;

        public Texture2D this[string index] {
            get { return loadedTextures[index]; }
        }

        public TextureManager()
        {
            loadedTextures = new Dictionary<string, Texture2D>();
        }

        public void LoadContent(ContentManager content) {
            LoadTextures(content);
        }

        /// <summary>
        /// If this list is incomplete the texture will automatically be loaded when it is requested, but the ones in the list are loaded when the game starts up.
        /// </summary>
        private void LoadTextures(ContentManager content)
        {
            LoadTexture("boatHitByCannonAnimation", content);
            LoadTexture("boundingCircle", content);
            LoadTexture("cannonAnimation", content);
            LoadTexture("cannonBall", content);
            LoadTexture("cannonSmokeAnimation", content);
            LoadTexture("ControllerImages\\xboxControllerButtonA", content);
            LoadTexture("ControllerImages\\xboxControllerButtonB", content);
            LoadTexture("dig", content);
            LoadTexture("floatingCrate", content);
            LoadTexture("gold", content);
            LoadTexture("goldGetAnimation", content);
            LoadTexture("goldLoseAnimation", content);
            LoadTexture("healthBar", content);
            LoadTexture("house", content);
            LoadTexture("island", content);
            LoadTexture("progressBar", content);
            LoadTexture("repair", content);
            LoadTexture("shipGunwale", content);
            LoadTexture("shipHull", content);
            LoadTexture("shipRigging", content);
            LoadTexture("shipRiggingSingleMast", content);
            LoadTexture("shipSinking", content);
            LoadTexture("splashAnimation", content);
            LoadTexture("tower", content);
        }

        private void LoadTexture(string textureName, ContentManager content) 
        {
            loadedTextures.Add(textureName, content.Load<Texture2D>(@"Content\" + textureName));
        }
    }
}
