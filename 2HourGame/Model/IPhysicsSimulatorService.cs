using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    interface IPhysicsSimulatorService
    {
        PhysicsSimulator PhysicsSimulator { get; }
        GameTime CollisionTime { get; }
    }
}
