using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2HourGame.Model;
using Microsoft.Xna.Framework;

namespace _2HourGame.Factories
{
    class PlayerFactory
    {
        Map map;

        public PlayerFactory(Map map) 
        {
            this.map = map;
        }

        public IEnumerable<Player> CreatePlayers(IEnumerable<PlayerIndex> playerIndexes, IEnumerable<Ship> ships, IEnumerable<Island> homeIslands) {
            return playerIndexes.Zip3(ships, homeIslands, (p, s, i) => CreatePlayer(p, s, i)).ToList();
        }

        public Player CreatePlayer(PlayerIndex playerIndex, Ship ship, Island homeIsland)
        {
            return new Player(playerIndex, ship, homeIsland, map);
        }
    }
}
