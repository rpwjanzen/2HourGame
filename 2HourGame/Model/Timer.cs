using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class Timer : GameComponent
    {
        public float Interval { get; set; }
        float elapsedTime;
        bool running;

        public event EventHandler TimeElapsed;
        public bool Expired
        {
            get { return elapsedTime >= Interval; }
        }

        public Timer(Game game) : base(game)
        {
            Interval = 100.0f;
            elapsedTime = 0.0f;
            running = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (running)
            {
                elapsedTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime > Interval && TimeElapsed != null)
                {
                    TimeElapsed(this, EventArgs.Empty);
                }
            }
            base.Update(gameTime);
        }

        public void Reset()
        {
            elapsedTime = 0.0f;
        }

        public void Stop()
        {
            running = false;
        }

        public void Start()
        {
            running = true;
        }

    }
}
