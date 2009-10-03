using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View;

namespace _2HourGame.Model
{
    interface IGameObject : IGameComponent, IPositionable, IRotatable
    {
        /// <summary>
        /// Ocfurs when the GameObject is removed from the Game's collection of Components
        /// </summary>
        event EventHandler GameObjectRemoved;

        /// <summary>
        /// The GemObject's width
        /// </summary>
        float Width { get; }

        /// <summary>
        /// The GameObject's height
        /// </summary>
        float Height { get; }
    }
}
