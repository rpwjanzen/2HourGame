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
        public enum PlayerViewPosition { UpperLeft = 0, UpperRight, LowerLeft, LowerRight }

        List<Ship> ships;
        List<PlayerViewPosition> positions;

        const int screenWidth = 1280;
        const int screenHeight = 720;

        public PlayerViewManager(List<Ship> ships, List<PlayerViewPosition> positions) 
        {
            this.ships = ships;
            this.positions = positions;
        }

        public Vector2 drawOffset(PlayerViewPosition playerViewPosition)
        {
            //return getScreenOffset(playerViewPosition) - ships[positions.IndexOf(playerViewPosition)].Position - new Vector2(screenWidth / 2f, screenHeight / 2f);
            return -ships[positions.IndexOf(playerViewPosition)].Position + new Vector2(screenWidth / 2f, screenHeight / 2f) + getScreenOffset(playerViewPosition);
        }

        private Vector2 getScreenOffset(PlayerViewPosition playerViewPosition)
        {
            switch (playerViewPosition)
            {
                case PlayerViewPosition.UpperLeft:
                    return new Vector2(-screenWidth / 4, -screenHeight / 4);
                case PlayerViewPosition.UpperRight:
                    return new Vector2(screenWidth / 4, -screenHeight / 4);
                case PlayerViewPosition.LowerLeft:
                    return new Vector2(-screenWidth / 4, screenHeight / 4);
                case PlayerViewPosition.LowerRight:
                    return new Vector2(screenWidth / 4, screenHeight / 4);
                default:
                    return Vector2.Zero;
            }
        }

        public Vector2 getPlayerScreenOffset(PlayerViewPosition playerViewPosition)
        {
            switch (playerViewPosition)
            {
                case PlayerViewPosition.UpperLeft:
                    return new Vector2(0, 0);
                case PlayerViewPosition.UpperRight:
                    return new Vector2(screenWidth / 2, 0);
                case PlayerViewPosition.LowerLeft:
                    return new Vector2(0, screenHeight / 2);
                case PlayerViewPosition.LowerRight:
                    return new Vector2(screenWidth / 2, screenHeight / 2);
                default:
                    return Vector2.Zero;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new PlayerViewManagerEnumerator();
        }

    }

    public class PlayerViewManagerEnumerator : IEnumerator 
    {
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public PlayerViewManagerEnumerator()
        {}

        public bool MoveNext()
        {
            position++;
            return (position < 4);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return (PlayerViewManager.PlayerViewPosition)position;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }
}
