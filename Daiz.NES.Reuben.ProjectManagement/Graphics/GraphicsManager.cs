using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Reuben.UI.ProjectManagement
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
        }

        public void CheckGraphicsFileChange(string fileName)
        {
            if (LastModified < File.GetLastWriteTime(fileName))
            {
                LoadGraphics(fileName);
                if (GraphicsUpdated != null)
                {
                    GraphicsUpdated(null, null);
                }
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

        
    }
}
