using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class TextureManager : ITextureManager
    {
        Dictionary<string, Texture2D> textures;

        Game game;

        public TextureManager(Game game) 
        {
            this.game = game;
            textures = new Dictionary<string, Texture2D>();
            game.Services.AddService(typeof(ITextureManager), this);

            LoadContent();
        }

        public void LoadContent()
        {
            addTexture("boat");
            addTexture("boundingCircle");
            addTexture("cannonBall");
            addTexture("cannonSmoke");
            addTexture("gold");
            addTexture("goldGetAnimation");
            addTexture("house");
            addTexture("island");
            addTexture("splash");
        }

        private void addTexture(string textureName) 
        {
            textures.Add(textureName, game.Content.Load<Texture2D>(@"Content\" + textureName));
        }

        public Texture2D getTexture(string textureName) 
        {
            Texture2D texture;
            if (textures.TryGetValue(textureName, out texture))
                return texture;
            else 
            {
                addTexture(textureName);
                textures.TryGetValue(textureName, out texture);
                return texture;
            }
        }
    }
}
