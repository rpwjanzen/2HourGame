using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.View;
using _2HourGame.Model;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class HouseFactory : DrawableGameObjectFactory {

        public HouseFactory(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch) { }

        public List<GameObject> CreateHouses(List<Color> colors, List<Vector2> positions) {
            return colors.Zip(positions, (c, p) => CreateHouse(c, p)).ToList();
        }

        public GameObject CreateHouse(Color houseColor, Vector2 houseLocation)
        {
            GameObject house = new GameObject(base.Game, houseLocation, 50, 50);
            GameObjectView houseView = new GameObjectView(base.Game, Content.House, houseColor, SpriteBatch, house, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house));
            base.Game.Components.Add(house);
            base.Game.Components.Add(houseView);
            return house;
        }
    }
}
