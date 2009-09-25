using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View.GameServices
{
    interface ITextureManager
    {
        Texture2D getTexture(string textureName);
        Vector2 getTextureCentre(string textureName, float scale);
        Vector2 getTextureCentre(string textureName, Vector2 scale);
    }
}
