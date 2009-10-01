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
        CannonBallManager cannonBallManager;

        public TowerFactory(Game game, CannonBallManager cannonBallManager, PhysicsSimulator physicsSimulator, SpriteBatch spriteBatch)
            : base(game, spriteBatch, physicsSimulator)
        {
            this.cannonBallManager = cannonBallManager;
        }

        public Tower getTower(Vector2 position, List<GameObject> targets) 
        {
            Tower tower = new Tower(base.Game, position, base.PhysicsSimulator, targets, cannonBallManager);
            GameObjectView towerView = new GameObjectView(base.Game, "tower", Color.White, base.SpriteBatch, tower,
                ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house), new Vector2(-10, -110));
            CannonView<Tower> cannonView = new CannonView<Tower>(base.Game, Color.White, SpriteBatch, tower.cannon);

            Game.Components.Add(tower);
            Game.Components.Add(towerView);
            Game.Components.Add(cannonView);

            return tower;
        }
    }
}
