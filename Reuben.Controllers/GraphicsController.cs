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

        public GraphicsController()
        {
            
            Tiles = new Tile[16 * 4 * 256]; // 16 tiles, 4 rows per bank, 256 banks
            GraphicsData = new GraphicsData();
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
            for (int dataPointer = 0, i = 0; dataPointer < Tiles.Length; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++)
                    {
                        nextTileChunk[dataPointer] = graphicsData[dataPointer++];
                    }
                    Tiles[i] = new Tile(nextTileChunk);
                }
            }
        }

        public void LoadPalettes(string fileName)
        {
            GraphicsData = JsonConvert.DeserializeObject<GraphicsData>(File.ReadAllText(fileName));
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
