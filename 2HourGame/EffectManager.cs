using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            textureInfos.Add("splash", new AnimatedTextureInfo(new Vector2(30, 70), 10, 10, 1f, true, new Vector2(0, -30)));
            textureInfos.Add("cannonSmoke", new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 1f, true, new Vector2(0, 0)));
        }

        public void GoldPickupEffect(Ship ship) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, ship.Position, "goldGetAnimation", 1f, Color.White, spriteBatch, animTextInfo, 0f));
            }
        }

        public void SplashEffect(CannonBall cannonBall) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("splash", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, cannonBall.Position, "splash", 1f, Color.White, spriteBatch, animTextInfo, 0f));
            }
        }

        public void CannonSmokeEffect(CannonBall cannonBall) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("cannonSmoke", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, cannonBall.Position, "cannonSmoke", 1f, Color.White, spriteBatch, animTextInfo, 0f));
            }
        }
    }
}
