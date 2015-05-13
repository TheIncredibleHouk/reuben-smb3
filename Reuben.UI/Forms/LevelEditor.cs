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
        private bool initializing = false;
        public void LoadLevel(LevelInfo info, LevelController levelController, GraphicsController graphicsController, StringController stringController, SpriteController spriteController)
        {
            initializing = true;
            levels = levelController;
            graphics = graphicsController;

            levelInfo = info;
            levelViewer.Level = level = levels.LoadLevel(info.File);
            levelViewer.LevelType = levels.LevelData.Types[level.LevelType];
            blockSelector = new BlockSelector();
            spriteSelector = new SpriteSelector();

            spriteSelector.ColorReference = blockSelector.ColorReference = paletteList.ColorReference = levelViewer.ColorReference = graphics.GraphicsData.Colors;

            sprites = spriteSelector.Sprites = spriteController;

            levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            blockSelector.PatternTable = levelViewer.PatternTable = graphics.MakePatternTable(new List<int>() { level.GraphicsID, level.GraphicsID + 1, 0xD0, 0xD1 });
            levelViewer.Sprites = sprites;
            spriteSelector.Graphics = levelViewer.Graphics = graphics;
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSpriteDisplay(0, 0, 16 * 15, 0x1B);

            blockSelector.BlockList = levelViewer.LevelType.Blocks;


            paletteList.Palettes = graphics.GraphicsData.Palettes;
            paletteList.SelectedPalette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            strings = stringController;

            musicList.DataSource = strings.GetStringList("music");
            musicList.SelectedIndex = level.MusicID;

            screenList.DataSource = strings.GetStringList("screens");
            screenList.SelectedIndex = level.NumberOfScreens - 1;

            UpdateScreenSize();

            blockSelector.Show();
            spriteSelector.Show();
            blockSelector.Editor = spriteSelector.Editor = this;
            MoveSelectors();
            initializing = false;
        }

        public void UpdatePalette()
        {
            spriteSelector.Palette = blockSelector.Palette = levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSpriteDisplay(0, 0, 16 * 15, 0x1B);
            blockSelector.UpdateGraphics();
            spriteSelector.UpdateGraphics();
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.PaletteID = paletteList.SelectedPalette.ID;
            UpdatePalette();
        }

        private int mouseStartX = 0;
        private int mouseStartY = 0;

        public EditMode EditMode
        {
            get { return (EditMode)editList.SelectedIndex; }
            set
            {
                editList.SelectedIndex = (int)value;
            }
        }

        private void levelViewer_MouseDown(object sender, MouseEventArgs e)
        {
            if (EditMode == UI.EditMode.Blocks && !rightMouseDragged && !leftMouseDragged)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left && !rightMouseDrag)
                {
                    levelViewer.SelectionType = SelectionType.Draw;
                    leftMouseDrag = true;
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && !leftMouseDrag)
                {
                    levelViewer.SelectionType = SelectionType.Select;
                    rightMouseDrag = true;
                }

                int col = e.X / 16;
                int row = e.Y / 16;
                mouseStartX = e.X;
                mouseStartY = e.Y;
                levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, 15, 15);
            }
            else if (EditMode == UI.EditMode.Sprites)
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
                else
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && !rightMouseDrag)
                    {
                        levelViewer.SelectionType = SelectionType.Draw;
                        leftMouseDrag = true;
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Right && !leftMouseDrag)
                    {
                        levelViewer.SelectionType = SelectionType.Select;
                        rightMouseDrag = true;
                    }

                    int col = e.X / 16;
                    int row = e.Y / 16;
                    mouseStartX = e.X;
                    mouseStartY = e.Y;
                    levelViewer.SelectionRectangle = Rectangle.Empty;
                }
            }
        }

        private bool leftMouseDrag = false;
        private bool leftMouseDragged = false;
        private bool rightMouseDrag = false;
        private bool rightMouseDragged = false;

        private void levelViewer_MouseMove(object sender, MouseEventArgs e)
        {

            if (leftMouseDrag || rightMouseDrag)
            {
                leftMouseDragged = leftMouseDrag;
                rightMouseDragged = rightMouseDrag;
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

        private List<DataChange> undoBuffer = new List<DataChange>();
        private byte[,] blockClipBoard;
        private int deletetile = 0x80;
        private void levelViewer_MouseUp(object sender, MouseEventArgs e)
        {
            int minX = Math.Min(e.X, mouseStartX);
            int minY = Math.Min(e.Y, mouseStartY);
            int maxX = Math.Max(e.X, mouseStartX);
            int maxY = Math.Max(e.Y, mouseStartY);

            var col = Math.Max(minX, 0) / 16;
            var row = Math.Max(minY, 0) / 16;
            var width = Math.Min((maxX / 16) - col, 0xEF) + 1;
            var height = Math.Min((maxY / 16) - row, 0x1A) + 1;
            if (EditMode == UI.EditMode.Blocks)
            {
                // left mouse dragged, left mouse up
                if (e.Button == System.Windows.Forms.MouseButtons.Left && leftMouseDragged)
                {
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
                }

                // left mouse drag, right mouse up
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && leftMouseDrag)
                {
                    if (blockClipBoard != null)
                    {

                        int maxWidth = blockClipBoard.GetLength(0);
                        int maxHeight = blockClipBoard.GetLength(1);
                        if (width == 1 && height == 1)
                        {
                            width = maxWidth;
                            height = maxHeight;
                        }

                        DataChange change = new DataChange();
                        change.Column = col;
                        change.Row = row;
                        change.Data = new byte[width, height];
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                change.Data[i, j] = level.Data[col + i, row + j];
                                level.Data[col + i, row + j] = blockClipBoard[i % maxWidth, j % maxHeight];
                            }
                        }

                        undoBuffer.Add(change);
                        levelViewer.UpdateBlockDisplay(col, row, width, height);
                    }
                }
                //  right mouse drag, right mouse up
                else if (e.Button == System.Windows.Forms.MouseButtons.Right && rightMouseDragged)
                {
                    if (width == 1 && height == 1)
                    {
                        blockSelector.SelectedBlock = level.Data[col, row];
                    }
                    else
                    {
                        blockClipBoard = new byte[width, height];
                        for (int i = 0; i < width; i++)
                        {
                            for (int j = 0; j < height; j++)
                            {
                                blockClipBoard[i, j] = level.Data[col + i, row + j];
                            }
                        }
                    }
                }

                // right mouse drag, left mouse up
                else if (e.Button == System.Windows.Forms.MouseButtons.Left && rightMouseDrag)
                {
                    DataChange change = new DataChange();
                    change.Column = col;
                    change.Row = row;
                    change.Data = new byte[width, height];
                    blockClipBoard = new byte[width, height];
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            change.Data[i, j] = blockClipBoard[i, j] = level.Data[col + i, row + j];
                            level.Data[col + i, row + j] = (byte)deletetile;
                        }
                    }

                    levelViewer.UpdateBlockDisplay(col, row, width, height);

                    undoBuffer.Add(change);
                }
                // right mouse click
                else if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    blockSelector.SelectedBlock = level.Data[col, row];
                }

                rightMouseDrag = leftMouseDrag = leftMouseDragged = rightMouseDragged = false;

            }
            else if (EditMode == UI.EditMode.Sprites)
            {
                if(leftMouseDragged)
                {
                    List<Tuple<Sprite, Rectangle>> boundCache = sprites.GetBounds(level.Sprites).ToList();
                    levelViewer.SelectedSprites.Clear();
                    List<Tuple<Sprite, Rectangle>> affectedSprites = boundCache.Where(t => t.Item2.IntersectsWith(levelViewer.SelectionRectangle)).ToList();
                    levelViewer.SelectedSprites.AddRange(affectedSprites.Select(s => s.Item1));
                    levelViewer.Invalidate(levelViewer.SelectionRectangle);
                    levelViewer.SelectionRectangle = Rectangle.Empty;
                }
                else if(leftMouseDrag)
                {
                    levelViewer.SelectedSprites.Clear();
                }
                rightMouseDrag = leftMouseDrag = leftMouseDragged = rightMouseDragged = false;
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
                if (blockSelector.Snapped)
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

        public void UpdateScreenSize()
        {
            lvlHost.Width = level.NumberOfScreens * 16 * 16;
        }

        private void screenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                level.NumberOfScreens = screenList.SelectedIndex + 1;
                UpdateScreenSize();
            }
        }

        private void editList_SelectedIndexChanged(object sender, EventArgs e)
        {
            levelViewer.EditMode = EditMode;
        }

    }
}
