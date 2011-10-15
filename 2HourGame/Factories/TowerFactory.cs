using System.Collections.Generic;
using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;

namespace _2HourGame.Factories
{
    internal class TowerFactory : PhysicsGameObjectFactory
    {
        public TowerFactory(PhysicsWorld world, TextureManager tm, AnimationManager am) : base(world, tm, am)
        {
        }

        public Tower CreateTower(Vector2 position, List<GameObject> targets)
        {
            var tower = new Tower(PhysicsWorld, position, targets, TextureManager, AnimationManager);
            var towerView = new TowerView(World, Color.White, tower, TextureManager, AnimationManager);
            tower.Spawn();

            return tower;
        }
    }
}