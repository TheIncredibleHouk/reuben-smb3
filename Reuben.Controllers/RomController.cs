using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuben.Controllers
{
    public class RomController
    {
        public bool BuildRom(string directory)
        {
            if (!File.Exists(directory + @"\nesasm.exe"))
            {
                throw new Exception("nesasm.exe file not found.");
            }

            Process.Start(directory + @"\nesasm.exe smb3.asm");
        }
    }
}
