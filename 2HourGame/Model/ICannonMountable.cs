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
        bool drawCannon();
        Vector2 Velocity { get; }
    }
}
