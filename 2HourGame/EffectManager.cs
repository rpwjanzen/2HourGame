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
            
            textureInfos.Add("goldPickup", new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, true));
        }

        public void GoldPickupEffect(Ship ship) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new GameObject(game, ship.Position, "goldGetAnimation", 1f, Color.White, spriteBatch, animTextInfo, 0f));
            }
        }
    }
}
