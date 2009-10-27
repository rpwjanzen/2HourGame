using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.Model;
using _2HourGame.View;

namespace _2HourGame.Factories
{
    class ShipFactory : PhysicsGameObjectFactory {

        public ShipFactory(PhysicsWorld world) : base(world) {
        }

        public List<Ship> CreatePlayerShips(List<Color> colors, List<Vector2> locations, List<Island> islands, List<float> shipAngles) {
            return colors.Zip4(locations, islands, shipAngles, (c, l, i, a) => CreatePlayerShip(c, l, i, a)).ToList();
        }

        public Ship CreatePlayerShip(Color color, Vector2 shipLocation, Island playerIsland, float shipAngle)
        {
            Ship ship = new Ship(PhysicsWorld, shipLocation, shipAngle);
            ShipView shipView = new ShipView(World, color, "shipHull", Color.White, ship);

            ship.Spawn();

            return ship;
        }
    }
}
