using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;

using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
            OpenFile();
        }

        public void Initialize()
        {

        }

        public void RefreshTileView()
        {
            levelsPanel.Controls.Clear();
            foreach (ProjectNode node in Controllers.Project.ProjectData.Structure.Nodes)
            {
                ItemDisplay item = new ItemDisplay();

                MetroHeader header = new MetroHeader();
                header.Text = node.Name;
                header.Dock = DockStyle.Top;
                levelsPanel.Controls.Add(header);

                FlowLayoutPanel levelsHost = new FlowLayoutPanel();
                AddLevelNodes(node, levelsHost);
                levelsHost.AutoSize = true;
                levelsHost.Dock = DockStyle.Top;
                levelsPanel.Controls.Add(levelsHost);
            }
        }

        private void AddLevelNodes(ProjectNode node, FlowLayoutPanel levelshost)
        {
            
            IEnumerable<Guid> levelGuids = node.Nodes.Select(n => n.ID);
            IEnumerable<LevelInfo> levels = Controllers.Levels.LevelData.Levels.Where(l => levelGuids.Contains(l.ID));
            
            foreach (ProjectNode n in node.Nodes)
            {
                LevelInfo info = Controllers.Levels.GetLevelInfoByID(n.ID);
                MetroTile tile = new MetroTile();

                tile.Text = info.Name;
                tile.Height = 256 + 20 + 15;
                tile.Width = 256 + 20;
                tile.Margin = new System.Windows.Forms.Padding(10);
                tile.Click += tile_Click;
                tile.Tag = info;

                string filePath = Controllers.Project.ProjectData.ProjectDirectory + @"\cache\" + info.Name + ".png";
                if (File.Exists(filePath))
                {
                    PictureBox box = new PictureBox();
                    FileStream fs = new FileStream(filePath,  FileMode.Open, FileAccess.Read);
                    box.Image = Image.FromStream(fs);
                    fs.Close();
                    tile.Controls.Add(box);
                    box.Width = 256;
                    box.Height = 256;
                    box.Location = new Point(10, 10);
                    box.Click += box_Click;
                }

                levelshost.Controls.Add(tile);
                foreach (ProjectNode n2 in n.Nodes)
                {
                    AddLevelNodes(n2, levelshost);
                }
            }
        }

        void box_Click(object sender, EventArgs e)
        {
            OpenLevel((LevelInfo) ((MetroTile) ((PictureBox)sender).Parent).Tag);
        }

        private void tile_Click(object sender, EventArgs e)
        {
            OpenLevel((LevelInfo) ((MetroTile) sender).Tag);
        }

        private void OpenFile()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "JSON File (*.json)|*json|All Files|*";
            if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Controllers.Initialize(openDialog.FileName))
                {
                    MessageBox.Show("Unable to load " + openDialog.FileName + ". Please verify the file is a proper Reuben project file.");
                }
                else
                {
                    Initialize();
                    RefreshTileView();
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenLevel(LevelInfo levelInfo)
        {
            if (levelInfo != null)
            {
                levelInfo.File = Controllers.Project.ProjectData.LevelsDirectory + "\\" + levelInfo.Name + ".json";
                LevelEditor editor = new LevelEditor();
                editor.Show();
                editor.LoadLevel(levelInfo);
            }
        }

        private void blocksButton_Click(object sender, EventArgs e)
        {
            ProjectView.ShowBlockEditor();
        }

        private static BlockEditor BlockEditor;
        public static void ShowBlockEditor()
        {
            ShowBlockEditor(-1, -1);
        }

        public static void ShowBlockEditor(int levelType, int blockSelected)
        {
            if (ProjectView.BlockEditor == null)
            {
                ProjectView.BlockEditor = new BlockEditor();
                ProjectView.BlockEditor.Initialize();
                ProjectView.BlockEditor.Show();
                ProjectView.BlockEditor.FormClosing += BlockEditor_FormClosing;
            }
            else
            {
                ProjectView.BlockEditor.Focus();
            }

            if (levelType >= 0 && blockSelected >= 0)
            {
                ProjectView.BlockEditor.SelectBlock(levelType, blockSelected);
            }
        }

        private static void BlockEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ProjectView.BlockEditor.DialogResult == DialogResult.OK)
            {
                Controllers.Levels.LevelData.Types = ProjectView.BlockEditor.LocalLevelTypes;
                Controllers.Levels.LevelData.Overlays = ProjectView.BlockEditor.Overlays;
                Controllers.Levels.Save();
            }
            ProjectView.BlockEditor.FormClosing -= BlockEditor_FormClosing;
            ProjectView.BlockEditor = null;
        }

        private void asmButton_Click(object sender, EventArgs e)
        {
            ShowASMEditor();
        }

        private static ASMEditor ASMEditor;
        public static void ShowASMEditor(string file = null, string tag = null)
        {

            if (ProjectView.ASMEditor == null)
            {
                ProjectView.ASMEditor = new ASMEditor();
                ProjectView.ASMEditor.Initialize();
                ProjectView.ASMEditor.Show();
                ProjectView.ASMEditor.FormClosing += ASMEditor_FormClosing;
            }
            else
            {
                ProjectView.ASMEditor.Focus();
            }

            if (file != null && tag != null)
            {
                ProjectView.ASMEditor.GoToTag(file, tag);
            }
        }

        private static void ASMEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProjectView.ASMEditor.FormClosing -= ASMEditor_FormClosing;
            ProjectView.ASMEditor = null;
        }

        private void spritesButton_Click(object sender, EventArgs e)
        {
            ShowSpriteEditor();
        }

        private static SpriteEditor SpriteEditor;
        public static void ShowSpriteEditor(int spriteId = -1)
        {
            if (ProjectView.SpriteEditor == null)
            {
                ProjectView.SpriteEditor = new SpriteEditor();
                ProjectView.SpriteEditor.Initialize();
                ProjectView.SpriteEditor.Show();
                ProjectView.SpriteEditor.FormClosed += SpriteEditor_FormClosed;
            }
            else
            {
                ProjectView.SpriteEditor.Focus();
            }
        }

        private static void SpriteEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectView.SpriteEditor.FormClosed -= SpriteEditor_FormClosed;
            ProjectView.SpriteEditor = null;
        }

        private static void BuildRom()
        {
            if (string.IsNullOrEmpty(Controllers.Project.ProjectData.RomFile))
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter =
                saveDialog.Filter = "NES File (*.nes)|*nes|All Files|*";
            }
            if (Controllers.Project.ProjectData.LastAsmBuildDateTime < File.GetLastWriteTime(Controllers.Project.ProjectData.ASMDirectory + @"\smb3.asm"))
            {
                Controllers.ROM.BuildRomFromSource(Controllers.Project.ProjectData.ASMDirectory, Controllers.Project.ProjectData.RomFile);
            }


        }

        private static StringManager StringsEditor;
        public static void ShowStringsEditor(string selectedStrings = null)
        {
            if (ProjectView.StringsEditor == null)
            {
                ProjectView.StringsEditor = new StringManager();
                ProjectView.StringsEditor.Show();
                ProjectView.StringsEditor.Initalize();
                ProjectView.StringsEditor.FormClosed += StringsEditor_FormClosed;
            }
            else
            {
                ProjectView.StringsEditor.Focus();
            }
        }

        static void StringsEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectView.StringsEditor.FormClosed -= StringsEditor_FormClosed;
            ProjectView.StringsEditor = null;
        }


        private static PaletteManager PaletteEditor;
        public static void ShowPaletteEditor()
        {
            if (ProjectView.PaletteEditor == null)
            {
                ProjectView.PaletteEditor = new PaletteManager();
                ProjectView.PaletteEditor.Show();
                ProjectView.PaletteEditor.Initialize();
                ProjectView.PaletteEditor.FormClosed += PaletteEditor_FormClosed;
            }
            else
            {
                ProjectView.PaletteEditor.Focus();
            }
        }

        static void PaletteEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectView.PaletteEditor.FormClosed -= PaletteEditor_FormClosed;
            ProjectView.PaletteEditor = null;
        }


        private void textButton_Click(object sender, EventArgs e)
        {
            ShowStringsEditor();
        }

        private void spritesButton_Click_1(object sender, EventArgs e)
        {
            ShowSpriteEditor();
        }

        private void palettesButton_Click(object sender, EventArgs e)
        {
            ShowPaletteEditor();
        }

        private void asmButton_Click_1(object sender, EventArgs e)
        {
            ShowASMEditor();
        }

        private void blocksButton_Click_1(object sender, EventArgs e)
        {
            ShowBlockEditor();
        }
    }
}
