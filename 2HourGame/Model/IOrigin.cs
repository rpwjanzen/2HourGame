using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    interface IOrigin
    {
        /// <summary>
        /// The object's origin
        /// </summary>
        Vector2 Origin { get; }
    }
}
