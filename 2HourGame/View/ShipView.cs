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
        private Color shipColor;
        private Texture2D gunwale;
        private Texture2D rigging;

        CannonView LeftCannonView;
        CannonView RightCannonView;
        HealthBarView healthBarView;

        //private const float ShipScale = 0.6f;

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
        public ShipView(Game game, Color shipColor, string contentName, Color color, SpriteBatch spriteBatch, Ship gameObject)
            : base(game, contentName, color, spriteBatch, gameObject, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipHull)) 
        {
            this.shipColor = shipColor;

            LeftCannonView = initializeCannonView(CannonType.LeftCannon);
            RightCannonView = initializeCannonView(CannonType.RightCannon);

            gameObject.ShipSank += hideShip;
            gameObject.ShipSpawned += unHideShip;
            gameObject.CannonHasBeenFired += playCannonViewFiringAnimation;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            gunwale = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipGunwale");
            rigging = ((ITextureManager)base.Game.Services.GetService(typeof(ITextureManager))).getTexture("shipRigging");

            healthBarView = new HealthBarView(base.Game, spriteBatch, (Ship)gameObject);
        }

        public override void Draw(GameTime gameTime)
        {
            if (((Ship)gameObject).isActive)
            {
                base.Draw(gameTime);
                base.spriteBatch.Draw(gunwale, gameObject.Position, null, shipColor, gameObject.Rotation, gameObject.Origin, gameObject.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipGunwale));
                base.spriteBatch.Draw(rigging, gameObject.Position, null, Color.White, gameObject.Rotation, gameObject.Origin, gameObject.Scale, SpriteEffects.None, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipRigging));
                healthBarView.Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateCannonView(LeftCannonView);
            UpdateCannonView(RightCannonView);
        }

        private CannonView initializeCannonView(CannonType cannonType)
        {
            GameObject cannon = new GameObject(Game, getCannonPosition(cannonType), "cannonAnimation", gameObject.Scale);

            CannonView newCannonView = new CannonView(
                Game,
                Color.White,
                spriteBatch,
                cannonType,
                cannon
                );

            newCannonView.UpdateRotation(getCannonRotation(cannonType));

            Game.Components.Add(cannon);
            Game.Components.Add(newCannonView);
            return newCannonView;
        }

        private void UpdateCannonView(CannonView cannonView)
        {
            cannonView.UpdateRotation(getCannonRotation(cannonView.cannonType));
            cannonView.UpdatePosition(getCannonPosition(cannonView.cannonType));
        }

        private float getCannonRotation(CannonType cannonType)
        {
            if (cannonType == CannonType.LeftCannon)
                return 2f * (float)Math.PI + gameObject.Rotation;
            else
                return (float)Math.PI + gameObject.Rotation;
        }

        private Vector2 getCannonPosition(CannonType cannonType)
        {
            if (cannonType == CannonType.LeftCannon)
                return new Vector2(((Ship)gameObject).Body.GetBodyMatrix().Left.X, ((Ship)gameObject).Body.GetBodyMatrix().Left.Y) * (gameObject.XRadius - 8) + gameObject.Position;
            else
                return new Vector2(((Ship)gameObject).Body.GetBodyMatrix().Right.X, ((Ship)gameObject).Body.GetBodyMatrix().Right.Y) * (gameObject.XRadius - 8) + gameObject.Position;
        }

        private void playCannonViewFiringAnimation(CannonType cannonType, GameTime gameTime) 
        {
            // start the firing animation
            if (cannonType == CannonType.LeftCannon)
                LeftCannonView.PlayAnimation(gameTime);
            else
                RightCannonView.PlayAnimation(gameTime);
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideShip()
        {
            LeftCannonView.isActive = false;
            RightCannonView.isActive = false;
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).ShipSinking(gameObject.Position);
            ((IEffectManager)base.Game.Services.GetService(typeof(IEffectManager))).FloatingCrate(gameObject.Position);
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void unHideShip()
        {
            LeftCannonView.isActive = true;
            RightCannonView.isActive = true;
        }
    }
}
