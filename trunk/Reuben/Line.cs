using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Daiz.NES.Reuben
{
    public class Line
    {
        public Point Start;
        public Point End;
        public Line(int x1, int y1, int x2, int y2)
        {
            Start.X = x1;
            Start.Y = y1;
            End.X = x2;
            End.Y = y2;
        }
    }
}
