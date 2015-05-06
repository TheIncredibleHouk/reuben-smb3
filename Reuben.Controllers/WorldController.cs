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

        public WorldInfo GetWorldByID(Guid id)
        {
            return WorldData.Worlds.Where(w => w.ID == id).FirstOrDefault();
        }

        public void SaveWorld(World world)
        {
            WorldInfo info = GetWorldByID(world.ID);
            File.WriteAllText(info.File, JsonConvert.SerializeObject(world));
        }
    }
}
