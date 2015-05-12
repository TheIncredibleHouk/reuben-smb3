using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class LevelEditor : Form
    {
        public LevelEditor()
        {
            InitializeComponent();
        }

        private Level level;
        private LevelInfo levelInfo;
        private LevelController levels;
        private GraphicsController graphics;
        private StringController strings;
        private SpriteController sprites;

        private BlockSelector blockSelector;
        private SpriteSelector spriteSelector;

        public void LoadLevel(LevelInfo info, LevelController levelController, GraphicsController graphicsController, StringController stringController, SpriteController spriteController)
        {
            levels = levelController;
            graphics = graphicsController;

            levelInfo = info;
            levelViewer.Level = level = levels.LoadLevel(info.File);
            levelViewer.LevelType = levels.LevelData.Types[level.LevelType];
            blockSelector = new BlockSelector();
            spriteSelector = new SpriteSelector();

            blockSelector.ColorReference = paletteList.ColorReference = levelViewer.ColorReference = graphics.GraphicsData.Colors;

            sprites = spriteController;

            levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            blockSelector.PatternTable = levelViewer.PatternTable = graphics.MakePatternTable(new List<int>() { level.GraphicsID, level.GraphicsID + 1, 0xD0, 0xD1 });
            levelViewer.Sprites = sprites;
            levelViewer.Sprites = sprites;
            levelViewer.Graphics = graphics;
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSpriteDisplay(0, 0, 16 * 15, 0x1B);

            blockSelector.BlockList = levelViewer.LevelType.Blocks;


            lvlHost.Width = level.NumberOfScreens * 16 * 16;

            paletteList.Palettes = graphics.GraphicsData.Palettes;
            paletteList.SelectedPalette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            strings = stringController;



            musicList.DataSource = strings.GetStringList("music");
            musicList.SelectedIndex = level.MusicID;

            screenList.DataSource = strings.GetStringList("screens");
            screenList.SelectedIndex = level.NumberOfScreens - 1;

            blockSelector.Show();
            spriteSelector.Show();
            blockSelector.Editor = spriteSelector.Editor = this;
            MoveSelectors();
        }

        public void UpdatePalette()
        {
            blockSelector.Palette = levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSpriteDisplay(0, 0, 16 * 15, 0x1B);
            blockSelector.UpdateGraphics();
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.PaletteID = paletteList.SelectedPalette.ID;
            UpdatePalette();
        }

        private int mouseStartX = 0;
        private int mouseStartY = 0;

        private EditMode editMode;
        public EditMode EditMode
        {
            get { return (EditMode)editList.SelectedIndex; }
            set { editList.SelectedIndex = (int)value; }
        }

        private void levelViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (EditMode == UI.EditMode.Blocks && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int col = e.X / 16;
                int row = e.Y / 16;
                mouseStartX = e.X;
                mouseStartY = e.Y;
                levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, 15, 15);
                leftMouseDrag = true;
            }
            else if (EditMode == UI.EditMode.Sprites)
            {
                if (!leftMouseDragged)
                {

                    List<Tuple<Sprite, Rectangle>> boundCache = sprites.GetBounds(level.Sprites).ToList();
                    Sprite selectedSprite = boundCache.Where(t => t.Item2.Contains(e.X, e.Y)).Select(s => s.Item1).FirstOrDefault(); // find the sprite clicked on

                    if (selectedSprite != null)
                    {
                        if (levelViewer.SelectedSprites.Count > 0)
                        {

                            List<Rectangle> affectedArea = boundCache.Where(s => levelViewer.SelectedSprites.Contains(s.Item1)).Select(s => s.Item2).ToList(); // only the bounds of the sprites that were previously selected                           
                            affectedArea.Add(sprites.GetClipBounds(selectedSprite)); // add the new sprite to the list
                            Rectangle updateArea = affectedArea.Combine();
                            levelViewer.Invalidate(new Rectangle(updateArea.X, updateArea.Y, updateArea.Width + 1, updateArea.Height + 1));
                        }
                        else
                        {
                            Rectangle updateArea = sprites.GetClipBounds(selectedSprite);
                            levelViewer.Invalidate(new Rectangle(updateArea.X, updateArea.Y, updateArea.Width + 1, updateArea.Height + 1));
                        }

                        levelViewer.SelectedSprites.Clear();
                        levelViewer.SelectedSprites.Add(selectedSprite);
                    }

                }
            }
        }

        private bool leftMouseDrag = false;
        private bool leftMouseDragged = false;

        private void levelViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (EditMode == UI.EditMode.Blocks && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (leftMouseDrag)
                {
                    leftMouseDragged = true;
                    int minX = Math.Min(e.X, mouseStartX);
                    int minY = Math.Min(e.Y, mouseStartY);
                    int maxX = Math.Max(e.X, mouseStartX);
                    int maxY = Math.Max(e.Y, mouseStartY);

                    var col = Math.Max(minX, 0) / 16;
                    var row = Math.Max(minY, 0) / 16;
                    var width = Math.Min((maxX / 16) - col, 0xEF) + 1;
                    var height = Math.Min((maxY / 16) - row, 0x1A) + 1;
                    levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, width * 16 - 1, height * 16 - 1);
                }
            }
        }

        private List<DataChange> undoBuffer = new List<DataChange>();
        private void levelViewer_MouseUp(object sender, MouseEventArgs e)
        {
            if (EditMode == UI.EditMode.Blocks && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int minX = Math.Min(e.X, mouseStartX);
                int minY = Math.Min(e.Y, mouseStartY);
                int maxX = Math.Max(e.X, mouseStartX);
                int maxY = Math.Max(e.Y, mouseStartY);

                var col = Math.Max(minX, 0) / 16;
                var row = Math.Max(minY, 0) / 16;
                var width = Math.Min((maxX / 16) - col, 0xEF) + 1;
                var height = Math.Min((maxY / 16) - row, 0x1A) + 1;

                DataChange change = new DataChange();
                change.Column = col;
                change.Row = row;
                change.Data = new byte[width, height];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        change.Data[i, j] = level.Data[col + i, row + j];
                        level.Data[col + i, row + j] = (byte)blockSelector.SelectedBlock;
                    }
                }

                undoBuffer.Add(change);
                levelViewer.UpdateBlockDisplay(col, row, width, height);
                leftMouseDrag = false;
            }
            else if (EditMode == UI.EditMode.Sprites)
            {

            }
        }

        public void MoveSelectors()
        {
            if (blockSelector != null && blockSelector.Snapped)
            {
                blockSelector.Location = new Point(this.Location.X - blockSelector.Width, this.Location.Y);
                blockSelector.Snapped = true;
            }

            if (spriteSelector != null && spriteSelector.Snapped)
            {
                if(blockSelector.Snapped)
                {
                    spriteSelector.Location = new Point(this.Location.X - blockSelector.Width, blockSelector.Bottom);
                }
                else
                {
                    spriteSelector.Location = new Point(this.Location.X - spriteSelector.Width, this.Location.Y);
                }
                spriteSelector.Snapped = true;
            }
        }
        protected override void OnClosed(EventArgs e)
        {
            blockSelector.Close();
            spriteSelector.Close();
            base.OnClosed(e);
        }

        private void LevelEditor_Move(object sender, EventArgs e)
        {
            MoveSelectors();
        }

        private void Undo()
        {
            if (undoBuffer.Count > 0)
            {
                DataChange change = undoBuffer.Last();
                int width = change.Data.GetLength(0), height = change.Data.GetLength(1);
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        level.Data[i + change.Column, j + change.Row] = change.Data[i, j];
                    }
                }

                levelViewer.UpdateBlockDisplay(change.Column, change.Row, width, height);

                undoBuffer.Remove(change);
            }
        }

        private void LevelEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Z:
                        Undo();
                        break;
                }
            }
        }

        private void LevelEditor_Activated(object sender, EventArgs e)
        {
      
        }

        private void LevelEditor_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized || WindowState == FormWindowState.Normal)
            {
                blockSelector.WindowState = spriteSelector.WindowState = WindowState;
            }
        }

    }
}
