using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    internal class Island : PhysicsGameObject
    {
        public Island(PhysicsWorld world, Vector2 position, int initialGold)
            : base(world, position, 128, 128)
        {
            Gold = initialGold;
            base.Body.IsStatic = true;
        }

        public int Gold { get; private set; }

        public bool HasGold
        {
            get { return Gold > 0; }
        }

        /// <summary>
        /// Removes one gold from the island.
        /// </summary>
        public void RemoveGold()
        {
            Gold--;
        }

        public void AddGold(int amount)
        {
            Gold += amount;
        }
    }
}