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
            panel1.MouseWheel += panel1_MouseWheel;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            mouseCap.Location = new Point(4, 4);
        }

        private const int colBounds = 0xF0;
        private const int rowBounds = 0x1B;

        private Level level;
        private LevelInfo levelInfo;

        public void LoadLevel(LevelInfo info)
        {

            levelInfo = info;
            level = Controllers.Levels.LoadLevel(info.File);

            int animationTable = 0;
            switch (level.AnimationType)
            {
                case 0:
                    animationTable = 0x80;
                    break;

                case 1:
                    animationTable = 0xD0;
                    break;

                case 2:
                    animationTable = 0xF0;
                    break;

                case 3:
                    animationTable = 0x58;
                    break;
            }

            var patternTable = Controllers.Graphics.MakePatternTable(level.GraphicsID, level.GraphicsID + 1, animationTable, animationTable + 1);
            var levelType = Controllers.Levels.LevelData.Types[level.LevelType];
            var palette = Controllers.Graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();
            var overlayPatternTable = Controllers.Graphics.MakeExtraPatternTable(4, 5, 6, 7);

            levelViewer.Initialize(level, levelType, palette, Controllers.Levels.LevelData.OverlayPalette, Controllers.Graphics.GraphicsData.Colors, Controllers.Levels.LevelData.Overlays, patternTable, overlayPatternTable);
            blockSelector.Initialize(patternTable, overlayPatternTable, levelType.Blocks, Controllers.Levels.LevelData.Overlays, palette, Controllers.Levels.LevelData.OverlayPalette, Controllers.Graphics.GraphicsData.Colors);

            spriteSelector.Initialize(Controllers.Graphics.GraphicsData.Colors, palette);
            paletteList.ColorReference = Controllers.Graphics.GraphicsData.Colors;


            levelViewer.UpdateSprites(level.Sprites);
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);

            paletteList.Palettes = Controllers.Graphics.GraphicsData.Palettes;
            paletteList.SelectedPalette = Controllers.Graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            musicList.DataSource = Controllers.Strings.GetStringList("music");
            musicList.SelectedIndex = level.MusicID;

            screenList.DataSource = Controllers.Strings.GetStringList("screens");
            screenList.SelectedIndex = level.NumberOfScreens - 1;

            scrollList.DataSource = Controllers.Strings.GetStringList("scrolling");
            scrollList.SelectedIndex = level.ScrollType;

            levelTypeList.DataSource = Controllers.Levels.LevelData.Types.Skip(1).Select(l => l.Name).ToList();
            levelTypeList.SelectedIndex = level.TypeID - 1;


            effectList.DataSource = Controllers.Strings.GetStringList("palette types");
            effectList.SelectedIndex = level.PaletteEffectType;

            for (int i = 0; i < 256; i++)
            {
                graphicsList.Items.Add(i.ToString("X2"));
            }

            graphicsList.SelectedIndex = level.GraphicsID;

            UpdateScreenSize();

            blockSelector.Editor = spriteSelector.Editor = this;
            animationList.Items.AddRange(Controllers.Strings.GetStringList("animation").ToArray());
            animationList.SelectedIndex = level.AnimationType;

            animationList.SelectedIndexChanged += UpdateGraphics;
            graphicsList.SelectedIndexChanged += UpdateGraphics;
            paletteList.SelectedIndexChanged += paletteList_SelectedIndexChanged;
            screenList.SelectedIndexChanged += screenList_SelectedIndexChanged;
            levelTypeList.SelectedIndexChanged += levelTypeList_SelectedIndexChanged;
            graphicsList.SelectedIndexChanged += graphicsList_SelectedIndexChanged;
            effectList.SelectedIndexChanged += effectList_SelectedIndexChanged;

            spriteProperty.Visible = false;
        }

        private void effectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.PaletteEffectType = effectList.SelectedIndex;
        }

        public void UpdatePalette()
        {
            var palette = Controllers.Graphics.GraphicsData.Palettes.Where(p => p.ID == level.PaletteID).FirstOrDefault();

            levelViewer.Update(levelPalette: palette);
            levelViewer.UpdateBlockDisplay(0, 0, 16 * 15, 0x1B);
            levelViewer.UpdateSprites(level.Sprites);
            blockSelector.Update(palette: palette);
            spriteSelector.Update(palette: palette);
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
                List<Tuple<Sprite, Rectangle>> boundCache = Controllers.Sprites.GetBounds(level.Sprites).ToList();
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
                            affectedArea.Add(Controllers.Sprites.GetClipBounds(selectedSprite)); // add the new sprite to the list
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
                            Rectangle updateArea = Controllers.Sprites.GetClipBounds(selectedSprite);
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

                        var affectedArea = Controllers.Sprites.GetBounds(levelViewer.SelectedSprites, spriteOverlay.Checked).Select(s => s.Item2).ToList();
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
                        Sprite clickedSprite = Controllers.Sprites.GetBounds(level.Sprites, spriteOverlay.Checked).ToList().Where(t => t.Item2.Contains(e.X, e.Y)).Select(s => s.Item1).FirstOrDefault(); // find the sprite clicked on
                        if (clickedSprite != null)
                        {
                            if (levelViewer.SelectedSprites.Contains(clickedSprite))
                            {
                                var affectedArea = Controllers.Sprites.GetBounds(levelViewer.SelectedSprites, spriteOverlay.Checked).Select(s => s.Item2).ToList();
                                foreach (Sprite s in levelViewer.SelectedSprites)
                                {
                                    s.ObjectID = spriteSelector.SelectedSprite.ObjectID;
                                }

                                levelViewer.UpdateSprites(levelViewer.SelectedSprites, affectedArea);
                            }
                            else
                            {
                                var affectedArea = Controllers.Sprites.GetClipBounds(selectedSprite);
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
                            var affectedArea = Controllers.Sprites.GetBounds(levelViewer.SelectedSprites, spriteOverlay.Checked).Select(s => s.Item2).ToList();
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

                if (levelViewer.SelectedSprites.Count == 0)
                {
                    spriteProperty.Visible = false;
                }

                else if (levelViewer.SelectedSprites.Count == 1)
                {
                    var descriptions = Controllers.Sprites.GetDefinition(hoveredSprite.ObjectID).PropertyDescriptions;
                    if (descriptions.Count == 0)
                    {
                        spriteProperty.Visible = false;
                    }
                    else
                    {
                        spriteProperty.Visible = true;
                        spriteProperty.DataSource = descriptions;
                        spriteProperty.SelectedIndex = levelViewer.SelectedSprites[0].Property;
                        var rect = Controllers.Sprites.GetBounds(levelViewer.SelectedSprites).FirstOrDefault();
                        spriteProperty.Location = new Point( rect.Item2.X + rect.Item2.Width, rect.Item2.Y);
                    }
                }
            }
        }

        private bool leftMouseDrag = false;
        private bool leftMouseDragged = false;
        private bool rightMouseDrag = false;
        private bool rightMouseDragged = false;

        private int startCol, startRow;
        private Sprite hoveredSprite;
        private void levelViewer_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCap.Focus();
            int minX = Math.Min(e.X, mouseStartX);
            int minY = Math.Min(e.Y, mouseStartY);
            int maxX = Math.Max(e.X, mouseStartX);
            int maxY = Math.Max(e.Y, mouseStartY);

            var col = e.X / 16;
            var row = e.Y / 17;
            var minCol = Math.Max(minX, 0) / 16;
            var minRow = Math.Max(minY, 0) / 16;
            var width = Math.Min((maxX / 16) - minCol, 0xEF) + 1;
            var height = Math.Min((maxY / 16) - minRow, 0x1A) + 1;

            if (EditMode == UI.EditMode.Blocks)
            {
                Block block = Controllers.Levels.LevelData.Types[level.LevelType].Blocks[level.Data[e.X / 16, e.Y / 16]];
                string solidity = "", interaction = "";
                switch (block.BlockSolidity)
                {
                    case 0x00:
                    case 0x10:
                    case 0x20:
                    case 0x30:
                        solidity = Controllers.Strings.GetStringList("solidity")[block.BlockSolidity >> 4];
                        interaction = Controllers.Strings.GetStringList("interaction")[block.BlockInteraction];
                        break;

                    case 0x40:
                        solidity = Controllers.Strings.GetStringList("solidity")[4];
                        interaction = Controllers.Strings.GetStringList("solid interaction")[block.BlockInteraction];
                        break;

                    case 0x80:
                        solidity = Controllers.Strings.GetStringList("solidity")[5];
                        interaction = Controllers.Strings.GetStringList("interaction")[block.BlockInteraction];
                        break;

                    case 0xC0:
                        solidity = Controllers.Strings.GetStringList("solidity")[6];
                        interaction = Controllers.Strings.GetStringList("solid interaction")[block.BlockInteraction];
                        break;

                    case 0xF0:
                        solidity = Controllers.Strings.GetStringList("solidity")[7];
                        interaction = Controllers.Strings.GetStringList("item box")[block.BlockInteraction];
                        break;
                }


                if (leftMouseDrag || rightMouseDrag)
                {
                    SetStatus(string.Format("({0},{1} - {2}x{3}) [{4} / {5}]", col, row, width, height, solidity, interaction));
                    leftMouseDragged = leftMouseDrag;
                    rightMouseDragged = rightMouseDrag;
                    levelViewer.SelectionRectangle = new Rectangle(minCol * 16, minRow * 16, width * 16 - 1, height * 16 - 1);
                }
                else
                {
                    SetStatus(string.Format("({0},{1}) [{2} / {3}]", col, row, solidity, interaction));
                }
            }
            else if (EditMode == UI.EditMode.Sprites)
            {
                List<Tuple<Sprite, Rectangle>> boundCache = Controllers.Sprites.GetBounds(level.Sprites).ToList();
                Sprite newHoveredSprite = boundCache.Where(t => t.Item2.Contains(e.X, e.Y)).Select(s => s.Item1).FirstOrDefault(); // find the sprite clicked on
                if (hoveredSprite != newHoveredSprite)
                {
                    hoveredSprite = newHoveredSprite;
                    if (hoveredSprite != null)
                    {
                        SpriteDefinition def = Controllers.Sprites.GetDefinition(hoveredSprite.ObjectID);
                        string displayTip = string.Format("({0:X2},{1:X2}) {2}", hoveredSprite.X, hoveredSprite.Y, def.Name);
                        if (def.PropertyDescriptions.Count > 0)
                        {
                            displayTip += " - " + def.PropertyDescriptions[hoveredSprite.Property];
                        }
                        toolTip.SetToolTip(levelViewer, displayTip);
                    }
                }

                if (hoveredSprite == null)
                {
                    toolTip.Hide(levelViewer);
                }
                if (leftMouseDrag && levelViewer.SelectedSprites.Count == 0)
                {
                    leftMouseDragged = leftMouseDrag;
                    levelViewer.SelectionRectangle = new Rectangle(minCol * 16, minRow * 16, width * 16 - 1, height * 16 - 1);
                }
                else if (leftMouseDrag && levelViewer.SelectedSprites.Count > 0)
                {
                    int index = 0;
                    int colChange = (e.X / 16) - startCol;
                    int rowChange = (e.Y / 16) - startRow;
                    if (colChange != 0 || rowChange != 0)
                    {
                        IEnumerable<Rectangle> existingAreas = Controllers.Sprites.GetClipBounds(levelViewer.SelectedSprites, spriteOverlay.Checked).ToList();
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
                    List<Tuple<Sprite, Rectangle>> boundCache = Controllers.Sprites.GetBounds(level.Sprites, spriteOverlay.Checked).ToList();
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

                    case Keys.S:
                        SaveLevel();
                        break;
                }
            }
        }

        public void UpdateScreenSize()
        {
            lvlHost.Width = level.NumberOfScreens * 16 * 16;
        }

        private void screenList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.NumberOfScreens = screenList.SelectedIndex + 1;
            UpdateScreenSize();
        }

        private void editList_SelectedIndexChanged(object sender, EventArgs e)
        {
            levelViewer.EditMode = EditMode;
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            mouseCap.Location = new Point(4, 4);
        }

        private void interactionOverlay_CheckedChanged(object sender, EventArgs e)
        {
            levelViewer.ShowInteractionOverlays = interactionOverlay.Checked;
            levelViewer.UpdateBlockDisplay(0, 0, 0xF0, 0x1B);
        }

        private void solidityOverlay_CheckedChanged(object sender, EventArgs e)
        {

            levelViewer.ShowSolidityOverlays = solidityOverlay.Checked;
            levelViewer.UpdateBlockDisplay(0, 0, 0xF0, 0x1B);
        }

        private void blockSelector_MouseDoubleClick(object sender, EventArgs e)
        {
            ProjectView.ShowBlockEditor(level.LevelType, blockSelector.SelectedBlock);
        }

        private void UpdateGraphics(object sender, EventArgs e)
        {
            level.AnimationType = animationList.SelectedIndex;
            //level.GraphicsID = graphicsList.SelectedIndex;
            int patternTable = 0;
            switch (level.AnimationType)
            {
                case 0:
                    patternTable = 0x80;
                    break;

                case 1:
                    patternTable = 0xD0;
                    break;

                case 2:
                    patternTable = 0xF0;
                    break;

                case 3:
                    patternTable = 0x58;
                    break;
            }

            var newGraphics = Controllers.Graphics.MakePatternTable(level.GraphicsID, level.GraphicsID + 1, patternTable, patternTable + 1);
            levelViewer.Update(patternTable: newGraphics);
            blockSelector.Update(patternTable: newGraphics);
        }

        private void spriteOverlay_CheckedChanged(object sender, EventArgs e)
        {
            levelViewer.ShowSpriteOverlays = spriteOverlay.Checked;
            levelViewer.UpdateSprites(level.Sprites, Controllers.Sprites.GetClipBounds(levelViewer.SelectedSprites, !spriteOverlay.Checked));
        }


        private bool lockedStatus = false;
        private string backupStatus = "";

        private void LockStatus(string text)
        {
            backupStatus = status.Text;
            status.Text = text;
            lockedStatus = true;

            System.Threading.TimerCallback callback = UnlockStatus;
            System.Threading.Timer t = new System.Threading.Timer(callback, null, 3000, System.Threading.Timeout.Infinite);
        }

        private void UnlockStatus(object o)
        {
            lockedStatus = false;
            status.Text = backupStatus;
        }

        private void SetStatus(string text)
        {
            if (lockedStatus)
            {
                backupStatus = text;
            }
            else
            {
                status.Text = text;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveLevel();
            using (Bitmap image = levelViewer.GetThumbnail())
            {
                image.Save(Controllers.Project.ProjectData.ProjectDirectory + @"\cache\" + levelInfo.Name + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void SaveLevel()
        {
            Controllers.Levels.SaveLevel(level);
            LockStatus("Level saved.");
        }

        private void scrollList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.ScrollType = scrollList.SelectedIndex;
        }

        private void levelTypeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.LevelType = levelTypeList.SelectedIndex + 1;
            LevelType type = Controllers.Levels.LevelData.Types[level.LevelType];
            levelViewer.Update(levelType: type);
            blockSelector.Update(blockList: type.Blocks);
        }

        void graphicsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            level.GraphicsID = graphicsList.SelectedIndex;
            int animationTable = 0;
            switch (level.AnimationType)
            {
                case 0:
                    animationTable = 0x80;
                    break;

                case 1:
                    animationTable = 0xD0;
                    break;

                case 2:
                    animationTable = 0xF0;
                    break;

                case 3:
                    animationTable = 0x58;
                    break;
            }

            var patternTable = Controllers.Graphics.MakePatternTable(level.GraphicsID, level.GraphicsID + 1, animationTable, animationTable + 1);
            levelViewer.Update(patternTable: patternTable);
            blockSelector.Update(patternTable: patternTable);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Controllers.ROM.BuildRomFromAssets(Controllers.Project, Controllers.Levels, Controllers.Worlds, Controllers.Strings, Controllers.Graphics);
        }
    }
}
