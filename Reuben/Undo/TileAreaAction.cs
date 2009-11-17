using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben
{
    public class TileAreaAction : IUndoableAction
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public byte[,] Data { get; private set; }

        public TileAreaAction(int x, int y, byte[,] data)
        {
            X = x;
            Y = y;
            Data = data;
        }

        #region IUndoableAction Members

        public ActionType Type
        {
            get { return ActionType.TileArea; }
        }

        #endregion
    }
}
