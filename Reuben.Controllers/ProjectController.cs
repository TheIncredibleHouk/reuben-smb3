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
            ProjectData.GraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\graphics.chr";
            ProjectData.ExtraGraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\extra.chr";
            ProjectData.PaletteFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\palettes.json";
            ProjectData.LevelDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\levels.json";
            ProjectData.WorldDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\worlds.json";
            ProjectData.StringDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\strings.json";
            ProjectData.SpriteDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\sprites.json";
            ProjectData.LevelsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\levels";
            ProjectData.WorldsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\worlds";
            ProjectData.ASMDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\asm";
            return ProjectData != null;
        }

        public bool Save(string fileName)
        {
            try
            {
                ProjectData.GraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\graphics.chr";
                ProjectData.ExtraGraphicsFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\extra.chr";
                ProjectData.PaletteFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\palettes.json";
                ProjectData.LevelDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\levels.json";
                ProjectData.WorldDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\worlds.json";
                ProjectData.StringDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\strings.json";
                ProjectData.SpriteDataFile = Path.GetDirectoryName(fileName).Trim('\\') + @"\assets\sprites.json";
                ProjectData.LevelsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\levels";
                ProjectData.WorldsDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\worlds";
                ProjectData.ASMDirectory = Path.GetDirectoryName(fileName).Trim('\\') + @"\asm";
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