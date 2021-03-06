﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.View
{
    abstract class ShipView : GameObjectView
    {
        Color shipOutlineColor;
        protected Texture2D gunwale;
        protected Texture2D rigging;

        HealthBarView healthBarView;
        IShip ship;

        CannonView LeftCannonView;
        CannonView RightCannonView;

        IEffectManager effectManager;

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
        public ShipView(Game game, Color shipOutlineColor, Content content, Color color, SpriteBatch spriteBatch, IShip ship)
            : base(game, content, color, spriteBatch, ship, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull)) 
        {
            this.shipOutlineColor = shipOutlineColor;
            this.ship = ship;

            ship.ObjectDestroyed += this.ShipSankEventHandler;
            healthBarView = new HealthBarView(base.Game, spriteBatch, ship);
            Game.Components.Add(healthBarView);

            LeftCannonView = new CannonView(Game, Color.White, SpriteBatch, ship.LeftCannons.First());
            RightCannonView = new CannonView(Game, Color.White, SpriteBatch, ship.RightCannons.First());
        }

        public override void Initialize()
        {
            LeftCannonView.Initialize();
            RightCannonView.Initialize();

            effectManager = (IEffectManager)base.Game.Services.GetService(typeof(IEffectManager));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            loadTextures();

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
            effectManager.PlayAnimation(Animation.ShipSinking, GameObject.Position);
            effectManager.PlayAnimation(Animation.FloatingCrate, GameObject.Position);
        }

        /// <summary>
        /// Each Ship View should set the rigging an gunwhale textures for their ship.
        /// </summary>
        protected abstract void loadTextures();
    }
}
