using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Reuben.UI
{
    public class MultiTileAction : IUndoableAction
    {
        private int LowestX, LowestY, HighestX, HighestY;
        public List<SingleTileChange> TileChanges { get; private set; }

        public MultiTileAction()
        {
            TileChanges = new List<SingleTileChange>();
            LowestX = LowestY = 3000;
            HighestX = HighestY = -1;
        }

        public void AddTileChange(int x, int y, int tile)
        {
            TileChanges.Add(new SingleTileChange(x, y, tile));
            if (x < LowestX) LowestX = x;
            if (x > HighestX) HighestX = x;
            if (y < LowestY) LowestY = y;
            if (y > HighestY) HighestY = y;
        }

        public Rectangle InvalidArea
        {
            get
            {
                return new Rectangle(LowestX, LowestY, HighestX - LowestX + 1, HighestY - LowestY + 1);
            }
        }

        #region IUndoableAction Members

        public ActionType Type
        {
            get { return ActionType.MultiTile; }
        }

        #endregion
    }
}
