using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reuben.UI
{
    public class SingleTileChange
    {
        public int X;
        public int Y;
        public int Tile;

        public SingleTileChange(int x, int y, int tile)
        {
            X = x;
            Y = y;
            Tile = tile;
        }
    }
}
