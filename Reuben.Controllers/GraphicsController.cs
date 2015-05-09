using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

using Reuben.Model;
using Reuben.NESGraphics;

namespace Reuben.Controllers
{
    public class GraphicsController
    {
        public GraphicsData GraphicsData { get; private set; }
        public Tile[] Tiles { get; private set; }

        private string lastFile;
        public GraphicsController()
        {

            Tiles = new Tile[16 * 4 * 256]; // 16 tiles, 4 rows per bank, 256 banks
            GraphicsData = new GraphicsData();
        }

        public PatternTable MakePatternTable(List<int> banks)
        {
            PatternTable patternTable = new PatternTable();
            int patternSection = 0;
            foreach (int bank in banks)
            {
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 16; col++)
                    {
                        patternTable.SetTile(col, row + (patternSection * 4), GetTileByBankIndex(bank, (row * 16 + col)));
                    }
                }
                patternSection++;
            }

            return patternTable;
        }

        public void LoadGraphics(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File not found.");
            }
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] graphicsData = new byte[fs.Length];

            fs.Read(graphicsData, 0, (int)fs.Length);
            fs.Close();
            int dataPointer = 0;
            for (int i = 0; i < Tiles.Length; i++)
            {
                byte[] nextTileChunk = new byte[16];
                for (int k = 0; k < 16; k++)
                {
                    nextTileChunk[k] = graphicsData[dataPointer++];
                }
                Tiles[i] = new Tile(nextTileChunk);
            }

        }

        public void LoadPalettes(string fileName)
        {
            lastFile = fileName;
            GraphicsData = JsonConvert.DeserializeObject<GraphicsData>(File.ReadAllText(fileName));
        }

        public void SavePalettes()
        {
            SavePalettes(lastFile);
        }

        public void SavePalettes(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(GraphicsData));
        }

        public Tile GetTileByBankIndex(int bank, int index)
        {
            return Tiles[(bank * (16 * 4)) + index];
        }
    }
}
