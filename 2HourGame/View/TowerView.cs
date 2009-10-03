﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace _2HourGame.View
{
    class TowerView : GameObjectView
    {
        Tower tower;
        CannonView<Tower> towerCannonView;

        public TowerView(Game game, Color color, SpriteBatch spriteBatch, Tower tower)
            : base(game, "tower", color, spriteBatch, tower, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.tower))
        {
            this.tower = tower;
            this.towerCannonView = new CannonView<Tower>(game, color, spriteBatch, tower.Cannon);
        }

        public override void Draw(GameTime gameTime)
        {
            if (tower.IsCannonVisible)
            {
                towerCannonView.Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}
