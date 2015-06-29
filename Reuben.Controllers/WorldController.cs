using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Reuben.Model;

namespace Reuben.Controllers
{
    public class WorldController
    {
        public WorldData WorldData { get; private set; }

        public WorldController()
        {
            WorldData = new WorldData();
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            WorldData = JsonConvert.DeserializeObject<WorldData>(File.ReadAllText(fileName));
        }

        public void Save(string fileName)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(WorldData));
        }


        public World LoadWorld(string fileName)
        {
            return JsonConvert.DeserializeObject<World>(File.ReadAllText(fileName));
        }

        public WorldInfo GetWorldByID(Guid id)
        {
            return WorldData.Worlds.Where(w => w.ID == id).FirstOrDefault();
        }

        public void SaveWorld(World world)
        {
            WorldInfo info = GetWorldByID(world.ID);
            File.WriteAllText(info.File, JsonConvert.SerializeObject(world));
        }

        public byte[] GetCompressedData(World world)
        {
            byte[] data = new byte[9 * 16 * world.NumberOfScreens];
            int counter = 0;
            for (int i = 0; i < world.NumberOfScreens; i++)
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        data[counter++] = world.Data[(i * 16) + x, y + 0x11];
                    }
                }
            }

            return data;
        }
    }
}
