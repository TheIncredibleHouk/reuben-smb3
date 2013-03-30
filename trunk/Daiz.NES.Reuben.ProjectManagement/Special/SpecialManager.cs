using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpecialManager
    {
        public PaletteInfo SpecialPalette { get; private set; }
        public PatternTable SpecialTable { get; private set; }
        private List<GraphicsBank> SpecialBanks;

        public SpecialManager()
        {
            SpecialPalette = new PaletteInfo();
            SpecialBanks = new List<GraphicsBank>();
        }

        public bool LoadSpecialGraphics(string fileName)
        {
            if (!File.Exists(fileName)) return false;
            int dataPointer = 0;
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int) fs.Length);
            fs.Close();

            SpecialBanks.Clear();
            for (int i = 0; i < 4; i++)
            {
                GraphicsBank nextBank = new GraphicsBank();
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++) nextTileChunk[k] = data[dataPointer++];
                    nextBank[j] = new Tile(nextTileChunk);
                }
                SpecialBanks.Add(nextBank);
            }

            SpecialTable = new PatternTable();
            for (int j = 0; j < 4; j++)
            {
                SpecialTable.SetGraphicsbank(j, SpecialBanks[j]);
            }
            return true;
        }

        public void LoadDefaultSpecialGraphics()
        {
            int dataPointer = 0;
            byte[] data = Resource.special_graphics;

            SpecialBanks.Clear();
            for (int i = 0; i < 4; i++)
            {
                GraphicsBank nextBank = new GraphicsBank();
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++) nextTileChunk[k] = data[dataPointer++];
                    nextBank[j] = new Tile(nextTileChunk);
                }
                SpecialBanks.Add(nextBank);
            }

            SpecialTable = new PatternTable();
            for (int j = 0; j < 4; j++)
            {
                SpecialTable.SetGraphicsbank(j, SpecialBanks[j]);
            }
        }

        public bool SaveGraphics(string filename)
        {
            byte[] Data = new byte[SpecialBanks.Count * 0x400];

            int dataPointer = 0;
            foreach (var b in SpecialBanks)
            {
                byte[] bankData = b.GetInterpolatedData();
                for (int i = 0; i < 1024; i++)
                {
                    Data[dataPointer++] = bankData[i];
                }
            }
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(Data, 0, Data.Length);
            fs.Close();
            return true;
        }


        public bool LoadSpecialDefinitions(string filename)
        {
            if (!File.Exists(filename)) return false;
            
            XDocument xDoc = XDocument.Load(filename);
            XElement e = xDoc.Element("specials");

            SpecialPalette.LoadFromElement(e.Element("palette"));
            SpecialPalette.IsSpecial = true;
            return true;
        }

        public void LoadDefaultSpecials()
        {
            XDocument xDoc = XDocument.Parse(Resource.special_definitions);
            XElement root = xDoc.Element("specials");

            SpecialPalette = new PaletteInfo();
            SpecialPalette.LoadFromElement(root.Element("palette"));
            SpecialPalette.IsSpecial = true;
        }

        public void SaveSpecials(string filename1)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("specials");

            root.Add(SpecialPalette.CreateElement());
            xDoc.Add(root);
            xDoc.Save(filename1);
        }

        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                LoadSpecialDefinitions(fileName);
            }
            else
            {
                LoadDefaultSpecials();
            }

            if (File.Exists(fileName))
            {
                LoadSpecialGraphics(fileName);
            }
            else
            {
                LoadDefaultSpecialGraphics();
            }
        }
    }
}
