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

        public TowerFactory(Game game, CannonBallManager cannonBallManager, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            this.cannonBallManager = cannonBallManager;
        }

        public Tower CreateTower(Vector2 position, List<IGameObject> targets) 
        {
            Tower tower = new Tower(base.Game, position, targets, cannonBallManager);
            TowerView towerView = new TowerView(Game, Color.White, SpriteBatch, tower);

            //GameObjectView towerView = new GameObjectView(base.Game, "tower", Color.White, base.SpriteBatch, tower,
                //ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house), new Vector2(-10, -110));


            Game.Components.Add(tower);
            Game.Components.Add(towerView);

            return tower;
        }
    }
}
