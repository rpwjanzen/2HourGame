using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerGames.FarseerPhysics;
using System.Collections.Generic;
using System.Linq;

using _2HourGame.Model;
using _2HourGame.View;
using _2HourGame.View.GameServices;

namespace _2HourGame.Factories
{
    class ShipFactory : PhysicsGameObjectFactory {

        CannonBallManager CannonBallManager { get; set; }

        public ShipFactory(Game game, SpriteBatch spriteBatch, CannonBallManager cannonBallManager) : base(game, spriteBatch) {
            this.CannonBallManager = cannonBallManager;
        }

        public List<IShip> CreatePlayerShips(List<Color> colors, List<Vector2> locations, List<Island> islands, List<float> shipAngles) {
            return colors.Zip4(locations, islands, shipAngles, (c, l, i, a) => ((IShip)CreateSloop(c, l, i, a))).ToList();
        }

        public Sloop CreateSloop(Color color, Vector2 shipLocation, Island playerIsland, float shipAngle)
        {
            Sloop ship = new Sloop(base.Game, shipLocation, CannonBallManager, shipAngle);
            SloopView shipView = new SloopView(base.Game, color, Color.White, SpriteBatch, ship);
            base.Game.Components.Add(ship);
            base.Game.Components.Add(shipView);
            return ship;
        }

        public Cutter CreateCutter(Color color, Vector2 shipLocation, Island playerIsland, float shipAngle)
        {
            Cutter ship = new Cutter(base.Game, shipLocation, CannonBallManager, shipAngle);
            CutterView shipView = new CutterView(base.Game, color, Color.White, SpriteBatch, ship);
            base.Game.Components.Add(ship);
            base.Game.Components.Add(shipView);
            return ship;
        }
    }
}
