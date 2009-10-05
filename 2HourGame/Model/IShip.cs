using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using _2HourGame.View;

namespace _2HourGame.Model
{
    interface IShip: IGameObject, IHealth
    {
        /// <summary>
        /// The amount of gold the ship is currently carrying
        /// </summary>
        int Gold { get; }

        /// <summary>
        /// The amount of gold this ship can hold
        /// </summary>
        int GoldCapacity { get; }

        /// <summary>
        /// True, if the ship is full of gold
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        /// The linear velocity of the ship
        /// </summary>
        float Speed { get; }
        
        /// <summary>
        /// True, if the ship is alive
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// The ships left cannon
        /// </summary>
        Cannon LeftCannon { get; }

        /// <summary>
        /// The ships right cannon
        /// </summary>
        Cannon RightCannon { get; }

        /// <summary>
        /// Occurs when the ship sinks
        /// </summary>
        event EventHandler<ShipSankEventArgs> ShipSank;

        /// <summary>
        /// Rotates the ship with the given force
        /// </summary>
        /// <param name="amount">The amount of rotational force to apply to move the ship</param>
        void Rotate(float amount);

        /// <summary>
        /// Accelerates the ship by applying the given force
        /// </summary>
        /// <param name="force">The amount of force accelerate with</param>
        void Accelerate(float force);

        /// <summary>
        /// Fires the cannons on the LEFT side of the ship.
        /// </summary>
        /// <param name="now">The current GameTime</param>
        void FireLeftCannons(GameTime now);

        /// <summary>
        /// Fires the cannons on the RIGHT side of the ship.
        /// </summary>
        /// <param name="now">The current GameTime</param>
        void FireRightCannons(GameTime now);

        /// <summary>
        /// Unloads all of the ships gold to the given Island
        /// </summary>
        /// <param name="island"></param>
        void UnloadGoldToIsland(Island island);

        /// <summary>
        /// Loads one Gold from the given Island to the ship
        /// </summary>
        /// <param name="island"></param>
        void LoadGoldFromIsland(Island island);

        /// <summary>
        /// Repairs the ship
        /// </summary>
        void Repair();
    }
}
