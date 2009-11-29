using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Daiz.Library;
using Daiz.NES.Reuben.ProjectManagement;
using Dotnetrix.Controls;

namespace Daiz.NES.Reuben
{
    public partial class WorldEditor : Form
    {
        PatternTable CurrentTable;
        PaletteInfo CurrentPalette;
        int LeftMouseTile = 0;
        int RightMouseTile = 0;
        private MouseMode MouseMode = MouseMode.RightClickSelection;
        public World CurrentWorld { get; private set; }

        public WorldEditor()
        {
            InitializeComponent();

            UndoBuffer = new List<IUndoableAction>();
            RedoBuffer = new List<IUndoableAction>();
            CmbLayouts.DisplayMember  = CmbPalettes.DisplayMember = CmbGraphics.DisplayMember = "Name";
            foreach (var g in ProjectController.GraphicsManager.GraphicsInfo)
            {
                CmbGraphics.Items.Add(g);
            }

            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                CmbPalettes.Items.Add(p);
            }

            for (int i = 1; i < 5; i++)
            {
                CmbLength.Items.Add(i);
            }

            foreach (var l in ProjectController.LayoutManager.BlockLayouts)
            {
                CmbLayouts.Items.Add(l);
            }

            CmbLayouts.SelectedIndex = 0;
            CurrentTable = new PatternTable();
            WldView.DelayDrawing = true;
            WldView.CurrentTable = CurrentTable;

            BlsSelector.CurrentTable = CurrentTable;
            BlsSelector.BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];
            BlsSelector.SpecialTable = WldView.SpecialTable = ProjectController.SpecialManager.SpecialTable;
            BlsSelector.SpecialPalette = WldView.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;
            BlsSelector.SelectionChanged += new EventHandler<TEventArgs<MouseButtons>>(BlsSelector_SelectionChanged);

            BlvRight.CurrentTable = BlvLeft.CurrentTable = CurrentTable;

            ProjectController.PaletteManager.PaletteAdded += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteAdded);
            ProjectController.PaletteManager.PaletteRemoved += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteRemoved);
            ProjectController.PaletteManager.PalettesSaved += new EventHandler(PaletteManager_PalettesSaved);
            ProjectController.BlockManager.DefinitionsSaved += new EventHandler(BlockManager_DefinitionsSaved);
            ProjectController.LayoutManager.LayoutAdded += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutAdded);
            ProjectController.GraphicsManager.GraphicsUpdated += new EventHandler(GraphicsManager_GraphicsUpdated);
            ProjectController.LayoutManager.LayoutRemoved += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutRemoved);
            WldView.Zoom = 1;
            PnlVerticalGuide.Guide1Changed += new EventHandler(PnlVerticalGuide_Guide1Changed);
            PnlVerticalGuide.Guide2Changed += new EventHandler(PnlVerticalGuide_Guide2Changed);
            PnlHorizontalGuide.Guide1Changed += new EventHandler(PnlHorizontalGuide_Guide1Changed);
            PnlHorizontalGuide.Guide2Changed += new EventHandler(PnlHorizontalGuide_Guide2Changed);
            ReubenController.GraphicsReloaded += new EventHandler(ReubenController_GraphicsReloaded);
            ReubenController.WorldReloaded += new EventHandler<TEventArgs<World>>(ReubenController_WorldReloaded);
            LoadSpriteSelector();
            WldView.HorizontalGuide1 = PnlHorizontalGuide.Guide1;
            WldView.HorizontalGuide2 = PnlHorizontalGuide.Guide2;
            WldView.VerticalGuide1 = PnlVerticalGuide.Guide1;
            WldView.VerticalGuide2 = PnlVerticalGuide.Guide2;
            BlsSelector.CurrentDefiniton = WldView.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(0);
        }

        void BlsSelector_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            if (MouseMode == MouseMode.RightClickSelection)
            {
                LblSelected.Text = "Drawing With: " + LeftMouseTile.ToHexString();
                LeftMouseTile = BlsSelector.SelectedTileIndex;
                BlvLeft.PaletteIndex = (LeftMouseTile & 0xC0) >> 6;
                BlvLeft.CurrentBlock = BlsSelector.SelectedBlock;
            }
            else
            {
                if (e != null)
                {
                    if (e.Data == MouseButtons.Left)
                    {
                        LeftMouseTile = BlsSelector.SelectedTileIndex;
                        BlvLeft.PaletteIndex = (LeftMouseTile & 0xC0) >> 6;
                        BlvLeft.CurrentBlock = BlsSelector.SelectedBlock;
                    }
                    else
                    {
                        RightMouseTile = BlsSelector.SelectedTileIndex;
                        BlvRight.PaletteIndex = (RightMouseTile & 0xC0) >> 6;
                        BlvRight.CurrentBlock = BlsSelector.SelectedBlock;
                    }
                }

                LblSelected.Text = "Left: " + LeftMouseTile + " - Right: " + RightMouseTile;
            }
        }

        void ReubenController_WorldReloaded(object sender, TEventArgs<World> e)
        {
            if (CurrentWorld == e.Data)
            {
                GetWorldInfo(e.Data);
            }
        }

        void ReubenController_GraphicsReloaded(object sender, EventArgs e)
        {
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank + 1]);
            UpdateGraphics();
        }

        void LayoutManager_LayoutRemoved(object sender, TEventArgs<BlockLayout> e)
        {
            CmbLayouts.Items.Add(e.Data);
        }

        void LayoutManager_LayoutAdded(object sender, TEventArgs<BlockLayout> e)
        {
            if (CmbLayouts.SelectedItem == e.Data)
            {
                CmbLayouts.SelectedIndex--;
            }

            CmbLayouts.Items.Remove(e.Data);
        }

        void PaletteManager_PalettesSaved(object sender, EventArgs e)
        {
            UpdateGraphics();
        }

        public void EditWorld(World w)
        {
            GetWorldInfo(w);

            if (!ProjectController.SettingsManager.HasLevelSettings(w.Guid))
            {
                ProjectController.SettingsManager.AddLevelSettings(w.Guid);
            }

            TsbGrid.Checked = ProjectController.SettingsManager.GetLevelSetting<bool>(w.Guid, "ShowGrid");
            TsbZoom.Checked = ProjectController.SettingsManager.GetLevelSetting<bool>(w.Guid, "Zoom");

            switch (ProjectController.SettingsManager.GetLevelSetting<string>(w.Guid, "Draw"))
            {
                case "Pencil":
                    TsbPencil.Checked = true;
                    DrawMode = DrawMode.Pencil;
                    break;

                case "Rectangle":
                    TsbRectangle.Checked = true;
                    DrawMode = DrawMode.Rectangle;
                    break;

                case "Outline":
                    TsbOutline.Checked = true;
                    DrawMode = DrawMode.Outline;
                    break;

                case "Line":
                    TsbLine.Checked = true;
                    DrawMode = DrawMode.Line;
                    break;

                case "Fill":
                    TsbBucket.Checked = true;
                    DrawMode = DrawMode.Fill;
                    break;
            }

            CmbLayouts.SelectedIndex = ProjectController.SettingsManager.GetLevelSetting<int>(w.Guid, "Layout");
            PnlHorizontalGuide.GuideColor = ProjectController.SettingsManager.GetLevelSetting<Color>(w.Guid, "HGuideColor");
            PnlVerticalGuide.GuideColor = ProjectController.SettingsManager.GetLevelSetting<Color>(w.Guid, "VGuideColor");

            this.Text = ProjectController.WorldManager.GetWorldInfo(w.Guid).Name;
            this.WindowState = FormWindowState.Maximized;
            this.Show();
            SetMiscText(0);
            BtnShowHideInfo_Click(null, null);
        }

        private void GetWorldInfo(World w)
        {
            WldView.CurrentWorld = w;
            CurrentWorld = w;
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank + 1]);
            CmbGraphics.SelectedIndex = w.GraphicsBank;
            CmbPalettes.SelectedIndex = w.Palette;
            CmbMusic.SelectedIndex = w.Music;
            CmbLength.SelectedItem = w.Length;
            PntEditor.CurrentPointer = null;
            BtnDeletePointer.Enabled = false;
            LblStartPoint.Text = "X:" + w.XStart.ToHexString() + " Y: " + (w.YStart - 0x0F).ToHexString();
            WldView.DelayDrawing = false;
            WldView.FullUpdate();
        }

        #region guide events
        private void PnlHorizontalGuide_Guide2Changed(object sender, EventArgs e)
        {
            WldView.UpdateGuide(Orientation.Horizontal, 2);
        }

        private void PnlHorizontalGuide_Guide1Changed(object sender, EventArgs e)
        {
            WldView.UpdateGuide(Orientation.Horizontal, 1);
        }

        private void PnlVerticalGuide_Guide2Changed(object sender, EventArgs e)
        {
            WldView.UpdateGuide(Orientation.Vertical, 2);
        }

        private void PnlVerticalGuide_Guide1Changed(object sender, EventArgs e)
        {
            WldView.UpdateGuide(Orientation.Vertical, 1);
        }

        private void freeGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.Free;
            freeGuideToolStripMenuItem.Checked = true;
            snapToEnemyBounceHeightToolStripMenuItem.Checked =
            showScreenHeightToolStripMenuItem.Checked =
            snapToJumpHeightToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked = false;
        }

        private void snapToJumpHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.JumpHeight1;
            snapToJumpHeightToolStripMenuItem.Checked = true;
            snapToEnemyBounceHeightToolStripMenuItem.Checked =
            showScreenHeightToolStripMenuItem.Checked =
            freeGuideToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked = false;
        }

        private void showScreenHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.Screen;
            showScreenHeightToolStripMenuItem.Checked = true;
            snapToEnemyBounceHeightToolStripMenuItem.Checked =
            snapToJumpHeightToolStripMenuItem.Checked =
            freeGuideToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked = false;

        }

        private void snapToRunningJumpHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.JumpHeight2;
            snapToRunningJumpHeightToolStripMenuItem.Checked = true;
            snapToEnemyBounceHeightToolStripMenuItem.Checked =
            showScreenHeightToolStripMenuItem.Checked =
            snapToJumpHeightToolStripMenuItem.Checked =
            freeGuideToolStripMenuItem.Checked =
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked = false;
        }

        private void snapToFullPMeterJumpHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.JumpHeight3;
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked = true;
            snapToEnemyBounceHeightToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            showScreenHeightToolStripMenuItem.Checked =
            snapToJumpHeightToolStripMenuItem.Checked =
            freeGuideToolStripMenuItem.Checked = false;
        }


        private void snapToEnemyBounceHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.GuideSnapMode = GuideMode.JumpHeight4;
            snapToEnemyBounceHeightToolStripMenuItem.Checked = true;
            snapToFullPMeterJumpHeightToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            showScreenHeightToolStripMenuItem.Checked =
            snapToJumpHeightToolStripMenuItem.Checked =
            freeGuideToolStripMenuItem.Checked = false;
        }

        private void hideGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlVerticalGuide.Guide1.Visible = PnlVerticalGuide.Guide2.Visible = false;
            WldView.UpdateGuide(Orientation.Vertical, 1);
            WldView.UpdateGuide(Orientation.Vertical, 2);
        }

        private void snapToJumpLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.JumpLength1;
            snapToScreenLengthToolStripMenuItem.Checked =
            snapToJumpLengthToolStripMenuItem.Checked = true;
            freeGuide2.Checked =
            snapToWalkingJumpLengthToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToFullMeterJumpLengthToolStripMenuItem.Checked = false;
        }

        private void snapToWalkingJumpLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.JumpLength2;
            snapToWalkingJumpLengthToolStripMenuItem.Checked = true;
            snapToScreenLengthToolStripMenuItem.Checked =
            freeGuide2.Checked =
            snapToJumpLengthToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToFullMeterJumpLengthToolStripMenuItem.Checked = false;
        }

        private void snapToRunningJumpLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.JumpLength3;
            snapToRunningJumpLengthToolStripMenuItem.Checked = true;
            snapToScreenLengthToolStripMenuItem.Checked =
            snapToWalkingJumpLengthToolStripMenuItem.Checked =
            freeGuide2.Checked =
            snapToJumpLengthToolStripMenuItem.Checked =
            snapToFullMeterJumpLengthToolStripMenuItem.Checked = false;
        }

        private void snapToFullMeterJumpLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.JumpLength4;
            snapToFullMeterJumpLengthToolStripMenuItem.Checked = true;
            snapToScreenLengthToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToWalkingJumpLengthToolStripMenuItem.Checked =
            freeGuide2.Checked =
            snapToJumpLengthToolStripMenuItem.Checked = false;
        }

        private void snapToScreenLengthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.Screen;
            snapToScreenLengthToolStripMenuItem.Checked = true;
            snapToFullMeterJumpLengthToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToWalkingJumpLengthToolStripMenuItem.Checked =
            freeGuide2.Checked =
            snapToJumpLengthToolStripMenuItem.Checked = false;
        }
        #endregion

        #region palette functions
        void PaletteManager_PaletteAdded(object sender, TEventArgs<PaletteInfo> e)
        {
            CmbPalettes.Items.Add(e.Data);
        }

        void PaletteManager_PaletteRemoved(object sender, TEventArgs<PaletteInfo> e)
        {
            int index = CmbPalettes.SelectedIndex;
            CmbPalettes.Items.Remove(sender);
            if (CmbPalettes.Items.Count - 1 < index)
            {
                CmbPalettes.SelectedIndex = CmbPalettes.Items.Count - 1;
            }
            else
            {
                CmbPalettes.SelectedIndex = index;
            }
        }
        #endregion

        #region graphics functions
        void GraphicsManager_GraphicsUpdated(object sender, EventArgs e)
        {
            UpdateGraphics();
        }

        private void UpdateGraphics()
        {
            CurrentTable = new PatternTable();
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex + 1]);
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.AnimationBank + 1]);
            BlvRight.CurrentPalette = BlvLeft.CurrentPalette = BlsSelector.CurrentPalette = WldView.CurrentPalette = CurrentPalette;
            BlsSelector.CurrentDefiniton = WldView.CurrentDefiniton = WldView.CurrentDefiniton;
            BlvRight.CurrentBlock = BlvRight.CurrentBlock;
            BlvLeft.CurrentBlock = BlvLeft.CurrentBlock;
        }
        #endregion

        #region mouse functions
        private bool useTransparentTile;
        private int StartX, StartY, FromX, FromY, ToX, ToY;
        private bool ContinueDragging;
        private DrawMode PreviousMode;

        private void LvlView_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (e.X / 16) / WldView.Zoom;
            int y = (e.Y / 16) / WldView.Zoom;
            PnlView.Focus();

            if (x < 0 || x >= CurrentWorld.Width || y < 0 || y >= CurrentWorld.Height) return;

            if (_SelectingStartPositionMode)
            {
                int oldX = CurrentWorld.XStart;
                int oldY = CurrentWorld.YStart;
                CurrentWorld.XStart = x;
                CurrentWorld.YStart = y;
                if (TsbStartPoint.Checked)
                {
                    WldView.UpdatePoint(x, y);
                    WldView.UpdatePoint(oldX, oldY);
                }
                _SelectingStartPositionMode = false;
                PnlDrawing.Enabled = TabLevelInfo.Enabled = true;
                SetMiscText(PreviousTextIndex);
                LblStartPoint.Text = "X:" + x.ToHexString() + " Y: " + (y - 0x0F).ToHexString();
            }

            else if (EditMode == EditMode.Tiles)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    BlsSelector.SelectedTileIndex = CurrentWorld.LevelData[x, y];
                    BlsSelector_SelectionChanged(this, new TEventArgs<MouseButtons>(e.Button));
                }
                else
                {
                    WldView.ClearSelection();
                    if (DrawMode == DrawMode.Selection)
                    {
                        DrawMode = PreviousMode;
                    }

                    if (e.Button == MouseButtons.Right && MouseMode == MouseMode.RightClickSelection)
                    {
                        PreviousMode = DrawMode;
                        DrawMode = DrawMode.Selection;
                    }

                    switch (DrawMode)
                    {
                        case DrawMode.Pencil:
                            CurrentMultiTile = new MultiTileAction();
                            CurrentMultiTile.AddTileChange(x, y, CurrentWorld.LevelData[x, y]);
                            CurrentWorld.SetTile(x, y, (byte)(DrawingTile));
                            ContinueDrawing = true;    
                            break;

                        case DrawMode.Outline:
                        case DrawMode.Rectangle:
                        case DrawMode.Selection:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            WldView.SelectionRectangle = new Rectangle(StartX, StartY, 1, 1);
                            break;

                        case DrawMode.Line:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            WldView.SelectionLine = new Line(StartX, StartY, StartX, StartY);
                            break;

                        case DrawMode.Fill:
                            Point start = new Point(x, y);
                            Stack<Point> stack = new Stack<Point>();
                            stack.Push(start);
                            int checkValue = CurrentWorld.LevelData[x, y];
                            if (checkValue == DrawingTile) return;

                            CurrentMultiTile = new MultiTileAction();
                            while (stack.Count > 0)
                            {
                                Point p = stack.Pop();
                                int lowestX, highestX; ;
                                int lowestY, highestY;
                                lowestX = highestX = x;
                                lowestY = highestY = y;
                                int i = p.X;
                                int j = p.Y;

                                if (j < 0 || j >= CurrentWorld.Height || i < 0 || i >= CurrentWorld.Length * 16)
                                {
                                    continue;
                                }

                                WldView.DelayDrawing = true;
                                if (checkValue == CurrentWorld.LevelData[i, j])
                                {
                                    CurrentMultiTile.AddTileChange(i, j, CurrentWorld.LevelData[i, j]);
                                    CurrentWorld.SetTile(i, j, (byte)DrawingTile);
                                    if (i < lowestX) lowestX = i;
                                    if (i > highestX) highestX = i;
                                    if (j < lowestY) lowestY = j;
                                    if (j > highestY) highestY = j;

                                    stack.Push(new Point(i + 1, j));
                                    stack.Push(new Point(i - 1, j));
                                    stack.Push(new Point(i, j + 1));
                                    stack.Push(new Point(i, j - 1));
                                }
                                UndoBuffer.Add(CurrentMultiTile);
                                WldView.DelayDrawing = false;
                                WldView.UpdateArea(new Rectangle(lowestX, lowestY, highestX - lowestX + 1, highestY - lowestY + 1));
                            }

                            break;
                    }
                }
            }
            else if (EditMode == EditMode.Sprites)
            {
                CurrentSprite = SelectSprite(x, y);
                if (CurrentSprite != null && MouseButtons == MouseButtons.Left)
                {
                    WldView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    ContinueDragging = true;
                    LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                }
                else if (CurrentSprite != null && MouseButtons == MouseButtons.Right && CurrentSelectorSprite != null)
                {
                    CurrentSprite.InGameID = CurrentSelectorSprite.InGameID;
                    SpriteDefinition sp = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID);
                    int xDiff = x - CurrentSprite.X;
                    int yDiff = y - CurrentSprite.Y;
                    int rectX = xDiff >= 0 ? (CurrentSprite.X * 16) + sp.MaxLeftX : (x * 16) + sp.MaxLeftX;
                    int rectY = yDiff >= 0 ? (CurrentSprite.Y * 16) + sp.MaxTopY : (y * 16) + sp.MaxTopY;
                    int width = xDiff >= 0 ? ((x * 16) + sp.MaxRightX) - ((CurrentSprite.X * 16) + sp.MaxLeftX) : ((CurrentSprite.X * 16) + sp.MaxRightX) - ((x * 16) + sp.MaxLeftX);
                    int height = yDiff >= 0 ? ((y * 16) + sp.MaxBottomY) - ((CurrentSprite.Y * 16) + sp.MaxTopY) : ((CurrentSprite.Y * 16) + sp.MaxBottomY) - ((y * 16) + sp.MaxTopY);
                    Rectangle r = new Rectangle(rectX, rectY, width, height);
                    CurrentSprite.X = x;
                    CurrentSprite.Y = y;
                    WldView.DelayDrawing = true;
                    WldView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    WldView.DelayDrawing = false;
                    WldView.UpdateSprites(r);
                }
                else if (CurrentSelectorSprite != null && MouseButtons == MouseButtons.Right)
                {
                    Sprite newSprite = new Sprite() {IsMapSprite = true,  X = x, Y = y, InGameID = CurrentSelectorSprite.InGameID };
                    CurrentWorld.AddSprite(newSprite);
                    CurrentSprite = newSprite;
                    WldView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    ContinueDragging = true;
                    LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                }

                else
                {
                    WldView.ClearSelection();
                    ContinueDragging = false;
                    LblSprite.Text = "None";
                }
            }
            else if (EditMode == EditMode.Pointers)
            {
                WorldPointer p = CurrentWorld.Pointers.Find(pt => pt.X == x && pt.Y == y);
                PntEditor.CurrentPointer = p;
                CurrentPointer = p;
                if (p != null)
                {
                    WldView.SelectionRectangle = new Rectangle(p.X, p.Y, 1, 1);
                    ContinueDragging = true;
                    BtnDeletePointer.Enabled = true;
                }
                else
                {
                    BtnDeletePointer.Enabled = false;
                    WldView.ClearSelection();
                }
            }
        }

        private int PreviousMouseX, PreviousMouseY;
        private void LvlView_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (e.X / (16 * WldView.Zoom));
            int y = (e.Y / (16 * WldView.Zoom));

            if (x < 0 || x >= CurrentWorld.Width || y < 0 || y >= CurrentWorld.Height) return;

            if (PreviousMouseX == x && PreviousMouseY == y) return;
            PreviousMouseX = x;
            PreviousMouseY = y;

            int XDiff = x - StartX;
            int YDiff = y - StartY;

            LblCoordinates.Text = "X: " + x.ToHexString() + " Y: " + (y - 0x0F).ToHexString();

            if (EditMode == EditMode.Tiles)
            {
                LevelToolTip.SetToolTip(WldView, ProjectController.BlockManager.GetBlockString(CurrentWorld.Type, CurrentWorld.LevelData[x, y]) + "\n(" + CurrentWorld.LevelData[x, y].ToHexString() + ")");
                if (ContinueDrawing && (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Middle || MouseButtons == MouseButtons.Right))
                {
                    switch (DrawMode)
                    {
                        case DrawMode.Pencil:
                            CurrentMultiTile.AddTileChange(x, y, CurrentWorld.LevelData[x, y]);
                            CurrentWorld.SetTile(x, y, (byte)DrawingTile);
                            break;

                        case DrawMode.Outline:
                        case DrawMode.Rectangle:
                        case DrawMode.Selection:
                            if (StartX == x && StartY == y) return;
                            if (x > StartX)
                            {
                                FromX = StartX;
                                ToX = x;
                            }
                            else
                            {
                                FromX = x;
                                ToX = StartX;
                            }

                            if (y > StartY)
                            {
                                FromY = StartY;
                                ToY = y;
                            }
                            else
                            {
                                FromY = y;
                                ToY = StartY;
                            }


                            WldView.SelectionRectangle = new Rectangle(FromX, FromY, (ToX - FromX) + 1, (ToY - FromY) + 1);
                            break;

                        case DrawMode.Line:
                            if (y > StartY)
                            {
                                if (x > StartX)
                                {
                                    WldView.SelectionLine = new Line(StartX, StartY, x, StartY + (x - StartX));
                                }
                                else
                                {
                                    WldView.SelectionLine = new Line(StartX, StartY, x, StartY - (x - StartX));
                                }
                            }
                            else
                            {
                                if (x > StartX)
                                {
                                    WldView.SelectionLine = new Line(StartX, StartY, x, StartY - (x - StartX));
                                }
                                else
                                {
                                    WldView.SelectionLine = new Line(StartX, StartY, x, StartY + (x - StartX));
                                }
                            }
                            break;

                    }
                }
            }
            else if (EditMode == EditMode.Sprites)
            {
                Sprite s = SelectSprite(x, y);

                if (s != null)
                {
                    LevelToolTip.SetToolTip(WldView, s.Name + "\n(" + s.InGameID.ToHexString() + ")");
                }
                else
                {
                    LevelToolTip.SetToolTip(WldView, null);
                }

                if (ContinueDragging && MouseButtons == MouseButtons.Left && CurrentSprite != null)
                {
                    if (x != CurrentSprite.X || y != CurrentSprite.Y)
                    {
                        SpriteDefinition sp = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID);
                        int xDiff = x - CurrentSprite.X;
                        int yDiff = y - CurrentSprite.Y;
                        int rectX = xDiff >= 0 ? (CurrentSprite.X * 16) + sp.MaxLeftX : (x * 16) + sp.MaxLeftX;
                        int rectY = yDiff >= 0 ? (CurrentSprite.Y * 16) + sp.MaxTopY : (y * 16) + sp.MaxTopY;
                        int width = xDiff >= 0 ? ((x * 16) + sp.MaxRightX) - ((CurrentSprite.X * 16) + sp.MaxLeftX) : ((CurrentSprite.X * 16) + sp.MaxRightX) - ((x * 16) + sp.MaxLeftX);
                        int height = yDiff >= 0 ? ((y * 16) + sp.MaxBottomY) - ((CurrentSprite.Y * 16) + sp.MaxTopY) : ((CurrentSprite.Y * 16) + sp.MaxBottomY) - ((y * 16) + sp.MaxTopY);
                        Rectangle r = new Rectangle(rectX, rectY, width, height);
                        CurrentSprite.X = x;
                        CurrentSprite.Y = y;
                        WldView.DelayDrawing = true;
                        WldView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                        WldView.DelayDrawing = false;
                        WldView.UpdateSprites(r);
                    }
                }
            }
            else if (ContinueDragging && EditMode == EditMode.Pointers && CurrentPointer != null && MouseButtons == MouseButtons.Left)
            {
                if (CurrentPointer != null)
                {
                    if (x == CurrentWorld.Width - 1 || y == CurrentWorld.Height - 1) return;
                    if (CurrentPointer.X == x && CurrentPointer.Y == y) return;
                    int oldX = CurrentPointer.X;
                    int oldY = CurrentPointer.Y;
                    WldView.DelayDrawing = true;
                    CurrentPointer.X = x;
                    CurrentPointer.Y = y;
                    WldView.UpdatePoint(oldX, oldY);
                    WldView.UpdatePoint(oldX, oldY + 1);
                    WldView.UpdatePoint(x, y);
                    WldView.UpdatePoint(x, y + 1);
                    WldView.DelayDrawing = false;
                    WldView.SelectionRectangle = new Rectangle(CurrentPointer.X, CurrentPointer.Y, 1, 1);
                    PntEditor.UpdatePosition();
                }
            }
            else
            {
                ContinueDragging = ContinueDrawing = false;
            }
        }

        private void LvlView_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ContinueDrawing) return;
            int _DrawTile = 0;
            int sX, sY;

            if (e.Button == MouseButtons.Middle)
            {
                _DrawTile = 0;
            }
            else if (DrawMode != DrawMode.Selection)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _DrawTile = LeftMouseTile;
                }
                else
                {
                    _DrawTile = RightMouseTile;
                }
            }
            else
            {
                _DrawTile = LeftMouseTile;
            }
            
            if (EditMode == EditMode.Tiles)
            {
                if (DrawMode == DrawMode.Pencil)
                {
                    UndoBuffer.Add(CurrentMultiTile);
                }
                else if ((WldView.HasSelection || WldView.HasSelectionLine))
                {
                    switch (DrawMode)
                    {
                        case DrawMode.Rectangle:
                            sX = WldView.SelectionRectangle.X;
                            sY = WldView.SelectionRectangle.Y;

                            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentWorld.GetData(sX, sY, WldView.SelectionRectangle.Width, WldView.SelectionRectangle.Height)));

                            WldView.DelayDrawing = true;
                            for (int y = WldView.SelectionRectangle.Y, i = 0; i < WldView.SelectionRectangle.Height; y++, i++)
                            {
                                for (int x = WldView.SelectionRectangle.X, j = 0; j < WldView.SelectionRectangle.Width; x++, j++)
                                {
                                    CurrentWorld.SetTile(x, y, (byte)_DrawTile);
                                }
                            }
                            WldView.DelayDrawing = false;
                            WldView.UpdateArea();
                            break;

                        case DrawMode.Outline:
                            sX = WldView.SelectionRectangle.X;
                            sY = WldView.SelectionRectangle.Y;

                            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentWorld.GetData(sX, sY, WldView.SelectionRectangle.Width, WldView.SelectionRectangle.Height)));

                            WldView.DelayDrawing = true;
                            for (int x = WldView.SelectionRectangle.X, i = 0; i < WldView.SelectionRectangle.Width; i++, x++)
                            {
                                CurrentWorld.SetTile(x, WldView.SelectionRectangle.Y, (byte)_DrawTile);
                                CurrentWorld.SetTile(x, WldView.SelectionRectangle.Y + WldView.SelectionRectangle.Height - 1, (byte)_DrawTile);
                            }

                            for (int y = WldView.SelectionRectangle.Y, i = 1; i < WldView.SelectionRectangle.Height; i++, y++)
                            {
                                CurrentWorld.SetTile(WldView.SelectionRectangle.X, y, (byte)_DrawTile);
                                CurrentWorld.SetTile(WldView.SelectionRectangle.X + WldView.SelectionRectangle.Width - 1, y, (byte)_DrawTile);
                            }
                            WldView.DelayDrawing = false;
                            WldView.UpdateArea();
                            break;

                        case DrawMode.Line:

                            WldView.DelayDrawing = true;
                            CurrentMultiTile = new MultiTileAction();
                            int breakAt = Math.Abs(WldView.SelectionLine.End.X - WldView.SelectionLine.Start.X) + 1;
                            if (WldView.SelectionLine.End.X > WldView.SelectionLine.Start.X)
                            {
                                if (WldView.SelectionLine.End.Y > WldView.SelectionLine.Start.Y)
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (WldView.SelectionLine.Start.X + i >= CurrentWorld.Width || WldView.SelectionLine.Start.Y + i >= CurrentWorld.Height) continue;
                                        sX = WldView.SelectionLine.Start.X + i;
                                        sY = WldView.SelectionLine.Start.Y + i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentWorld.LevelData[sX, sY]);
                                        CurrentWorld.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (WldView.SelectionLine.Start.X + i >= CurrentWorld.Width || WldView.SelectionLine.Start.Y - i >= CurrentWorld.Height) continue;
                                        sX = WldView.SelectionLine.Start.X + i;
                                        sY = WldView.SelectionLine.Start.Y - i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentWorld.LevelData[sX, sY]);
                                        CurrentWorld.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                            }
                            else
                            {
                                if (WldView.SelectionLine.End.Y > WldView.SelectionLine.Start.Y)
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (WldView.SelectionLine.Start.X - i >= CurrentWorld.Width || WldView.SelectionLine.Start.Y + i >= CurrentWorld.Height) continue;
                                        sX = WldView.SelectionLine.Start.X - i;
                                        sY = WldView.SelectionLine.Start.Y + i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentWorld.LevelData[sX, sY]);
                                        CurrentWorld.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (WldView.SelectionLine.Start.X - i >= CurrentWorld.Width || WldView.SelectionLine.Start.Y - i >= CurrentWorld.Height) continue;
                                        sX = WldView.SelectionLine.Start.X - i;
                                        sY = WldView.SelectionLine.Start.Y - i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentWorld.LevelData[sX, sY]);
                                        CurrentWorld.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                            }

                            UndoBuffer.Add(CurrentMultiTile);
                            WldView.DelayDrawing = false;
                            WldView.ClearLine();
                            break;

                        case DrawMode.Selection:
                            useTransparentTile = e.Button == MouseButtons.Right;
                            break;
                    }
                }
            }
        }

        private WorldPointer CurrentPointer;
        private bool ContinueDrawing;
        #endregion

        #region display options

        private void TsbGrid_CheckedChanged(object sender, EventArgs e)
        {
            WldView.ShowGrid = TsbGrid.Checked;
        }

        private void TsbStartPoint_CheckedChanged(object sender, EventArgs e)
        {
            WldView.DisplayStartingPosition = TsbStartPoint.Checked;
        }

        private void TsbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (TsbZoom.Checked)
            {
                WldView.Zoom = 2;
                PnlLengthControl.Size = new Size(PnlLengthControl.Size.Width * 2, PnlLengthControl.Size.Height * 2);
            }
            else
            {
                WldView.Zoom = 1;
                PnlLengthControl.Size = new Size(PnlLengthControl.Size.Width / 2, PnlLengthControl.Size.Height / 2);
            }
        }
        #endregion

        #region drawing modes
        private DrawMode DrawMode = DrawMode.Pencil;

        private void TsbPencil_Click(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Pencil;
            TsbPencil.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbRectangle.Checked = false;
            SetMiscText(0);
        }

        private void TsbRectangle_Click(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Rectangle;
            TsbRectangle.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
            SetMiscText(4);
        }

        private void TsbOutline_Click(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Outline;
            TsbOutline.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
            SetMiscText(5);
        }

        private void TsbBucket_Click(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Fill;
            TsbBucket.Checked = true;
            TsbLine.Checked = TsbOutline.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
            SetMiscText(6);
        }

        private void TsbLine_Click(object sender, EventArgs e)
        {
            DrawMode = DrawMode.Line;
            TsbLine.Checked = true;
            TsbRectangle.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
            SetMiscText(4);
        }

        #endregion

        #region level header changes


        private void CmbLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentWorld.Length = CmbLength.SelectedItem.ToInt();
            PnlLengthControl.Size = new Size(CurrentWorld.Length * 256 * WldView.Zoom, 9 * 16 * WldView.Zoom);
        }

        public void SwitchObjects(PatternTable table, BlockDefinition definition, PaletteInfo palette)
        {
            BlsSelector.HaltRendering = true;
            BlsSelector.CurrentTable = table;
            BlsSelector.CurrentDefiniton = definition;
            BlsSelector.HaltRendering = false;
            BlsSelector.CurrentPalette = palette;
        }

        private void CmbGraphics_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex + 1]);
            LblHexGraphics.Text = "x" + CmbGraphics.SelectedIndex.ToHexString();
        }

        private void CmbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentPalette != null)
            {
                CurrentPalette.PaletteChanged -= CurrentPalette_PaletteChanged;
            }

            CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            CurrentPalette.PaletteChanged += new EventHandler<TEventArgs<DoubleValue<int, int>>>(CurrentPalette_PaletteChanged);
            WldView.CurrentPalette = CurrentPalette;
            BlsSelector.CurrentPalette = CurrentPalette;
            BlvRight.CurrentPalette = BlvLeft.CurrentPalette = CurrentPalette;

            foreach (var sv in SpriteViewers)
            {
                sv.CurrentPalette = CurrentPalette;
            }
        }

        private void CurrentPalette_PaletteChanged(object sender, TEventArgs<DoubleValue<int, int>> e)
        {
            WldView.Redraw();
            BlsSelector.Redraw();
        }

        private void BtnCompress_Click(object sender, EventArgs e)
        {
            //if (ReubenController.SaveTestLevel(CurrentWorld))
            //{
            //    MessageBox.Show("Rom successfully saved!");
            //}
        }

        private bool _SelectingStartPositionMode;
        private void BtnStartPoint_Click(object sender, EventArgs e)
        {
            _SelectingStartPositionMode = true;
            TabLevelInfo.Enabled = false;
            PnlDrawing.Enabled = false;
            SetMiscText(3);
        }

        #endregion

        #region sprites
        private Sprite CurrentSprite = null;

        private Sprite SelectSprite(int x, int y)
        {
            var possibleSprites = (from s in CurrentWorld.SpriteData
                                   where x >= s.X && x <= s.X + (s.Width - 1) &&
                                         y >= s.Y && y <= s.Y + (s.Height - 1)
                                   select s).FirstOrDefault();
            return possibleSprites;
        }

        private List<SpriteViewer> SpriteViewers = new List<SpriteViewer>();

        private void LoadSpriteSelector()
        {
            SpriteViewer spViewer = new SpriteViewer(8);
            spViewer.IsViewingMapSprites = true;
            spViewer.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;

            int x = 0;
            List<Sprite> CurrentList = new List<Sprite>();
            foreach (var ks in ProjectController.SpriteManager.MapSpriteDefinitions.Values)
            {
                Sprite next = new Sprite();
                next.IsMapSprite = true;
                next.X = x;
                next.Y = 0;
                next.InGameID = ks.InGameId;
                CurrentList.Add(next);
                x += next.Width + 1;
            }

            spViewer.SpriteList = CurrentList;
            spViewer.Location = new Point(0, 0);
            SpriteViewers.Add(spViewer);
            spViewer.CurrentPalette = CurrentPalette;
            spViewer.UpdateSprites();
            spViewer.SelectionChanged += new EventHandler<TEventArgs<Sprite>>(spViewer_SelectionChanged);

            TabPage tPage = new TabPage();
            tPage.Text = "Map";
            tPage.AutoScroll = true;
            tPage.Controls.Add(spViewer);

            TabClass1.TabPages.Add(tPage);
        }

        private Sprite CurrentSelectorSprite;

        private void spViewer_SelectionChanged(object sender, TEventArgs<Sprite> e)
        {
            CurrentSelectorSprite = e.Data;
            if (CurrentSelectorSprite != null)
            {
                foreach (var sp in SpriteViewers)
                {
                    if (sp.SelectedSprite != CurrentSelectorSprite)
                    {
                        sp.SelectedSprite = null;
                    }
                }
                LblSpriteSelected.Text = "Sprite: " + CurrentSelectorSprite.InGameID.ToHexString() + " - " + CurrentSelectorSprite.Name;
            }
            else
            {
                LblSpriteSelected.Text = "None";
            }
        }

        #endregion

        #region blocks
        public BlockLayout CurrentLayout { get; private set; }

        void BlockManager_DefinitionsSaved(object sender, EventArgs e)
        {
            UpdateGraphics();
        }
        #endregion

        #region form gui
        private void BtnShowHideInfo_Click(object sender, EventArgs e)
        {
            if (TabLevelInfo.Visible)
            {
                PnlInfo.Height = 30;
                TabLevelInfo.Visible = false;
                BtnShowHideInfo.Image = Properties.Resources.up;
            }
            else
            {
                PnlInfo.Height = 160;
                TabLevelInfo.Visible = true;
                BtnShowHideInfo.Image = Properties.Resources.down;
            }
        }

        private void CmbLayouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentLayout = CmbLayouts.SelectedItem as BlockLayout;
            BlsSelector.BlockLayout = CurrentLayout;
        }

        private void TabEditSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabEditSelector.SelectedIndex)
            {
                case 0:
                    EditMode = EditMode.Tiles;
                    switch (DrawMode)
                    {
                        case DrawMode.Pencil:
                            SetMiscText(0);
                            break;

                        case DrawMode.Rectangle:
                            SetMiscText(4);
                            break;

                        case DrawMode.Outline:
                            SetMiscText(5);
                            break;

                        case DrawMode.Fill:
                            SetMiscText(6);
                            break;
                    }
                    TlsDrawing.Visible = true;
                    break;
                case 1:
                    EditMode = EditMode.Sprites;
                    SetMiscText(1);
                    TlsDrawing.Visible = false;
                    break;

                case 2:
                    EditMode = EditMode.Pointers;
                    TlsDrawing.Visible = false;
                    SetMiscText(2);
                    break;
            }
        }

        private void LvlView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Add:
                        if (!TsbZoom.Checked)
                            TsbZoom.Checked = true;
                        break;

                    case Keys.Subtract:
                        if (TsbZoom.Checked)
                            TsbZoom.Checked = false;
                        break;

                    case Keys.S:
                        Save();
                        break;

                    case Keys.G:
                        TsbGrid.Checked = !TsbGrid.Checked;
                        break;

                    case Keys.X:
                        Cut();
                        break;

                    case Keys.C:
                        Copy();
                        break;

                    case Keys.V:
                        Paste(useTransparentTile);
                        break;


                    case Keys.R:
                        TsbStartPoint.Checked = !TsbStartPoint.Checked;
                        break;

                    case Keys.F:
                        TsbStartPoint.Checked = true;
                        BtnStartPoint_Click(null, null);
                        break;

                    case Keys.D:
                        ToggleRightClickMode();
                        break;

                    case Keys.Z:
                        Undo();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Delete:

                        if (CurrentSprite != null && EditMode == EditMode.Sprites)
                        {
                            CurrentWorld.SpriteData.Remove(CurrentSprite);
                            WldView.DelayDrawing = true;
                            WldView.ClearSelection();
                            WldView.DelayDrawing = false;
                            WldView.UpdateSprites();
                            CurrentSprite = null;
                        }
                        else if (EditMode == EditMode.Tiles && DrawMode == DrawMode.Selection)
                        {
                            DeleteTiles();
                        }
                        else if (EditMode == EditMode.Pointers)
                        {
                            DeleteCurrentPointer();
                        }
                        break;

                    case Keys.Escape:
                        ContinueDrawing = false;
                        WldView.ClearSelection();
                        WldView.ClearLine();
                        if (DrawMode == DrawMode.Selection)
                            DrawMode = PreviousMode;
                        break;
                }
            }
        }

        private void ToggleRightClickMode()
        {
            MouseMode = MouseMode == MouseMode.RightClickSelection ? MouseMode.RightClickTile : MouseMode.RightClickSelection;
            LblRightClickMode.Text = "Right Click Mode: " + (MouseMode == MouseMode.RightClickSelection ? "Selector" : "Tile Placement");
        }

        private byte[,] TileBuffer;

        private void DeleteTiles()
        {
            int sX = WldView.SelectionRectangle.X;
            int sY = WldView.SelectionRectangle.Y;
            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentWorld.GetData(sX, sY, WldView.SelectionRectangle.Width, WldView.SelectionRectangle.Height)));
            WldView.DelayDrawing = true;
            for (int y = sY, i = 0; i < WldView.SelectionRectangle.Height; y++, i++)
            {
                for (int x = sX, j = 0; j < WldView.SelectionRectangle.Width; x++, j++)
                {
                    CurrentWorld.SetTile(x, y, 0);
                }
            }
            WldView.DelayDrawing = false;
            WldView.UpdateArea();
        }

        private void Cut()
        {
            Copy();
            DeleteTiles();
        }

        private void Copy()
        {
            TileBuffer = CurrentWorld.GetData(WldView.SelectionRectangle.X, WldView.SelectionRectangle.Y, WldView.SelectionRectangle.Width, WldView.SelectionRectangle.Height);
        }

        private void Paste(bool transparentTile)
        {
            WldView.DelayDrawing = true;
            Rectangle usedRectangle = WldView.SelectionRectangle;

            if (WldView.SelectionRectangle.Width == 1 && WldView.SelectionRectangle.Height == 1)
            {
                usedRectangle.Width = TileBuffer.GetLength(0);
                usedRectangle.Height = TileBuffer.GetLength(1);
            }

            int sX = usedRectangle.X;
            int sY = usedRectangle.Y;
            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentWorld.GetData(sX, sY, usedRectangle.Width, usedRectangle.Height)));

            for (int j = 0; j < usedRectangle.Height; j++)
            {
                for (int i = 0; i < usedRectangle.Width; i++)
                {
                    if (transparentTile && TileBuffer[i % TileBuffer.GetLength(0), j % TileBuffer.GetLength(1)] == 0) continue;
                    CurrentWorld.SetTile(usedRectangle.X + i, usedRectangle.Y + j, TileBuffer[i % TileBuffer.GetLength(0), j % TileBuffer.GetLength(1)]);
                }
            }
            WldView.DelayDrawing = false;
            WldView.UpdateArea(usedRectangle);
        }

        private void TabCoins_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 25, 65, 270, 65);
        }

        public EditMode EditMode { get; set; }
        private int PreviousTextIndex = 0;
        private int CurrentTextIndex = 0;
        public void SetMiscText(int index)
        {
            switch (index)
            {
                case 0:
                    LblMisc.Text = "Left Mouse Button: Place tile; Right Mouse Button: Erase tile; Shift + Left Mouse: Set tile on level as currently selected tile";
                    break;

                case 1:
                    LblMisc.Text = "Left Mouse Button: Select sprite; Right Mouse Button: Add or replace sprite. Delete will remove the sprite.";
                    break;

                case 2:
                    LblMisc.Text = "Left Mouse Button: Select pointer. To change entry point, click and drag pointer";
                    break;

                case 3:
                    LblMisc.Text = "Click on the level screen to set starting position";
                    break;

                case 4:
                    LblMisc.Text = "Click and drag to create a area to fill. Use the right mouse button to erase instead.";
                    break;

                case 5:
                    LblMisc.Text = "Click and drag to select an area to outline. Use the right mouse button to erase instead.";
                    break;

                case 6:
                    LblMisc.Text = "Click on any area to fill an enclosed area. Use the right mouse button to erase instead.";
                    break;
            }

            PreviousTextIndex = CurrentTextIndex;
            CurrentTextIndex = index;
        }

        private void BtnLevelSize_Click(object sender, EventArgs e)
        {
            LblSpriteSize.Text = "Sprite Data Size: " + (((CurrentWorld.SpriteData.Count) * 3) + 1).ToString() + " bytes";
            LblLevelSize.Text = "Level Data Size: " + (CurrentWorld.GetCompressedData().Length + 12 + (CurrentWorld.Pointers.Count * 9)).ToString() + " bytes";
        }

        public Bitmap GetLevelBitmap()
        {
            Bitmap bitmap = new Bitmap(PnlLengthControl.Bounds.Width, PnlLengthControl.Bounds.Height);
            PnlLengthControl.DrawToBitmap(bitmap, new Rectangle(0, 0, PnlLengthControl.Width, PnlLengthControl.Height));
            return bitmap;
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {
            Save();
            //ReubenController.SaveTestLevel(CurrentWorld);
        }

        private void Save()
        {
            CurrentWorld.GraphicsBank = CmbGraphics.SelectedIndex;
            CurrentWorld.Palette = CmbPalettes.SelectedIndex;
            CurrentWorld.Music = CmbMusic.SelectedIndex;
            CurrentWorld.Save();
            MessageBox.Show("World succesfully saved.");
        }
        #endregion

        #region pointers
        private void BtnAddPointer_Click(object sender, EventArgs e)
        {
            CurrentWorld.AddPointer();
            PntEditor.CurrentPointer = CurrentWorld.Pointers[CurrentWorld.Pointers.Count - 1];
            WldView.DelayDrawing = true;
            WldView.UpdatePoint(8, 0x16);
            WldView.DelayDrawing = false;
            WldView.SelectionRectangle = new Rectangle(0, 0, 1, 1);
            CurrentPointer = PntEditor.CurrentPointer;
        }

        private void BtnDeletePointer_Click(object sender, EventArgs e)
        {
            DeleteCurrentPointer();
        }

        private void DeleteCurrentPointer()
        {
            if (CurrentPointer != null)
            {
                WldView.DelayDrawing = true;
                CurrentWorld.Pointers.Remove(CurrentPointer);
                WldView.UpdatePoint(CurrentPointer.X, CurrentPointer.Y);
                WldView.DelayDrawing = false;
                WldView.ClearSelection();
                PntEditor.CurrentPointer = null;
                BtnDeletePointer.Enabled = false;
                BtnAddPointer.Enabled = true;
            }
        }
        #endregion

        private void BlsSelector_DoubleClick(object sender, EventArgs e)
        {
            ReubenController.OpenBlockEditor(CurrentWorld.Type, LeftMouseTile, CmbGraphics.SelectedIndex, CurrentWorld.AnimationBank, CmbPalettes.SelectedIndex);
        }

        int PreviousSelectorX, PreviousSelectorY;
        private void BlsSelector_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            int index =(e.X / 16) + ((e.Y / 16) * 16);
            if(index > 255) return;
            if (PreviousSelectorX == x && PreviousSelectorY == y) return;
            PreviousSelectorX = x;
            PreviousSelectorY = y;
            int tile = BlsSelector.BlockLayout.Layout[index];
            LblSelectorHover.Text = "Block: " + tile.ToHexString();
            LevelToolTip.SetToolTip(BlsSelector, ProjectController.BlockManager.GetBlockString(CurrentWorld.Type, tile));
        }

        public int DrawingTile
        {
            get
            {
                if (MouseButtons == MouseButtons.Middle)
                    return (int)0;

                if(DrawMode != DrawMode.Selection)
                {
                    if (MouseButtons == MouseButtons.Left)
                        return LeftMouseTile;
                    else
                        return RightMouseTile;
                }

                return LeftMouseTile;
            }
        }

        private void LblRightClickMode_Click(object sender, EventArgs e)
        {
            ToggleRightClickMode();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.Guide1.Visible = PnlHorizontalGuide.Guide2.Visible = false;
            WldView.UpdateGuide(Orientation.Horizontal, 1);
            WldView.UpdateGuide(Orientation.Horizontal, 2);
        }

        private void freeGuide2_Click(object sender, EventArgs e)
        {
            PnlHorizontalGuide.GuideSnapMode = GuideMode.Free;
            freeGuide2.Checked = true;
            snapToScreenLengthToolStripMenuItem.Checked =
            snapToFullMeterJumpLengthToolStripMenuItem.Checked =
            snapToRunningJumpHeightToolStripMenuItem.Checked =
            snapToWalkingJumpLengthToolStripMenuItem.Checked =
            snapToJumpLengthToolStripMenuItem.Checked = false;
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "ShowGrid", TsbGrid.Checked);
            ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "ShowStart", TsbStartPoint.Checked);
            ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "Zoom", TsbZoom.Checked);
            ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "Draw", DrawMode.ToString());
            ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "Layout", CmbLayouts.SelectedIndex);
            ProjectController.Save();
        }

        private List<IUndoableAction> UndoBuffer;
        private List<IUndoableAction> RedoBuffer;
        private MultiTileAction CurrentMultiTile;

        private void Undo()
        {
            if (UndoBuffer.Count == 0) return;
            IUndoableAction action = UndoBuffer[UndoBuffer.Count - 1];
            switch (action.Type)
            {
                case ActionType.TileArea:
                    UndoTileArea((TileAreaAction)action);
                    break;

                case ActionType.MultiTile:
                    UndoMultiTile((MultiTileAction)action);
                    break;
            }
        }

        private void UndoTileArea(TileAreaAction action)
        {
            WldView.DelayDrawing = true;
            Rectangle usedRectangle = new Rectangle(action.X, action.Y, action.Data.GetLength(0), action.Data.GetLength(1));

            int sX = usedRectangle.X;
            int sY = usedRectangle.Y;
            RedoBuffer.Add(action);

            for (int j = 0; j < usedRectangle.Height; j++)
            {
                for (int i = 0; i < usedRectangle.Width; i++)
                {
                    CurrentWorld.SetTile(usedRectangle.X + i, usedRectangle.Y + j, action.Data[i ,j]);
                }
            }
            WldView.DelayDrawing = false;
            WldView.UpdateArea(usedRectangle);
            UndoBuffer.Remove(action);
        }

        private void UndoMultiTile(MultiTileAction action)
        {
            RedoBuffer.Add(action);
            UndoBuffer.Remove(action);

            WldView.DelayDrawing = true;
            foreach (SingleTileChange stc in action.TileChanges.Reverse<SingleTileChange>())
            {
                CurrentWorld.SetTile(stc.X, stc.Y, (byte)stc.Tile);
            }

            WldView.DelayDrawing = false;
            WldView.UpdateArea(action.InvalidArea);
        }

        private void changeGuideColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.Color = ProjectController.SettingsManager.GetLevelSetting<Color>(CurrentWorld.Guid, "VGuideColor");
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "VGuideColor", cDialog.Color);
                PnlVerticalGuide.GuideColor = cDialog.Color;
            }
        }

        private void changeGuideColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.Color = ProjectController.SettingsManager.GetLevelSetting<Color>(CurrentWorld.Guid, "HGuideColor");
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                ProjectController.SettingsManager.SetLevelSetting(CurrentWorld.Guid, "HGuideColor", cDialog.Color);
                PnlHorizontalGuide.GuideColor = cDialog.Color;
            }
        }
    }
}
