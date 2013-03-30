using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class BlockTransition
    {
        public int FromValue { get; set; }
        public int ToValue { get; set; }

        public BlockTransition(int from, int to)
        {
            FromValue = from;
            ToValue = to;
        }
    }
}
