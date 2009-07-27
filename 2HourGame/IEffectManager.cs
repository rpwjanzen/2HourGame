using Microsoft.Xna.Framework;

namespace _2HourGame
{
    interface IEffectManager
    {
        void GoldPickupEffect(Vector2 position);
        void SplashEffect(CannonBall cannonBall);
        void CannonSmokeEffect(CannonBall cannonBall);
    }
}
