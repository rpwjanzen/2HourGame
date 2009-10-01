using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.View
{
    class AnimatedTextureInfo
    {
        /// <summary>
        /// Size of the Animation sliding window
        /// </summary>
        public Vector2 WindowSize { get; private set; }
        /// <summary>
        /// The total number of frames in the animation
        /// </summary>
        public int TotalFrames { get; private set; }
        /// <summary>
        /// The number of frames to draw per second of animation
        /// </summary>
        public double FramesPerSecond { get; private set; }
        /// <summary>
        /// The amount the drawing is scaled by
        /// </summary>
        public float Scale { get; private set; }
        /// <summary>
        /// The number of times to repeat the animation
        /// </summary>
        public int NumAnimationIterations { get; private set; }

        public Vector2 WindowCenter {
            get
            {
                return new Vector2(WindowSize.X / 2f, WindowSize.Y / 2f);
            }
        }
        /// <summary>
        /// The amount to offset the animation draw position by
        /// </summary>
        private Vector2 offset;

        public AnimatedTextureInfo(Vector2 imageSize, int totalFrames, double framesPerSecond, float scale, int numAnimationIterations, Vector2 offset)
        {
            this.WindowSize = imageSize;
            this.TotalFrames = totalFrames;
            this.FramesPerSecond = framesPerSecond;
            this.Scale = scale;
            this.NumAnimationIterations = numAnimationIterations;
            this.offset = offset;
        }
        /// <summary>
        /// Calculates the animation draw position offset rotated by the given amount of radians
        /// </summary>
        /// <param name="rotationRadians">The amount to rotate the offset by.</param>
        /// <returns>The rotated image offset</returns>
        public Vector2 GetRotatedOffset(float rotationRadians)
        {
            Matrix rotationMatrix = Matrix.CreateRotationZ(rotationRadians);
            return Vector2.Transform(offset, rotationMatrix);
        }
    }
}
