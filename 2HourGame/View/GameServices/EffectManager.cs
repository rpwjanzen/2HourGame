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

        public EffectManager(Game game, SpriteBatch spriteBatch) 
        {
            this.game = game;
            game.Services.AddService(typeof(IEffectManager), this);

            this.spriteBatch = spriteBatch;
            textureInfos = new Dictionary<String, AnimatedTextureInfo>();

            textureInfos.Add("goldPickup", new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, 1, new Vector2(0, -50)));
            textureInfos.Add("splash", new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, 1, new Vector2(0, -7)));
            textureInfos.Add("cannonSmoke", new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, 1, new Vector2(0, 0)));
            textureInfos.Add("cannon", new AnimatedTextureInfo(new Vector2(27, 12), 8, 4, 0.6f, 1, new Vector2(0, 4)));
            textureInfos.Add("goldLost", new AnimatedTextureInfo(new Vector2(80, 100), 10, 20, 0.3f, 1, new Vector2(0, 20)));
            textureInfos.Add("boatHitByCannon", new AnimatedTextureInfo(new Vector2(80, 100), 10, 40, 0.3f, 1, new Vector2(0, 0)));
            textureInfos.Add("shipSinking", new AnimatedTextureInfo(new Vector2(100, 100), 1, 0.3, 0.3f, 1, new Vector2(0, 0)));
            textureInfos.Add("floatingCrate", new AnimatedTextureInfo(new Vector2(20, 20), 6, 3, 0.7f, 3, new Vector2(5, 5)));
        }

        public void GoldPickupEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "goldGetAnimation", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation)));
            }
        }

        public void GoldLostEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldLost", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "goldLoseAnimation", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation)));
            }
        }

        public void SplashEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("splash", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "splashAnimation", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.splashAnimation)));
            }
        }

        public void CannonSmokeEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("cannonSmoke", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "cannonSmokeAnimation", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonSmokeAnimation)));
            }
        }

        public void BoatHitByCannonEffect(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("boatHitByCannon", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "boatHitByCannonAnimation", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.boatHitByCannonAnimation)));
            }
        }

        public void ShipSinking(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("shipSinking", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "shipSinking", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.shipSinking)));
            }
        }

        public void FloatingCrate(Vector2 position)
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("floatingCrate", out animTextInfo))
            {
                game.Components.Add(new AnimationObject(game, position, "floatingCrate", animTextInfo.scale, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.floatingCrate)));
            }
        }

        public AnimatedTextureInfo getAnimatedTextureInfo(string effectName) 
        {
            AnimatedTextureInfo animTextInfo = null;
            textureInfos.TryGetValue(effectName, out animTextInfo);
            return animTextInfo;
        }
    }
}
