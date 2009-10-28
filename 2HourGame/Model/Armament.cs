using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model
{
    enum FiringGroup { Left, Right };
    abstract class Armament
    {
        Dictionary<FiringGroup, List<Cannon>> cannonGroups;

        public Armament() 
        {
            cannonGroups = new Dictionary<FiringGroup,List<Cannon>>();


        }
    }
}
