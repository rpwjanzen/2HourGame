﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics;

namespace _2HourGame.Model
{
    interface IPhysicsService
    {
        PhysicsSimulator PhysicsSimulator { get; }
    }
}
