using Microsoft.Xna.Framework;

namespace _2HourGame.View.GameServices
{
    interface IEffectManager
    {
        void PlayAnimation(Animation animation, Vector2 position);
        AnimatedTextureInfo this[Animation animation] { get; }        
    }
}
