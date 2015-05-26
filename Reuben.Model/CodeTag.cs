using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reuben.Model
{
    public class CodeTag
    {
        public string File { get; set; }
        public string Tag { get; set; }

        public CodeTag(string file, string tag)
        {
            File = file;
            Tag = tag;
        }
    }
}
