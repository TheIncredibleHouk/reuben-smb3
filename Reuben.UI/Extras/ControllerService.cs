using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reuben.Controllers;

namespace Reuben.UI
{
    public static class ControllerService
    {
        public static ProjectController Project { get; private set; }
        public static GraphicsController Graphics { get; private set; }
        public static LevelController Levels { get; private set; }
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

            return Project.Load(fileName);
        }
    }
}
