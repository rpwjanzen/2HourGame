using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    interface IPositionable
    {
        /// <summary>
        /// The 2D position of the object
        /// </summary>
        Vector2 Position { get; }
    }
}
