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
        private ASMController localASMController;

        public void Initialize(ASMController asmController, GraphicsController graphicsController, SpriteController spriteController, LevelController levelController)
        {
            localASMController = asmController;
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

            List<string> parseCollisions = localASMController.LinesFromTagOffset("prg000.asm", "ObjectCollision", 13 * 4);

            for (int index = 0, item = 0; index < parseCollisions.Count; item++)
            {
                string bottom = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string top = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string left = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string right = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                collisionBox.Items[item] = collisionBox.Items[item] + string.Format(" (L: {0} T: {1} R: {2} B: {3})", left, top, right, bottom).Replace("$", "");
            }
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
        private bool spriteUpdating = false;

        void spriteSelector_SelectedSpriteChanged(object sender, EventArgs e)
        {
            spriteUpdating = true;
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

                string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);


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

                LoadGfxAttributes();
                LoadHitBoxOptions();
            }
            spriteUpdating = false;
        }

        private string GetFileLocation(int spriteId)
        {
            if (spriteId < 0x24)
            {
                return "prg001.asm";
            }
            else if (spriteId < 0x48)
            {
                return "prg002.asm";
            }
            else if (spriteId < 0x6C)
            {
                return "prg003.asm";
            }
            else if (spriteId < 0x9B)
            {
                return "prg004.asm";
            }
            else
            {
                return "prg005.asm";
            }
        }

        private void LoadGfxAttributes()
        {
            spriteUpdating = true;
            TextLocation loc = localASMController.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsGfxAttr@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the tag for this property.");
                return;
            }

            string line = loc.Text;
            foreach (string s in line.Split(' ', '|'))
            {
                switch (s.Trim())
                {
                    case "OA1_PAL0":
                        gamePalette.SelectedIndex = 0;
                        break;

                    case "OA1_PAL1":
                        gamePalette.SelectedIndex = 1;
                        break;

                    case "OA1_PAL2":
                        gamePalette.SelectedIndex = 2;
                        break;

                    case "OA1_PAL3":
                        gamePalette.SelectedIndex = 3;
                        break;

                    case "OA1_HEIGHT16":
                        clipHeight.SelectedIndex = 0;
                        break;

                    case "OA1_HEIGHT32":
                        clipHeight.SelectedIndex = 1;
                        break;

                    case "OA1_HEIGHT48":
                        clipHeight.SelectedIndex = 2;
                        break;

                    case "OA1_HEIGHT64":
                        clipHeight.SelectedIndex = 3;
                        break;

                    case "OA1_WIDTH8":
                        clipWidth.SelectedIndex = 0;
                        break;

                    case "OA1_WIDTH16":
                        clipWidth.SelectedIndex = 1;
                        break;

                    case "OA1_WIDTH24":
                        clipWidth.SelectedIndex = 2;
                        break;

                    case "OA1_WIDTH32":
                        clipWidth.SelectedIndex = 3;
                        break;

                    case "OA1_WIDTH40":
                        clipWidth.SelectedIndex = 4;
                        break;

                    case "OA1_WIDTH48":
                        clipWidth.SelectedIndex = 5;
                        break;

                    case "OA1_WIDTH64":
                        clipWidth.SelectedIndex = 6;
                        break;

                }
            }

            spriteUpdating = false;
        }

        private void UpdateGfxAttributesString()
        {
            string palValue = "OA1_PAL" + gamePalette.SelectedIndex;
            string clipHeightValue = "OA1_HEIGHT" + (clipHeight.SelectedIndex + 1) * 16;
            string clipWidthValue = "OA1_WIDTH" + (clipWidth.SelectedIndex + 1) * 8;

            string newLine = string.Format("\t.byte {0} | {1} | {2} ; Object{3:X2}", palValue, clipWidthValue, clipHeightValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);

            spriteViewer.CurrentDefinition.GfxAttributes2Code = newLine;
            localASMController.UpdateTagLine("ObjectsGfxAttr@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadHitBoxOptions()
        {
            spriteUpdating = true;
            TextLocation loc = localASMController.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsHitBox@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the tag for this property.");
                return;
            }

            string line = loc.Text;

            stompApathy.Checked = false;
            shellStomp.Checked = true;
            foreach (string s in line.Split(' ', '|'))
            {
                switch (s.Trim())
                {
                    case "OA2_NOSHELLORSQUASH":
                        shellStomp.Checked = false;
                        break;

                    case "OA2_STOMPDONTCARE":
                        stompApathy.Checked = true;
                        break;

                    case "OA2_TDOGRP0":
                        collisionBox.SelectedIndex = 0;
                        break;

                    case "OA2_TDOGRP1":
                        collisionBox.SelectedIndex = 1;
                        break;

                    case "OA2_TDOGRP2":
                        collisionBox.SelectedIndex = 2;
                        break;

                    case "OA2_TDOGRP3":
                        collisionBox.SelectedIndex = 3;
                        break;

                    case "OA2_TDOGRP4":
                        collisionBox.SelectedIndex = 4;
                        break;

                    case "OA2_TDOGRP5":
                        collisionBox.SelectedIndex = 5;
                        break;

                    case "OA2_TDOGRP6":
                        collisionBox.SelectedIndex = 6;
                        break;

                    case "OA2_TDOGRP7":
                        collisionBox.SelectedIndex = 7;
                        break;

                    case "OA2_TDOGRP8":
                        collisionBox.SelectedIndex = 8;
                        break;

                    case "OA2_TDOGRP9":
                        collisionBox.SelectedIndex = 9;
                        break;

                    case "OA2_TDOGRP10":
                        collisionBox.SelectedIndex = 10;
                        break;

                    case "OA2_TDOGRP11":
                        collisionBox.SelectedIndex = 11;
                        break;

                    case "OA2_TDOGRP12":
                        collisionBox.SelectedIndex = 12;
                        break;
                }
            }
            spriteUpdating = false;
        }

        private void UpdateHitBoxString()
        {
            string collisionBoxValue = "OA2_TDOGRP" + collisionBox.SelectedIndex;
            string shellValue = !shellStomp.Checked ? " | OA2_NOSHELLORSQUASH " : "";
            string apathyValue = stompApathy.Checked ? " | OA2_STOMPDONTCARE" : "";

            string newLine = string.Format("\t.byte {0}{1}{2} ; Object{3:X2}", collisionBoxValue, shellValue, apathyValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            spriteViewer.CurrentDefinition.GfxAttributes2Code = newLine;
            localASMController.UpdateTagLine("ObjectsHitBox@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadOptions()
        {
            spriteUpdating = true;
            TextLocation loc = localASMController.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsOptions@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the tag for this property.");
                return;
            }

            string line = loc.Text;

            tailImmune.Checked = true;

            foreach (string s in line.Split(' ', '|'))
            {
                switch (s.Trim())
                {
                    case "OA3_HALT_JUSTDRAW":
                        gameHalt.SelectedIndex = 2;
                        break;

                    case "OA3_HALT_JUSTDRAWTALL":
                        gameHalt.SelectedIndex = 5;
                        break;

                    case "OA3_HALT_DONOTHING":
                        gameHalt.SelectedIndex = 0;
                        break;

                    case "OA3_HALT_NORMALONLY":
                        gameHalt.SelectedIndex = 1;
                        break;

                    case "OA3_HALT_JUSTDRAWWIDE":
                        gameHalt.SelectedIndex = 5;
                        break;

                    case "OA3_HALT_JUSTDRAWMIRROR":
                        gameHalt.SelectedIndex = 3;
                        break;

                    case "OA3_TAILATKIMMUNE":
                        tailImmune.Checked = true;
                        break;
                }
            }
            spriteUpdating = false;
        }

        private void UpdateOptions()
        {
            string haltValue = "";
            switch (gameHalt.SelectedIndex)
            {
                case 0:
                    haltValue = "OA3_HALT_DONOTHING";
                    break;

                case 1:
                    haltValue = "OA3_HALT_NORMALONLY";
                    break;

                case 2:
                    haltValue = "OA3_HALT_JUSTDRAW";
                    break;

                case 3:
                    haltValue = "OA3_HALT_JUSTDRAWMIRROR";
                    break;

                case 4:
                    haltValue = "OA3_HALT_JUSTDRAWTALL";
                    break;

                case 5:
                    haltValue = "OA3_HALT_JUSTDRAWWIDE";
                    break;

            }
            string shellValue = shellStomp.Checked ? " | OA3_DIESHELLED " : "";
            string stompValue = stompApathy.Checked ? " | OA3_NOTSTOMPABLE " : "";
            string squashValue = squashState.Checked ? " | OA3_SQUASH " : "";
            string tailValue = tailImmune.Checked ? " | OA3_TAILATKIMMUNE " : "";

            string newLine = string.Format("\t.byte {0}{1}{2}{3}{4} ; Object{5:X2}", haltValue, shellValue, stompValue, squashValue, tailValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            spriteViewer.CurrentDefinition.GfxAttributes2Code = newLine;
            localASMController.UpdateTagLine("ObjectsOptions@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
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

            List<string> changedFiles = new List<string>();
            foreach (SpriteDefinition def in localSpriteController.SpriteData.Definitions.Where(d => d.GfxAttributes2Code != null))
            {
                string file = GetFileLocation(def.GameID);
                if (localASMController.IsDirty(file))
                {
                    MessageBox.Show("Unable to save changes to " + file + " due to unsaved changes in the ASM Editor. Save these files then save the Sprite Editor again.");
                    break;
                }

                localASMController.UpdateTagLine("ObjectsGfxAttr@" + def.GameID.ToString("X2"), file, def.GfxAttributes2Code);
                changedFiles.Add(file);
            }

            localASMController.Save(changedFiles.Distinct().ToList());
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
            Tuple<string, string> tag = (Tuple<string, string>)codeTags.SelectedItems[0].Tag;
            ProjectView.ShowASMEditor(tag.Item1, tag.Item2);
        }

        private void gamePalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateGfxAttributesString();
            }
        }

        private void clipWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateGfxAttributesString();
            }
        }

        private void clipHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateGfxAttributesString();
            }
        }

        private void collisionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateHitBoxString();
            }
        }

        private void shellStomp_CheckedChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateHitBoxString();
                UpdateOptions();
            }
        }

        private void stompApathy_CheckedChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateHitBoxString();
            }
        }

        private void harmfulStomp_CheckedChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateOptions();
            }
        }

        private void tailImmune_CheckedChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateOptions();
            }
        }

        private void squashState_CheckedChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateOptions();
            }
        }

        private void gameHalt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateOptions();
            }
        }
    }
}
