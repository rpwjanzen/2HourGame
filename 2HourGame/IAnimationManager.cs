using Microsoft.Xna.Framework;

namespace _2HourGame
{
    interface IAnimationManager
    {
        void GoldPickupEffect(Vector2 position);
        void SplashEffect(Vector2 position);
        void CannonSmokeEffect(Vector2 position);
        AnimatedTextureInfo getAnimatedTextureInfo(string name);
    }
}
