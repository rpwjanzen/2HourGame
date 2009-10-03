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

        public ShipFactory(Game game, SpriteBatch spriteBatch, CannonBallManager cannonBallManager) : base(game, spriteBatch) {
            this.CannonBallManager = cannonBallManager;
        }

        public List<IShip> CreatePlayerShips(List<Color> colors, List<Vector2> locations, List<Island> islands, List<float> shipAngles) {
            return colors.Zip4(locations, islands, shipAngles, (c, l, i, a) => CreatePlayerShip(c, l, i, a)).ToList();
        }

        public IShip CreatePlayerShip(Color color, Vector2 shipLocation, Island playerIsland, float shipAngle)
        {
            IShip ship = new Ship(base.Game, shipLocation, CannonBallManager, "shipHull", shipAngle);
            ShipView shipView = new ShipView(base.Game, color, "shipHull", Color.White, SpriteBatch, ship);
            base.Game.Components.Add(ship);
            base.Game.Components.Add(shipView);
            return ship;
        }
    }
}
