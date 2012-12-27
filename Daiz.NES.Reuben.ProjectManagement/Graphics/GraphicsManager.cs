using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class GraphicsManager
    {
        public event EventHandler GraphicsUpdated;

        public List<GraphicsInfo> GraphicsInfo { get; private set; }
        public List<GraphicsBank> GraphicsBanks { get; private set; }
        private DateTime LastModified;

        public GraphicsManager()
        {
            GraphicsBanks = new List<GraphicsBank>();
            GraphicsInfo = new List<GraphicsInfo>();
            LoadDefault();
        }

        public void LoadDefault()
        {
            byte[] graphicsData = Resource.default_graphics;
            int dataPointer = 0;

            GraphicsBanks.Clear();

            for (int i = 0; i < 256; i++)
            {
                GraphicsBank nextBank = new GraphicsBank();
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++) nextTileChunk[k] = graphicsData[dataPointer++];
                    nextBank[j] = new Tile(nextTileChunk);
                }
                GraphicsBanks.Add(nextBank);
            }

            XDocument xDoc = XDocument.Parse(Resource.default_graphics_names);
            foreach (var e in xDoc.Element("graphicsinfo").Elements("graphics"))
            {
                GraphicsInfo gi = new GraphicsInfo();
                gi.LoadFromElement(e);
                GraphicsInfo.Add(gi);
            }
        }


        public PatternTable BuildPatternTable(int index)
        {
            PatternTable returnTable = new PatternTable();
            for (int j = 0; j < 4; j++)
            {
                returnTable.SetGraphicsbank(j, GraphicsBanks[j]);
            }

            return returnTable;
        }

        public PatternTable BuildPatternTable(int index1, int index2)
        {
            return null;
        }

        public PatternTable BuildPatternTable(int index1, int index2, int index3, int index4)
        {
            return null;
        }

        public bool LoadGraphics(string filename)
        {
            if (!File.Exists(filename)) return false;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] graphicsData = new byte[0x40000];

            fs.Read(graphicsData, 0, (int)fs.Length);
            fs.Close();
            int dataPointer = 0;

            GraphicsBanks.Clear();

            for (int i = 0; i < 256; i++)
            {
                GraphicsBank nextBank = new GraphicsBank();
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++) nextTileChunk[k] = graphicsData[dataPointer++];
                    nextBank[j] = new Tile(nextTileChunk);
                }
                GraphicsBanks.Add(nextBank);
            }

            LastModified = File.GetLastWriteTime(filename);
            return true;
        }

        public bool ImportGraphics(string filename)
        {
            if (!File.Exists(filename)) return false;
            bool IsChr = Path.GetExtension(filename) == "chr";
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            byte[] graphicsData = new byte[0x40000];
            int length, offset;

            if (!IsChr)
            {
                byte[] header = new byte[16];
                fs.Read(header, 0, 16);

                length = header[5] * 8192;
                offset = header[4] * 16384 + 16;
            }
            else
            {
                length = (int)fs.Length;
                offset = 0;
            }

            fs.Seek(offset, SeekOrigin.Begin);
            fs.Read(graphicsData, 0, length);
            fs.Close();
            int dataPointer = 0;

            GraphicsBanks.Clear();

            for (int i = 0; i < 256; i++)
            {
                GraphicsBank nextBank = new GraphicsBank();
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++) nextTileChunk[k] = graphicsData[dataPointer++];
                    nextBank[j] = new Tile(nextTileChunk);
                }
                GraphicsBanks.Add(nextBank);
            }

            if (GraphicsUpdated != null)
            {
                GraphicsUpdated(this, null);
            }

            LastModified = File.GetLastWriteTime(filename);
            //ProjectController.GraphicsManager.SaveGraphics(ProjectController.RootDirectory + @"\" + ProjectController.ProjectName + ".chr");
            return true;
        }

        public bool SaveGraphics(string filename)
        {
            return SaveGraphics(filename, true, false);
        }

        public bool SaveGraphics(string filename, bool notify, bool forced)
        {
            if (!forced && LastModified < File.GetLastWriteTime(filename))
            {
                return false;
            }

            byte[] Data = new byte[GraphicsBanks.Count * 0x400];

            int dataPointer = 0;
            foreach (var b in GraphicsBanks)
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

            if (notify)
            {
                if (GraphicsUpdated != null)
                {
                    GraphicsUpdated(this, null);
                }
            }

            LastModified = File.GetLastWriteTime(filename);
            return true;
        }

        public Tile QuickTileGrab(int bank, int tile)
        {
            if (bank > 0)
            {
                return GraphicsBanks[bank][tile % 0xC0];
            }
            else
            {
                return ProjectController.SpecialManager.SpecialTable[tile];
            }
        }
    }
}
