using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model
{
    interface IRotatable
    {
        /// <summary>
        /// The object's rotation in Radians
        /// </summary>
        float Rotation { get; }
    }
}
