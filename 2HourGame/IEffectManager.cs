﻿using Microsoft.Xna.Framework;

namespace _2HourGame
{
    interface IEffectManager
    {
        void GoldPickupEffect(Vector2 position);
        void SplashEffect(Vector2 position);
        void CannonSmokeEffect(Vector2 position);
    }
}
