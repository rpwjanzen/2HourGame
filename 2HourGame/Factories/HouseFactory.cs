using System.Collections.Generic;
using System.Linq;
using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;
using Microsoft.Xna.Framework;

namespace _2HourGame.Factories
{
    internal class HouseFactory : GameObjectFactory
    {
        public HouseFactory(World world, TextureManager textureManager, AnimationManager am)
            : base(world, textureManager, am)
        {
        }

        public List<GameObject> CreateHouses(List<Color> colors, List<Vector2> positions)
        {
            return colors.Zip(positions, (c, p) => CreateHouse(c, p)).ToList();
        }

        public GameObject CreateHouse(Color houseColor, Vector2 houseLocation)
        {
            string contentName = "house";
            var house = new GameObject(World, houseLocation, 50, 50);
            var houseView = new GameObjectView(World, contentName, houseColor, house,
                                               ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house),
                                               TextureManager, AnimationManager);
            house.Spawn();

            return house;
        }
    }
}