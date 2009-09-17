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

        CannonBallManager CannonBallManager { get; set; }

        public ShipFactory(Game game, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, CannonBallManager cannonBallManager) : base(game, spriteBatch, physicsSimulator) {
            this.CannonBallManager = cannonBallManager;
        }

        public List<Ship> CreatePlayerShips(List<Color> colors, List<Vector2> locations, List<Island> islands, List<float> shipAngles) {
            return colors.Zip4(locations, islands, shipAngles, (c, l, i, a) => CreatePlayerShip(c, l, i, a)).ToList();
        }

        public Ship CreatePlayerShip(Color color, Vector2 shipLocation, Island playerIsland, float shipAngle)
        {
            Ship ship = new Ship(base.Game, shipLocation, base.PhysicsSimulator, CannonBallManager, "shipHull", shipAngle);
            ShipView shipView = new ShipView(base.Game, color, "shipHull", Color.White, SpriteBatch, ship);
            base.Game.Components.Add(ship);
            base.Game.Components.Add(shipView);
            return ship;
            //return new Ship(base.Game, color, shipLocation, base.SpriteBatch, base.PhysicsSimulator, playerIsland, this.CannonBallManager);
        }
    }
}
