using _2HourGame.Model;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class TowerView : GameObjectView
    {
        Tower tower;
        CannonView cannonView;

        public TowerView(World world, Color color, Tower tower)
            : base(world, "tower", color, tower, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.tower))
        {
            this.tower = tower;
            this.cannonView = new CannonView(world, tower.Cannon, color);
        }

        public override void LoadContent(ContentManager content) {
            cannonView.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (tower.IsAlive) {
                cannonView.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime, spriteBatch);
        }
    }
}
