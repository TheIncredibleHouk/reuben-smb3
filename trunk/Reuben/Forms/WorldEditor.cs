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
            CmbMusic.DisplayMember = CmbLayouts.DisplayMember  = CmbPalettes.DisplayMember = CmbGraphics.DisplayMember = "Name";
            foreach (var g in ProjectController.GraphicsManager.GraphicsInfo)
            {
                CmbGraphics.Items.Add(g);
            }

            CmbGraphics.Items.RemoveAt(254);

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

            foreach (var m in ProjectController.MusicManager.MusicList)
            {
                CmbMusic.Items.Add(m);
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

            ReubenController.GraphicsReloaded += new EventHandler(ReubenController_GraphicsReloaded);
            ReubenController.WorldReloaded += new EventHandler<TEventArgs<World>>(ReubenController_WorldReloaded);
            LoadSpriteSelector();

            BlsSelector.CurrentDefiniton = WldView.CurrentDefiniton = ProjectController.BlockManager.GetDefiniton(0);
        }

        void BlsSelector_SelectionChanged(object sender, TEventArgs<MouseButtons> e)
        {
            if (MouseMode == MouseMode.RightClickSelection)
            {
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
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank + 1]);
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

            TsbGrid.Checked = CurrentWorld.Settings.ShowGrid;
            TsbPointers.Checked = CurrentWorld.Settings.ShowPointers;
            switch (CurrentWorld.Settings.DrawMode)
            {
                case TileDrawMode.Pencil:
                    TsbPencil.Checked = true;
                    break;

                case TileDrawMode.Rectangle:
                    TsbRectangle.Checked = true;
                    break;

                case TileDrawMode.Outline:
                    TsbOutline.Checked = true;
                    break;

                case TileDrawMode.Line:
                    TsbLine.Checked = true;
                    break;

                case TileDrawMode.Fill:
                    TsbBucket.Checked = true;
                    break;
            }

            switch(CurrentWorld.Settings.EditMode)
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

            CmbLayouts.SelectedIndex = CurrentWorld.Settings.Layout;

            this.Text = ProjectController.WorldManager.GetWorldInfo(w.Guid).Name;
            this.WindowState = FormWindowState.Maximized;
            this.Show();
        }

        private void GetWorldInfo(World w)
        {
            WldView.CurrentWorld = w;
            CurrentWorld = w;
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[0x14]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[0x14]);
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank + 1]);
            CmbGraphics.SelectedIndex = w.GraphicsBank;
            CmbPalettes.SelectedIndex = w.Palette;
            CmbMusic.SelectedIndex = w.Music >= CmbMusic.Items.Count ? 0 : w.Music;
            CmbLength.SelectedItem = w.Length;
            PntEditor.CurrentPointer = null;
            BtnDeletePointer.Enabled = false;
            TsbPointers.Checked = CurrentWorld.Settings.ShowPointers;
            WldView.DelayDrawing = false;
            WldView.FullUpdate();
        }

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
            CurrentTable.SetGraphicsbank(0, ProjectController.GraphicsManager.GraphicsBanks[0x14]);
            CurrentTable.SetGraphicsbank(1, ProjectController.GraphicsManager.GraphicsBanks[0x15]);
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CurrentWorld.GraphicsBank + 1]);
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
        private TileDrawMode PreviousMode;

        private void WldView_MouseDown(object sender, MouseEventArgs e)
        {
            int x = (e.X / 16) / WldView.Zoom;
            int y = (e.Y / 16) / WldView.Zoom;
            PnlView.Focus();

            if (x < 0 || x >= CurrentWorld.Width || y < 0 || y >= CurrentWorld.Height) return;

            if (_PlacingPointer)
            {
                _PlacingPointer = false;
                CurrentWorld.AddPointer();
                PntEditor.CurrentPointer = CurrentWorld.Pointers[CurrentWorld.Pointers.Count - 1];
                CurrentPointer = PntEditor.CurrentPointer;
                CurrentPointer.X = x;
                CurrentPointer.Y = y;
                PnlDrawing.Enabled = TabLevelInfo.Enabled = true;
                WldView.SelectionRectangle = new Rectangle(x, y, 1, 1);
                WldView.UpdatePoint(x, y);
                PntEditor.UpdatePosition();
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
                            CurrentMultiTile.AddTileChange(x, y, CurrentWorld.LevelData[x, y]);
                            CurrentWorld.SetTile(x, y, (byte)(DrawingTile));
                            ContinueDrawing = true;
                            break;

                        case TileDrawMode.Outline:
                        case TileDrawMode.Rectangle:
                        case TileDrawMode.Selection:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            WldView.SelectionRectangle = new Rectangle(StartX, StartY, 1, 1);
                            break;

                        case TileDrawMode.Line:
                            StartX = x;
                            StartY = y;
                            ContinueDrawing = true;
                            WldView.SelectionLine = new Line(StartX, StartY, StartX, StartY);
                            break;

                        case TileDrawMode.Fill:
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
                    LblSprite.Text = "Current Sprite: " + CurrentSprite.InGameID.ToHexString() + " - " + CurrentSprite.Name + " - Item: " + CurrentSprite.Item;
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
                    Sprite newSprite = new Sprite() { IsMapSprite = true, X = x, Y = y, InGameID = CurrentSelectorSprite.InGameID };
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
        private void WldView_MouseMove(object sender, MouseEventArgs e)
        {
            int x = (e.X / (16 * WldView.Zoom));
            int y = (e.Y / (16 * WldView.Zoom));

            if (x < 0 || x >= CurrentWorld.Width || y < 0 || y >= CurrentWorld.Height) return;

            if (PreviousMouseX == x && PreviousMouseY == y) return;
            PreviousMouseX = x;
            PreviousMouseY = y;

            int XDiff = x - StartX;
            int YDiff = y - StartY;


            LblPositition.Text = "X: " + x.ToHexString() + " Y: " + (y - 0x11).ToHexString();

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
                LevelToolTip.SetToolTip(WldView, ProjectController.BlockManager.GetBlockString(CurrentWorld.Type, CurrentWorld.LevelData[x, y]));
                if (ContinueDrawing && (MouseButtons == MouseButtons.Left || MouseButtons == MouseButtons.Middle || MouseButtons == MouseButtons.Right))
                {
                    switch (TileDrawMode)
                    {
                        case TileDrawMode.Pencil:
                            CurrentMultiTile.AddTileChange(x, y, CurrentWorld.LevelData[x, y]);
                            CurrentWorld.SetTile(x, y, (byte)DrawingTile);
                            break;

                        case TileDrawMode.Outline:
                        case TileDrawMode.Rectangle:
                        case TileDrawMode.Selection:
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

                        case TileDrawMode.Line:
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
                    WldView.UpdatePoint(x, y);
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

        private void WldView_MouseUp(object sender, MouseEventArgs e)
        {
            if (!ContinueDrawing) return;
            int _DrawTile = 0;
            int sX, sY;

            if (e.Button == MouseButtons.Middle)
            {
                _DrawTile = 0;
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
                else if ((WldView.HasSelection || WldView.HasSelectionLine))
                {
                    switch (TileDrawMode)
                    {
                        case TileDrawMode.Rectangle:
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

                        case TileDrawMode.Outline:
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

                        case TileDrawMode.Line:

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

                        case TileDrawMode.Selection:
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

        #endregion

        #region drawing modes
        private TileDrawMode TileDrawMode = TileDrawMode.Pencil;

        private void TsbPencil_Click(object sender, EventArgs e)
        {
            TileDrawMode = TileDrawMode.Pencil;
            TsbPencil.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbRectangle.Checked = false;
        }

        private void TsbRectangle_Click(object sender, EventArgs e)
        {
            TileDrawMode = TileDrawMode.Rectangle;
            TsbRectangle.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
        }

        private void TsbOutline_Click(object sender, EventArgs e)
        {
            TileDrawMode = TileDrawMode.Outline;
            TsbOutline.Checked = true;
            TsbLine.Checked = TsbBucket.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
        }

        private void TsbBucket_Click(object sender, EventArgs e)
        {
            TileDrawMode = TileDrawMode.Fill;
            TsbBucket.Checked = true;
            TsbLine.Checked = TsbOutline.Checked = TsbRectangle.Checked = TsbPencil.Checked = false;
        }

        private void TsbLine_Click(object sender, EventArgs e)
        {
            TileDrawMode = TileDrawMode.Line;
            TsbLine.Checked = true;
            TsbRectangle.Checked = TsbBucket.Checked = TsbOutline.Checked = TsbPencil.Checked = false;
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
            CurrentTable.SetGraphicsbank(2, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex]);
            CurrentTable.SetGraphicsbank(3, ProjectController.GraphicsManager.GraphicsBanks[CmbGraphics.SelectedIndex + 1]);
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

        private bool _SelectingStartPositionMode;
        private bool _PlacingPointer;

        private void BtnStartPoint_Click(object sender, EventArgs e)
        {
            _SelectingStartPositionMode = true;
            TabLevelInfo.Enabled = PnlDrawing.Enabled = false;
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

        private void WldView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {

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
                        else if (EditMode == EditMode.Tiles && TileDrawMode == TileDrawMode.Selection)
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
                        if (TileDrawMode == TileDrawMode.Selection)
                            TileDrawMode = PreviousMode;
                        break;
                }
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

        public EditMode EditMode { get; set; }

        private void BtnLevelSize_Click(object sender, EventArgs e)
        {
            LblSpriteSize.Text = "Sprite Data Size: " + (((CurrentWorld.SpriteData.Count) * 3) + 1).ToString() + " bytes";
            LblLevelSize.Text = "Level Data Size: " + (CurrentWorld.GetCompressedData().Length + 5 + (CurrentWorld.Pointers.Count * 3)).ToString() + " bytes";
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
            ProjectController.Save();
        }

        private void Save()
        {
            CurrentWorld.Settings.DrawMode = TileDrawMode;
            CurrentWorld.Settings.ShowGrid = TsbGrid.Checked;
            CurrentWorld.Settings.EditMode = EditMode;
            CurrentWorld.Settings.ShowPointers = TsbPointers.Checked;

            CurrentWorld.GraphicsBank = CmbGraphics.SelectedIndex;
            CurrentWorld.Palette = CmbPalettes.SelectedIndex;
            CurrentWorld.Music = CmbMusic.SelectedIndex;
            CurrentWorld.Save();
            MessageBox.Show("World succesfully saved.");
        }

        #region pointers
        private void BtnAddPointer_Click(object sender, EventArgs e)
        {
            _PlacingPointer = true;
            if (!TsbPointers.Checked) TsbPointers.Checked = true;
            TabLevelInfo.Enabled = PnlDrawing.Enabled = false;
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
            ReubenController.OpenBlockEditor(CurrentWorld.Type, LeftMouseTile, 0x14, CurrentWorld.GraphicsBank, CmbPalettes.SelectedIndex);
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

                if(TileDrawMode != TileDrawMode.Selection)
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

        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
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

        private void TsbPointers_CheckedChanged(object sender, EventArgs e)
        {
            WldView.ShowPointers = TsbPointers.Checked;
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

        private string PreviousHelperText;
        private string CurrentHelperText;
        private void SetHelpText(string text)
        {
            if (text == CurrentHelperText) return;
            PreviousHelperText = CurrentHelperText;
            CurrentHelperText = text;
            LblHelpText.Text = CurrentHelperText;
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
            Paste(true);
        }

        private void TsbDelete_Click(object sender, EventArgs e)
        {
            DeleteTiles();
        }
    }
}
