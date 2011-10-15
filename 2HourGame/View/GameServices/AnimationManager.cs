using System;
using System.Collections.Generic;
using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace _2HourGame
{
    public enum Animation
    {
        None,
        GetGold,
        Splash,
        CannonSmoke,
        CannonFired,
        LoseGold,
        BoatHitByCannon,
        ShipSinking,
        FloatingCrate
    } ;

    internal class AnimationManager
    {
        private readonly ContentManager content;

        private readonly Dictionary<AnimationView, GameObject> currentAnimations;
        private readonly Dictionary<Animation, AnimatedTextureInfo> textureInformation;
        private readonly TextureManager textureManager;
        private readonly World world;

        public AnimationManager(World world, TextureManager tm, ContentManager content)
        {
            this.world = world;
            textureManager = tm;
            this.content = content;

            textureInformation = new Dictionary<Animation, AnimatedTextureInfo>();
            currentAnimations = new Dictionary<AnimationView, GameObject>();
            LoadTexturesInformation();
        }

        public AnimatedTextureInfo this[Animation animation]
        {
            get { return textureInformation[animation]; }
        }

        private void LoadTexturesInformation()
        {
            textureInformation[Animation.GetGold] = new AnimatedTextureInfo(new Vector2(30, 100), 9, 9, 0.3f, 1,
                                                                            new Vector2(0, -50), "goldGetAnimation");
            textureInformation[Animation.Splash] = new AnimatedTextureInfo(new Vector2(30, 30), 10, 10, 1f, 1,
                                                                           new Vector2(0, -7), "splashAnimation");
            textureInformation[Animation.CannonSmoke] = new AnimatedTextureInfo(new Vector2(50, 50), 6, 2, 0.25f, 1,
                                                                                new Vector2(0, 0),
                                                                                "cannonSmokeAnimation");
            textureInformation[Animation.CannonFired] = new AnimatedTextureInfo(new Vector2(12, 27), 8, 4, 0.6f, 1,
                                                                                new Vector2(4, 0), "cannonAnimation");
            textureInformation[Animation.LoseGold] = new AnimatedTextureInfo(new Vector2(80, 100), 10, 20, 0.3f, 1,
                                                                             new Vector2(0, 20), "goldLoseAnimation");
            textureInformation[Animation.BoatHitByCannon] = new AnimatedTextureInfo(new Vector2(80, 100), 10, 40, 0.3f,
                                                                                    1, new Vector2(0, 0),
                                                                                    "boatHitByCannonAnimation");
            textureInformation[Animation.ShipSinking] = new AnimatedTextureInfo(new Vector2(100, 100), 1, 0.3, 0.3f, 1,
                                                                                new Vector2(0, 0), "shipSinking");
            textureInformation[Animation.FloatingCrate] = new AnimatedTextureInfo(new Vector2(20, 20), 6, 3, 0.7f, 3,
                                                                                  new Vector2(5, 5), "floatingCrate");
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
            AddAnimationView(position, textureInformation[animation], Color.White, zIndex);
        }

        private void AddAnimationView(Vector2 position, AnimatedTextureInfo animTextInfo, Color color, float zIndex)
        {
            var animationObject = new GameObject(world, position, animTextInfo.WindowSize.X, animTextInfo.WindowSize.Y);
            var animationView = new AnimationView(world, animTextInfo.ContentName, color, animTextInfo, animationObject,
                                                  zIndex, textureManager, this);
            animationView.LoadContent(content);

            animationView.AnimationFinished += HandleAnimationFinished;
            animationObject.Spawn();

            currentAnimations.Add(animationView, animationObject);
        }

        private void HandleAnimationFinished(object sender, EventArgs e)
        {
            var av = sender as AnimationView;
            GameObject ao = currentAnimations[av];
            ao.Die();

            currentAnimations.Remove(av);
        }
    }
}