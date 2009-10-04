using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    class ShipView : GameObjectView
    {
        Color shipOutlineColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        HealthBarView healthBarView;
        IShip ship;

        CannonView<IShip> LeftCannonView;
        CannonView<IShip> RightCannonView;
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="shipColor">The color that the highlights on the ship should be drawn.</param>
        /// <param name="contentName"></param>
        /// <param name="scale"></param>
        /// <param name="color"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="gameObject">The game object that this is a view for.  Must be of type Ship.</param>
        public ShipView(Game game, Color shipOutlineColor, string contentName, Color color, SpriteBatch spriteBatch, IShip ship)
            : base(game, contentName, color, spriteBatch, ship, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull)) 
        {
            this.shipOutlineColor = shipOutlineColor;
            this.ship = ship;

            ship.ShipSank += this.ShipSankEventHandler;
            healthBarView = new HealthBarView(base.Game, spriteBatch, ship);
            Game.Components.Add(healthBarView);

            LeftCannonView = new CannonView<IShip>(Game, Color.White, SpriteBatch, ship.LeftCannon);
            RightCannonView = new CannonView<IShip>(Game, Color.White, SpriteBatch, ship.RightCannon);
        }

        public override void Initialize()
        {
            LeftCannonView.Initialize();
            RightCannonView.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {            
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))["shipGunwale"];
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager)))["shipRigging"];

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            healthBarView.Enabled = ship.IsAlive;
            if (ship.IsAlive)
            {
                base.SpriteBatch.Draw(gunwale, GameObject.Position, null, shipOutlineColor, GameObject.Rotation, base.Origin, base.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGunwale));
                base.SpriteBatch.Draw(rigging, GameObject.Position, null, Color.White, GameObject.Rotation, base.Origin, base.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipRigging));

                LeftCannonView.Draw(gameTime);
                RightCannonView.Draw(gameTime);

                base.Draw(gameTime);
            }
        }

        private void ShipSankEventHandler(object sender, EventArgs e)
        {
            this.playShipSinkingAnimations();
        }

        private void playShipSinkingAnimations()
        {
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.ShipSinking, GameObject.Position);
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).PlayAnimation(Animation.FloatingCrate, GameObject.Position);
        }
    }
}
