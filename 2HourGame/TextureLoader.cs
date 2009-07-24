using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame {
    class TextureLoader {
        public Dictionary<string, Texture2D> LoadTextures(ContentManager contentManager, IEnumerable<string> textureNames) {
            var loadedTextures = new Dictionary<string, Texture2D>();
            foreach (var name in textureNames) {
                var texture = LoadTexture2D(name, contentManager);
                loadedTextures.Add(name, texture);
            }
            return loadedTextures;
        }

        public Texture2D LoadTexture2D(string name, ContentManager contentManager) {
            return contentManager.Load<Texture2D>(name);
        }
    }
}
