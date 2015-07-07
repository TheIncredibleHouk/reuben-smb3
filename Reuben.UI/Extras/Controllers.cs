using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reuben.Controllers;

namespace Reuben.UI
{
    public static class Controllers
    {
        public static ProjectController Project { get; private set; }
        public static GraphicsController Graphics { get; private set; }
        public static LevelController Levels { get; private set; }
        public static WorldController Worlds { get; private set; }
        public static StringController Strings { get; private set; }
        public static SpriteController Sprites { get; private set; }
        public static ASMController ASM { get; private set; }
        public static RomController ROM { get; private set; }

        public static bool Initialize(string fileName)
        {
            Project = new ProjectController();
            Graphics = new GraphicsController();
            Levels = new LevelController();
            Strings = new StringController();
            Sprites = new SpriteController();
            ASM = new ASMController();
            ROM = new RomController();
            Worlds = new WorldController();

            if (Project.Load(fileName))
            {
                Graphics.LoadExtraGraphics(Project.ProjectData.ExtraGraphicsFile);
                Graphics.LoadGraphics(Project.ProjectData.GraphicsFile);
                Graphics.LoadPalettes(Project.ProjectData.PaletteFile);
                Levels.Load(Project.ProjectData.LevelDataFile);
                Strings.Load(Project.ProjectData.StringDataFile);
                Sprites.Load(Project.ProjectData.SpriteDataFile);
                ASM.Load(Project.ProjectData.ASMDirectory);
                Worlds.Load(Project.ProjectData.WorldDataFile);
                return true;
            }

            return false;
        }
    }
}
