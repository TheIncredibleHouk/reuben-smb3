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
        public static MusicManager MusicManager { get; private set; }
        public static AutoScrollManager AutoScrollManager { get; private set; }
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
            MusicManager = new MusicManager();
            AutoScrollManager = new ProjectManagement.AutoScrollManager();
            ReubenDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Reuben";
            if (!Directory.Exists(ReubenDirectory))
            {
                Directory.CreateDirectory(ReubenDirectory);
            }
        }

        public static bool CreateNewProject(string filename, string name)
        {
            RootDirectory = filename.Substring(0, filename.LastIndexOf(Path.DirectorySeparatorChar));
            if (!Directory.Exists(RootDirectory))
            {
                Directory.CreateDirectory(RootDirectory);
            }
            LevelDirectory = string.Format("{0}{1}Levels", RootDirectory, Path.DirectorySeparatorChar);
            WorldDirectory = string.Format("{0}{1}Worlds", RootDirectory, Path.DirectorySeparatorChar);
            Directory.CreateDirectory(LevelDirectory);
            Directory.CreateDirectory(WorldDirectory);
            ProjectName = name;

            // Load defaults
            SpriteManager.LoadDefaultSprites();
            SpriteManager.Save(string.Format("{0}{1}sprites.xml", RootDirectory, Path.DirectorySeparatorChar));

            BlockManager.LoadDefault();
            BlockManager.SaveDefinitions(string.Format("{0}{1}{2}.tsa", RootDirectory, Path.DirectorySeparatorChar, ProjectName));
            BlockManager.SaveBlockStrings(string.Format("{0}{1}strings.xml", RootDirectory, Path.DirectorySeparatorChar, ProjectName));


            GraphicsManager.LoadDefault();
            GraphicsManager.SaveGraphics(string.Format("{0}{1}{2}.chr", RootDirectory, Path.DirectorySeparatorChar, ProjectName));

            LevelManager.Default();
            WorldManager.Default();
            ColorManager.LoadDefaultColor();
            MusicManager.LoadDefault();

            PaletteManager.Default();

            SpecialManager.LoadDefaultSpecialGraphics();
            SpecialManager.LoadDefaultSpecials();
            SpecialManager.SaveGraphics(string.Format("{0}{1}special.chr", RootDirectory, Path.DirectorySeparatorChar));
            SpecialManager.SaveSpecials(string.Format("{0}{1}special.xml", RootDirectory, Path.DirectorySeparatorChar));

            LayoutManager.LoadDefault();
            ProjectManager.New(name);
            ProjectManager.Save(string.Format("{0}{1}{2}.rbn", RootDirectory, Path.DirectorySeparatorChar, ProjectName));
            return true;
        }

        public static bool LoadProject(string name)
        {
            if (!ProjectManager.Load(name)) return false;
            ProjectName = ProjectManager.CurrentProject.Name;
            RootDirectory = name.Substring(0, name.LastIndexOf(Path.DirectorySeparatorChar)); ;
            LevelDirectory = string.Format("{0}{1}{2}", RootDirectory, Path.DirectorySeparatorChar, "Levels");
            WorldDirectory = string.Format("{0}{1}{2}", RootDirectory, Path.DirectorySeparatorChar, "Worlds");

            // load from file
            if (!SpriteManager.LoadSpritesFromFile(string.Format("{0}{1}sprites.xml", RootDirectory, Path.DirectorySeparatorChar)))
                SpriteManager.LoadDefaultSprites();

            if (!BlockManager.LoadDefinitions(string.Format("{0}{1}{2}.tsa", RootDirectory, Path.DirectorySeparatorChar, ProjectName)))
                BlockManager.LoadDefault();

            BlockManager.LoadBlockStrings(string.Format("{0}{1}strings.xml", RootDirectory, Path.DirectorySeparatorChar));

            if (!GraphicsManager.LoadGraphics(string.Format("{0}{1}{2}.chr", RootDirectory, Path.DirectorySeparatorChar, ProjectName)))
                GraphicsManager.LoadDefault();

            if (!SpecialManager.LoadSpecialGraphics(string.Format("{0}{1}special.chr", RootDirectory, Path.DirectorySeparatorChar)))
                SpecialManager.LoadDefaultSpecialGraphics();

            if (!SpecialManager.LoadSpecialDefinitions(string.Format("{0}{1}special.xml", RootDirectory, Path.DirectorySeparatorChar)))
                SpecialManager.LoadDefaultSpecials();

            if (!MusicManager.LoadMusic(string.Format("{0}{1}music.xml", RootDirectory, Path.DirectorySeparatorChar)))
                MusicManager.LoadDefault();

            AutoScrollManager.LoadAutoScrollSets(string.Format("{0}{1}scroll.xml", RootDirectory, Path.DirectorySeparatorChar)));
            return true;
        }

        public static bool Save()
        {
            ProjectManager.Save(string.Format("{0}{1}{2}.rbn", RootDirectory, Path.DirectorySeparatorChar, ProjectName));
            return true;
        }
    }
}
