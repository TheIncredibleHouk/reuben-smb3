using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reuben.NESGraphics;

namespace Reuben.Controllers
{
    public static class PatternTableFactory
    {
        public static PatternTable MakePatternTable(GraphicsController controller, List<int> banks)
        {
            PatternTable patternTable = new PatternTable();
            foreach(int bank in banks)
            {
                for(int x = 0; x < 16; x++)
                {
                    for(int y = 0; y < 4; y++)
                    {
                        patternTable.SetTile(x, y, controller.GetTileByBankIndex(bank, y * 4 + x));
                    }
                }
            }

            return patternTable;
        }
    }
}
