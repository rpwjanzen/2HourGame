using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model
{
    interface IHealth
    {
        double HealthPercentage { get; }
        bool IsDamaged { get; }
    }
}
