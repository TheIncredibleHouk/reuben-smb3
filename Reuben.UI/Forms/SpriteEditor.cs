using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

using Reuben.Model;
using Reuben.Controllers;
using Reuben.NESGraphics;
using Reuben.UI.Controls;

namespace Reuben.UI
{
    public partial class SpriteEditor : Form
    {
        public SpriteEditor()
        {
            InitializeComponent();
            ColumnHeader h = new ColumnHeader();
            h.Width = codeTags.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            codeTags.Columns.Add(h);
        }

        private SpriteController localSpriteController;
        private List<SpriteDefinition> backUpDefinitions;
        private GraphicsController localGraphicsController;

        public void Initialize(GraphicsController graphicsController, SpriteController spriteController, LevelController levelController)
        {
            localGraphicsController = graphicsController;
            localSpriteController = spriteController;
            backUpDefinitions = spriteController.SpriteData.Definitions.MakeCopy();
            spriteSelector.Initialize(graphicsController, spriteController, graphicsController.GraphicsData.Colors, graphicsController.GraphicsData.Palettes[0]);
            spriteViewer.Initialize(graphicsController, spriteController, graphicsController.GraphicsData.Colors, graphicsController.GraphicsData.Palettes[0], levelController.LevelData.OverlayPalette);
            paletteList.Palettes = graphicsController.GraphicsData.Palettes;
            paletteList.ColorReference = graphicsController.GraphicsData.Colors;
            paletteList.UpdateList();
            paletteList.SelectedIndex = 0;
            paletteList.SelectedIndexChanged += paletteList_SelectedIndexChanged;
            spriteSelector.SelectedSpriteChanged += spriteSelector_SelectedSpriteChanged;
            graphicsController.GraphicsUpdated += graphicsController_GraphicsUpdated;
            graphicsController.ExtraGraphicsUpdated += graphicsController_ExtraGraphicsUpdated;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            localGraphicsController.GraphicsUpdated -= graphicsController_ExtraGraphicsUpdated;
            localGraphicsController.ExtraGraphicsUpdated -= graphicsController_ExtraGraphicsUpdated;
        }

        void graphicsController_ExtraGraphicsUpdated(object sender, EventArgs e)
        {
            spriteViewer.UpdateGraphics();
        }

        void graphicsController_GraphicsUpdated(object sender, EventArgs e)
        {
            spriteSelector.Update(colors: null);
            spriteViewer.UpdateGraphics();
        }

        void spriteSelector_SelectedSpriteChanged(object sender, EventArgs e)
        {
            if (spriteSelector.SelectedSprite != null)
            {
                spriteViewer.CurrentDefinition = localSpriteController.GetDefinition(spriteSelector.SelectedSprite.ObjectID);
                definitionCode.Text = JsonConvert.SerializeObject(spriteViewer.CurrentDefinition.SpriteInfo, Formatting.Indented);
                if (spriteViewer.CurrentDefinition != null)
                {
                    spriteName.Text = spriteViewer.CurrentDefinition.Name;
                    displayProperty.Items.Clear();
                    displayProperty.Items.AddRange(spriteViewer.CurrentDefinition.PropertyDescriptions.ToArray());
                    if (displayProperty.Items.Count > 0)
                    {
                        displayProperty.SelectedIndex = 0;
                    }
                }

                definitionCode.Text = EditorSpriteInfo.Serialize(spriteViewer.CurrentDefinition.SpriteInfo);
                codeTags.Items.Clear();
                string file = "";
                if(spriteSelector.SelectedSprite.ObjectID < 0x24)
                {
                    file = "prg001.asm";
                }
                else if(spriteSelector.SelectedSprite.ObjectID < 0x48)
                {
                    file = "prg002.asm";
                }
                else if(spriteSelector.SelectedSprite.ObjectID < 0x6C)
                {
                    file = "prg003.asm";
                }
                else if (spriteSelector.SelectedSprite.ObjectID < 0x9B)
                {
                    file = "prg004.asm";
                }
                else
                {
                    file = "prg005.asm";
                }

                ListViewItem item1 = new ListViewItem();
                item1.Text = "Initialization";
                item1.Tag = new Tuple<string, string>(file + "|prg000.asm", "ObjectsInit@" + spriteSelector.SelectedSprite.ObjectID.ToString("X2"));

                ListViewItem item2 = new ListViewItem();
                item2.Text = "Game Loop";
                item2.Tag = new Tuple<string, string>(file + "|prg000.asm", "ObjectsNorm@" + spriteSelector.SelectedSprite.ObjectID.ToString("X2"));

                ListViewItem item3 = new ListViewItem();
                item3.Text = "Player Collision";
                item3.Tag = new Tuple<string, string>(file + "|prg000.asm", "ObjectsHit@" + spriteSelector.SelectedSprite.ObjectID.ToString("X2"));

                codeTags.Items.Add(item1);
                codeTags.Items.Add(item2);
                codeTags.Items.Add(item3);
            }
        }

        private void UpdateCode()
        {
            List<SpriteInfo> info;
            try
            {
                info = EditorSpriteInfo.Deserialize(definitionCode.Text);
                if (info != null)
                {
                    spriteViewer.CurrentDefinition.SpriteInfo = info;
                    syntaxError.Visible = false;
                    spriteViewer.UpdateGraphics();
                    spriteSelector.Update(colors: null);
                }
                else
                {
                    syntaxError.Visible = true;
                }
            }
            catch
            {
                syntaxError.Visible = true;
            }
        }

        void editor_SpriteInfoSelected(object sender, EventArgs e)
        {
            var s = ((SpriteInfoEditor)sender);
            if (s.Selected)
            {
                spriteViewer.HighlightedSpriteInfo.Add(s.SpriteInfo);
            }
            else
            {
                spriteViewer.HighlightedSpriteInfo.Remove(s.SpriteInfo);
            }

            spriteViewer.UpdateGraphics();
        }

        void editor_SpriteInfoChanged(object sender, EventArgs e)
        {
            spriteViewer.UpdateGraphics();
            spriteSelector.Update(colors: null);
        }

        void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteSelector.Update(palette: paletteList.SelectedPalette);
            spriteViewer.Update(palette: paletteList.SelectedPalette);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            spriteViewer.DisplaySpecialTiles = checkBox1.Checked;
            spriteViewer.UpdateGraphics();
        }

        private void displayProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            spriteViewer.Property = displayProperty.SelectedIndex;
            spriteViewer.UpdateGraphics();
        }

        private void editorPropertyList_TextChanged(object sender, EventArgs e)
        {
            int oldSelection = displayProperty.SelectedIndex;
            displayProperty.Items.Clear();
            displayProperty.Items.AddRange(spriteViewer.CurrentDefinition.PropertyDescriptions.ToArray());
            if (oldSelection >= -1 && displayProperty.Items.Count > 0)
            {
                if (oldSelection < displayProperty.Items.Count - 1)
                {
                    displayProperty.SelectedIndex = oldSelection;
                }
                else
                {
                    displayProperty.SelectedIndex = 0;
                }
            }
            else if (displayProperty.Items.Count > 0)
            {
                displayProperty.SelectedIndex = 0;
            }
        }

        private void spriteName_TextChanged(object sender, EventArgs e)
        {
            if (spriteViewer.CurrentDefinition.Name != spriteName.Text)
            {
                spriteViewer.CurrentDefinition.Name = spriteName.Text;
                spriteSelector.Update(colors: null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            localSpriteController.SpriteData.Definitions = backUpDefinitions;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            localSpriteController.Save();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string text = Prompt.GetText("Enter sprite id (in hexadecimal).");
            if (text != null)
            {
                try
                {
                    int val = Convert.ToInt32(text, 16);
                    if (val > 255)
                    {
                        MessageBox.Show("Invalid id.");
                    }

                    var existing = localSpriteController.SpriteData.Definitions.Where(d => d.GameID == val).FirstOrDefault();
                    if (existing != null)
                    {
                        MessageBox.Show(text + " already exists as an object id.");
                    }

                    localSpriteController.SpriteData.Definitions.Add(new SpriteDefinition() { GameID = val, Name = "New Sprite" });
                    spriteSelector.Update(colors: null);
                    spriteSelector.SelectedSprite = new Sprite() { ObjectID = val };
                }
                catch
                {
                    MessageBox.Show("Invalid id.");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (spriteSelector.SelectedSprite != null)
            {
                var def = localSpriteController.GetDefinition(spriteSelector.SelectedSprite.ObjectID);
                if (def != null)
                {
                    localSpriteController.SpriteData.Definitions.Remove(def);
                    spriteSelector.Update(colors: null);
                }
            }
        }

        private void SpriteEditor_Activated(object sender, EventArgs e)
        {
            localGraphicsController.CheckFiles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateCode();
        }

        private void codeTags_DoubleClick(object sender, EventArgs e)
        {
            Tuple<string, string> tag = (Tuple<string, string>) codeTags.SelectedItems[0].Tag;
            ProjectView.ShowASMEditor(tag.Item1, tag.Item2);
        }
    }
}
