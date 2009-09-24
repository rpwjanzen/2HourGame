using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class PhysicsService : IPhysicsService
    {
        public PhysicsSimulator PhysicsSimulator { get; set; }

        public PhysicsService(Game game, PhysicsSimulator ps)
        {
            PhysicsSimulator = ps;
            game.Services.AddService(typeof(IPhysicsService), this);
        }
    }
}
