using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model.GameServices
{
    class CollisionGroupManager
    {
        int collisionGroupCounter;

        public CollisionGroupManager(Game game) 
        {
            collisionGroupCounter = 1;

            game.Services.AddService(typeof(CollisionGroupManager), this);
        }

        public int getNextFreeCollisionGroup() 
        {
            return collisionGroupCounter++;
        }
    }
}
