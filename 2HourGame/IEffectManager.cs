using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame
{
    interface IEffectManager
    {
        void GoldPickupEffect(Ship ship);
        void SplashEffect(CannonBall cannonBall);
        void CannonSmokeEffect(CannonBall cannonBall);
    }
}
