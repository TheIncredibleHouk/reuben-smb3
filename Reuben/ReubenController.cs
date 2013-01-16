using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Daiz.NES.Reuben.ProjectManagement;
using Daiz.Library;

namespace Daiz.NES.Reuben
{
    public static class ReubenController
    {
        public static event EventHandler GraphicsReloaded;
        public static event EventHandler<TEventArgs<Level>> LevelReloaded;
        public static event EventHandler<TEventArgs<World>> WorldReloaded;

        public static bool CreateNewProject()
        {
            InputForm iForm = new InputForm();
            iForm.StartPosition = FormStartPosition.CenterParent;
            iForm.Owner = ReubenController.MainWindow;

            string projectName = iForm.GetInput("Please enter the name of your project");
            if (projectName != null)
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.InitialDirectory = ProjectController.ReubenDirectory;
                SFD.FileName = projectName + ".rbn";
                DialogResult result = SFD.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return ProjectController.CreateNewProject(SFD.FileName, projectName);
                }
            }

            return false;
        }

        public static void CreateNewLevel()
        {
            CreateNewLevel(ProjectController.WorldManager.Worlds[0]);
        }

        public static void CreateNewLevel(WorldInfo world)
        {
            NewLevelForm newLevelForm = new NewLevelForm();
            newLevelForm.StartPosition = FormStartPosition.CenterParent;
            newLevelForm.Owner = ReubenController.MainWindow;

            DialogResult result = newLevelForm.ShowDialog(world);
            if (result == DialogResult.OK)
            {
                ProjectController.LevelManager.CreateNewLevel(newLevelForm.LevelName, newLevelForm.LevelType, newLevelForm.LevelLayout, newLevelForm.SelectedWorld);
            }
        }

        public static void CreateNewWorld()
        {
            InputForm iForm = new InputForm();
            iForm.StartPosition = FormStartPosition.CenterParent;
            iForm.Owner = ReubenController.MainWindow;

            string name = iForm.GetInput("Please enter a world name.");
            if (name != null)
            {
                WorldInfo wi = new WorldInfo();
                wi.WorldGuid = Guid.NewGuid();
                wi.Name = name;
                ProjectController.WorldManager.AddWorld(wi);
            }
        }

        public static bool OpenProject()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "Reuben Project Files|*.rbn";
            bool success = false;
            DialogResult result = OFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                foreach (var k in editorTable.Keys.ToList())
                {
                    editorTable[k].Close();
                }

                success = ProjectController.LoadProject(OFD.FileName);
            }
            OFD.Dispose();

            return success;
        }

        public static void CloseAll()
        {
            foreach (var k in editorTable.Keys.ToList())
            {
                editorTable[k].Close();
            }
        }

        public static void ImportGraphics()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Filter = "NES Graphics File|*.chr|NES ROM File|*.nes";
            OFD.InitialDirectory = ProjectController.RootDirectory;
            DialogResult result = OFD.ShowDialog();
            if (result == DialogResult.OK)
            {
                ProjectController.GraphicsManager.ImportGraphics(OFD.FileName);
            }
            OFD.Dispose();
        }

        public static void OpenPaletteViewer()
        {
            PaletteManager pm = new PaletteManager();
            pm.StartPosition = FormStartPosition.CenterParent;
            pm.Owner = ReubenController.MainWindow;

            if (ActiveEditor != null)
            {
                if (ActiveEditor is LevelEditor)
                {
                    pm.ShowDialog(((LevelEditor)ActiveEditor).CurrentLevel.Palette);
                }
                else
                {
                    pm.ShowDialog(((WorldEditor)ActiveEditor).CurrentWorld.Palette);
                }
            }
            else
            {

                pm.ShowDialog();
            }
        }

        public static void OpenGraphicsEditor()
        {
            GraphicsEditor ge = new GraphicsEditor();
            ge.StartPosition = FormStartPosition.CenterParent;
            ge.Owner = ReubenController.MainWindow;

            if (ActiveEditor != null)
            {
                if (ActiveEditor is LevelEditor)
                {
                    LevelEditor le = (LevelEditor)ActiveEditor;
                    ge.ShowDialog(le.CurrentLevel.GraphicsBank, le.CurrentLevel.AnimationBank, le.CurrentLevel.Palette);
                }
                else
                {
                    WorldEditor we = (WorldEditor)ActiveEditor;
                    ge.ShowDialog(0x14, we.CurrentWorld.GraphicsBank, we.CurrentWorld.Palette);
                }
            }
            else
            {
                ge.ShowDialog();
            }
        }

        public static void OpenBlockEditor()
        {
            Map16Editor me = new Map16Editor();
            me.StartPosition = FormStartPosition.CenterParent;
            me.Owner = ReubenController.MainWindow;

            if (ActiveEditor != null)
            {
                if (ActiveEditor is LevelEditor)
                {
                    LevelEditor le = (LevelEditor)ActiveEditor;
                    me.ShowDialog(le.CurrentLevel.Type, 0, le.CurrentLevel.GraphicsBank, le.CurrentLevel.AnimationBank, le.CurrentLevel.Palette);
                }
                else
                {
                    WorldEditor we = (WorldEditor)ActiveEditor;
                    me.ShowDialog(we.CurrentWorld.Type, 0, 0x14, we.CurrentWorld.GraphicsBank, we.CurrentWorld.Palette);
                }
            }
            else
            {
                me.ShowDialog();
            }
        }

        public static void OpenBlockEditor(int definitionIndex, int selectedTileIndex, int graphics1, int graphics2, int paletteIndex)
        {
            Map16Editor me = new Map16Editor();
            me.StartPosition = FormStartPosition.CenterParent;
            me.Owner = ReubenController.MainWindow;

            me.ShowDialog(definitionIndex, selectedTileIndex, graphics1, graphics2, paletteIndex);
        }

        private static Dictionary<Guid, Form> editorTable = new Dictionary<Guid, Form>();
        public static void EditLevel(LevelInfo li)
        {
            if (editorTable.ContainsKey(li.LevelGuid))
            {
                editorTable[li.LevelGuid].Activate();
                return;
            }

            Level l = new Level();
            l.Load(li);
            LevelEditor le = new LevelEditor();
            le.MdiParent = MainWindow;
            le.EditLevel(l);
            le.FormClosed += new FormClosedEventHandler(le_FormClosed);
            le.Activated += new EventHandler(le_Activated);
            ActiveEditor = le;
            editorTable.Add(li.LevelGuid, le);
            MainWindow.HideProjectview();
        }

        public static void EditWorld(WorldInfo wi)
        {
            if (editorTable.ContainsKey(wi.WorldGuid))
            {
                editorTable[wi.WorldGuid].Activate();
                return;
            }

            World w = new World();

            if (!w.Load(wi))
            {
                w.New(wi);
            }

            WorldEditor we = new WorldEditor();
            we.MdiParent = MainWindow;
            we.EditWorld(w);
            we.FormClosed += new FormClosedEventHandler(le_FormClosed);
            we.Activated += new EventHandler(le_Activated);
            ActiveEditor = we;
            editorTable.Add(wi.WorldGuid, we);
            MainWindow.HideProjectview();
        }

        private static void le_Activated(object sender, EventArgs e)
        {
            ActiveEditor = (Form)sender;
        }

        public static void OpenLayoutManager()
        {
            LayoutEditor lee = new LayoutEditor();
            lee.StartPosition = FormStartPosition.CenterParent;
            lee.Owner = ReubenController.MainWindow;

            if (ActiveEditor != null)
            {
                if (ActiveEditor is LevelEditor)
                {
                    LevelEditor le = (LevelEditor)ActiveEditor;
                    lee.ShowDialog(le.CurrentLevel.Type, le.CurrentLevel.GraphicsBank, le.CurrentLevel.AnimationBank, le.CurrentLevel.Palette, le.CurrentLayout);
                }
                else
                {
                    WorldEditor we = (WorldEditor)ActiveEditor;
                    lee.ShowDialog(we.CurrentWorld.Type, 0x14, we.CurrentWorld.GraphicsBank, we.CurrentWorld.Palette, we.CurrentLayout);
                }
            }
            else
            {
                lee.ShowDialog();
            }
        }

        private static void le_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var k in editorTable.Keys.ToList())
            {
                if (editorTable[k] == sender)
                {
                    editorTable.Remove(k);
                    if (sender == ActiveEditor)
                        ActiveEditor = null;
                    break;
                }
            }
        }

        public static void ImportLevel()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.DefaultExt = "lvl";
            DialogResult result = OFD.ShowDialog();
            OFD.Dispose();

            if (result == DialogResult.OK)
            {
                Level level = new Level();
                level.Load(OFD.FileName);
                LevelInfo li = new LevelInfo();
                li.LevelGuid = level.Guid;
                li.WorldGuid = ProjectController.WorldManager.Worlds[0].WorldGuid;
                li.Name = "Imported Level";
                li.LevelType = level.Type;
                if (!ProjectController.LevelManager.AddLevel(li))
                {
                    MessageBox.Show("Could not import level. Make sure the level does not already exist.");
                }
                else
                {
                    level.Save();
                }
            }
        }

        public static void SaveCurrentLevelToBitmap()
        {
            if (ActiveEditor == null)
            {
                MessageBox.Show("There are no levels current opened!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "png";
            sfd.FileName = ActiveEditor.Text + ".png";
            DialogResult result = sfd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Bitmap image;
                if (ActiveEditor is LevelEditor)
                {
                    image = ((LevelEditor)ActiveEditor).GetLevelBitmap();
                }
                else
                {
                    image = ((WorldEditor)ActiveEditor).GetLevelBitmap();

                }

                image.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                MessageBox.Show("Level successfully exported!");
            }
        }

        public static void ReloadGraphics()
        {
            ProjectController.GraphicsManager.LoadGraphics(ProjectController.RootDirectory + @"\" + ProjectController.ProjectName + ".chr");
            if (GraphicsReloaded != null)
            {
                GraphicsReloaded(null, null);
            }
        }

        public static void ReloadLevel()
        {
            if (ActiveEditor is LevelEditor)
            {
                LevelEditor le = (LevelEditor)ActiveEditor;
                le.CurrentLevel.Load(ProjectController.LevelManager.GetLevelInfo(le.CurrentLevel.Guid));
                if (LevelReloaded != null)
                {
                    LevelReloaded(null, new TEventArgs<Level>(le.CurrentLevel));
                }
            }
            else
            {
                WorldEditor we = (WorldEditor)ActiveEditor;
                we.CurrentWorld.Load(ProjectController.WorldManager.GetWorldInfo(we.CurrentWorld.Guid));
                if (WorldReloaded != null)
                {
                    WorldReloaded(null, new TEventArgs<World>(we.CurrentWorld));
                }
            }

        }

        public static void DumpRawLevel()
        {
            SaveFileDialog SFD = new SaveFileDialog();
            if (SFD.ShowDialog() == DialogResult.OK)
            {
                if (ActiveEditor is LevelEditor)
                {
                    LevelEditor le = ((LevelEditor)ActiveEditor);
                    Level l = le.CurrentLevel;
                    FileStream fs = new FileStream(SFD.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] data = l.GetCompressedData();
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    MessageBox.Show("Raw level data dumped");
                }

                else
                {
                    WorldEditor we = ((WorldEditor)ActiveEditor);
                    World w = we.CurrentWorld;
                    FileStream fs = new FileStream(SFD.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    byte[] output = new byte[0x40 * 0x10];
                    for (int i = 0; i < 0x1B; i++)
                    {
                        for (int j = 0; j < 0x10; j++)
                        {
                            output[i * 0x1B + j] = w.LevelData[j, i];
                        }
                    }

                    fs.Write(output, 0, output.Length);
                    fs.Close();
                    MessageBox.Show("Raw level data dumped");
                }
            }
        }

        public static void CompileRom(bool useDefaultROM)
        {
            ROMManager romMan = new ROMManager();

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Filter = "NES ROM Images|*.nes";
            if (useDefaultROM && ProjectController.ProjectManager.CurrentProject.ROMFile.Length > 0)
            {
                int remaining = romMan.CompileRom(ProjectController.ProjectManager.CurrentProject.ROMFile, true);
                MessageBox.Show("ROM successfully compiled.\nLevel space remaining: " + remaining + " bytes");
            }
            else if (SFD.ShowDialog() == DialogResult.OK)
            {
                if (SFD.FileName != ProjectController.ProjectManager.CurrentProject.ROMFile)
                {
                    DialogResult dr = DialogResult.None;
                    dr = MessageBox.Show("Would you like to set this ROM as your project ROM? All compiles will save to this ROM.", "Default ROM", MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        ProjectController.ProjectManager.CurrentProject.ROMFile = SFD.FileName;
                    }
                }
                romMan.CompileRom(SFD.FileName, true);
            }
        }

        public static void CloseLevelEditor(LevelInfo li)
        {
            if (editorTable.ContainsKey(li.LevelGuid))
            {
                editorTable[li.LevelGuid].Close();
            }
        }

        public static void CloseWorldEditor(WorldInfo wi)
        {
            if (editorTable.ContainsKey(wi.WorldGuid))
            {
                editorTable[wi.WorldGuid].Close();
            }
        }

        public static void GetPaletteFile()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.DefaultExt = "pal";
            DialogResult result = OFD.ShowDialog();
            OFD.Dispose();

            if (result == DialogResult.OK)
            {
                ProjectController.ProjectManager.CurrentProject.SetPaletteFile(OFD.FileName);
            }
        }

        public static void DefaulPaletteFile()
        {
            ProjectController.ProjectManager.CurrentProject.DefaultPalette();
        }

        private static Form ActiveEditor;
        public static Main MainWindow { get; set; }
    }
}
