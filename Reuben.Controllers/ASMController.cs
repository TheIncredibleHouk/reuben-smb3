using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Reuben.Model;

namespace Reuben.Controllers
{
    public class ASMController
    {
        public List<String> ParseSymbols(string text)
        {
            var noMatch = new Regex("(^\\s*;)");
            var match = new Regex("[A-Za-z0-9_]+(?=\\:)|[A-Za-z0-9_]+\\s*(?=\\=)");

            List<string> lines = text.Split('\n').ToList();
            List<string> symbols = new List<string>();
            foreach(string s in lines)
            {
                if(noMatch.Match(s).Value == "")
                {
                    var m = match.Match(s);
                    if (m.Value != "" && m.Value.Length > 3)
                    {
                        symbols.Add(m.Value.Trim());
                    }
                }
            }

            return symbols;
        }
    }
}
