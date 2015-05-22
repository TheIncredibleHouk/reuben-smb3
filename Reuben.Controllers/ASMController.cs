using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuben.Controllers
{
    public class ASMController
    {
        List<string> allAsmLines;
        public void LoadSymbolsFile(string file)
        {
            allAsmLines = File.ReadAllLines(file).ToList();
        }
    }
}
