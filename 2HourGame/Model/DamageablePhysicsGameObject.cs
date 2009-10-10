using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    class DamageablePhysicsGameObject : PhysicsGameObject
    {
        const double healthRepairAmount = 0.10;
        public bool IsAlive { get; private set; }

        double Health { get; set; }
        readonly double maxHealth = 5;

        public bool IsDamaged
        {
            get { return Health < maxHealth; }
        }
        public double HealthPercentage
        {
            get { return Health / maxHealth; }
        }

        Vector2 spawnPoint;
        Timer respawnTimer = new Timer(10);

        public DamageablePhysicsGameObject(Game game, Vector2 initialPosition, float width, float height, float rotation, int collisionGroup)
            : base(game, initialPosition, width, height, rotation, collisionGroup)
        {
            this.Health = 5;
            this.IsAlive = true;
            this.spawnPoint = initialPosition;
        } 
    }
}
