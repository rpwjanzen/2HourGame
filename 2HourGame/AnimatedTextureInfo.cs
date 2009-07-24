using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _2HourGame
{
    class AnimatedTextureInfo
    {
        public Texture2D Texture { get; private set; }
        public int ImageWidth { get; private set; }
        public int ImageHeight { get; private set; }
        public int TotalFrames { get; private set; }
        public double FramesPerSecond { get; private set; }
        public bool AnimateOnceOnly { get; private set; }

        public AnimatedTextureInfo(int imageWidth, int imageHeight, int totalFrames, double framesPerSecond, bool animateOnceOnly)
        {
            this.ImageWidth = imageWidth;
            this.ImageHeight = imageHeight;
            this.TotalFrames = totalFrames;
            this.FramesPerSecond = framesPerSecond;
            this.AnimateOnceOnly = animateOnceOnly;
        }
    }
}
