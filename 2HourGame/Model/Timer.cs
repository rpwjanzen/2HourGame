using System;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class Timer
    {
        private readonly float timerDuration;

        public Timer(float timerDuration)
        {
            this.timerDuration = timerDuration;
            timerStartTime = new TimeSpan();
        }

        public TimeSpan timerStartTime { get; private set; }

        public void resetTimer(TimeSpan timeNow)
        {
            timerStartTime = timeNow;
        }

        public bool TimerHasElapsed(GameTime now)
        {
            return now.TotalGameTime.TotalSeconds - timerStartTime.TotalSeconds > timerDuration;
        }
    }
}