using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using _2HourGame.View;
using _2HourGame.View.GameServices;
using _2HourGame.Model;

namespace _2HourGame
{
    public enum Animation { None, GetGold, Splash, CannonSmoke, CannonFired, LoseGold, BoatHitByCannon, ShipSinking, FloatingCrate };

    class EffectManager : GameComponent, IEffectManager
    {        
        SpriteBatch spriteBatch;

        Dictionary<Animation, AnimatedTextureInfo> textureInformation;
        Dictionary<AnimationView, GameObject> currentAnimations;

        public AnimatedTextureInfo this[Animation animation]
        {
            get { return textureInformation[animation]; }
        }

        public EffectManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            textureInformation = new Dictionary<Animation, AnimatedTextureInfo>();
            currentAnimations = new Dictionary<AnimationView, GameObject>();

            Game.Services.AddService(typeof(IEffectManager), this);
        }

        public override void Initialize()
        {
            LoadTexturesInformation();
            base.Initialize();
        }

        private void LoadTexturesInformation()
        {
            textureInformation[Animation.GetGold] = new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, 1, new Vector2(0, -50), Content.GoldGetAnimation);
            textureInformation[Animation.Splash] = new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, 1, new Vector2(0, -7), Content.SplashAnimation);
            textureInformation[Animation.CannonSmoke] = new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, 1, new Vector2(0, 0), Content.CannonSmokeAnimation);
            textureInformation[Animation.CannonFired] = new AnimatedTextureInfo(new Vector2(12, 27), 8, 4, 0.6f, 1, new Vector2(4, 0), Content.CannonAnimation);
            textureInformation[Animation.LoseGold] = new AnimatedTextureInfo(new Vector2(80, 100), 10, 20, 0.3f, 1, new Vector2(0, 20), Content.GoldLoseAnimation);
            textureInformation[Animation.BoatHitByCannon] = new AnimatedTextureInfo(new Vector2(80, 100), 10, 40, 0.3f, 1, new Vector2(0, 0), Content.BoatHitByCannonAnimation);
            textureInformation[Animation.ShipSinking] = new AnimatedTextureInfo(new Vector2(100, 100), 1, 0.3, 0.3f, 1, new Vector2(0, 0), Content.ShipSinking);
            textureInformation[Animation.FloatingCrate] = new AnimatedTextureInfo(new Vector2(20, 20), 6, 3, 0.7f, 3, new Vector2(5, 5), Content.FloatingCrate);
        }

        public void PlayAnimation(Animation animation, Vector2 position)
        {
            float zIndex;
            switch (animation)
            {
                case Animation.BoatHitByCannon:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.boatHitByCannonAnimation);
                    break;
                case Animation.CannonSmoke:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonSmokeAnimation);
                    break;
                case Animation.FloatingCrate:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.floatingCrate);
                    break;
                case Animation.GetGold:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation);
                    break;
                case Animation.LoseGold:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation);
                    break;
                case Animation.ShipSinking:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipSinking);
                    break;
                case Animation.Splash:
                    zIndex = ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.splashAnimation);
                    break;

                case Animation.None:
                default:
                    zIndex = 1.0f;
                    break;
            }
            addAnimationView(position, textureInformation[animation], Color.White, zIndex);
        }

        private void addAnimationView(Vector2 position, AnimatedTextureInfo animTextInfo, Color color, float zIndex)
        {
            GameObject animationObject = new GameObject(Game, position, animTextInfo.WindowSize.X, animTextInfo.WindowSize.Y);
            Game.Components.Add(animationObject);

            var animationView = new AnimationView(Game, animTextInfo.Content, color, spriteBatch, animTextInfo, animationObject, zIndex);
            animationView.AnimationFinished += HandleAnimationFinished;
            Game.Components.Add(animationView);

            currentAnimations.Add(animationView, animationObject);
        }

        private void HandleAnimationFinished(object sender, EventArgs e)
        {
            var av = sender as AnimationView;
            var ao = currentAnimations[av];
            Game.Components.Remove(av);
            Game.Components.Remove(ao);

            currentAnimations.Remove(av);
        }
    }
}
