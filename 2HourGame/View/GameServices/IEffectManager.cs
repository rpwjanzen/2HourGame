using Microsoft.Xna.Framework;

namespace _2HourGame.View.GameServices
{
    interface IEffectManager
    {
        void GoldPickupEffect(Vector2 position);
        void SplashEffect(Vector2 position);
        void CannonSmokeEffect(Vector2 position);
        AnimatedTextureInfo getAnimatedTextureInfo(string effectName);
    }
}
