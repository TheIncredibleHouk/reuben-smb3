﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using Reuben.Model;
using Reuben.Controllers;
using Reuben.NESGraphics;
namespace Reuben.UI
{
    public unsafe static class Drawer
    {

        public unsafe static void DrawTileNoAlpha(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 3) + (x * 3));
                    Color c = reference[tile.Pixels[col, row]];

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                }
            }
        }

        public unsafe static void DrawTileAlpha(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 4) + (x * 4));
                    Color c = reference[tile.Pixels[col, row]];

                    if (c == Color.Transparent)
                    {
                        continue;
                    }

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                    *(dataPointer + offset + 3) = 255;
                }
            }
        }

        public unsafe static void DrawTileVerticalFlipAlpha(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 4) + (x * 4));
                    Color c = reference[tile.Pixels[col, 7 - row]];

                    if (c == Color.Transparent)
                    {
                        continue;
                    }

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                    *(dataPointer + offset + 3) = 255;
                }
            }
        }

        public unsafe static void DrawTileHorizontalFlipAlpha(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 4) + (x * 4));
                    Color c = reference[tile.Pixels[7 - col, row]];

                    if (c == Color.Transparent)
                    {
                        continue;
                    }

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                    *(dataPointer + offset + 3) = 255;
                }
            }
        }

        public unsafe static void DrawTileHorizontalVerticalFlipAlpha(Tile tile, int x, int y, Color[] reference, BitmapData bitmap)
        {
            byte* dataPointer = (byte*)bitmap.Scan0;

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    long offset = (bitmap.Stride * y + (row * bitmap.Stride)) + ((col * 4) + (x * 4));
                    Color c = reference[tile.Pixels[7 - col, 7 - row]];

                    if (c == Color.Transparent)
                    {
                        continue;
                    }

                    *(dataPointer + offset) = c.B;
                    *(dataPointer + offset + 1) = c.G;
                    *(dataPointer + offset + 2) = c.R;
                    *(dataPointer + offset + 3) = 255;
                }
            }
        }
    }
}