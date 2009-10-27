using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.View;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class HouseFactory : GameObjectFactory {

        public HouseFactory(World world, TextureManager textureManager, AnimationManager am) : base(world, textureManager, am) { }

        public List<GameObject> CreateHouses(List<Color> colors, List<Vector2> positions) {
            return colors.Zip(positions, (c, p) => CreateHouse(c, p)).ToList();
        }

        public GameObject CreateHouse(Color houseColor, Vector2 houseLocation)
        {
            string contentName = "house";
            GameObject house = new GameObject(World, houseLocation, 50, 50);
            GameObjectView houseView = new GameObjectView(World, contentName, houseColor, house, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house), TextureManager, AnimationManager);
            house.Spawn();

            return house;
        }
    }
}
