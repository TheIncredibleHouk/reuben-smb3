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
    public class LevelController
    {
        public LevelData LevelData { get; set; }
        private string lastFile;

        public LevelController()
        {
            LevelData = new LevelData();
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            lastFile = fileName;
            LevelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(fileName));
        }

        public void Save()
        {
            Save(lastFile);
        }

        public void Save(string fileName)
        {
            lastFile = fileName;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(LevelData, Formatting.Indented));
        }

        public void SaveLevel(Level level)
        {
            LevelInfo info = GetLevelInfoByID(level.ID);
            File.WriteAllText(info.File, JsonConvert.SerializeObject(level));
        }

        public Level LoadLevel(string fileName)
        {
            return JsonConvert.DeserializeObject<Level>(File.ReadAllText(fileName));
        }

        public LevelInfo GetLevelInfoByID(Guid id)
        {
            return LevelData.Levels.Where(l => l.ID == id).FirstOrDefault();
        }
    }
}
