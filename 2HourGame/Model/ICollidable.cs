using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;

namespace _2HourGame.Model
{
    /// <summary>
    /// An object that can collide via the Farseer Physics Engine
    /// </summary>
    interface ICollidable
    {
        /// <summary>
        /// The object's geometry
        /// </summary>
        Geom Geometry { get; }

        /// <summary>
        /// The object's body
        /// </summary>
        Body Body { get; }
    }
}
