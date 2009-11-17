//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Linq;
//using System.Drawing.Imaging;
//using System.Text;
//using System.Windows.Forms;

//using Daiz.NES.Graphics;

//namespace Daiz.NES.Reuben
//{
//    public unsafe class Canvas: Control
//    {

//        public Canvas()
//        {
//        }

//        private int RowCount;
//        private int ColumnCount;

//        private byte[,] TileValueMap;

//        public byte[,] RawData
//        {
//            get { return TileValueMap; }
//        }

                
//        private Bitmap BackBuffer;

//        public void Initialize(int columns, int rows, RawBlock[] blockTable)
//        {
//            TileValueMap = new byte[rows][];
//            for (var i = 0; i < rows; i++)
//            {
//                TileValueMap[i] = new byte[columns];
//            }
//            RowCount = rows;
//            ColumnCount = columns;
//            BackBuffer = new Bitmap(columns * 16, rows * 16, PixelFormat.Format24bppRgb);
//            BlockTable = blockTable;
//        }

//        public void Clear(byte value)
//        {
//            if (TileValueMap == null)
//                throw new Exception("Uninitialized canvas");

//            for (var i = 0; i < RowCount; i++)
//            {
//                for (var j = 0; j < ColumnCount; j++)
//                {
//                    TileValueMap[i][j] = value;
//                }
//            }
//        }

//        public void Draw(int X, int Y, byte value)
//        {
//            TileValueMap[Y][X] = value;
//        }

//        private RawBlock[] BlockTable;

//        private BitmapData BitmapData = null;
//        private byte* DataPointer = null;

//        public void BeginRender()
//        {
//            Rectangle rect = new Rectangle(0, 0, BackBuffer.Width, BackBuffer.Height);
//            BitmapData = BackBuffer.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
//            DataPointer = (byte*) BitmapData.Scan0;
//        }

//        public void FullRender()
//        {
//            BeginRender();
//            for (int i = 0; i < ColumnCount; i++)
//            {
//                for (int j = 0; j < RowCount; j++)
//                {
//                    RenderRawBlock(BlockTable[TileValueMap[j][i]]);
//                }
//            }
//        }

//        public void RenderRawBlock(RawBlock block)
//        {
//            long Offset = 0;
//            for (var i = 0; i < 16; i++)
//            {
//                Offset = BitmapData.Stride * i;
//                byte[] RasterLine = block.RasterLine(i);
//                for (var j = 0; j < 16; j++)
//                {
//                    byte* FinalOffset = DataPointer + Offset;
//                    int LowOffset = j * 16;
//                    *(FinalOffset) = RasterLine[LowOffset];
//                    *(FinalOffset + 1) = RasterLine[LowOffset + 1];
//                    *(FinalOffset + 2) = RasterLine[LowOffset + 2];
//                }
//            }
//        }

//        public void EndRender()
//        {
//            BackBuffer.UnlockBits(BitmapData);
//            DataPointer = null;
//        }
//    }
//}
