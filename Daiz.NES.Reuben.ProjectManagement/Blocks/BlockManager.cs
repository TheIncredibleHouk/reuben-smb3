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

        public BlockManager()
        {
            lookupTable = new Dictionary<int, BlockDefinition>();
            for (int i = 0; i < 15; i++)
            {
                lookupTable.Add(i, null);
            }

            LoadDefault();
        }

        public List<BlockDefinition> AllDefinitions
        {
            get { return lookupTable.Values.ToList(); }
        }

        public string GetBlockString(int defIndex, int block)
        {
            return lookupTable[defIndex][block].Description + " (" + block.ToHexString() + ")\n" + lookupTable[defIndex][block].BlockProperty.GetString();
        }

        public void SetBlockString(int defIndex, int block, string description)
        {
            lookupTable[defIndex][block].Description =  description;
        }

        public void LoadBlockStrings(string fileName)
        {
            if (File.Exists(fileName))
            {
                XDocument xDoc = XDocument.Load(fileName);
                foreach (XElement x in xDoc.Element("strings").Elements("set"))
                {
                    int levelType = x.Attribute("leveltype").Value.ToIntFromHex();
                    foreach (XElement e in x.Elements("block"))
                    {
                        lookupTable[levelType][e.Attribute("value").Value.ToIntFromHex()].Description = e.Value;
                    }
                }
            }
        }

        public void SaveBlockStrings(string fileName)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("strings");
            foreach (int k in lookupTable.Keys)
            {
                XElement s = new XElement("set");
                s.SetAttributeValue("leveltype", k.ToHexString());
                for (int i = 0; i < 256; i++)
                {
                    XElement b = new XElement("block");
                    b.SetAttributeValue("value", i.ToHexString());
                    b.SetValue(lookupTable[k][i].Description);
                    s.Add(b);
                }
                root.Add(s);
            }

            xDoc.Add(root);
            xDoc.Save(fileName);
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
            byte[] data = new byte[0x4CE0];

            fs.Read(data, 0, (int)fs.Length);
            fs.Close();


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

            
            for (int i = 0; i < 15; i++)
            {
                l = 0x4A00 + (i * 0x20);
                lookupTable[i].FireBallTransitions.Clear();
                for (int k = 0; k < 4; k++)
                {
                    lookupTable[i].FireBallTransitions.Add(new BlockTransition(data[l++], data[l++]));
                }

                lookupTable[i].IceBallTransitions.Clear();
                for (int k = 0; k < 4; k++)
                {
                    lookupTable[i].IceBallTransitions.Add(new BlockTransition(data[l++], data[l++]));
                }

                lookupTable[i].HammerTransitions.Clear();
                for (int k = 0; k < 4; k++)
                {
                    lookupTable[i].HammerTransitions.Add(new BlockTransition(data[l++], data[l++]));
                }

                lookupTable[i].PSwitchTransitions.Clear();
                for (int k = 0; k < 8; k++)
                {
                    lookupTable[i].PSwitchTransitions.Add(new BlockTransition(data[l++], data[l++]));
                }
            }

            return true;
        }

        public void ResetTSA(int levelType)
        {
            lookupTable.Clear();
            byte[] data = Resource.default_tsa;

            BlockDefinition bd = new BlockDefinition();
            int bankOffset = levelType * 0x400;
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
            byte[] data = new byte[0x4CE0];
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
                    data[dataPointer++] = (byte)lookupTable[i][j].BlockProperty;
                }
            }

            for (int i = 0; i < 15; i++)
            {
                dataPointer = 0x4A00 + (i * 0x20);
                foreach(var k in lookupTable[i].FireBallTransitions)
                {
                    data[dataPointer++] = (byte)k.FromValue;
                    data[dataPointer++] = (byte)k.ToValue;
                }

                foreach (var k in lookupTable[i].IceBallTransitions)
                {
                    data[dataPointer++] = (byte)k.FromValue;
                    data[dataPointer++] = (byte)k.ToValue;
                }

                foreach (var k in lookupTable[i].HammerTransitions)
                {
                    data[dataPointer++] = (byte)k.FromValue;
                    data[dataPointer++] = (byte)k.ToValue;
                }

                foreach (var k in lookupTable[i].PSwitchTransitions)
                {
                    data[dataPointer++] = (byte)k.FromValue;
                    data[dataPointer++] = (byte)k.ToValue;
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
