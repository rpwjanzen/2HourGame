using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View;

namespace _2HourGame.Factories
{
    class TowerFactory : PhysicsGameObjectFactory
    {
        public TowerFactory(PhysicsWorld world) : base(world) { }

        public Tower CreateTower(Vector2 position, List<GameObject> targets) 
        {
            Tower tower = new Tower(PhysicsWorld, position, targets);
            TowerView towerView = new TowerView(World, Color.White, tower);
            tower.Spawn();            

            return tower;
        }
    }
}
