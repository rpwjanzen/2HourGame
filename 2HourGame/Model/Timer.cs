using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class Timer
    {
        float timerDuration;
        TimeSpan timerStartTime;

        public Timer(float timerDuration) 
        {
            this.timerDuration = timerDuration;
            timerStartTime = new TimeSpan();
        }

        public void resetTimer(TimeSpan timeNow)
        {
            timerStartTime = timeNow;
        }

        public bool TimerHasElapsed(GameTime now)
        {
            return now.TotalGameTime.TotalSeconds - timerStartTime.TotalSeconds > this.timerDuration;
        }
    }
}
