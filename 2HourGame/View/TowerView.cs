using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class TowerView : GameObjectView
    {
        Tower tower;
        CannonView towerCannonView;

        public TowerView(Game game, Color color, SpriteBatch spriteBatch, Tower tower)
            : base(game, Content.Tower, color, spriteBatch, tower, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.tower))
        {
            this.tower = tower;
            this.towerCannonView = new CannonView(game, color, spriteBatch, tower.Cannon);
        }

        public override void Initialize()
        {
            towerCannonView.Initialize();

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            if (tower.IsAlive)
                towerCannonView.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
