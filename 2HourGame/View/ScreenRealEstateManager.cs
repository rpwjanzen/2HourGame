using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2HourGame.View
{
    class ScreenRealEstateManager
    {
        // 32 x 24 makes 32 pixel squares
        const int width = 32;
        const int height = 24;
        public ScreenBlock[,] screenBlocks = new ScreenBlock[width, height];

        public ScreenRealEstateManager()
        {
            for(int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++) 
                {
                    screenBlocks[x, y] = new ScreenBlock(x, y);
                }
            }
        }
    }

    struct ScreenBlock 
    {
        int x;
        int y;

        public ScreenBlock(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }
    }
}
