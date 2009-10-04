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
        Texture2D this[string index] { get; }
    }
}
