using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame.View
{
    class ShipView : GameObjectView
    {
        Color shipOutlineColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        HealthBarView healthBarView;
        Ship ship;

        CannonView LeftCannonView;
        CannonView RightCannonView;

        IAnimationManager effectManager;

        public ShipView(World world, Color shipOutlineColor, string contentName, Color color, Ship ship)
            : base(world, contentName, color, ship, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull)) 
        {
            this.shipOutlineColor = shipOutlineColor;
            this.ship = ship;
            
            ship.Died += new EventHandler(ship_Died);
            
            healthBarView = new HealthBarView(world, ship);
            LeftCannonView = new CannonView(world, ship.LeftCannons.First(), Color.White);
            RightCannonView = new CannonView(world, ship.RightCannons.First(), Color.White);
        }

        public override void LoadContent(ContentManager content)
        {            
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))["shipGunwale"];
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))["shipRigging"];
            effectManager = (IAnimationManager)base.Game.Services.GetService(typeof(IAnimationManager));

            base.LoadContent(content);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            if (ship.IsAlive)
            {
                spriteBatch.Draw(gunwale, GameObject.Position, null, shipOutlineColor, GameObject.Rotation, base.Origin, base.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGunwale));
                spriteBatch.Draw(rigging, GameObject.Position, null, Color.White, GameObject.Rotation, base.Origin, base.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipRigging));
            }

            base.Draw(gameTime, spriteBatch);
        }

        void ship_Died(object sender, EventArgs e) {
            effectManager.PlayAnimation(Animation.ShipSinking, ship.Position);
            effectManager.PlayAnimation(Animation.FloatingCrate, ship.Position);
        }
    }
}
