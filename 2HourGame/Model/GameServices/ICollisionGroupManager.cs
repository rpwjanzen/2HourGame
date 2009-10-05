using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model.GameServices
{
    interface ICollisionGroupManager
    {
        int getNextFreeCollisionGroup();
    }
}
