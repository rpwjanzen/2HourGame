using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2HourGame.View;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    interface ICannonMountable
    {
        /// <summary>
        /// Should the Cannon that is mounted draw itself?
        /// </summary>
        /// <returns>True, if the cannon should draw itself, false otherwise</returns>
        bool IsCannonVisible { get; }

        /// <summary>
        /// The velocity of the object the cannon is mounted to.
        /// Used for adjusting the velocity of fired cannonballs
        /// </summary>
        Vector2 Velocity { get; }
    }
}
