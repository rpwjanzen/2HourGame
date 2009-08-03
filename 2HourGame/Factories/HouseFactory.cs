using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.View;
using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class HouseFactory : DrawableGameObjectFactory {

        public HouseFactory(Game game, SpriteBatch spriteBatch) : base(game, spriteBatch) { }

        public List<GameObject> CreateHouses(List<Color> colors, List<Vector2> positions) {
            return colors.Zip(positions, (c, p) => CreateHouse(c, p)).ToList();
        }

        public GameObject CreateHouse(Color houseColor, Vector2 houseLocation) {
            return new GameObject(base.Game, houseLocation, "house", 1f, houseColor, base.SpriteBatch, ZIndexManager.getZIndex(ZIndexManager.drawnItemOrders.house));
        }
    }
}
