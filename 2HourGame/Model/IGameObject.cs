using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    interface IGameObject : IGameComponent, IPositionable, IRotatable, IOrigin, IScalable
    {
        /// <summary>
        /// Ocfurs when the GameObject is removed from the Game's collection of Components
        /// </summary>
        event EventHandler GameObjectRemoved;
    }
}
