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
    class EffectManager : IEffectManager
    {
        Game game;
        SpriteBatch spriteBatch;

        Dictionary<String, AnimatedTextureInfo> textureInfos;
        Dictionary<AnimationView, GameObject> currentAnimations;

        public EffectManager(Game game, SpriteBatch spriteBatch) 
        {
            this.game = game;
            game.Services.AddService(typeof(IEffectManager), this);

            this.spriteBatch = spriteBatch;
            textureInfos = new Dictionary<String, AnimatedTextureInfo>();

            textureInfos.Add("goldGetAnimation", new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, 1, new Vector2(0, -50)));
            textureInfos.Add("splashAnimation", new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, 1, new Vector2(0, -7)));
            textureInfos.Add("cannonSmokeAnimation", new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, 1, new Vector2(0, 0)));
            textureInfos.Add("cannonAnimation", new AnimatedTextureInfo(new Vector2(12, 27), 8, 4, 0.6f, 1, new Vector2(4, 0)));
            textureInfos.Add("goldLoseAnimation", new AnimatedTextureInfo(new Vector2(80, 100), 10, 20, 0.3f, 1, new Vector2(0, 20)));
            textureInfos.Add("boatHitByCannonAnimation", new AnimatedTextureInfo(new Vector2(80, 100), 10, 40, 0.3f, 1, new Vector2(0, 0)));
            textureInfos.Add("shipSinking", new AnimatedTextureInfo(new Vector2(100, 100), 1, 0.3, 0.3f, 1, new Vector2(0, 0)));
            textureInfos.Add("floatingCrate", new AnimatedTextureInfo(new Vector2(20, 20), 6, 3, 0.7f, 3, new Vector2(5, 5)));

            currentAnimations = new Dictionary<AnimationView, GameObject>();
        }

        public void GoldPickupEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldGetAnimation", out animTextInfo))
            {
                addAnimationView(position, "goldGetAnimation", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation));
            }
        }

        public void GoldLostEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldLoseAnimation", out animTextInfo))
            {
                addAnimationView(position, "goldLoseAnimation", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation));
            }
        }

        public void SplashEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("splashAnimation", out animTextInfo))
            {
                addAnimationView(position, "splashAnimation", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.splashAnimation));
            }
        }

        public void CannonSmokeEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("cannonSmokeAnimation", out animTextInfo))
            {
                addAnimationView(position, "cannonSmokeAnimation", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonSmokeAnimation));
            }
        }

        public void BoatHitByCannonEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("boatHitByCannonAnimation", out animTextInfo))
            {
                addAnimationView(position, "boatHitByCannonAnimation", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.boatHitByCannonAnimation));
            }
        }

        public void ShipSinking(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("shipSinking", out animTextInfo))
            {
                addAnimationView(position, "shipSinking", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipSinking));
            }
        }

        public void FloatingCrate(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("floatingCrate", out animTextInfo))
            {
                addAnimationView(position, "floatingCrate", animTextInfo, Color.White, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.floatingCrate));
            }
        }

        public AnimatedTextureInfo getAnimatedTextureInfo(string effectName) 
        {
            AnimatedTextureInfo animTextInfo = null;
            textureInfos.TryGetValue(effectName, out animTextInfo);
            return animTextInfo;
        }

        private void addAnimationView(Vector2 position, string contentName, AnimatedTextureInfo animTextInfo, Color color, float zIndex)
        {
            GameObject animationObject = new GameObject(game, position, animTextInfo.WindowSize.X, animTextInfo.WindowSize.Y);
            game.Components.Add(animationObject);

            var animationView = new AnimationView(game, contentName, color, spriteBatch, animTextInfo, animationObject, zIndex);
            animationView.AnimationFinished += HandleAnimationFinished;
            game.Components.Add(animationView);

            currentAnimations.Add(animationView, animationObject);
        }

        private void HandleAnimationFinished(object sender, EventArgs e)
        {
            var av = sender as AnimationView;
            var ao = currentAnimations[av];
            game.Components.Remove(av);
            game.Components.Remove(ao);

            currentAnimations.Remove(av);
        }
    }
}
