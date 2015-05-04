using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class PatternTable
    {
        public event EventHandler<TEventArgs<int>> GraphicsChanged;
        private Tile[,] _TileData;

        public PatternTable()
        {
            _TileData = new Tile[16, 16];
        }

        public void SetGraphicsbank(int index, GraphicsBank bank)
        {
            int limit = (index + 1) * 4;
            for (int y = 0, i = index * 4; i < limit; i++, y++)
            {
                for (int j = 0, x = 0; j < 16; j++, x++)
                {
                    _TileData[j, i] = bank[x, y];
                }
            }

            if (GraphicsChanged != null) GraphicsChanged(this, new TEventArgs<int>(index));
        }

        public Tile this[int index]
        {
            get
            {
                return _TileData[index % 16, index / 16];
            }
        }

        public Tile this[int x, int y]
        {
            get { return _TileData[x, y]; }
            set { _TileData[x, y] = value; }
        }
    }
}
