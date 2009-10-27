using Microsoft.Xna.Framework;

namespace _2HourGame.View.GameServices
{
    interface IAnimationManager
    {
        void PlayAnimation(Animation animation, Vector2 position);
        AnimatedTextureInfo this[Animation animation] { get; }        
    }
}
