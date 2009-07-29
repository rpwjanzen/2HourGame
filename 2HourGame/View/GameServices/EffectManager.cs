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

            textureInfos.Add("goldPickup", new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, true, new Vector2(0, -50)));
            textureInfos.Add("splash", new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, true, new Vector2(13, -7)));
            textureInfos.Add("cannonSmoke", new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, true, new Vector2(6, 0)));
            textureInfos.Add("cannon", new AnimatedTextureInfo(new Vector2(27, 12), 8, 4, 1f, true, new Vector2(0, 0)));
        }

        public void GoldPickupEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "goldGetAnimation", 1f, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.goldAnimation)));
            }
        }

        public void SplashEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("splash", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "splashAnimation", 1f, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.splashAnimation)));
            }
        }

        public void CannonSmokeEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("cannonSmoke", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "cannonSmokeAnimation", 1f, Color.White, spriteBatch, animTextInfo, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.cannonSmokeAnimation)));
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
