using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal static class TextureExtensions
    {
        public static Vector2 Center(this Texture2D texture)
        {
            return new Vector2(texture.Width/2.0f, texture.Height/2.0f);
        }

        public static Vector2 Center(this Texture2D texture, float scale)
        {
            return texture.Center()*scale;
        }

        public static Vector2 Center(this Texture2D texture, Vector2 scale)
        {
            return texture.Center()*scale;
        }
    }
}