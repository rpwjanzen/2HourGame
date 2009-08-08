using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View.GameServices
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
            addTexture("boatHitByCannonAnimation");
            addTexture("boundingCircle");
            addTexture("cannonAnimation");
            addTexture("cannonBall");
            addTexture("cannonSmokeAnimation");
            addTexture("floatingCrate");
            addTexture("gold");
            addTexture("goldGetAnimation");
            addTexture("goldLoseAnimation");
            addTexture("house");
            addTexture("island");
            addTexture("shipGunwale");
            addTexture("shipHull");
            addTexture("shipRigging");
            addTexture("shipSinking");
            addTexture("splashAnimation");
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

        public Vector2 getTextureOrigin(string textureName, float scale)
        {
            AnimatedTextureInfo animTextInfo = ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo(textureName);
            Texture2D texture = getTexture(textureName);
            if (animTextInfo == null)
                return new Vector2(texture.Width / 2, texture.Height / 2);
            else 
                return animTextInfo.textureOrigin;
        }
    }
}
