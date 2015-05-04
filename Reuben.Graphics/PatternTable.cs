using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reuben.NESGraphics
{
    public class PatternTable
    {
        public Tile[,] Tiles { get; set; }

        public PatternTable()
        {
            Tiles = new Tile[16, 16];
        }
        public Tile GetTileByIndex(int index)
        {
            return Tiles[index % 16, index / 16];
        }

        public Tile GetTile(int x, int y)
        {
            return Tiles[x, y];
        }

        public void SetTile(int x, int y, Tile tile)
        {
            Tiles[x, y] = tile;
        }
    }
}
