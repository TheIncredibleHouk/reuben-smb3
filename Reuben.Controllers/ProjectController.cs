using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reuben.Model;
using Newtonsoft.Json;

namespace Reuben.Controllers
{
    public class ProjectController
    {
        private Project currentProject;
        public ProjectController()
        {
        }

        public void NewProject(string name)
        {
            currentProject = new Project();
            currentProject.Name = name;
        }

        public void AddPalette(Palette palette)
        {
            currentProject.Palettes.Add(palette);
        }

        public bool LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            currentProject = JsonConvert.DeserializeObject<Project>(File.ReadAllText(fileName));

            return currentProject != null;
        }

        public bool SaveToFile(string fileName)
        {
            try
            {
                File.WriteAllText(fileName, JsonConvert.SerializeObject(currentProject));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void AddLevelType(LevelType type)
        {
            if(currentProject.LevelTypes.Count > 15)
            {
                throw new Exception("Maximum number of level types allowed reached (15).");
            }

            currentProject.LevelTypes.Add(type);
        }

        public LevelType GetLevelTypeByID(int id)
        {
            return currentProject.LevelTypes[id];
        }

        public void AddWorldInfo(WorldInfo world)
        {
            if(currentProject.Worlds.Count > 9)
            {
                throw new Exception("Maximum number of words allowed reached (9).");
            }

            currentProject.Worlds.Add(world);
        }

        public WorldInfo GetWorldByNumber(int number)
        {
            return currentProject.Worlds.Where(w => w.WorldNumber == number).FirstOrDefault();
        }

        public WorldInfo GetNoWorld()
        {
            return currentProject.NoWorld;
        }
    }
}