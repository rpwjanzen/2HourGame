using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame.View
{
    internal class TowerView : GameObjectView
    {
        private readonly CannonView cannonView;
        private readonly Tower tower;

        public TowerView(World world, Color color, Tower tower, TextureManager textureManager, AnimationManager am)
            : base(
                world, "tower", color, tower, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.tower),
                textureManager, am)
        {
            this.tower = tower;
            cannonView = new CannonView(world, tower.Cannon, color, textureManager, am);
        }

        public override void LoadContent(ContentManager content)
        {
            cannonView.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (tower.IsAlive)
            {
                cannonView.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime, spriteBatch);
        }
    }
}