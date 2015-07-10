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

        private List<SpriteDefinition> backUpDefinitions;
        private bool spriteUpdating = false;
        private List<SpriteDefinition> modifiedDefinitions;

        public void Initialize()
        {
            backUpDefinitions = Controllers.Sprites.SpriteData.Definitions.MakeCopy();
            spriteSelector.Initialize(Controllers.Graphics.GraphicsData.Colors, Controllers.Graphics.GraphicsData.Palettes[0]);
            spriteViewer.Initialize(Controllers.Graphics.GraphicsData.Colors, Controllers.Graphics.GraphicsData.Palettes[0], Controllers.Levels.LevelData.OverlayPalette);
            paletteList.Palettes = Controllers.Graphics.GraphicsData.Palettes;
            paletteList.ColorReference = Controllers.Graphics.GraphicsData.Colors;
            paletteList.UpdateList();
            paletteList.SelectedIndex = 0;
            paletteList.SelectedIndexChanged += paletteList_SelectedIndexChanged;
            spriteSelector.SelectedSpriteChanged += spriteSelector_SelectedSpriteChanged;
            Controllers.Graphics.GraphicsUpdated += GraphicsUpdated;
            Controllers.Graphics.ExtraGraphicsUpdated += ExtraGraphicsUpdated;

            List<string> parseCollisions = Controllers.ASM.LinesFromTagOffset("prg000.asm", "ObjectCollision", 13 * 4);

            for (int index = 0, item = 0; index < parseCollisions.Count; item++)
            {
                string bottom = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string top = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string left = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                string right = string.Join(",", parseCollisions[index++].Split(' ').Where(s => s.StartsWith("$")).Reverse().ToList()).Trim(',');
                collisionBox.Items[item] = collisionBox.Items[item] + string.Format(" (L: {0} T: {1} R: {2} B: {3})", left, top, right, bottom).Replace("$", "");
            }

            for (int i = 0; i < 0x80; i++)
            {
                patTable.Items.Add(i.ToString("X2"));
            }

            modifiedDefinitions = new List<SpriteDefinition>();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Controllers.Graphics.GraphicsUpdated -= ExtraGraphicsUpdated;
            Controllers.Graphics.ExtraGraphicsUpdated -= GraphicsUpdated;
        }

        private void ExtraGraphicsUpdated(object sender, EventArgs e)
        {
            spriteViewer.UpdateGraphics();
        }

        private void GraphicsUpdated(object sender, EventArgs e)
        {
            spriteSelector.Update(colors: null);
            spriteViewer.UpdateGraphics();
        }

        private void spriteSelector_SelectedSpriteChanged(object sender, EventArgs e)
        {
            spriteUpdating = true;
            if (spriteSelector.SelectedSprite != null)
            {
                spriteViewer.CurrentDefinition = Controllers.Sprites.GetDefinition(spriteSelector.SelectedSprite.ObjectID);
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

                    gameID.Text = "Game Object ID: " + spriteViewer.CurrentDefinition.GameID.ToString("X2");
                }

                definitionCode.Text = EditorSpriteInfo.Serialize(spriteViewer.CurrentDefinition.SpriteInfo);
                codeTags.Items.Clear();

                string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);

                if (spriteViewer.CurrentDefinition.GameID >= 0xB4 && spriteViewer.CurrentDefinition.GameID <= 0xBB)
                {
                    attrGroups.Enabled = false;

                    ListViewItem item1 = new ListViewItem();
                    item1.Text = "Event Code";
                    item1.Tag = new Tuple<string, string>("prg005.asm", "ObjectsEvent@" + spriteSelector.SelectedSprite.ObjectID.ToString("X2"));

                    codeTags.Items.Add(item1);
                }
                else if (spriteViewer.CurrentDefinition.GameID >= 0xBC && spriteViewer.CurrentDefinition.GameID <= 0xD0)
                {
                    attrGroups.Enabled = false;

                    ListViewItem item1 = new ListViewItem();
                    item1.Text = "Generator Code";
                    item1.Tag = new Tuple<string, string>("prg007.asm", "ObjectsGenerator@" + spriteSelector.SelectedSprite.ObjectID.ToString("X2"));

                    codeTags.Items.Add(item1);
                }
                else
                {
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

                    attrGroups.Enabled = true;
                    LoadGfxAttributes();
                    LoadHitBoxOptions();
                    LoadOptions();
                    LoadPatternTableOptions();
                    LoadKillActionOptions();
                    LoadAttributeOptions();
                }
            }
            spriteUpdating = false;
        }

        private void editor_SpriteInfoSelected(object sender, EventArgs e)
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

        private void editor_SpriteInfoChanged(object sender, EventArgs e)
        {
            spriteViewer.UpdateGraphics();
            spriteSelector.Update(colors: null);
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
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
            Controllers.Sprites.SpriteData.Definitions = backUpDefinitions;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controllers.Sprites.Save();

            List<string> changedFiles = new List<string>();
            foreach (SpriteDefinition def in modifiedDefinitions)
            {
                changedFiles.Add(GetFileLocation(def.GameID));
            }

            Controllers.ASM.Save(changedFiles.Distinct().ToList());
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

                    var existing = Controllers.Sprites.SpriteData.Definitions.Where(d => d.GameID == val).FirstOrDefault();
                    if (existing != null)
                    {
                        MessageBox.Show(text + " already exists as an object id.");
                    }

                    Controllers.Sprites.SpriteData.Definitions.Add(new SpriteDefinition() { GameID = val, Name = "New Sprite" });
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
                var def = Controllers.Sprites.GetDefinition(spriteSelector.SelectedSprite.ObjectID);
                if (def != null)
                {
                    Controllers.Sprites.SpriteData.Definitions.Remove(def);
                    spriteSelector.Update(colors: null);
                }
            }
        }

        private void SpriteEditor_Activated(object sender, EventArgs e)
        {
            Controllers.Graphics.CheckFiles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateDrawingCode();
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

        private void gfxBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdatePatternTableOptions();
            }
        }

        private void patTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdatePatternTableOptions();
            }
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
            TextLocation loc = Controllers.ASM.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsGfxAttr@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsGfxAttr tag for this property.");
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

            modifiedDefinitions.Add(spriteViewer.CurrentDefinition);
            Controllers.ASM.UpdateTagLine("ObjectsGfxAttr@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadHitBoxOptions()
        {
            spriteUpdating = true;
            TextLocation loc = Controllers.ASM.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsHitBox@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsHitBox tag for this property.");
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
            modifiedDefinitions.Add(spriteViewer.CurrentDefinition);
            Controllers.ASM.UpdateTagLine("ObjectsHitBox@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadOptions()
        {
            spriteUpdating = true;
            TextLocation loc = Controllers.ASM.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsOptions@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsOptions tag for this property.");
                return;
            }

            string line = loc.Text;

            tailImmune.Checked = false;
            harmfulStomp.Checked = false;
            squashState.Checked = false;
            tailImmune.Checked = false;
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

                    case "OA3_NOTSTOMPABLE":
                        harmfulStomp.Checked = true;
                        break;

                    case "OA3_SQUASH":
                        squashState.Checked = true;
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
            string stompValue = harmfulStomp.Checked ? " | OA3_NOTSTOMPABLE " : "";
            string squashValue = squashState.Checked ? " | OA3_SQUASH " : "";
            string tailValue = tailImmune.Checked ? " | OA3_TAILATKIMMUNE " : "";

            string newLine = string.Format("\t.byte {0}{1}{2}{3}{4} ; Object{5:X2}", haltValue, shellValue, stompValue, squashValue, tailValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            modifiedDefinitions.Add(spriteViewer.CurrentDefinition);
            Controllers.ASM.UpdateTagLine("ObjectsOptions@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadPatternTableOptions()
        {
            spriteUpdating = true;
            TextLocation loc = Controllers.ASM.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsPatTable@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsPatTable tag for this property.");
                return;
            }

            string line = loc.Text;
            foreach (string s in line.Split(' ', '|'))
            {
                string t = s.Trim();
                if (t == "OPTS_NOCHANGE")
                {
                    gfxBank.SelectedIndex = 0;
                    patTable.Enabled = false;
                    patTable.SelectedIndex = -1;
                    break;
                }
                else if (t == "OPTS_SETPT5")
                {
                    gfxBank.SelectedIndex = 1;
                    patTable.Enabled = true;
                }
                else if (t == "OPTS_SETPT6")
                {
                    gfxBank.SelectedIndex = 2;
                    patTable.Enabled = true;
                }
                else if (t.Contains(";"))
                {
                    break;
                }
                else if (t.StartsWith("$"))
                {
                    patTable.SelectedIndex = Convert.ToInt32(t.Trim('$'), 16);
                }
            }
            spriteUpdating = false;
        }

        private void UpdatePatternTableOptions()
        {
            string bank = "";
            switch (gfxBank.SelectedIndex)
            {
                case 0:
                    bank = "OPTS_NOCHANGE";
                    break;

                case 1:
                    bank = "OPTS_SETPT5";
                    break;

                case 2:
                    bank = "OPTS_SETPT6";
                    break;
            }

            string patTableValue = "";
            if (patTable.Enabled)
            {
                patTableValue = " | $" + patTable.SelectedIndex.ToString("X2");
            }
            string newLine = string.Format("\t.byte {0}{1}; Object{5:X2}", bank, patTableValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            Controllers.ASM.UpdateTagLine("ObjectsPatTable@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private string lastDefcode = "";
        private void UpdateDrawingCode()
        {
            List<SpriteInfo> info;


            info = EditorSpriteInfo.Deserialize(definitionCode.Text);
            if (info != null)
            {
                spriteViewer.CurrentDefinition.SpriteInfo = info;
                syntaxError.Visible = false;
                spriteViewer.UpdateGraphics();
                spriteSelector.Update(colors: null);
            }

            lastDefcode = definitionCode.Text;


        }

        private void LoadKillActionOptions()
        {
            spriteUpdating = true;
            TextLocation loc = Controllers.ASM.FindTagLine(GetFileLocation(spriteViewer.CurrentDefinition.GameID), "ObjectsKill@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsKill tag for this property.");
                return;
            }

            string line = loc.Text;

            foreach (string s in line.Split(' ', '|'))
            {
                switch (s.Trim())
                {
                    case "KILLACT_STANDARD":
                        killAction.SelectedIndex = 0;
                        break;

                    case "KILLACT_JUSTDRAW16X16":
                        killAction.SelectedIndex = 1;
                        break;

                    case "KILLACT_JUSTDRAWMIRROR":
                        killAction.SelectedIndex = 2;
                        break;

                    case "KILLACT_JUSTDRAW16X32":
                        killAction.SelectedIndex = 3;
                        break;

                    case "KILLACT_JUSTDRAWTALLFLIP":
                        killAction.SelectedIndex = 4;
                        break;

                    case "KILLACT_NORMALANDKILLED":
                        killAction.SelectedIndex = 5;
                        break;

                    case "KILLACT_GIANTKILLED":
                        killAction.SelectedIndex = 6;
                        break;

                    case "KILLACT_POOFDEATH":
                        killAction.SelectedIndex = 7;
                        break;

                    case "KILLACT_DRAWMOVENOHALT":
                        killAction.SelectedIndex = 8;
                        break;

                    case "KILLACT_NORMALSTATE":
                        killAction.SelectedIndex = 9;
                        break;
                }
            }

            spriteUpdating = false;
        }

        private void UpdateKillActionOptions()
        {
            string killValue = "";
            switch (killAction.SelectedIndex)
            {

                case 0:
                    killValue = "KILLACT_STANDARD";
                    break;

                case 1:
                    killValue = "KILLACT_JUSTDRAW16X16";
                    break;

                case 2:
                    killValue = "KILLACT_JUSTDRAWMIRROR";
                    break;

                case 3:
                    killValue = "KILLACT_JUSTDRAW16X32";
                    break;

                case 4:
                    killValue = "KILLACT_JUSTDRAWTALLFLIP";
                    break;

                case 5:
                    killValue = "KILLACT_NORMALANDKILLED";
                    break;

                case 6:
                    killValue = "KILLACT_GIANTKILLED";
                    break;

                case 7:
                    killValue = "KILLACT_POOFDEATH";
                    break;

                case 8:
                    killValue = "KILLACT_DRAWMOVENOHALT";
                    break;

                case 9:
                    killValue = "KILLACT_NORMALSTATE";
                    break;
            }

            string newLine = string.Format("\t.byte {0}; Object{5:X2}", killValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            Controllers.ASM.UpdateTagLine("ObjectsKill@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void LoadAttributeOptions()
        {
            spriteUpdating = true;
            TextLocation loc = Controllers.ASM.FindTagLine("prg000.asm", "ObjectsAttr@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"));
            if (loc == null)
            {
                MessageBox.Show("Unable to locate the ObjectsAttr tag for this property.");
                return;
            }

            string line = loc.Text;

            bounceOthers.Checked = false;
            immuneIce.Checked = false;
            immuneFire.Checked = false;
            immuneNinjaHammers.Checked = false;
            foreach (string s in line.Split(' ', '|'))
            {
                switch (s.Trim())
                {
                    case "OAT_BOUNDBOX00":
                        boundBox.SelectedIndex = 0;
                        break;

                    case "OAT_BOUNDBOX01":
                        boundBox.SelectedIndex = 1;
                        break;

                    case "OAT_BOUNDBOX02":
                        boundBox.SelectedIndex = 2;
                        break;

                    case "OAT_BOUNDBOX03":
                        boundBox.SelectedIndex = 3;
                        break;

                    case "OAT_BOUNDBOX04":
                        boundBox.SelectedIndex = 4;
                        break;

                    case "OAT_BOUNDBOX05":
                        boundBox.SelectedIndex = 5;
                        break;

                    case "OAT_BOUNDBOX06":
                        boundBox.SelectedIndex = 6;
                        break;

                    case "OAT_BOUNDBOX07":
                        boundBox.SelectedIndex = 7;
                        break;

                    case "OAT_BOUNDBOX08":
                        boundBox.SelectedIndex = 8;
                        break;

                    case "OAT_BOUNDBOX09":
                        boundBox.SelectedIndex = 9;
                        break;

                    case "OAT_BOUNDBOX10":
                        boundBox.SelectedIndex = 10;
                        break;

                    case "OAT_BOUNDBOX11":
                        boundBox.SelectedIndex = 11;
                        break;

                    case "OAT_BOUNDBOX12":
                        boundBox.SelectedIndex = 12;
                        break;

                    case "OAT_BOUNDBOX13":
                        boundBox.SelectedIndex = 13;
                        break;

                    case "OAT_BOUNDBOX14":
                        boundBox.SelectedIndex = 14;
                        break;

                    case "OAT_BOUNDBOX15":
                        boundBox.SelectedIndex = 15;
                        break;

                    case "OAT_BOUNCEOFFOTHERS":
                        bounceOthers.Checked = true;
                        break;

                    case "OAT_ICEIMMUNITY":
                        immuneIce.Checked = true;
                        break;

                    case "OAT_FIREIMMUNITY":
                        immuneFire.Checked = true;
                        break;

                    case "OAT_HITNOTKILL":
                        immuneNinjaHammers.Checked = true;
                        break;
                }
            }

            spriteUpdating = false;
        }

        private void UpdateAttributeOptions()
        {
            string boundBoxValue = "OAT_BOUNDBOX" + boundBox.SelectedIndex.ToString("X2");
            string bounceValue = bounceOthers.Checked ? " | OAT_BOUNCEOFFOTHERS" : "";
            string iceValue = immuneIce.Checked ? " | OAT_ICEIMMUNITY" : "";
            string fireValue = immuneFire.Checked ? " | OAT_FIREIMMUNITY" : "";
            string ninjaHammerValue = immuneNinjaHammers.Checked ? " | OAT_HITNOTKILL" : "";

            string newLine = string.Format("\t.byte {0}{1}{2}{3}{4}; Object{5:X2}", boundBoxValue, bounceValue, iceValue, fireValue, ninjaHammerValue, spriteViewer.CurrentDefinition.GameID);
            string file = GetFileLocation(spriteViewer.CurrentDefinition.GameID);
            Controllers.ASM.UpdateTagLine("ObjectsAtt@" + spriteViewer.CurrentDefinition.GameID.ToString("X2"), file, newLine);
        }

        private void killAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!spriteUpdating)
            {
                UpdateKillActionOptions();
            }
        }

        private int highlightedLine = -1;
        private void HighlightSprites()
        {

            if (definitionCode.Text.Length > 0)
            {
                string[] lines = definitionCode.Text.Split('\n');
                int current = lines[0].Length;
                int lineNumber = 0;
                while(true)
                {
                    if (definitionCode.SelectionStart > current)
                    {
                        lineNumber++;
                        current += lines[lineNumber].Length + 1;
                        
                    }
                    else
                    {
                        break;
                    }
                }

                if (highlightedLine != lineNumber)
                {
                    if (lineNumber < spriteViewer.CurrentDefinition.SpriteInfo.Count)
                    {
                        spriteViewer.HighlightedSpriteInfo.Clear();
                        spriteViewer.HighlightedSpriteInfo.Add(spriteViewer.CurrentDefinition.SpriteInfo[lineNumber]);
                        spriteViewer.UpdateGraphics();
                        highlightedLine = lineNumber;
                    }
                }
            }
        }

        private void definitionCode_KeyUp(object sender, KeyEventArgs e)
        {
            HighlightSprites();
            if (e.KeyCode == Keys.F5)
            {
                UpdateDrawingCode();
            }
        }

        private void definitionCode_MouseUp(object sender, MouseEventArgs e)
        {
            HighlightSprites();
        }

        private void spriteViewer_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle bounds = Controllers.Sprites.GetClipBounds(spriteViewer.CurrentSprite);
            int x = (512 / 2) - bounds.Width / 2;
            int y = (512 / 2) - bounds.Height / 2;
            List<Rectangle> rectangles = spriteViewer.CurrentDefinition.SpriteInfo.Select(i => new Rectangle(i.X + x, i.Y + y, 8, 16)).ToList();
            Point p = new Point(e.X, e.Y);
            Rectangle rect = rectangles.Where(r => r.Contains(p)).FirstOrDefault();
            int start = 0;
            int end = 0;
            if (rect != default(Rectangle))
            {
                int index = rectangles.IndexOf(rect);
                string[] lines = definitionCode.Text.Split('\n');
                int i = 0;
                while (i < index)
                {
                    start += lines[i++].Length + 1;
                }

                end = lines[i].Length;
                definitionCode.Focus();
                definitionCode.SelectionStart = start;
                definitionCode.SelectionLength = end;
                HighlightSprites();
            }
        }

        private void spriteSelector_Load(object sender, EventArgs e)
        {

        }
    }
}
