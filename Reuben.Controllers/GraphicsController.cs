using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Reuben.Model;
using Reuben.NESGraphics;

namespace Reuben.Controllers
{
    public class GraphicsController
    {
        private Color[] colorReference;
        private List<Palette> palettes;
        private Tile[] graphicTiles;

        public GraphicsController()
        {
            colorReference = new Color[0x40];
            palettes = new List<Palette>();
            graphicTiles = new Tile[16 * 4 * 256]; // 16 tiles, 4 rows per bank, 256 banks
        }

        public void LoadGraphicsFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("File not found.");
            }
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            byte[] graphicsData = new byte[fs.Length];

            fs.Read(graphicsData, 0, (int)fs.Length);
            fs.Close();
            for (int dataPointer = 0, i = 0; dataPointer < graphicTiles.Length; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    byte[] nextTileChunk = new byte[16];
                    for (int k = 0; k < 16; k++)
                    {
                        nextTileChunk[dataPointer] = graphicsData[dataPointer++];
                    }
                    graphicTiles[i] = new Tile(nextTileChunk);
                }
            }
        }

        public Tile GetTileByBankIndex(int bank, int index)
        {
            return graphicTiles[(bank * (16 * 4)) + index];
        }

        public Color GetColorReferenceByIndex(int index)
        {
            if (index < 0 || index > 0x40)
            {
                index = 0;
            }

            return colorReference[index];
        }

        public IEnumerable<Palette> GetPalettes()
        {
            return palettes.AsReadOnly();
        }
    }
}
