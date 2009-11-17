using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class GraphicsBank
    {
        private Tile[,] _Tiles;

        public GraphicsBank()
        {
            _Tiles = new Tile[16, 4];
        }

        public Tile this[int x, int y]
        {
            get { return _Tiles[x, y]; }
        }

        public Tile this[int index]
        {
            get { return _Tiles[index % 16, index / 16]; }
            set { _Tiles[index % 16, index / 16] = value; }
        }

        public byte[] GetInterpolatedData()
        {
            byte[] returnData = new byte[1024];

            int dataPointer = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    byte[] tileData = _Tiles[j, i].GetInterpolatedData();
                    for (int k = 0; k < 16; k++)
                    {
                        returnData[dataPointer++] = tileData[k];
                    }
                }
            }

            return returnData;
        }
    }
}
