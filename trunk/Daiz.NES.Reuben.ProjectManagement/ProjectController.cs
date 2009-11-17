using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public static class ProjectController
    {
        public static string ReubenDirectory { get; set; }
        public static string LevelDirectory { get;  set; }
        public static string WorldDirectory { get; set; }
        public static string RootDirectory { get; set; }
        public static SpriteManager SpriteManager { get; private set; }
        public static SpecialManager SpecialManager { get; private set; }
        public static BlockManager BlockManager { get; private set; }
        public static GraphicsManager GraphicsManager { get; private set; }
        public static ColorManager ColorManager { get; private set; }
        public static PaletteManager PaletteManager { get; private set; }
        public static ProjectManager ProjectManager { get; private set; }
        public static LevelManager LevelManager { get; private set; }
        public static WorldManager WorldManager { get; private set; }
        public static LayoutManager LayoutManager { get; private set; }
        public static SettingsManager SettingsManager { get; private set; }
        public static string ProjectName;

        static ProjectController()
        {
            SpriteManager = new SpriteManager();
            BlockManager = new BlockManager();
            GraphicsManager = new GraphicsManager();
            ProjectManager = new ProjectManager();
            LevelManager = new LevelManager();
            WorldManager = new WorldManager();
            ColorManager = new ColorManager();
            PaletteManager = new PaletteManager();
            SpecialManager = new SpecialManager();
            LayoutManager = new LayoutManager();
            SettingsManager = new SettingsManager();
            ReubenDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Reuben";
            if (!Directory.Exists(ReubenDirectory))
            {
                Directory.CreateDirectory(ReubenDirectory);
            }
        }

        public static bool CreateNewProject(string filename, string name)
        {
            RootDirectory = filename.Substring(0, filename.LastIndexOf('\\'));
            if (!Directory.Exists(RootDirectory))
            {
                Directory.CreateDirectory(RootDirectory);
            }
            LevelDirectory = RootDirectory + @"\Levels";
            WorldDirectory = RootDirectory + @"\Worlds";
            Directory.CreateDirectory(LevelDirectory);
            Directory.CreateDirectory(WorldDirectory);
            ProjectName = name;

            // Load defaults
            SpriteManager.LoadDefaultSprites();
            SpriteManager.Save(RootDirectory + @"\sprites.xml");

            BlockManager.LoadDefault();
            BlockManager.SaveDefinitions(RootDirectory + @"\" + ProjectName + ".tsa");


            GraphicsManager.LoadDefault();
            GraphicsManager.SaveGraphics(RootDirectory + @"\" + ProjectName + ".chr");

            LevelManager.Default();
            WorldManager.Default();
            ColorManager.LoadDefaultColor();

            PaletteManager.Default();

            SpecialManager.LoadDefaultSpecialGraphics();
            SpecialManager.LoadDefaultSpecials();
            SpecialManager.SaveGraphics(RootDirectory + @"\" + "special.chr");
            SpecialManager.SaveDefinitions(RootDirectory + @"\" + "special.xml");

            LayoutManager.LoadDefault();
            ProjectManager.New(name);
            ProjectManager.Save(RootDirectory + @"\" + ProjectName + ".rbn");
            return true;
        }

        public static bool LoadProject(string name)
        {
            ProjectManager.Load(name);
            ProjectName = ProjectManager.CurrentProject.Name;
            RootDirectory = name.Substring(0, name.LastIndexOf('\\')); ;
            LevelDirectory = RootDirectory + @"\Levels";
            WorldDirectory = RootDirectory + @"\Worlds";

            // load from file
            if (!SpriteManager.LoadSpritesFromFile(RootDirectory + @"\sprites.xml"))
                SpriteManager.LoadDefaultSprites();
            if (!BlockManager.LoadDefinitions(RootDirectory + @"\" + ProjectName + ".tsa"))
                BlockManager.LoadDefault();
            if (!GraphicsManager.LoadGraphics(RootDirectory + @"\" + ProjectName + ".chr"))
                GraphicsManager.LoadDefault();
            if (!SpecialManager.LoadSpecialGraphics(RootDirectory + @"\" + "special.chr"))
                SpecialManager.LoadDefaultSpecialGraphics();
            if (!SpecialManager.LoadSpecialDefinitions(RootDirectory + @"\" + "special.xml"))
                SpecialManager.LoadDefaultSpecials();

            return true;
        }

        public static bool Save()
        {
            ProjectManager.Save(RootDirectory + @"\" + ProjectName + ".rbn");
            return true;
        }
    }
}
