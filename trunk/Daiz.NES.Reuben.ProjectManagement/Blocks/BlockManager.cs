using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class BlockManager
    {
        public event EventHandler DefinitionsSaved;

        private Dictionary<int, BlockDefinition> lookupTable;
        private Dictionary<int, Dictionary<int, string>> blockStrings;

        public BlockManager()
        {
            lookupTable = new Dictionary<int, BlockDefinition>();
            blockStrings = new Dictionary<int, Dictionary<int, string>>();
            for (int i = 0; i < 15; i++)
            {
                lookupTable.Add(i, null);
            }

            LoadDefault();
            LoadBlockStrings();
        }

        public List<BlockDefinition> AllDefinitions
        {
            get { return lookupTable.Values.ToList(); }
        }

        public string GetBlockString(int defIndex, int block)
        {

            if (blockStrings[defIndex].ContainsKey(block))
                return blockStrings[defIndex][block];

            return "Tile";
        }

        private void LoadBlockStrings()
        {
            XDocument xDoc = XDocument.Parse(Resource.blockstrings);
            foreach (XElement x in xDoc.Element("strings").Elements("set"))
            {
                int levelType = x.Attribute("leveltype").Value.ToIntFromHex();
                blockStrings.Add(levelType, new Dictionary<int, string>());
                foreach (XElement e in x.Elements("block"))
                {
                    blockStrings[levelType].Add(e.Attribute("value").Value.ToIntFromHex(), e.Value);
                }
            }
        }

        public void LoadDefault()
        {
            lookupTable.Clear();
            byte[] data = Resource.default_tsa;

            for (int i = 0; i < 15; i++)
            {
                BlockDefinition bd = new BlockDefinition();
                int bankOffset = i * 0x400;
                for (int j = 0; j < 256; j++)
                {
                    bd[j][0, 0] = data[bankOffset + j];
                    bd[j][0, 1] = data[bankOffset + 0x100 + j];
                    bd[j][1, 0] = data[bankOffset + 0x200 + j];
                    bd[j][1, 1] = data[bankOffset + 0x300 + j];
                }
                lookupTable.Add(i, bd);
            }
        }

        public bool LoadDefinitions(string filename)
        {
            if (!File.Exists(filename)) return false;

            lookupTable.Clear();
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);
            byte[] data = new byte[0x4B00];

            fs.Read(data, 0, (int)fs.Length);
            fs.Close();


            for (int i =  0; i < 15; i++)
            {
                BlockDefinition bd = new BlockDefinition();
                int bankOffset = i * 0x400;
                for (int j = 0; j < 256; j++)
                {
                    bd[j][0, 0] = data[bankOffset + j];
                    bd[j][0, 1] = data[bankOffset + 0x100 + j];
                    bd[j][1, 0] = data[bankOffset + 0x200 + j];
                    bd[j][1, 1] = data[bankOffset + 0x300 + j];
                }
                lookupTable[i] = bd;
            }

            var l = 0x3C00;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    lookupTable[i][j].BlockProperty = (BlockProperty)data[l++];
                }
            }

            return true;
        }

        public void ResetTSA(int levelType)
        {
            lookupTable.Clear();
            byte[] data = Resource.default_tsa;

            BlockDefinition bd = new BlockDefinition();
            int bankOffset = levelType *0x400;
            for (int j = 0; j < 256; j++)
            {
                bd[j][0, 0] = data[bankOffset + j];
                bd[j][0, 1] = data[bankOffset + 0x100 + j];
                bd[j][1, 0] = data[bankOffset + 0x200 + j];
                bd[j][1, 1] = data[bankOffset + 0x300 + j];
            }
            lookupTable[levelType] = bd;
        }

        public BlockDefinition GetDefiniton(int gameID)
        {
            return lookupTable[gameID];
        }

        public bool SaveDefinitions(string filename)
        {
            byte[] data = new byte[0x4B00];
            int dataPointer = 0;

            for (int i = 0; i < 15; i++)
            {
                byte[] blockData = lookupTable[i].GetBlockData();
                for (int j = 0; j < 0x400; j++)
                {
                    data[dataPointer++] = blockData[j];
                }
            }

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 0x100; j++)
                {
                    data[dataPointer++] = (byte) lookupTable[i][j].BlockProperty;
                }
            }

            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Close();

            if (DefinitionsSaved != null) DefinitionsSaved(this, null);

            return true;
        }
    }
}
