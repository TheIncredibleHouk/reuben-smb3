
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

namespace Daiz.NES.Reuben
{
    public partial class LevelEditor : Form
    {
        PatternTable CurrentTable;
        PaletteInfo CurrentPalette;
        int LeftMouseTile = 0;
        int RightMouseTile = 0;
        private MouseMode MouseMode = MouseMode.RightClickSelection;
        public Level CurrentLevel { get; private set; }

        public LevelEditor()
        {
            InitializeComponent();

            LvlView.DelayDrawing = true;
            UndoBuffer = new List<IUndoableAction>();
            RedoBuffer = new List<IUndoableAction>();
            CmbMusic.DisplayMember = CmbLayouts.DisplayMember = CmbTypes.DisplayMember = CmbPalettes.DisplayMember = CmbGraphics.DisplayMember = "Name";
            foreach (var g in ProjectController.GraphicsManager.GraphicsInfo)
            {
                CmbGraphics.Items.Add(g);
            }

            CmbGraphics.Items.RemoveAt(254);

            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                CmbPalettes.Items.Add(p);
            }

            foreach (var t in ProjectController.LevelManager.LevelTypes)
            {
                if (t.InGameID != 0)
                    CmbTypes.Items.Add(t);
            }

            for (int i = 1; i < 16; i++)
            {
                CmbLength.Items.Add(i);
            }

            foreach (var m in ProjectController.MusicManager.MusicList)
            {
                CmbMusic.Items.Add(m);
            }

            foreach (var l in ProjectController.LayoutManager.BlockLayouts)
            {
                CmbLayouts.Items.Add(l);
            }

            CurrentTable = new PatternTable();
            LvlView.CurrentTable = BlsSelector.CurrentTable = CurrentTable;
            BlsSelector.BlockLayout = ProjectController.LayoutManager.BlockLayouts[0];
            BlsSelector.SpecialTable = LvlView.SpecialTable = ProjectController.SpecialManager.SpecialTable;
            BlsSelector.SpecialPalette = LvlView.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;
            BlsSelector.SelectionChanged += new EventHandler<TEventArgs<MouseButtons>>(BlsSelector_SelectionChanged);

            // BlvRight.CurrentTable = BlvLeft.CurrentTable = CurrentTable;

            ProjectController.PaletteManager.PaletteAdded += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteAdded);
            ProjectController.PaletteManager.PaletteRemoved += new EventHandler<TEventArgs<PaletteInfo>>(PaletteManager_PaletteRemoved);
            ProjectController.PaletteManager.PalettesSaved += new EventHandler(PaletteManager_PalettesSaved);
            ProjectController.BlockManager.DefinitionsSaved += new EventHandler(BlockManager_DefinitionsSaved);
            ProjectController.LayoutManager.LayoutAdded += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutAdded);
            ProjectController.GraphicsManager.GraphicsUpdated += new EventHandler(GraphicsManager_GraphicsUpdated);
            ProjectController.LayoutManager.LayoutRemoved += new EventHandler<TEventArgs<BlockLayout>>(LayoutManager_LayoutRemoved);
            PnlVerticalGuide.Guide1Changed += new EventHandler(PnlVerticalGuide_Guide1Changed);
            PnlVerticalGuide.Guide2Changed += new EventHandler(PnlVerticalGuide_Guide2Changed);
            PnlHorizontalGuide.Guide1Changed += new EventHandler(PnlHorizontalGuide_Guide1Changed);
            PnlHorizontalGuide.Guide2Changed += new EventHandler(PnlHorizontalGuide_Guide2Changed);
            ReubenController.GraphicsReloaded += new EventHandler(ReubenController_GraphicsReloaded);
            ReubenController.LevelReloaded += new EventHandler<TEventArgs<Level>>(ReubenController_LevelReloaded);

            LoadSpriteSelector();
            LvlView.HorizontalGuide1 = PnlHorizontalGuide.Guide1;
            LvlView.HorizontalGuide2 = PnlHorizontalGuide.Guide2;
            LvlView.VerticalGuide1 = PnlVerticalGuide.Guide1;
            LvlView.VerticalGuide2 = PnlVerticalGuide.Guide2;
            LvlView.DelayDrawing = false;
            LvlView.FullUpdate();
        }

        int previousLeftIndex;

        void BlsSelector_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            if (MouseMode == MouseMode.RightClickSelection)
            {
                previousLeftIndex = LeftMouseTile;
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
                        previousLeftIndex = LeftMouseTile;
                        LeftMouseTile = BlsSelector.SelectedTileIndex;
                        BlvLeft.PaletteIndex = (LeftMouseTile & 0xC0) >> 6;
                        BlvLeft.CurrentBlock = BlsSelector.SelectedBlock;
                    }
                    else
                    {
                        RightMouseTile = BlsSelector.SelectedTileIndex;
                        //BlvRight.PaletteIndex = (RightMouseTile & 0xC0) >> 6;
                        //BlvRight.CurrentBlock = BlsSelector.SelectedBlock;
                    }
                }
            }
        }

        void ReubenController_LevelReloaded(object sender, TEventArgs<Level> e)
        {
            if (CurrentLevel == e.Data)
            {
                GetLevelInfo(e.Data);
            }
        }

        void ReubenController_GraphicsReloaded(object sender, EventArgs e)
        {
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

        public void EditLevel(Level l)
        {
            GetLevelInfo(l);
            PntEditor.CurrentLevel = l;
            LblStartPoint.Text = string.Format("X: {0} Y: {1}", CurrentLevel.XStart.ToHexString(), CurrentLevel.YStart.ToHexString());
            TsbGrid.Checked = CurrentLevel.Settings.ShowGrid;
            TsbItems.Checked = CurrentLevel.Settings.SpecialTiles;
            TsbSriteSpecials.Checked = CurrentLevel.Settings.SpecialSprites;
            TsbSolidity.Checked = CurrentLevel.Settings.BlockProperties;
            TsbStartPoint.Checked = CurrentLevel.Settings.ShowStart;
            TsbZoom.Checked = CurrentLevel.Settings.Zoom;
            CmbSpecialType.SelectedIndex = CurrentLevel.SpecialLevelType;
            txtMisc1.Text = CurrentLevel.MiscByte1.ToHexString();
            txtMisc2.Text = CurrentLevel.MiscByte2.ToHexString();
            txtMisc3.Text = CurrentLevel.MiscByte3.ToHexString();
            CmbAnim.SelectedIndex = CurrentLevel.AnimationType;
            ChkInvincibleEnemies.Checked = CurrentLevel.InvincibleEnemies;
            CmbPaletteEffect.SelectedIndex = CurrentLevel.PaletteEffect;
            ChkProjectileSpins.Checked = CurrentLevel.ProjectileBlocksTemporary;
            ChkRhythm.Checked = CurrentLevel.RhythmPlatforms;

            switch (CurrentLevel.Settings.DrawMode)
            {
                case TileDrawMode.Pencil:
                    TsbPencil.Checked = true;
                    TileDrawMode = TileDrawMode.Pencil;
                    break;

                case TileDrawMode.Rectangle:
                    TsbRectangle.Checked = true;
                    TileDrawMode = TileDrawMode.Rectangle;
                    break;

                case TileDrawMode.Outline:
                    TsbOutline.Checked = true;
                    TileDrawMode = TileDrawMode.Outline;
                    break;

                case TileDrawMode.Line:
                    TsbLine.Checked = true;
                    TileDrawMode = TileDrawMode.Line;
                    break;

                case TileDrawMode.Fill:
                    TileDrawMode = TileDrawMode.Fill;
                    TsbBucket.Checked = true;
                    break;

                case TileDrawMode.Replace:
                    TileDrawMode = TileDrawMode.Replace;
                    TsbReplace.Checked = true;
                    break;
            }

            switch (CurrentLevel.Settings.EditMode)
            {
                case EditMode.Tiles:
                    TabEditSelector.SelectedIndex = 0;
                    EditMode = EditMode.Tiles;
                    break;

                case EditMode.Sprites:
                    TabEditSelector.SelectedIndex = 1;
                    EditMode = EditMode.Sprites;
                    break;

                case EditMode.Pointers:
                    TabEditSelector.SelectedIndex = 2;
                    EditMode = EditMode.Pointers;
                    break;
            }

            CmbLayouts.SelectedIndex = CurrentLevel.Settings.Layout;
            PnlHorizontalGuide.GuideColor = CurrentLevel.Settings.HGuideColor;
            PnlVerticalGuide.GuideColor = CurrentLevel.Settings.VGuideColor;
            TsbPointers.Checked = CurrentLevel.Settings.ShowPointers;

            this.Text = ProjectController.LevelManager.GetLevelInfo(l.Guid).Name;
            this.WindowState = FormWindowState.Maximized;
            this.Show();
            LvlView.FullUpdate();
        }

        private void GetLevelInfo(Level l)
        {
            LvlView.Zoom = 1;
            LvlView.CurrentLevel = l;
            CurrentLevel = l;
            NumTime.Value = l.Time;
            NumBackground.Value = l.ClearValue;
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentLevel.AnimationBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentLevel.AnimationBank + 1]);
            CmbGraphics.SelectedIndex = l.GraphicsBank;
            if (CmbPalettes.Items.Count < l.Palette)
            {
                l.Palette = CmbPalettes.Items.Count - 1;
            }

            CmbPalettes.SelectedIndex = l.Palette;
            CmbTypes.SelectedIndex = l.Type - 1;
            CmbScroll.SelectedIndex = l.ScrollType;
            CmbMusic.SelectedIndex = l.Music >= CmbMusic.Items.Count ? 0 : l.Music;
            CmbLength.SelectedItem = l.Length;
            PntEditor.CurrentPointer = null;
            BtnAddPointer.Enabled = CurrentLevel.Pointers.Count <= 10;
            BtnDeletePointer.Enabled = false;
            NumSpecials.Value = (decimal)CurrentLevel.Settings.ItemTransparency;
            LvlView.DelayDrawing = false;
            LvlView.FullUpdate();
        }

        #region guide events
        private void PnlHorizontalGuide_Guide2Changed(object sender, EventArgs e)
        {
            LvlView.UpdateGuide(Orientation.Horizontal, 2);
        }

        private void PnlHorizontalGuide_Guide1Changed(object sender, EventArgs e)
        {
            LvlView.UpdateGuide(Orientation.Horizontal, 1);
        }

        private void PnlVerticalGuide_Guide2Changed(object sender, EventArgs e)
        {
            LvlView.UpdateGuide(Orientation.Vertical, 2);
        }

        private void PnlVerticalGuide_Guide1Changed(object sender, EventArgs e)
        {
            LvlView.UpdateGuide(Orientation.Vertical, 1);
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
            LvlView.UpdateGuide(Orientation.Vertical, 1);
            LvlView.UpdateGuide(Orientation.Vertical, 2);
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
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex + 1]);
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentLevel.AnimationBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentLevel.AnimationBank + 1]);
            LvlView.CurrentPalette = BlvLeft.CurrentPalette = BlsSelector.CurrentPalette = LvlView.CurrentPalette = CurrentPalette;
            BlsSelector.CurrentDefiniton = LvlView.CurrentDefiniton = LvlView.CurrentDefiniton;
            BlvLeft.CurrentBlock = BlvLeft.CurrentBlock;
        }
        #endregion

        #region mouse functions
        private bool useTransparentTile;
        private int StartX, StartY, FromX, FromY, ToX, ToY;
        private bool ContinueDragging;
        private TileDrawMode PreviousMode;

        private void LvlView_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (e.X / 16) / LvlView.Zoom;
            int y = (e.Y / 16) / LvlView.Zoom;
            PnlView.Focus();

            vMirrorButton.Enabled = hMirrorButton.Enabled = false;
            if (x < 0 || x >= CurrentLevel.Width || y < 0 || y >= CurrentLevel.Height)
                return;

            if (_SelectingStartPositionMode)
            {
                int oldX = CurrentLevel.XStart;
                int oldY = CurrentLevel.YStart;
                CurrentLevel.XStart = x;
                CurrentLevel.YStart = y;
                if (TsbStartPoint.Checked)
                {
                    LvlView.UpdatePoint(x, y);
                    LvlView.UpdatePoint(oldX, oldY);
                }
                _SelectingStartPositionMode = false;
                TlsTileCommands.Enabled = TlsDrawing.Enabled = TabLevelInfo.Enabled = true;
                SetHelpText(PreviousHelperText);
                LblStartPoint.Text = string.Format("X: {0} Y: {1}", CurrentLevel.XStart.ToHexString(), CurrentLevel.YStart.ToHexString());
            }
            else if (_PlacingPointer)
            {
                _PlacingPointer = false;
                CurrentLevel.AddPointer();
                CurrentPointer = PntEditor.CurrentPointer = CurrentLevel.Pointers[CurrentLevel.Pointers.Count - 1];
                CurrentPointer.XEnter = x;
                CurrentPointer.YEnter = y;
                LvlView.DelayDrawing = true;
                LvlView.UpdatePoint(x, y);
                LvlView.UpdatePoint(x + 1, y);
                LvlView.UpdatePoint(x, y + 1);
                LvlView.UpdatePoint(x + 1, y + 1);
                LvlView.DelayDrawing = false;
                LvlView.SelectionRectangle = new Rectangle(x, y, 2, 2);
                BtnAddPointer.Enabled = CurrentLevel.Pointers.Count <= 10;
                TabEditSelector.Enabled = PnlInfo.Enabled = true;
                SetHelpText(Reuben.Properties.Resources.PointerHelper);
            }

            else if (EditMode == EditMode.Tiles)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    BlsSelector.SelectedTileIndex = CurrentLevel.LevelData[x, y];
                    BlsSelector_SelectionChanged(this, new TEventArgs<MouseButtons>(e.Button));
                }
                else
                {
                    LvlView.ClearSelection();
                    if (TileDrawMode == TileDrawMode.Selection)
                    {
                        TileDrawMode = PreviousMode;
                    }

                    if (e.Button == MouseButtons.Right && MouseMode == MouseMode.RightClickSelection)
                    {
                        PreviousMode = TileDrawMode;
                        TileDrawMode = TileDrawMode.Selection;
                    }

                    switch (TileDrawMode)
                    {
                        case TileDrawMode.Pencil:
                            CurrentMultiTile = new MultiTileAction();
                            CurrentMultiTile.AddTileChange(x, y, CurrentLevel.LevelData[x, y]);
                            CurrentLevel.SetTile(x, y, (byte)(DrawingTile));
                            ContinueDrawing = true;
                            break;

                        case TileDrawMode.Outline:
                        case TileDrawMode.Rectangle:
                        case TileDrawMode.Scatter:
                        case TileDrawMode.Selection:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            LvlView.SelectionRectangle = new Rectangle(StartX, StartY, 1, 1);
                            break;

                        case TileDrawMode.Line:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            LvlView.SelectionLine = new Line(StartX, StartY, StartX, StartY);
                            break;

                        case TileDrawMode.Fill:
                            Point start = new Point(x, y);
                            Stack<Point> stack = new Stack<Point>();
                            stack.Push(start);
                            int checkValue = CurrentLevel.LevelData[x, y];
                            if (checkValue == DrawingTile)
                                return;

                            CurrentMultiTile = new MultiTileAction();
                            while (stack.Count > 0)
                            {
                                Point p = stack.Pop();
                                int lowestX, highestX;
                                ;
                                int lowestY, highestY;
                                lowestX = highestX = x;
                                lowestY = highestY = y;
                                int i = p.X;
                                int j = p.Y;
                                if (CurrentLevel.LevelLayout == LevelLayout.Horizontal)
                                {
                                    if (j < 0 || j >= CurrentLevel.Height || i < 0 || i >= CurrentLevel.Length * 16)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    if (j < 0 || j >= (CurrentLevel.Length * 15) - 4 || i < 0 || i >= CurrentLevel.Width)
                                    {
                                        continue;
                                    }
                                }

                                LvlView.DelayDrawing = true;
                                if (checkValue == CurrentLevel.LevelData[i, j])
                                {
                                    CurrentMultiTile.AddTileChange(i, j, CurrentLevel.LevelData[i, j]);
                                    CurrentLevel.SetTile(i, j, (byte)DrawingTile);
                                    if (i < lowestX)
                                        lowestX = i;
                                    if (i > highestX)
                                        highestX = i;
                                    if (j < lowestY)
                                        lowestY = j;
                                    if (j > highestY)
                                        highestY = j;

                                    stack.Push(new Point(i + 1, j));
                                    stack.Push(new Point(i - 1, j));
                                    stack.Push(new Point(i, j + 1));
                                    stack.Push(new Point(i, j - 1));
                                }
                                UndoBuffer.Add(CurrentMultiTile);
                                LvlView.DelayDrawing = false;
                                LvlView.UpdateArea(new Rectangle(lowestX, lowestY, highestX - lowestX + 1, highestY - lowestY + 1));
                            }

                            break;

                        case TileDrawMode.Replace:
                            LvlView.DelayDrawing = true;
                            int findTile = CurrentLevel.LevelData[x, y];
                            MultiTileAction mta = new MultiTileAction();
                            for (int i = 0; i < (15 * 16); i++)
                            {
                                for (int j = 0; j < 0x1B; j++)
                                {
                                    if (CurrentLevel.LevelData[i, j] == findTile)
                                    {
                                        mta.AddTileChange(i, j, CurrentLevel.LevelData[i, j]);
                                        CurrentLevel.LevelData[i, j] = (byte)DrawingTile;
                                    }
                                }
                            }

                            LvlView.DelayDrawing = false;
                            ;
                            LvlView.FullUpdate();
                            break;
                    }
                }
            }
            else if (EditMode == EditMode.Sprites)
            {
                previousSprite = CurrentSprite;
                CurrentSprite = SelectSprite(x, y);

                modifySpriteVisiblity = false;

                if (CurrentSprite == null)
                {
                    CmbSpriteProperty.Enabled = false;
                    CmbSpriteProperty.SelectedIndex = -1;
                    CmbSpriteProperty.DataSource = null;
                }

                if (CurrentSprite != null)
                {
                    SpriteDefinition def = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID);
                    if (def != null)
                    {
                        CmbSpriteProperty.DataSource = def.PropertyDescriptions;
                    }
                    else
                    {
                        CmbSpriteProperty.DataSource = null;
                    }
                }

                if (CurrentSprite != null && MouseButtons == MouseButtons.Left)
                {
                    if (CmbSpriteProperty.Items.Count > 0)
                    {
                        CmbSpriteProperty.SelectedIndex = CurrentSprite.Property;
                    }
                    CmbSpriteProperty.Enabled = true;

                    LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    ContinueDragging = true;
                    LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                    if (ModifierKeys == Keys.Shift)
                    {
                        ((SpriteViewer)TabClass1.SelectedTab.Controls[0]).SelectedSprite = null;
                        ((SpriteViewer)TabClass2.SelectedTab.Controls[0]).SelectedSprite = null;
                        ((SpriteViewer)TabClass3.SelectedTab.Controls[0]).SelectedSprite = null;

                        SpriteDefinition sDef = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID);
                        switch (sDef.Class)
                        {
                            case 1:
                                foreach (TabPage t in TabClass1.TabPages)
                                {
                                    if (t.Text == sDef.Group)
                                    {
                                        TabClass1.SelectedTab = t;
                                    }
                                }
                                break;

                            case 2:
                                foreach (TabPage t in TabClass2.TabPages)
                                {
                                    if (t.Text == sDef.Group)
                                    {
                                        TabClass2.SelectedTab = t;
                                    }
                                }
                                break;

                            case 3:
                                foreach (TabPage t in TabClass3.TabPages)
                                {
                                    if (t.Text == sDef.Group)
                                    {
                                        TabClass3.SelectedTab = t;
                                    }
                                }
                                break;
                        }

                        SpriteViewer sv = SpriteViewers.Find(s => s.SpriteList.Find(c => c.InGameID == CurrentSprite.InGameID) != null);
                        sv.SetSelectedSprite(CurrentSprite);
                        CurrentSelectorSprite = sv.SelectedSprite;
                    }
                }
                else if (CurrentSprite != null && MouseButtons == MouseButtons.Right && CurrentSelectorSprite != null)
                {
                    CurrentSprite.InGameID = CurrentSelectorSprite.InGameID;
                    CurrentSprite.Property = 0;
                    CurrentSprite.Property = CurrentSelectorSprite.Property;
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
                    LvlView.DelayDrawing = true;
                    LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    LvlView.DelayDrawing = false;
                    LvlView.UpdateSprites(r);
                }
                else if (CurrentSelectorSprite != null && MouseButtons == MouseButtons.Right)
                {
                    Sprite newSprite = new Sprite() { X = x, Y = y, InGameID = CurrentSelectorSprite.InGameID };
                    if (previousSprite != null && newSprite.InGameID == previousSprite.InGameID)
                    {
                        newSprite.Property = previousSprite.Property;
                    }
                    
                    CurrentLevel.AddSprite(newSprite);
                    CurrentSprite = newSprite;
                    LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                    ContinueDragging = true;
                    LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                    CmbSpriteProperty.DataSource = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID).PropertyDescriptions;
                    CmbSpriteProperty.Enabled = CmbSpriteProperty.DataSource != null && CmbSpriteProperty.Items.Count > 1;

                    if (CmbSpriteProperty.Enabled)
                    {
                        CmbSpriteProperty.SelectedIndex = CurrentSprite.Property;
                    }
                }
                else
                {
                    LvlView.ClearSelection();
                    ContinueDragging = false;
                    LblSprite.Text = "None";
                }

                modifySpriteVisiblity = true;
            }
            else if (EditMode == EditMode.Pointers)
            {
                LevelPointer p = CurrentLevel.Pointers.Find(pt => (pt.XEnter == x || pt.XEnter + 1 == x) && (pt.YEnter == y || pt.YEnter + 1 == y));
                PntEditor.CurrentPointer = p;
                CurrentPointer = p;
                if (p != null)
                {
                    LvlView.SelectionRectangle = new Rectangle(p.XEnter, p.YEnter, 2, 2);
                    ContinueDragging = true;
                    BtnDeletePointer.Enabled = true;
                }
                else
                {
                    BtnDeletePointer.Enabled = false;
                    LvlView.ClearSelection();
                }
            }
        }

        private int PreviousMouseX, PreviousMouseY;
        private void LvlView_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (e.X / (16 * LvlView.Zoom));
            int y = (e.Y / (16 * LvlView.Zoom));

            if (x < 0 || x >= CurrentLevel.Width || y < 0 || y >= CurrentLevel.Height)
                return;

            if (PreviousMouseX == x && PreviousMouseY == y)
                return;
            PreviousMouseX = x;
            PreviousMouseY = y;

            int XDiff = x - StartX;
            int YDiff = y - StartY;

            LblPositition.Text = "X: " + x.ToHexString() + " Y: " + y.ToHexString();

            if (_PlacingPointer)
            {
                SetHelpText(Reuben.Properties.Resources.PointerPlacementHelper);
            }
            else if (_SelectingStartPositionMode)
            {
                SetHelpText(Reuben.Properties.Resources.StartPlacementHelper);
            }
            else if (EditMode == EditMode.Tiles)
            {
                SetTileModeText();
                LevelToolTip.SetToolTip(LvlView, ProjectController.BlockManager.GetBlockString(CurrentLevel.Type, CurrentLevel.LevelData[x, y]));

                if (ContinueDrawing && (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Middle || MouseButtons == MouseButtons.Right))
                {
                    switch (TileDrawMode)
                    {
                        case TileDrawMode.Pencil:
                            CurrentMultiTile.AddTileChange(x, y, CurrentLevel.LevelData[x, y]);
                            CurrentLevel.SetTile(x, y, (byte)DrawingTile);
                            break;

                        case TileDrawMode.Outline:
                        case TileDrawMode.Rectangle:
                        case TileDrawMode.Selection:
                            if (StartX == x && StartY == y)
                                return;
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


                            LvlView.SelectionRectangle = new Rectangle(FromX, FromY, (ToX - FromX) + 1, (ToY - FromY) + 1);
                            vMirrorButton.Enabled = hMirrorButton.Enabled = true;
                            break;

                        case TileDrawMode.Line:
                            SetHelpText(Reuben.Properties.Resources.LineModeHelper);
                            if (y > StartY)
                            {
                                if (x > StartX)
                                {
                                    LvlView.SelectionLine = new Line(StartX, StartY, x, StartY + (x - StartX));
                                }
                                else
                                {
                                    LvlView.SelectionLine = new Line(StartX, StartY, x, StartY - (x - StartX));
                                }
                            }
                            else
                            {
                                if (x > StartX)
                                {
                                    LvlView.SelectionLine = new Line(StartX, StartY, x, StartY - (x - StartX));
                                }
                                else
                                {
                                    LvlView.SelectionLine = new Line(StartX, StartY, x, StartY + (x - StartX));
                                }
                            }
                            break;
                    }
                }
            }
            else if (EditMode == EditMode.Sprites)
            {
                SetHelpText(Reuben.Properties.Resources.SpriteLevelHelper);
                Sprite s = SelectSprite(x, y);

                if (s != null)
                {
                    LevelToolTip.SetToolTip(LvlView, s.Name + "\n(" + s.InGameID.ToHexString() + ")");
                }
                else
                {
                    LevelToolTip.SetToolTip(LvlView, null);
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
                        LvlView.DelayDrawing = true;
                        LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                        LvlView.DelayDrawing = false;
                        LvlView.UpdateSprites(r);
                    }
                }
            }
            else if (ContinueDragging && EditMode == EditMode.Pointers && CurrentPointer != null && MouseButtons == MouseButtons.Left)
            {
                SetHelpText(Reuben.Properties.Resources.PointerHelper);
                if (CurrentPointer != null)
                {
                    if (x == CurrentLevel.Width - 1 || y == CurrentLevel.Height - 1)
                        return;
                    if (CurrentPointer.XEnter == x && CurrentPointer.YEnter == y)
                        return;
                    int oldX = CurrentPointer.XEnter;
                    int oldY = CurrentPointer.YEnter;
                    LvlView.DelayDrawing = true;
                    CurrentPointer.XEnter = x;
                    CurrentPointer.YEnter = y;
                    LvlView.UpdatePoint(oldX, oldY);
                    LvlView.UpdatePoint(oldX + 1, oldY);
                    LvlView.UpdatePoint(oldX, oldY + 1);
                    LvlView.UpdatePoint(oldX + 1, oldY + 1);
                    LvlView.UpdatePoint(x, y);
                    LvlView.UpdatePoint(x + 1, y);
                    LvlView.UpdatePoint(x, y + 1);
                    LvlView.UpdatePoint(x + 1, y + 1);
                    LvlView.DelayDrawing = false;
                    LvlView.SelectionRectangle = new Rectangle(CurrentPointer.XEnter, CurrentPointer.YEnter, 2, 2);
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
            if (!ContinueDrawing)
                return;
            int _DrawTile = 0;
            int sX, sY;

            if (e.Button == MouseButtons.Middle)
            {
                _DrawTile = (int)NumBackground.Value;
            }
            else if (TileDrawMode != TileDrawMode.Selection)
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
                if (TileDrawMode == TileDrawMode.Pencil)
                {
                    UndoBuffer.Add(CurrentMultiTile);
                }
                else if ((LvlView.HasSelection || LvlView.HasSelectionLine))
                {
                    switch (TileDrawMode)
                    {
                        case TileDrawMode.Rectangle:
                            sX = LvlView.SelectionRectangle.X;
                            sY = LvlView.SelectionRectangle.Y;

                            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentLevel.GetData(sX, sY, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height)));

                            LvlView.DelayDrawing = true;
                            for (int y = LvlView.SelectionRectangle.Y, i = 0; i < LvlView.SelectionRectangle.Height; y++, i++)
                            {
                                for (int x = LvlView.SelectionRectangle.X, j = 0; j < LvlView.SelectionRectangle.Width; x++, j++)
                                {
                                    CurrentLevel.SetTile(x, y, (byte)_DrawTile);
                                }
                            }
                            LvlView.DelayDrawing = false;
                            LvlView.UpdateArea();
                            break;

                        case TileDrawMode.Outline:
                            sX = LvlView.SelectionRectangle.X;
                            sY = LvlView.SelectionRectangle.Y;

                            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentLevel.GetData(sX, sY, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height)));

                            LvlView.DelayDrawing = true;
                            for (int x = LvlView.SelectionRectangle.X, i = 0; i < LvlView.SelectionRectangle.Width; i++, x++)
                            {
                                CurrentLevel.SetTile(x, LvlView.SelectionRectangle.Y, (byte)_DrawTile);
                                CurrentLevel.SetTile(x, LvlView.SelectionRectangle.Y + LvlView.SelectionRectangle.Height - 1, (byte)_DrawTile);
                            }

                            for (int y = LvlView.SelectionRectangle.Y, i = 1; i < LvlView.SelectionRectangle.Height; i++, y++)
                            {
                                CurrentLevel.SetTile(LvlView.SelectionRectangle.X, y, (byte)_DrawTile);
                                CurrentLevel.SetTile(LvlView.SelectionRectangle.X + LvlView.SelectionRectangle.Width - 1, y, (byte)_DrawTile);
                            }
                            LvlView.DelayDrawing = false;
                            LvlView.UpdateArea();
                            break;

                        case TileDrawMode.Line:

                            LvlView.DelayDrawing = true;
                            CurrentMultiTile = new MultiTileAction();
                            int breakAt = Math.Abs(LvlView.SelectionLine.End.X - LvlView.SelectionLine.Start.X) + 1;
                            if (LvlView.SelectionLine.End.X > LvlView.SelectionLine.Start.X)
                            {
                                if (LvlView.SelectionLine.End.Y > LvlView.SelectionLine.Start.Y)
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (LvlView.SelectionLine.Start.X + i >= CurrentLevel.Width || LvlView.SelectionLine.Start.Y + i >= CurrentLevel.Height)
                                            continue;
                                        sX = LvlView.SelectionLine.Start.X + i;
                                        sY = LvlView.SelectionLine.Start.Y + i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentLevel.LevelData[sX, sY]);
                                        CurrentLevel.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (LvlView.SelectionLine.Start.X + i >= CurrentLevel.Width || LvlView.SelectionLine.Start.Y - i >= CurrentLevel.Height)
                                            continue;
                                        sX = LvlView.SelectionLine.Start.X + i;
                                        sY = LvlView.SelectionLine.Start.Y - i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentLevel.LevelData[sX, sY]);
                                        CurrentLevel.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                            }
                            else
                            {
                                if (LvlView.SelectionLine.End.Y > LvlView.SelectionLine.Start.Y)
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (LvlView.SelectionLine.Start.X - i >= CurrentLevel.Width || LvlView.SelectionLine.Start.Y + i >= CurrentLevel.Height)
                                            continue;
                                        sX = LvlView.SelectionLine.Start.X - i;
                                        sY = LvlView.SelectionLine.Start.Y + i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentLevel.LevelData[sX, sY]);
                                        CurrentLevel.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < breakAt; i++)
                                    {
                                        if (LvlView.SelectionLine.Start.X - i >= CurrentLevel.Width || LvlView.SelectionLine.Start.Y - i >= CurrentLevel.Height)
                                            continue;
                                        sX = LvlView.SelectionLine.Start.X - i;
                                        sY = LvlView.SelectionLine.Start.Y - i;
                                        CurrentMultiTile.AddTileChange(sX, sY, CurrentLevel.LevelData[sX, sY]);
                                        CurrentLevel.SetTile(sX, sY, (byte)_DrawTile);
                                    }
                                }
                            }

                            UndoBuffer.Add(CurrentMultiTile);
                            LvlView.DelayDrawing = false;
                            LvlView.ClearLine();
                            break;

                        case TileDrawMode.Selection:
                            useTransparentTile = e.Button == MouseButtons.Right;
                            break;

                        case TileDrawMode.Scatter:
                            LvlView.DelayDrawing = true;
                            CurrentMultiTile = new MultiTileAction();
                            break;
                    }
                }
            }
        }

        private LevelPointer CurrentPointer;
        private bool ContinueDrawing;
        #endregion

        #region display options

        private void TsbGrid_CheckedChanged(object sender, EventArgs e)
        {
            LvlView.ShowGrid = TsbGrid.Checked;
        }

        private void TsbTileSpecials_CheckedChanged(object sender, EventArgs e)
        {
            BlsSelector.ShowSpecialBlocks = LvlView.ShowSpecialBlocks = TsbItems.Checked;
        }

        private void TsbStartPoint_CheckedChanged(object sender, EventArgs e)
        {
            LvlView.DisplayStartingPosition = TsbStartPoint.Checked;
        }

        private void TsbSriteSpecials_CheckedChanged(object sender, EventArgs e)
        {
            LvlView.ShowSpecialSprites = TsbSriteSpecials.Checked;
        }

        private void TsbZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (TsbZoom.Checked)
            {
                LvlView.Zoom = 2;
                PnlLengthControl.Size = new Size(PnlLengthControl.Size.Width * 2, PnlLengthControl.Size.Height * 2);
            }
            else
            {
                LvlView.Zoom = 1;
                PnlLengthControl.Size = new Size(PnlLengthControl.Size.Width / 2, PnlLengthControl.Size.Height / 2);
            }
        }
        #endregion

        #region drawing modes
        private TileDrawMode TileDrawMode = TileDrawMode.Pencil;

        private void TsbPencil_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.TileModeHelper);
            TileDrawMode = TileDrawMode.Pencil;
            TsbPencil.Checked = true;
            TsbReplace.Checked = TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbRectangle.Checked = false;
        }

        private void TsbRectangle_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.RectangleModeHelper);
            TileDrawMode = TileDrawMode.Rectangle;
            TsbRectangle.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
        }

        private void TsbOutline_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.OutlineModeHelper);
            TileDrawMode = TileDrawMode.Outline;
            TsbOutline.Checked = true;
            TsbReplace.Checked = TsbLine.Checked = TsbBucket.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
        }

        private void TsbBucket_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.BucketModeHelper);
            TileDrawMode = TileDrawMode.Fill;
            TsbBucket.Checked = true;
            TsbReplace.Checked = TsbLine.Checked = TsbOutline.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
        }

        private void TsbLine_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.LineModeHelper);
            TileDrawMode = TileDrawMode.Line;
            TsbLine.Checked = true;
            TsbReplace.Checked = TsbReplace.Checked = TsbRectangle.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
        }
        #endregion

        #region level header changes

        private void CmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentLevel.Type = (CmbTypes.SelectedItem as LevelType).InGameID;
            BlsSelector.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(CurrentLevel.Type);
            LvlView.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(CurrentLevel.Type);
            ProjectController.LevelManager.GetLevelInfo(CurrentLevel.Guid).LevelType = CurrentLevel.Type;
        }

        private void CmbLength_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentLevel.Length = CmbLength.SelectedItem.ToInt();
            switch (CurrentLevel.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    PnlLengthControl.Size = new Size(CurrentLevel.Length * 256 * LvlView.Zoom, 432 * LvlView.Zoom);
                    break;

                case LevelLayout.Vertical:
                    PnlLengthControl.Size = new Size(256 * LvlView.Zoom, ((CurrentLevel.Length * 240) - 64) * LvlView.Zoom);
                    break;
            }
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

        private void BtnClear_Click(object sender, EventArgs e)
        {
            LvlView.DelayDrawing = true;
            for (int y = 0; y < CurrentLevel.Height; y++)
            {
                for (int x = 0; x < CurrentLevel.Width; x++)
                {
                    if (CurrentLevel.LevelData[x, y] == CurrentLevel.ClearValue)
                        CurrentLevel.SetTile(x, y, (byte)NumBackground.Value);
                }
            }

            LvlView.DelayDrawing = true;
            LvlView.Redraw();

            CurrentLevel.ClearValue = (int)NumBackground.Value;
        }

        private void CmbPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentPalette != null)
            {
                CurrentPalette.PaletteChanged -= CurrentPalette_PaletteChanged;
            }

            CurrentLevel.Palette = CmbPalettes.SelectedIndex;
            CurrentPalette = CmbPalettes.SelectedItem as PaletteInfo;
            CurrentPalette.PaletteChanged += new EventHandler<TEventArgs<DoubleValue<int, int>>>(CurrentPalette_PaletteChanged);
            BlvLeft.CurrentPalette = BlsSelector.CurrentPalette = LvlView.CurrentPalette = CurrentPalette;
            foreach (var sv in SpriteViewers)
            {
                sv.CurrentPalette = CurrentPalette;
            }
        }

        private void CurrentPalette_PaletteChanged(object sender, TEventArgs<DoubleValue<int, int>> e)
        {
            BlvLeft.CurrentPalette = BlsSelector.CurrentPalette = LvlView.CurrentPalette = CurrentPalette;
            LvlView.Redraw();
            BlsSelector.Redraw();
        }

        private bool _SelectingStartPositionMode;

        private void BtnStartPoint_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.StartPlacementHelper);
            _SelectingStartPositionMode = true;
            TlsTileCommands.Enabled = TlsDrawing.Enabled = TabLevelInfo.Enabled = false;
        }

        #endregion

        #region sprites
        private Sprite CurrentSprite = null;
        private Sprite previousSprite = null;

        private Sprite SelectSprite(int x, int y)
        {
            var possibleSprites = (from s in CurrentLevel.SpriteData
                                   where x >= s.X && x <= s.X + (s.Width - 1) &&
                                         y >= s.Y && y <= s.Y + (s.Height - 1) &&
                                         s.IsViewable
                                   select s).FirstOrDefault();
            return possibleSprites;
        }

        private List<SpriteViewer> SpriteViewers = new List<SpriteViewer>();

        private void LoadSpriteSelector()
        {
            List<Sprite> CurrentList;
            foreach (var s in ProjectController.SpriteManager.SpriteGroups.Keys)
            {
                foreach (var k in from l in ProjectController.SpriteManager.SpriteGroups[s].Keys orderby l select l)
                {
                    if (k == "Map")
                        continue;
                    SpriteViewer spViewer = new SpriteViewer(ProjectController.SpriteManager.SpriteGroups[s][k].Count);
                    spViewer.SpecialPalette = ProjectController.SpecialManager.SpecialPalette;
                    CurrentList = new List<Sprite>();

                    int x = 0;
                    foreach (var ks in ProjectController.SpriteManager.SpriteGroups[s][k])
                    {
                        Sprite next = new Sprite();
                        next.Property = 0;
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
                    tPage.Text = k;
                    tPage.AutoScroll = true;
                    tPage.Controls.Add(spViewer);


                    switch (s)
                    {
                        case 1:
                            TabClass1.TabPages.Add(tPage);
                            break;

                        case 2:
                            TabClass2.TabPages.Add(tPage);
                            break;

                        case 3:
                            TabClass3.TabPages.Add(tPage);
                            break;
                    }
                }
            }
        }

        private Sprite CurrentSelectorSprite;
        private int currentSpriteProperty;

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
                    TlsDrawing.Visible = true;
                    TlsTileCommands.Visible = MouseMode == MouseMode.RightClickSelection;
                    break;
                case 1:
                    EditMode = EditMode.Sprites;
                    TlsDrawing.Visible = false;
                    TlsTileCommands.Visible = false;
                    break;

                case 2:
                    EditMode = EditMode.Pointers;
                    TlsDrawing.Visible = false;
                    TlsTileCommands.Visible = false;
                    break;
            }
        }

        private void ToggleRightClickMode()
        {
            MouseMode = MouseMode == MouseMode.RightClickSelection ? MouseMode.RightClickTile : MouseMode.RightClickSelection;
            switch (MouseMode)
            {
                case MouseMode.RightClickSelection:
                    TlsTileCommands.Visible = true;
                    SetHelpText(Reuben.Properties.Resources.RightClickSelectHelper);
                    break;

                case MouseMode.RightClickTile:
                    TlsTileCommands.Visible = false;
                    SetHelpText(Reuben.Properties.Resources.RightClickTileHelper);
                    break;
            }
        }

        public static byte[,] TileBuffer;

        private void DeleteTiles()
        {
            int sX = LvlView.SelectionRectangle.X;
            int sY = LvlView.SelectionRectangle.Y;
            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentLevel.GetData(sX, sY, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height)));
            LvlView.DelayDrawing = true;
            for (int y = sY, i = 0; i < LvlView.SelectionRectangle.Height; y++, i++)
            {
                for (int x = sX, j = 0; j < LvlView.SelectionRectangle.Width; x++, j++)
                {
                    CurrentLevel.SetTile(x, y, (byte)NumBackground.Value);
                }
            }
            LvlView.DelayDrawing = false;
            LvlView.UpdateArea();
        }

        private void Cut()
        {
            Copy();
            DeleteTiles();
        }

        private void Copy()
        {
            TileBuffer = CurrentLevel.GetData(LvlView.SelectionRectangle.X, LvlView.SelectionRectangle.Y, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height);
        }

        private void Paste()
        {

            LvlView.DelayDrawing = true;
            Rectangle usedRectangle = LvlView.SelectionRectangle;

            if (LvlView.SelectionRectangle.Width == 1 && LvlView.SelectionRectangle.Height == 1)
            {
                usedRectangle.Width = TileBuffer.GetLength(0);
                usedRectangle.Height = TileBuffer.GetLength(1);
            }

            int sX = usedRectangle.X;
            int sY = usedRectangle.Y;
            UndoBuffer.Add(new TileAreaAction(sX, sY, CurrentLevel.GetData(sX, sY, usedRectangle.Width, usedRectangle.Height)));

            for (int j = 0; j < usedRectangle.Height; j++)
            {
                for (int i = 0; i < usedRectangle.Width; i++)
                {
                    CurrentLevel.SetTile(usedRectangle.X + i, usedRectangle.Y + j, TileBuffer[i % TileBuffer.GetLength(0), j % TileBuffer.GetLength(1)]);
                }
            }
            LvlView.DelayDrawing = false;
            LvlView.UpdateArea(usedRectangle);
        }


        public EditMode EditMode { get; set; }

        public Bitmap GetLevelBitmap()
        {
            Bitmap bitmap = new Bitmap(PnlLengthControl.Bounds.Width, PnlLengthControl.Bounds.Height);
            PnlLengthControl.DrawToBitmap(bitmap, new Rectangle(0, 0, PnlLengthControl.Width, PnlLengthControl.Height));
            return bitmap;
        }

        private void TsbSave_Click(object sender, EventArgs e)
        {
            Save();
            MessageBox.Show("Level succesfully saved.");
        }

        private void Save()
        {
            CurrentLevel.MiscByte1 = txtMisc1.Text.ToIntFromHex();
            CurrentLevel.MiscByte2 = txtMisc2.Text.ToIntFromHex();
            CurrentLevel.MiscByte3 = txtMisc3.Text.ToIntFromHex();
            CurrentLevel.Settings.DrawMode = TileDrawMode;
            CurrentLevel.Settings.ShowGrid = TsbGrid.Checked;
            CurrentLevel.Settings.ShowStart = TsbStartPoint.Checked;
            CurrentLevel.Settings.EditMode = EditMode;
            CurrentLevel.Settings.BlockProperties = TsbSolidity.Checked;
            CurrentLevel.Settings.SpecialSprites = TsbSriteSpecials.Checked;
            CurrentLevel.Settings.ShowPointers = TsbPointers.Checked;
            CurrentLevel.AnimationType = CmbAnim.SelectedIndex;

            CurrentLevel.ClearValue = (int)NumBackground.Value;
            CurrentLevel.GraphicsBank = CmbGraphics.SelectedIndex;
            CurrentLevel.Palette = CmbPalettes.SelectedIndex;
            CurrentLevel.Time = (int)NumTime.Value;
            CurrentLevel.Type = CmbTypes.SelectedIndex + 1;
            CurrentLevel.Music = CmbMusic.SelectedIndex;
            CurrentLevel.ScrollType = CmbScroll.SelectedIndex;
            CurrentLevel.PaletteEffect = CmbPaletteEffect.SelectedIndex;
            CurrentLevel.InvincibleEnemies = ChkInvincibleEnemies.Checked;
            CurrentLevel.SpecialLevelType = CmbSpecialType.SelectedIndex;
            CurrentLevel.Save();
        }

        #region pointers
        private bool _PlacingPointer;
        private void BtnAddPointer_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.PointerPlacementHelper);
            TabEditSelector.Enabled = PnlInfo.Enabled = false;
            _PlacingPointer = true;
        }

        private void BtnDeletePointer_Click(object sender, EventArgs e)
        {
            DeleteCurrentPointer();
        }

        private void DeleteCurrentPointer()
        {
            if (CurrentPointer != null)
            {
                LvlView.DelayDrawing = true;
                CurrentLevel.Pointers.Remove(CurrentPointer);
                LvlView.UpdatePoint(CurrentPointer.XEnter, CurrentPointer.YEnter);
                LvlView.UpdatePoint(CurrentPointer.XEnter, CurrentPointer.YEnter + 1);
                LvlView.UpdatePoint(CurrentPointer.XEnter + 1, CurrentPointer.YEnter);
                LvlView.UpdatePoint(CurrentPointer.XEnter + 1, CurrentPointer.YEnter + 1);
                LvlView.DelayDrawing = false;
                LvlView.ClearSelection();
                PntEditor.CurrentPointer = null;
                BtnDeletePointer.Enabled = false;
                BtnAddPointer.Enabled = true;
            }
        }
        #endregion

        private void BlsSelector_DoubleClick(object sender, EventArgs e)
        {
            if (previousLeftIndex == LeftMouseTile)
            {
                ReubenController.OpenBlockEditor(CurrentLevel.Type, LeftMouseTile, CmbGraphics.SelectedIndex, CurrentLevel.AnimationBank, CmbPalettes.SelectedIndex);
            }
        }


        int PreviousSelectorX, PreviousSelectorY;
        private void BlsSelector_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            int index = (e.X / 16) + ((e.Y / 16) * 16);
            if (index > 255)
                return;
            if (PreviousSelectorX == x && PreviousSelectorY == y)
                return;
            PreviousSelectorX = x;
            PreviousSelectorY = y;
            int tile = BlsSelector.BlockLayout.Layout[index];
            if (tile > 0)
            {
                LblSelectorHover.Text = "Block: " + tile.ToHexString();
                LevelToolTip.SetToolTip(BlsSelector, ProjectController.BlockManager.GetBlockString(CurrentLevel.Type, tile));
            }
            else
            {
                LblSelectorHover.Text = "Block: None";
                LevelToolTip.SetToolTip(BlsSelector, "No block.");
            }
        }

        public int DrawingTile
        {
            get
            {
                if (MouseButtons == MouseButtons.Middle)
                    return (int)NumBackground.Value;

                if (TileDrawMode != TileDrawMode.Selection)
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
            LvlView.UpdateGuide(Orientation.Horizontal, 1);
            LvlView.UpdateGuide(Orientation.Horizontal, 2);
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

        private void TsbProperties_CheckStateChanged(object sender, EventArgs e)
        {
            BlsSelector.ShowBlockSolidity = LvlView.ShowBlockSolidity = TsbSolidity.Checked;
        }

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProjectController.Save();
            ProjectController.PaletteManager.PaletteAdded -= PaletteManager_PaletteAdded;
            ProjectController.PaletteManager.PaletteRemoved -= PaletteManager_PaletteRemoved;
            ProjectController.PaletteManager.PalettesSaved -= PaletteManager_PalettesSaved;
            ProjectController.BlockManager.DefinitionsSaved -= BlockManager_DefinitionsSaved;
            ProjectController.LayoutManager.LayoutAdded -= LayoutManager_LayoutAdded;
            ProjectController.GraphicsManager.GraphicsUpdated -= GraphicsManager_GraphicsUpdated;
            ProjectController.LayoutManager.LayoutRemoved -= LayoutManager_LayoutRemoved;
            PnlVerticalGuide.Guide1Changed -= PnlVerticalGuide_Guide1Changed;
            PnlVerticalGuide.Guide2Changed -= PnlVerticalGuide_Guide2Changed;
            PnlHorizontalGuide.Guide1Changed -= PnlHorizontalGuide_Guide1Changed;
            PnlHorizontalGuide.Guide2Changed -= PnlHorizontalGuide_Guide2Changed;
            ReubenController.GraphicsReloaded -= ReubenController_GraphicsReloaded;
            ReubenController.LevelReloaded -= ReubenController_LevelReloaded;
            BlsSelector.SelectionChanged -= BlsSelector_SelectionChanged;
            CurrentPalette.PaletteChanged -= CurrentPalette_PaletteChanged;
            foreach (SpriteViewer sv in SpriteViewers)
            {
                sv.SelectionChanged -= spViewer_SelectionChanged;
            }
        }

        private List<IUndoableAction> UndoBuffer;
        private List<IUndoableAction> RedoBuffer;
        private MultiTileAction CurrentMultiTile;
        private void Undo()
        {
            if (UndoBuffer.Count == 0)
                return;
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
            LvlView.DelayDrawing = true;
            Rectangle usedRectangle = new Rectangle(action.X, action.Y, action.Data.GetLength(0), action.Data.GetLength(1));

            int sX = usedRectangle.X;
            int sY = usedRectangle.Y;
            RedoBuffer.Add(action);

            for (int j = 0; j < usedRectangle.Height; j++)
            {
                for (int i = 0; i < usedRectangle.Width; i++)
                {
                    CurrentLevel.SetTile(usedRectangle.X + i, usedRectangle.Y + j, action.Data[i, j]);
                }
            }
            LvlView.DelayDrawing = false;
            LvlView.UpdateArea(usedRectangle);
            UndoBuffer.Remove(action);
        }

        private void UndoMultiTile(MultiTileAction action)
        {
            RedoBuffer.Add(action);
            UndoBuffer.Remove(action);

            LvlView.DelayDrawing = true;
            foreach (SingleTileChange stc in action.TileChanges.Reverse<SingleTileChange>())
            {
                CurrentLevel.SetTile(stc.X, stc.Y, (byte)stc.Tile);
            }
            LvlView.DelayDrawing = false;
            LvlView.UpdateArea(action.InvalidArea);
        }

        private void changeGuideColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.Color = CurrentLevel.Settings.VGuideColor;
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentLevel.Settings.VGuideColor = PnlVerticalGuide.GuideColor = cDialog.Color;
            }
        }

        private void changeGuideColorToolStripMenuFromValue_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            cDialog.Color = CurrentLevel.Settings.HGuideColor;
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentLevel.Settings.HGuideColor = PnlHorizontalGuide.GuideColor = cDialog.Color;
            }
        }

        private void NumSpecials_ValueChanged(object sender, EventArgs e)
        {
            LvlView.SpecialTransparency = CurrentLevel.Settings.ItemTransparency = (double)NumSpecials.Value;
            LvlView.FullUpdate();
        }

        private void TsbPointers_Click(object sender, EventArgs e)
        {
            LvlView.ShowPointers = TsbPointers.Checked;
        }

        private void TsbCut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void TsbCopy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void TsbPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void TsbDelete_Click(object sender, EventArgs e)
        {
            DeleteTiles();
        }

        private string PreviousHelperText;
        private string CurrentHelperText;

        private void SetHelpText(string text)
        {
            if (text == CurrentHelperText)
                return;
            PreviousHelperText = CurrentHelperText;
            CurrentHelperText = text;
        }

        private void tabPage1_MouseMove(object sender, MouseEventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.TileModeHelper);
        }

        private void tabPage3_MouseMove(object sender, MouseEventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.PointerHelper);
        }

        private void tabPage2_MouseMove(object sender, MouseEventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.SpriteModeHelper);
        }

        private void SetTileModeText()
        {
            switch (TileDrawMode)
            {
                case TileDrawMode.Fill:
                    SetHelpText(Reuben.Properties.Resources.BucketModeHelper);
                    break;

                case TileDrawMode.Line:
                    SetHelpText(Reuben.Properties.Resources.LineModeHelper);
                    break;

                case TileDrawMode.Outline:
                    SetHelpText(Reuben.Properties.Resources.OutlineModeHelper);
                    break;

                case TileDrawMode.Pencil:
                    SetHelpText(Reuben.Properties.Resources.PencilModeHelper);
                    break;

                case TileDrawMode.Rectangle:
                    SetHelpText(Reuben.Properties.Resources.RectangleModeHelper);
                    break;

                case TileDrawMode.Selection:
                    SetHelpText(Reuben.Properties.Resources.RightClickSelectHelper);
                    break;
            }
        }

        private void BtnSetAltPoint_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.StartPlacementHelper);
            TlsTileCommands.Enabled = TlsDrawing.Enabled = TabLevelInfo.Enabled = false;
        }

        private void TsbReplace_Click(object sender, EventArgs e)
        {
            SetHelpText(Reuben.Properties.Resources.ReplaceTileHelper);
            TileDrawMode = TileDrawMode.Replace;
            TsbReplace.Checked = true;
            TsbBucket.Checked = TsbLine.Checked = TsbOutline.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
        }

        private void hMirrorButton_Click(object sender, EventArgs e)
        {
            byte[,] tempBuffer = CurrentLevel.GetData(LvlView.SelectionRectangle.X, LvlView.SelectionRectangle.Y, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height);
            int x = tempBuffer.GetLength(0), y = tempBuffer.GetLength(1);
            byte[,] backUpBuffer = TileBuffer;
            TileBuffer = new byte[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    TileBuffer[i, j] = tempBuffer[x - i - 1, j];
                }
            }

            Paste();
            TileBuffer = backUpBuffer;
        }

        private void vMirrorButton_Click(object sender, EventArgs e)
        {
            byte[,] tempBuffer = CurrentLevel.GetData(LvlView.SelectionRectangle.X, LvlView.SelectionRectangle.Y, LvlView.SelectionRectangle.Width, LvlView.SelectionRectangle.Height);
            int x = tempBuffer.GetLength(0), y = tempBuffer.GetLength(1);
            byte[,] backUpBuffer = TileBuffer;
            TileBuffer = new byte[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    TileBuffer[i, j] = tempBuffer[i, y - j - 1];
                }
            }
            Paste();
            TileBuffer = backUpBuffer;
        }

        private void TsbInteractions_CheckedChanged(object sender, EventArgs e)
        {
            BlsSelector.ShowTileInteractions = LvlView.ShowTileInteractions = TsbInteractions.Checked;
        }

        private void LevelEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Modifiers & Keys.Control) > Keys.None)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        LvlView.SelectionRectangle = new Rectangle(0, 0, CurrentLevel.Length * 16, 0x1B);
                        break;

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
                        MessageBox.Show("Level succesfully saved.");
                        break;


                    case Keys.X:
                        Cut();
                        break;

                    case Keys.C:
                        Copy();
                        break;

                    case Keys.V:
                        Paste();
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
                            CurrentLevel.SpriteData.Remove(CurrentSprite);
                            LvlView.DelayDrawing = true;
                            LvlView.ClearSelection();
                            LvlView.DelayDrawing = false;
                            LvlView.UpdateSprites();
                            CurrentSprite = null;
                        }
                        else if (EditMode == EditMode.Tiles && TileDrawMode == TileDrawMode.Selection)
                        {
                            DeleteTiles();
                        }
                        else if (EditMode == EditMode.Pointers)
                        {
                            DeleteCurrentPointer();
                        }
                        break;

                    case Keys.F11:
                        if (EditMode == EditMode.Sprites)
                        {
                            var ordered = CurrentLevel.SpriteData.OrderBy(p => p.X * 32 + p.Y).ToList();
                            var index = ordered.IndexOf(CurrentSprite);
                            if (index > 0)
                            {
                                CurrentSprite = ordered[index - 1];
                                LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                                ContinueDragging = true;
                                LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                            }


                            if (CurrentSprite != null)
                            {
                                if (CurrentSprite.X * 16 < PnlView.HorizontalScroll.Value)
                                {
                                    PnlView.HorizontalScroll.Value = CurrentSprite.X * 16 + CurrentSprite.Width - 100;
                                }
                                else if (CurrentSprite.X * 16 >= PnlView.HorizontalScroll.Value + PnlView.Width)
                                {
                                    PnlView.HorizontalScroll.Value = (CurrentSprite.X * 16 - (PnlView.Width)) + 100;
                                }
                            }
                        }
                        break;

                    case Keys.F12:
                        if (EditMode == EditMode.Sprites)
                        {
                            var ordered = CurrentLevel.SpriteData.OrderBy(p => p.X * 32 + p.Y).ToList();
                            var index = ordered.IndexOf(CurrentSprite);
                            if (index < ordered.Count - 1)
                            {
                                CurrentSprite = ordered[index + 1];
                                LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                                ContinueDragging = true;
                                LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name;
                            }

                            if (CurrentSprite != null)
                            {
                                if (CurrentSprite.X * 16 < PnlView.HorizontalScroll.Value)
                                {
                                    PnlView.HorizontalScroll.Value = CurrentSprite.X * 16 + CurrentSprite.Width - 100;
                                }
                                else if (CurrentSprite.X * 16 >= PnlView.HorizontalScroll.Value + PnlView.Width)
                                {
                                    PnlView.HorizontalScroll.Value = (CurrentSprite.X * 16 - (PnlView.Width)) + 100;
                                }
                            }
                        }
                        break;

                    case Keys.Escape:
                        ContinueDrawing = false;
                        LvlView.ClearSelection();
                        LvlView.ClearLine();
                        if (TileDrawMode == TileDrawMode.Selection)
                            TileDrawMode = PreviousMode;
                        break;

                    case Keys.F2:
                        TsbStartPoint.Checked = !TsbStartPoint.Checked;
                        break;

                    case Keys.F3:
                        TsbGrid.Checked = !TsbGrid.Checked;
                        break;

                    case Keys.F4:
                        TsbItems.Checked = !TsbItems.Checked;
                        break;

                    case Keys.F5:
                        TsbSolidity.Checked = !TsbSolidity.Checked;
                        break;

                    case Keys.F6:
                        TsbInteractions.Checked = !TsbInteractions.Checked;
                        break;

                    case Keys.F7:
                        TsbSriteSpecials.Checked = !TsbSriteSpecials.Checked;
                        break;

                    case Keys.F8:
                        TsbPointers.Checked = !TsbPointers.Checked;
                        break;
                    case Keys.D1:
                        TsbPencil_Click(null, null);
                        break;

                    case Keys.D2:
                        TsbLine_Click(null, null);
                        break;

                    case Keys.D3:
                        TsbRectangle_Click(null, null);
                        break;

                    case Keys.D4:
                        TsbOutline_Click(null, null);
                        break;

                    case Keys.D5:
                        TsbBucket_Click(null, null);
                        break;

                    case Keys.D6:
                        TsbReplace_Click(null, null);
                        break;

                    case Keys.Q:
                        TabEditSelector.SelectedIndex = 0;
                        break;

                    case Keys.W:
                        TabEditSelector.SelectedIndex = 1;
                        break;

                    case Keys.E:
                        TabEditSelector.SelectedIndex = 2;
                        break;
                }
            }
        }

        private void CmbAnim_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReubenController_GraphicsReloaded(null, null);
        }

        private bool modifySpriteVisiblity = false;
        private void CmbSpriteVis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modifySpriteVisiblity && CurrentSprite != null)
            {
                CurrentSprite.Property = CmbSpriteProperty.SelectedIndex;

                SpriteDefinition sp = ProjectController.SpriteManager.GetDefinition(CurrentSprite.InGameID);

                int rectX = CurrentSprite.X * 16 + sp.MaxLeftX;
                int rectY = CurrentSprite.Y * 16 + sp.MaxTopY;
                int width = (rectX + sp.MaxRightX) - rectX;
                int height = (rectY + sp.MaxBottomY) - rectY;
                Rectangle r = new Rectangle(rectX, rectY, width, height);
                LvlView.DelayDrawing = true;
                LvlView.SelectionRectangle = new Rectangle(CurrentSprite.X, CurrentSprite.Y, CurrentSprite.Width, CurrentSprite.Height);
                LvlView.DelayDrawing = false;
                LvlView.UpdateSprites(r);
            }
        }

        private void ChkProjectileSpins_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLevel.ProjectileBlocksTemporary = ChkProjectileSpins.Checked;
        }

        private void ChkRhythm_CheckedChanged(object sender, EventArgs e)
        {
            CurrentLevel.RhythmPlatforms = ChkRhythm.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (LevelInfo info in ProjectController.LevelManager.Levels)
            {
                Level l = new Level();
                l.Load(info);
                if (l.SpriteData.Contains(CurrentSprite))
                {
                    builder.AppendLine(info.Name);
                }
            }

            MessageBox.Show(builder.ToString());
        }
    }
}
