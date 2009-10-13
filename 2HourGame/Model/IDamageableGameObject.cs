using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.Model
{
    interface IDamageableGameObject : IGameObject
    {
        /// <summary>
        /// Is fired when the object is destroyed.
        /// </summary>
        event EventHandler<ObjectDestroyedEventArgs> ObjectDestroyed;

        /// <summary>
        /// Is fired when the object respawns.
        /// </summary>
        event EventHandler<ObjectRespawnedEventArgs> ObjectRespawned;

        /// <summary>
        /// The remaining percentage of health.
        /// </summary>
        double HealthPercentage { get; }

        /// <summary>
        /// Returns true if the object has damage.
        /// </summary>
        bool IsDamaged { get; }

        /// <summary>
        /// Repairs the object
        /// </summary>
        void Repair();

        /// <summary>
        /// True, if the object is alive
        /// </summary>
        bool IsAlive { get; }
    }
}
