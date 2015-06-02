using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using Reuben.Model;
using System.Drawing;

namespace Reuben.Controllers
{
    public class SpriteController
    {
        public SpriteData SpriteData { get; set; }
        private Dictionary<int, Rectangle> boundCache;
        private string lastFile;

        public SpriteController()
        {
            SpriteData = new SpriteData();
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            lastFile = fileName;
            SpriteData = JsonConvert.DeserializeObject<SpriteData>(File.ReadAllText(fileName));
            boundCache = new Dictionary<int, Rectangle>();
            foreach (SpriteDefinition definition in SpriteData.Definitions)
            {
                int minX = 1000, maxX = 0, minY = 1000, maxY = 0;
                bool useOverlays = definition.SpriteInfo.Where(s => !s.Overlay).Count() == 0;
                foreach (SpriteInfo info in definition.SpriteInfo)
                {
                    if (info.Overlay && !useOverlays)
                    {
                        continue;
                    }

                    if (info.X < minX)
                    {
                        minX = info.X;
                    }

                    if (info.X + 8 > maxX)
                    {
                        maxX = info.X + 8;
                    }

                    if (info.Y < minY)
                    {
                        minY = info.Y;
                    }

                    if (info.Y + 16 > maxY)
                    {
                        maxY = info.Y + 16;
                    }
                }

                boundCache[definition.GameID] = new Rectangle(minX, minY, maxX - minX - 1, maxY - minY - 1);
            }
        }

        public void Save()
        {
            Save(lastFile);
        }

        public void Save(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(SpriteData));
        }

        public SpriteDefinition GetDefinition(int spriteid)
        {
            return SpriteData.Definitions.Where(s => s.GameID == spriteid).FirstOrDefault();
        }

        public Rectangle GetClipBounds(Sprite sprite)
        {
            Rectangle r = boundCache[sprite.ObjectID];
            return new Rectangle(r.X + sprite.X * 16, r.Y + sprite.Y * 16, r.Width, r.Height);
        }


        public IEnumerable<Rectangle> GetClipBounds(IEnumerable<Sprite> sprites)
        {
            return sprites.Select(s => GetClipBounds(s));
        }

        public IEnumerable<Tuple<Sprite, Rectangle>> GetBounds(IEnumerable<Sprite> sprites)
        {
            return sprites.Select(s => new Tuple<Sprite, Rectangle>(s, GetClipBounds(s)));
        }
    }
}
