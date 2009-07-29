using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class AnimationManager : IAnimationManager
    {
        Game game;
        SpriteBatch spriteBatch;

        Dictionary<String, AnimatedTextureInfo> textureInfos;

        public AnimationManager(Game game, SpriteBatch spriteBatch) 
        {
            this.game = game;
            game.Services.AddService(typeof(IAnimationManager), this);

            this.spriteBatch = spriteBatch;
            textureInfos = new Dictionary<String, AnimatedTextureInfo>();

            textureInfos.Add("goldPickup", new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, new Vector2(0, -50), new AnimateAndDeleteAnimationBehaviour()));
            textureInfos.Add("splash", new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, new Vector2(13, -7), new AnimateAndDeleteAnimationBehaviour()));
            textureInfos.Add("cannonSmoke", new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, new Vector2(6, 0), new AnimateAndDeleteAnimationBehaviour()));
            // the fps should be the number of frames over the ship reload time, and the scale should be the same as the ship
            textureInfos.Add("cannon", new AnimatedTextureInfo(new Vector2(27, 12), 8, 4, 1f, new Vector2(6, 0), new AnimateWhenStartTimeResetAnimationBehaviour()));
        }

        public void GoldPickupEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "goldGetAnimation", 1f, Color.White, spriteBatch, animTextInfo, (float)ZIndexManager.drawnItemOrders.goldAnimation / 100));
            }
        }

        public void SplashEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("splash", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "splashAnimation", 1f, Color.White, spriteBatch, animTextInfo, (float)ZIndexManager.drawnItemOrders.splashAnimation / 100));
            }
        }

        public void CannonSmokeEffect(Vector2 position) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("cannonSmoke", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, position, "cannonSmokeAnimation", 1f, Color.White, spriteBatch, animTextInfo, (float)ZIndexManager.drawnItemOrders.cannonSmokeAnimation / 100));
            }
        }

        public AnimatedTextureInfo getAnimatedTextureInfo(string name) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue(name, out animTextInfo))
            {
                return animTextInfo;
            }
            return null;
        }
    }
}
