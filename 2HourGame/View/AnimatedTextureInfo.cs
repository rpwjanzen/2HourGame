using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.View
{
    class AnimatedTextureInfo
    {
        public Vector2 imageSize { get; private set; }
        public int totalFrames { get; private set; }
        public double framesPerSecond { get; private set; }
        public float scale { get; private set; }
        public bool animateOnceOnly { get; private set; }
        public Vector2 textureDrawOffset { get; private set; }

        public AnimatedTextureInfo(Vector2 imageSize, int totalFrames, double framesPerSecond, float scale, bool animateOnceOnly, Vector2 drawOffset)
        {
            this.imageSize = imageSize;
            this.totalFrames = totalFrames;
            this.framesPerSecond = framesPerSecond;
            this.scale = scale;
            this.animateOnceOnly = animateOnceOnly;
            textureDrawOffset = calculateTextureDrawOffset(drawOffset);
        }

        private Vector2 calculateTextureDrawOffset(Vector2 drawOffset)
        {
            return new Vector2(((totalFrames - 1) / 2) * imageSize.X * scale, 0) + drawOffset;
        }

    }
}
