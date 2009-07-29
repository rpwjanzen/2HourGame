using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.Model;

namespace _2HourGame.Factories
{
    class ShipFactory : PhysicsGameObjectFactory {

        CannonBallManager CannonBallManager { get; set; }

        public ShipFactory(Game game, SpriteBatch spriteBatch, PhysicsSimulator physicsSimulator, CannonBallManager cannonBallManager) : base(game, spriteBatch, physicsSimulator) {
            this.CannonBallManager = cannonBallManager;
        }

        public List<Ship> CreatePlayerShips(List<Color> colors, List<Vector2> locations, List<Island> islands) {
            return colors.Zip3(locations, islands, (c, l, i) => CreatePlayerShip(c, l, i)).ToList();
        }

        public Ship CreatePlayerShip(Color color, Vector2 shipLocation, Island playerIsland) {
            return new Ship(base.Game, color, shipLocation, base.SpriteBatch, base.PhysicsSimulator, playerIsland, this.CannonBallManager);
        }
    }
}
