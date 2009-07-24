using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class AnimationManager
    {
        Game game;
        SpriteBatch spriteBatch;

        Dictionary<String, AnimatedTextureInfo> textureInfos;

        public AnimationManager(Game game, SpriteBatch spriteBatch) 
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            textureInfos = new Dictionary<String, AnimatedTextureInfo>();

            // TODO: scale should be 0.3f
            textureInfos.Add("goldPickup", new AnimatedTextureInfo(30, 100, 9, 9, true));

        }

        public void PlayGoldPickupAnimation(Ship ship) 
        {
            AnimatedTextureInfo animTextInfo;
            if (textureInfos.TryGetValue("goldPickup", out animTextInfo))
            {
                game.Components.Add(new DrawableGameObject(ship.Position, 0.0f, null, this.game.Content.Load<Texture2D>("goldGetAnimation"), Color.White, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f));
            }
        }
    }
}
