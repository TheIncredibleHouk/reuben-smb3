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
        public Project ProjectData { get; private set; }

        public ProjectController()
        {
        }

        public void NewProject(string name)
        {
            ProjectData = new Project();
            ProjectData.Name = name;
        }

        public bool Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }
            ProjectData = JsonConvert.DeserializeObject<Project>(File.ReadAllText(fileName));
            ProjectData.ProjectDirectory = Path.GetDirectoryName(fileName).Trim('\\');
            ProjectData.GraphicsFile = ProjectData.ProjectDirectory + @"\assets\graphics.chr";
            ProjectData.ExtraGraphicsFile = ProjectData.ProjectDirectory + @"\assets\extra.chr";
            ProjectData.PaletteFile = ProjectData.ProjectDirectory + @"\assets\palettes.json";
            ProjectData.LevelDataFile = ProjectData.ProjectDirectory + @"\assets\levels.json";
            ProjectData.WorldDataFile = ProjectData.ProjectDirectory + @"\assets\worlds.json";
            ProjectData.StringDataFile = ProjectData.ProjectDirectory + @"\assets\strings.json";
            ProjectData.SpriteDataFile = ProjectData.ProjectDirectory + @"\assets\sprites.json";
            ProjectData.LevelsDirectory = ProjectData.ProjectDirectory + @"\levels";
            ProjectData.WorldsDirectory = ProjectData.ProjectDirectory + @"\worlds";
            ProjectData.ASMDirectory = ProjectData.ProjectDirectory + @"\asm";
            return ProjectData != null;
        }

        public bool Save(string fileName)
        {
            try
            {
                ProjectData = JsonConvert.DeserializeObject<Project>(File.ReadAllText(fileName));
                ProjectData.ProjectDirectory = Path.GetDirectoryName(fileName).Trim('\\');

                ProjectData.GraphicsFile = ProjectData.ProjectDirectory + @"\assets\graphics.chr";
                ProjectData.ExtraGraphicsFile = ProjectData.ProjectDirectory + @"\assets\extra.chr";
                ProjectData.PaletteFile = ProjectData.ProjectDirectory + @"\assets\palettes.json";
                ProjectData.LevelDataFile = ProjectData.ProjectDirectory + @"\assets\levels.json";
                ProjectData.WorldDataFile = ProjectData.ProjectDirectory + @"\assets\worlds.json";
                ProjectData.StringDataFile = ProjectData.ProjectDirectory + @"\assets\strings.json";
                ProjectData.SpriteDataFile = ProjectData.ProjectDirectory + @"\assets\sprites.json";
                ProjectData.LevelsDirectory = ProjectData.ProjectDirectory + @"\levels";
                ProjectData.WorldsDirectory = ProjectData.ProjectDirectory + @"\worlds";
                ProjectData.ASMDirectory = ProjectData.ProjectDirectory + @"\asm";
                File.WriteAllText(fileName, JsonConvert.SerializeObject(ProjectData));
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}