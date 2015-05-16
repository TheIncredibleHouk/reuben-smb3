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

        private const int colBounds = 0xF0;
        private const int rowBounds = 0x1B;

        private Level level;
        private LevelInfo levelInfo;
        private LevelController levels;
        private GraphicsController graphics;
        private StringController strings;
        private SpriteController sprites;

        private bool initializing = false;
        public void LoadLevel(LevelInfo info, LevelController levelController, GraphicsController graphicsController, StringController stringController, SpriteController spriteController)
        {
            initializing = true;
            levels = levelController;
            graphics = graphicsController;

            levelInfo = info;
            levelViewer.Level = level = levels.LoadLevel(info.File);
            levelViewer.LevelType = levels.LevelData.Types[level.LevelType];

            spriteSelector.ColorReference = blockSelector.ColorReference = paletteList.ColorReference = levelViewer.ColorReference = graphics.GraphicsData.Colors;

            sprites = spriteSelector.Sprites = spriteController;

            levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            blockSelector.PatternTable = levelViewer.PatternTable = graphics.MakePatternTable(new List<int>() { level.GraphicsID, level.GraphicsID + 1, 0xD0, 0xD1 });
            levelViewer.Sprites = sprites;
            spriteSelector.Graphics = levelViewer.Graphics = graphics;

            levelViewer.UpdateSprites(level.Sprites);
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);

            blockSelector.BlockList = levelViewer.LevelType.Blocks;


            paletteList.Palettes = graphics.GraphicsData.Palettes;
            paletteList.SelectedPalette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            strings = stringController;

            musicList.DataSource = strings.GetStringList("music");
            musicList.SelectedIndex = level.MusicID;

            screenList.DataSource = strings.GetStringList("screens");
            screenList.SelectedIndex = level.NumberOfScreens - 1;

            UpdateScreenSize();

            blockSelector.Editor = spriteSelector.Editor = this;

            initializing = false;
        }

        public void UpdatePalette()
        {
            spriteSelector.Palette = blockSelector.Palette = levelViewer.Palette = graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSprites(level.Sprites);
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
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {
                    Point start = new Point(col, row);
                    Stack<Point> stack = new Stack<Point>();
                    stack.Push(start);
                    int checkValue = level.Data[col, row];
                    if (checkValue == blockSelector.SelectedBlock)
                    {
                        return;
                    }

                    int lowestX, highestX;
                    int lowestY, highestY;
                    lowestX = lowestY = 1000;
                    highestX = highestY = -1;

                    List<Point> changes = new List<Point>();
                    while (stack.Count > 0)
                    {
                        Point p = stack.Pop();

                        col = p.X;
                        row = p.Y;

                        if (row < 0 || row > 0x1A || col < 0 || col > 0xEF)
                        {
                            continue;
                        }

                        Point currentPoint = new Point(col, row);
                        Point hasPoint = changes.Where(o => o.X == col && o.Y == row).FirstOrDefault();
                        if (checkValue == level.Data[col, row] && (hasPoint.X == 0 && hasPoint.Y == 0))
                        {
                            changes.Add(currentPoint);
                            if (col < lowestX)
                            {
                                lowestX = col;
                            }
                            if (col > highestX)
                            {
                                highestX = col;
                            }
                            if (row < lowestY)
                            {
                                lowestY = row;
                            }
                            if (row > highestY)
                            {
                                highestY = row;
                            }

                            stack.Push(new Point(col + 1, row));
                            stack.Push(new Point(col - 1, row));
                            stack.Push(new Point(col, row + 1));
                            stack.Push(new Point(col, row - 1));
                        }
                    }
                    int width = highestX - lowestX + 1;
                    int height = highestY - lowestY + 1;
                    DataChange change = new DataChange();
                    change.Column = lowestX;
                    change.Row = lowestY;
                    change.Data = new byte[width, height];

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            change.Data[i, j] = level.Data[i + lowestX, j + lowestY];
                        }
                    }
                    foreach (Point p in changes)
                    {
                        level.Data[p.X, p.Y] = (byte)blockSelector.SelectedBlock;
                    } 

                    undoBuffer.Add(change);
                    levelViewer.UpdateBlockDisplay(lowestX, lowestY, width, height);
                }
                else
                {
                    mouseStartX = e.X;
                    mouseStartY = e.Y;
                    levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, 15, 15);
                }
            }
            else if (EditMode == UI.EditMode.Sprites)
            {
                List<Tuple<Sprite, Rectangle>> boundCache = sprites.GetBounds(level.Sprites).ToList();
                Sprite selectedSprite = boundCache.Where(t => t.Item2.Contains(e.X, e.Y)).Select(s => s.Item1).FirstOrDefault(); // find the sprite clicked on

                mouseStartX = e.X;
                mouseStartY = e.Y;
                startCol = e.X / 16;
                startRow = e.Y / 16;
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (selectedSprite != null)
                    {
                        if (levelViewer.SelectedSprites.Count > 0 && !levelViewer.SelectedSprites.Contains(selectedSprite))
                        {

                            List<Rectangle> affectedArea = boundCache.Where(s => levelViewer.SelectedSprites.Contains(s.Item1)).Select(s => s.Item2).ToList(); // only the bounds of the sprites that were previously selected                           
                            affectedArea.Add(sprites.GetClipBounds(selectedSprite)); // add the new sprite to the list
                            Rectangle updateArea = affectedArea.Combine();
                            levelViewer.Invalidate(new Rectangle(updateArea.X, updateArea.Y, updateArea.Width + 1, updateArea.Height + 1));
                            levelViewer.SelectedSprites.Clear();
                            levelViewer.SelectedSprites.Add(selectedSprite);
                        }
                        else if (levelViewer.SelectedSprites.Count > 0 && levelViewer.SelectedSprites.Contains(selectedSprite))
                        {
                        }
                        else
                        {
                            Rectangle updateArea = sprites.GetClipBounds(selectedSprite);
                            levelViewer.Invalidate(new Rectangle(updateArea.X, updateArea.Y, updateArea.Width + 1, updateArea.Height + 1));
                            levelViewer.SelectedSprites.Clear();
                            levelViewer.SelectedSprites.Add(selectedSprite);
                        }


                        leftMouseDrag = true;
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

                        var affectedArea = sprites.GetBounds(levelViewer.SelectedSprites).Select(s => s.Item2).ToList();
                        if (affectedArea.Count > 0)
                        {
                            levelViewer.Invalidate(affectedArea.Combine());
                            levelViewer.SelectedSprites.Clear();
                            levelViewer.SelectionRectangle = Rectangle.Empty;
                        }

                        leftMouseDrag = true;
                    }
                }
                else
                {
                    if (leftMouseDrag)
                    {
                        List<Tuple<Sprite, Rectangle>> affectedSprites = boundCache.Where(t => t.Item2.IntersectsWith(levelViewer.SelectionRectangle) || t.Item2.Contains(e.X, e.Y)).ToList();
                        foreach (var s in affectedSprites)
                        {
                            level.Sprites.Remove(s.Item1);
                        }

                        levelViewer.SelectedSprites.Clear();
                        levelViewer.UpdateSprites(new List<Sprite>(), affectedSprites.Select(s => s.Item2));
                    }

                    else if (spriteSelector.SelectedSprite != null)
                    {
                        Sprite clickedSprite = sprites.GetBounds(level.Sprites).ToList().Where(t => t.Item2.Contains(e.X, e.Y)).Select(s => s.Item1).FirstOrDefault(); // find the sprite clicked on
                        if (clickedSprite != null)
                        {
                            if (levelViewer.SelectedSprites.Contains(clickedSprite))
                            {
                                var affectedArea = sprites.GetBounds(levelViewer.SelectedSprites).Select(s => s.Item2).ToList();
                                foreach (Sprite s in levelViewer.SelectedSprites)
                                {
                                    s.ObjectID = spriteSelector.SelectedSprite.ObjectID;
                                }

                                levelViewer.UpdateSprites(levelViewer.SelectedSprites, affectedArea);
                            }
                            else
                            {
                                var affectedArea = sprites.GetClipBounds(selectedSprite);
                                levelViewer.SelectedSprites.Clear();
                                levelViewer.SelectedSprites.Add(selectedSprite);
                                foreach (Sprite s in levelViewer.SelectedSprites)
                                {
                                    s.ObjectID = spriteSelector.SelectedSprite.ObjectID;
                                }

                                levelViewer.UpdateSprites(levelViewer.SelectedSprites, new List<Rectangle>() { affectedArea });
                            }
                        }
                        else
                        {
                            var affectedArea = sprites.GetBounds(levelViewer.SelectedSprites).Select(s => s.Item2).ToList();
                            selectedSprite = spriteSelector.SelectedSprite.MakeCopy();
                            selectedSprite.X = startCol;
                            selectedSprite.Y = startRow;
                            level.Sprites.Add(selectedSprite);
                            levelViewer.SelectedSprites.Clear();
                            levelViewer.SelectedSprites.Add(selectedSprite);
                            levelViewer.UpdateSprites(levelViewer.SelectedSprites, affectedArea);
                        }
                        leftMouseDrag = true;
                    }
                }
            }
        }

        private bool leftMouseDrag = false;
        private bool leftMouseDragged = false;
        private bool rightMouseDrag = false;
        private bool rightMouseDragged = false;

        private int startCol, startRow;
        private void levelViewer_MouseMove(object sender, MouseEventArgs e)
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
                Block block = levels.LevelData.Types[level.LevelType].Blocks[level.Data[e.X / 16, e.Y / 16]];
                switch (block.BlockSolidity)
                {
                    case 0x00:
                    case 0x10:
                    case 0x20:
                    case 0x30:
                        solidity.Text = strings.GetStringList("solidity")[block.BlockSolidity >> 4];
                        interaction.Text = strings.GetStringList("interaction")[block.BlockInteraction];
                        break;

                    case 0x40:
                        solidity.Text = strings.GetStringList("solidity")[4];
                        interaction.Text = strings.GetStringList("solid interaction")[block.BlockInteraction];
                        break;

                    case 0x80:
                        solidity.Text = strings.GetStringList("solidity")[5];
                        interaction.Text = strings.GetStringList("interaction")[block.BlockInteraction];
                        break;

                    case 0xC0:
                        solidity.Text = strings.GetStringList("solidity")[6];
                        interaction.Text = strings.GetStringList("solid interaction")[block.BlockInteraction];
                        break;

                    case 0xF0:
                        solidity.Text = strings.GetStringList("solidity")[7];
                        interaction.Text = strings.GetStringList("item box")[block.BlockInteraction];
                        break;
                }

                if (leftMouseDrag || rightMouseDrag)
                {
                    leftMouseDragged = leftMouseDrag;
                    rightMouseDragged = rightMouseDrag;
                    levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, width * 16 - 1, height * 16 - 1);
                }
            }
            else if (EditMode == UI.EditMode.Sprites)
            {
                if (leftMouseDrag && levelViewer.SelectedSprites.Count == 0)
                {
                    leftMouseDragged = leftMouseDrag;
                    levelViewer.SelectionRectangle = new Rectangle(col * 16, row * 16, width * 16 - 1, height * 16 - 1);
                }
                else if (leftMouseDrag && levelViewer.SelectedSprites.Count > 0)
                {
                    int index = 0;
                    int colChange = (e.X / 16) - startCol;
                    int rowChange = (e.Y / 16) - startRow;
                    if (colChange != 0 || rowChange != 0)
                    {
                        IEnumerable<Rectangle> existingAreas = sprites.GetClipBounds(levelViewer.SelectedSprites).ToList();
                        foreach (Sprite sprite in levelViewer.SelectedSprites)
                        {
                            levelViewer.SelectedSprites[index].X += colChange;
                            levelViewer.SelectedSprites[index].Y += rowChange;
                            index++;
                        }

                        startCol = e.X / 16;
                        startRow = e.Y / 16;

                        levelViewer.UpdateSprites(levelViewer.SelectedSprites, existingAreas);
                    }
                }
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
                if (e.Button == System.Windows.Forms.MouseButtons.Left && leftMouseDrag)
                {
                    DataChange change = new DataChange();
                    change.Column = col;
                    change.Row = row;
                    change.Data = new byte[width, height];
                    if (width == height && width == 1)
                    {
                        if (level.Data[col, row] == (byte)blockSelector.SelectedBlock)
                        {
                            return;
                        }
                    }

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
                if (leftMouseDragged && levelViewer.SelectionRectangle != Rectangle.Empty)
                {
                    List<Tuple<Sprite, Rectangle>> boundCache = sprites.GetBounds(level.Sprites).ToList();
                    levelViewer.SelectedSprites.Clear();
                    List<Tuple<Sprite, Rectangle>> affectedSprites = boundCache.Where(t => t.Item2.IntersectsWith(levelViewer.SelectionRectangle)).ToList();
                    levelViewer.SelectedSprites.AddRange(affectedSprites.Select(s => s.Item1));
                    levelViewer.SelectionRectangle = Rectangle.Empty;
                }
                rightMouseDrag = leftMouseDrag = leftMouseDragged = rightMouseDragged = false;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void LevelEditor_Move(object sender, EventArgs e)
        {

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
