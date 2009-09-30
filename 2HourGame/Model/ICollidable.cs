using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerGames.FarseerPhysics.Dynamics;

namespace _2HourGame.Model
{
    interface ICollidable
    {
        Geom Geometry { get; }
        Body Body { get; }
    }
}
