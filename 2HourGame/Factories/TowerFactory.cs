using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class TowerFactory : PhysicsGameObjectFactory
    {
        public TowerFactory(PhysicsWorld world, TextureManager tm, AnimationManager am) : base(world, tm, am) { }

        public Tower CreateTower(Vector2 position, List<GameObject> targets) 
        {
            Tower tower = new Tower(PhysicsWorld, position, targets);
            TowerView towerView = new TowerView(World, Color.White, tower, TextureManager, AnimationManager);
            tower.Spawn();            

            return tower;
        }
    }
}
