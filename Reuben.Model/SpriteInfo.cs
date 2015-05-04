using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuben.Model
{
    public class SpriteInfo
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int Palette { get; set; }
        public bool HorizontalFlip { get; set; }
        public bool VerticalFlip { get; set; }
        public int Table { get; set; }
        public string Name { get; private set; }
        public List<int> Property { get; private set; }
    }
}
