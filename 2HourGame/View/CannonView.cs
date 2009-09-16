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
    public enum CannonType { LeftCannon, RightCannon }

    class CannonView : AnimationView
    {
        public CannonType cannonType { get; private set; }

        public bool isActive;

        private const string cannonTextureName = "cannonAnimation";
        private readonly Vector2 cannonOrigin;

        public CannonView(Game game, Color color, SpriteBatch spriteBatch, CannonType cannonType, Ship gameObject)
            : base(game, cannonTextureName, color, spriteBatch, ((IEffectManager)game.Services.GetService(typeof(IEffectManager))).getAnimatedTextureInfo("cannonAnimation"), gameObject, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipCannon))
        {
            this.cannonType = cannonType;
            isActive = true;

            cannonOrigin = ((ITextureManager)Game.Services.GetService(typeof(ITextureManager))).getTextureOrigin(cannonTextureName, gameObject.Scale);

            gameObject.CannonHasBeenFired += playCannonViewFiringAnimation;
            gameObject.ShipSank += hideShip;
            gameObject.ShipSpawned += unHideShip;
        }

        public override void Draw(GameTime gameTime)
        {
            if (isActive)
            {
                if (firstDraw)
                {
                    firstDraw = false;
                    animationStartTime = gameTime.TotalGameTime;
                }

                // get the frame to draw
                int totalFrame = (int)Math.Round(((gameTime.TotalGameTime.TotalSeconds - animationStartTime.TotalSeconds)
                    * animatedTextureInfo.framesPerSecond));

                int frame;
                if (totalFrame >= animatedTextureInfo.totalFrames)
                    frame = 0;
                else
                    frame = totalFrame % animatedTextureInfo.totalFrames;

                Rectangle source = new Rectangle((int)animatedTextureInfo.imageSize.X * frame, 0, (int)animatedTextureInfo.imageSize.X, (int)animatedTextureInfo.imageSize.Y);
                spriteBatch.Draw(
                    texture,
                    getCannonPosition() + (cannonType == CannonType.LeftCannon ? animatedTextureInfo.drawOffset(getCannonRotation()) : -animatedTextureInfo.drawOffset(getCannonRotation())),
                    source,
                    Color,
                    getCannonRotation(),
                    cannonOrigin,
                    animatedTextureInfo.scale,
                    SpriteEffects.None,
                    ZIndex
                );
            }
        }

        private void playCannonViewFiringAnimation(CannonType cannonType, GameTime gameTime)
        {
            // start the firing animation
            if (cannonType == this.cannonType)
                PlayAnimation(gameTime);
        }

        private float getCannonRotation()
        {
            if (cannonType == CannonType.LeftCannon)
                return 2f * (float)Math.PI + gameObject.Rotation;
            else
                return (float)Math.PI + gameObject.Rotation;
        }

        private Vector2 getCannonPosition()
        {
            if (cannonType == CannonType.LeftCannon)
                return new Vector2(((Ship)gameObject).Body.GetBodyMatrix().Left.X, ((Ship)gameObject).Body.GetBodyMatrix().Left.Y) * (gameObject.XRadius - 8) + gameObject.Position;
            else
                return new Vector2(((Ship)gameObject).Body.GetBodyMatrix().Right.X, ((Ship)gameObject).Body.GetBodyMatrix().Right.Y) * (gameObject.XRadius - 8) + gameObject.Position;
        }

        private void PlayAnimation(GameTime gameTime) 
        {
            animationStartTime = gameTime.TotalGameTime;
        }

        /// <summary>
        /// disables drawing, control, and physics of ship
        /// </summary>
        private void hideShip()
        {
            isActive = false;
        }

        /// <summary>
        /// enables drawing, control, and physics of ship
        /// </summary>
        private void unHideShip()
        {
            isActive = true;
        }
    }
}
