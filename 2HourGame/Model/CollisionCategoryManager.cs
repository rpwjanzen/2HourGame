using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerGames.FarseerPhysics;
using Microsoft.Xna.Framework;

namespace _2HourGame.Model
{
    /// <summary>
    /// Holds all the logic for collision categories so that its all in one place.
    /// </summary>
    class CollisionCategoryManager
    {
        List<CollisionObjectAssociation> collisionObjectAssociations;

        public enum CustomCollisionCategory 
        {
            All = CollisionCategory.All,
            None = CollisionCategory.None,
            WorldBorder = CollisionCategory.Cat1,
            CannonBall = CollisionCategory.Cat2,
            Island = CollisionCategory.Cat3,
            Tower = CollisionCategory.Cat4
        }

        public CollisionCategoryManager(Game game) 
        {
            collisionObjectAssociations = new List<CollisionObjectAssociation>();

            collisionObjectAssociations.Add(
                new CollisionObjectAssociation(
                    typeof(WorldBorder),
                    CustomCollisionCategory.WorldBorder,
                    CustomCollisionCategory.All & ~CustomCollisionCategory.CannonBall
                    )
                );

            collisionObjectAssociations.Add(
                new CollisionObjectAssociation(
                    typeof(CannonBall),
                    CustomCollisionCategory.CannonBall,
                    CustomCollisionCategory.All & ~CustomCollisionCategory.WorldBorder & ~CustomCollisionCategory.Island
                    )
                );

            collisionObjectAssociations.Add(
                new CollisionObjectAssociation(
                    typeof(Island),
                    CustomCollisionCategory.Island,
                    CustomCollisionCategory.All & ~CustomCollisionCategory.CannonBall & ~CustomCollisionCategory.Tower
                    )
                );

            collisionObjectAssociations.Add(
                new CollisionObjectAssociation(
                    typeof(Tower),
                    CustomCollisionCategory.Tower,
                    CustomCollisionCategory.All & ~CustomCollisionCategory.Island
                    )
                );

            game.Services.AddService(typeof(CollisionCategoryManager), this);
        }

        /// <summary>
        /// Gets the collision category for an object.
        /// </summary>
        /// <param name="objectType">The type of object that you want the collision category for.</param>
        /// <returns></returns>
        public CollisionCategory getCollisionCategory(Type objectType)
        {
            IEnumerable<CollisionCategory> collisionCategory =
                collisionObjectAssociations
                .Where(c => objectType == c.objectType)
                .Select(c => c.collisionCategory);

            if (collisionCategory.Count() > 0)
                return collisionCategory.First();
            else
                return CollisionCategory.All;
        }

        public CollisionCategory getCollidesWith(Type objectType)
        {
            IEnumerable<CollisionCategory> collidesWith =
                collisionObjectAssociations
                .Where(c => objectType == c.objectType)
                .Select(c => c.collidesWith);

            if (collidesWith.Count() > 0)
                return collidesWith.First();
            else
                return CollisionCategory.All;
        }
    }

    class CollisionObjectAssociation 
    {
        public Type objectType { get; private set; }
        public CollisionCategory collisionCategory { get; private set; }
        public CollisionCategory collidesWith { get; private set; }

        public CollisionObjectAssociation(Type objectType, CollisionCategoryManager.CustomCollisionCategory collisionCategory, CollisionCategoryManager.CustomCollisionCategory collidesWith) 
        {
            this.objectType = objectType;
            this.collisionCategory = (CollisionCategory)collisionCategory;
            this.collidesWith = (CollisionCategory)collidesWith;
        }
    }
}
