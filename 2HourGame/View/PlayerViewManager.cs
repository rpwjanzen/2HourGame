using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//using _2HourGame.View.GameServices;
using _2HourGame.Model;

namespace _2HourGame.View
{
    class PlayerViewManager : IEnumerable
    {
        //public enum PlayerViewPosition { UpperLeft = 0, UpperRight, LowerLeft, LowerRight }
        public enum PlayerView { One = 0, Two, Three, Four }

        List<Ship> ships;
        List<PlayerView> views;

        // map squares (to be drawn to)
        const int mapSquareWidth = 80;
        const int mapSquareHeight = 45;
        MapSquare[,] mapSquares;

        // position on screen to draw each ship
        List<Vector2> shipScreenPosition;
        List<List<MapSquare>> shipMapSquares;

        const int screenWidth = 1280;
        const int screenHeight = 720;

        public PlayerViewManager(List<Ship> ships, List<PlayerView> positions) 
        {
            this.ships = ships;
            this.views = positions;

            shipScreenPosition = new List<Vector2>();
            shipMapSquares = new List<List<MapSquare>>();
            mapSquares = new MapSquare[mapSquareWidth, mapSquareHeight];
            for(int x = 0; x < mapSquareWidth; x++)
            {
                for(int y = 0; y < mapSquareHeight; y++)
                {
                    mapSquares[x, y] = new MapSquare(x, y);
                }
            }
        }

        /// <summary>
        /// Initialize the PlayerViewManager so that its ready to draw.
        /// </summary>
        public void prepareToDraw() 
        {
            // Get the Central Point of all the ships (should also be the center of the screen).
            Vector2 center = Vector2.Zero;
            foreach (Ship s in ships)
                center += s.Position;
            center = center / ships.Count;

            // Get the new positions of all the ships.
            // For now I will treat the screen as a circle to make the math easier but this should be upgraded later.
            shipScreenPosition.Clear();
            foreach (Ship s in ships) 
            {
                Vector2 relativeShipPos = s.Position - center;
                float shrinkRatio = relativeShipPos.Length() / (screenHeight / 2);
                //double radiansFromCenterToShip = Math.Atan(Convert.ToDouble(relativeShipPos.X / relativeShipPos.Y));
                //shipScreenPosition.Add(center + new Vector2((float)Math.Sin(radiansFromCenterToShip) * screenHeight / 2, (float)Math.Cos(radiansFromCenterToShip) * screenHeight / 2));
                //shipScreenPosition.Add(center + (relativeShipPos / shrinkRatio));
                shipScreenPosition.Add(new Vector2(screenWidth / 2f, screenHeight / 2f) + (relativeShipPos / shrinkRatio));
            }

            // Each square goes to the closest ship (based on relative ship position), this may not be optimal but can be changed later.
            shipMapSquares.Clear();
            foreach (Ship s in ships)
                shipMapSquares.Add(new List<MapSquare>());
            for (int x = 0; x < mapSquareWidth; x++) 
            {
                for (int y = 0; y < mapSquareHeight; y++) 
                {
                    shipMapSquares[ships.IndexOf(getClosestShip(mapSquares[x, y]))].Add(mapSquares[x, y]);
                }
            }
        }

        public List<MapSquare> getPlayerMapSquares(PlayerView playerView) 
        {
            return shipMapSquares[(int)playerView];
        }

        //public Vector2 getPlayerScreenPosition(PlayerView playerView) 
        //{
        
        //}

        private Ship getClosestShip(MapSquare mapSquare) 
        {
            Ship closestShip = ships[0];
            double closestDistance = Math.Abs((double)(mapSquare.center - shipScreenPosition[0]).Length());

            for (int i = 1; i < ships.Count; i++) 
            {
                double distance = Math.Abs((double)(mapSquare.center - shipScreenPosition[i]).Length());
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestShip = ships[i];
                }
            }

            return closestShip;
        }

        public Vector2 drawOffset(PlayerView playerView)
        {
            //return -ships[views.IndexOf(playerView)].Position + new Vector2(screenWidth / 2f, screenHeight / 2f) +getScreenOffset(playerViewPosition);
            return shipScreenPosition[(int)playerView] - ships[views.IndexOf(playerView)].Position;// + new Vector2(screenWidth / 4f, screenHeight / 4f);// shipScreenPosition[(int)playerView] 
        }

        //private Vector2 getScreenOffset(PlayerView playerView)
        //{
        //    switch (playerViewPosition)
        //    {
        //        case PlayerViewPosition.UpperLeft:
        //            return new Vector2(-screenWidth / 4, -screenHeight / 4);
        //        case PlayerViewPosition.UpperRight:
        //            return new Vector2(screenWidth / 4, -screenHeight / 4);
        //        case PlayerViewPosition.LowerLeft:
        //            return new Vector2(-screenWidth / 4, screenHeight / 4);
        //        case PlayerViewPosition.LowerRight:
        //            return new Vector2(screenWidth / 4, screenHeight / 4);
        //        default:
        //            return Vector2.Zero;
        //    }
        //}

        //public Vector2 getPlayerScreenOffset(PlayerViewPosition playerViewPosition)
        //{
        //    switch (playerViewPosition)
        //    {
        //        case PlayerViewPosition.UpperLeft:
        //            return new Vector2(0, 0);
        //        case PlayerViewPosition.UpperRight:
        //            return new Vector2(screenWidth / 2, 0);
        //        case PlayerViewPosition.LowerLeft:
        //            return new Vector2(0, screenHeight / 2);
        //        case PlayerViewPosition.LowerRight:
        //            return new Vector2(screenWidth / 2, screenHeight / 2);
        //        default:
        //            return Vector2.Zero;
        //    }
        //}

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PlayerViewManagerEnumerator();
        }

    }


    /// <summary>
    /// Represents a single square of map vision.
    /// </summary>
    class MapSquare
    {
        const float mapSquareSize = 16;

        public int x { get; private set; }
        public int y { get; private set; }
        public Vector2 origin { get; private set; }
        public Vector2 center
        {
            get
            {
                return origin + new Vector2(mapSquareSize/2, mapSquareSize/2);
            }
        }
        public MapSquare(int x, int y) 
        {
            this.x = x;
            this.y = y;
            origin = new Vector2(x * mapSquareSize, y * mapSquareSize);
        }
    }

    public class PlayerViewManagerEnumerator : IEnumerator 
    {
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int view = -1;

        public PlayerViewManagerEnumerator()
        {}

        public bool MoveNext()
        {
            view++;
            return (view < 4);
        }

        public void Reset()
        {
            view = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return (PlayerViewManager.PlayerView)view;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }
}
