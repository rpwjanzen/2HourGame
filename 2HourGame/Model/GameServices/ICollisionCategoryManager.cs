using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics;

namespace _2HourGame.Model.GameServices
{
    interface ICollisionCategoryManager
    {
        CollisionCategory getCollisionCategory(Type objectType);
        CollisionCategory getCollidesWith(Type objectType);
    }
}
