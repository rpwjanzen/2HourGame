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
        public int numAnimationIterations { get; private set; }

        private Vector2 textureDrawOffset;

        public Vector2 textureOrigin {
            get
            {
                return new Vector2(imageSize.X / 2f, imageSize.Y / 2f);
            }
        }

        public AnimatedTextureInfo(Vector2 imageSize, int totalFrames, double framesPerSecond, float scale, int numAnimationIterations, Vector2 drawOffset)
        {
            this.imageSize = imageSize;
            this.totalFrames = totalFrames;
            this.framesPerSecond = framesPerSecond;
            this.scale = scale;
            this.numAnimationIterations = numAnimationIterations;
            textureDrawOffset = drawOffset;
        }

        public Vector2 drawOffset(float rotationRadians)
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(rotationRadians);
            return Vector2.Transform(textureDrawOffset, rotationMatrix);
        }
    }
}
