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
    public class TextLocation
    {
        public string Text { get; set; }
        public int LineNumber { get; set; }
    }

    public class ASMController
    {
        private Dictionary<string, string[]> codeFiles;
        private Dictionary<string, string> codeFileNames;

        public ASMController()
        {
            codeFiles = new Dictionary<string, string[]>();
            codeFileNames = new Dictionary<string, string>();
        }

        public List<string> GetFileList()
        {
            return codeFileNames.Keys.ToList();
        }

        public string GetFile(string fileName)
        {
            if (codeFiles.ContainsKey(fileName))
            {
                return string.Join("\r\n", codeFiles[fileName]);
            }

            return null;
        }

        public string FindTagLine(string file, string text)
        {
            // find offset - #ObjectsInit@39
            //  ;#ObjectsInit.word@28

            //      .word DSKFWEERD
            string[] split1 = text.Split('@'); // "#ObjectsInit, 39
            int myOffset = Convert.ToInt32(split1[1].Substring(0, 2), 16); // 0x39

            TextLocation tagLine = FindTextByLine(0, file, split1[0]); // ;#ObjectsInit.word@28
            string[] split2 = tagLine.Text.Split('.', '@'); // ;#ObjectsInit, word, 28

            int startOffset = Convert.ToInt32(split2[2].Substring(0, 2), 16); // 0x28
            int actualOffset = (myOffset - startOffset) + 1; // 0x11

            TextLocation foundLine = FindTextByLine(tagLine.LineNumber, file, split2[1], actualOffset);
            if (foundLine != null)
            {
                return foundLine.Text;
            }

            return null;

        }

        private TextLocation FindTextByLine(int startPlace, string file, string text, int offset = 0)
        {
            string[] lines = codeFiles[file];
            TextLocation location = new TextLocation();
            for (int i = startPlace; i < lines.Length; i++)
            {
                if (lines[i].Contains(text))
                {
                    if (offset == 0)
                    {
                        location.Text = lines[i];
                        location.LineNumber = i;
                        return location;
                    }

                    offset--;
                }
            }

            return null;
        }


        public void Load(string directory)
        {
            codeFiles.Clear();
            codeFileNames.Clear();
            if (File.Exists(directory + @"\smb3.asm"))
            {
                codeFiles["smb3.asm"] = File.ReadAllLines(directory + @"\smb3.asm");
                codeFileNames["smb3.asm"] = directory + @"\smb3.asm";
            }

            foreach (string file in Directory.GetFiles(directory + @"\PRG"))
            {
                codeFiles[Path.GetFileName(file)] = File.ReadAllLines(file);
                codeFileNames[Path.GetFileName(file)] = file;
            }
        }

        public void Save(string file, string contents)
        {
            File.WriteAllText(codeFileNames[file], contents);
            codeFiles[Path.GetFileName(file)] = File.ReadAllLines(codeFileNames[file]);
        }
        public List<String> ParseSymbols(string text)
        {
            var noMatch = new Regex("(^\\s*;)");
            var match = new Regex("[A-Za-z0-9_]+(?=\\:)|[A-Za-z0-9_]+\\s*(?=\\=)");

            List<string> lines = text.Split('\n').ToList();
            List<string> symbols = new List<string>();
            foreach (string s in lines)
            {
                if (noMatch.Match(s).Value == "")
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
