using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class ROMManager
    {
        private string Filename;
        public byte[] Rom;
        public bool LoadRom(string filename)
        {
            if(!File.Exists(filename)) return false;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Rom = new byte[fs.Length];
            fs.Read(Rom, 0, (int) fs.Length);
            fs.Close();
            Filename = filename;
            return true;
        }

        public bool WriteLevel(Level l, int levelAddress, int spriteAddress)
        {
            int yStart = 0;
            switch (l.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    yStart = l.YStart - 1;
                    break;

                case LevelLayout.Vertical:
                    yStart = l.YStart - 1;
                    break;
            }

            Rom[levelAddress++] = (byte) l.ClearValue;
            Rom[levelAddress++] = (byte) l.GraphicsBank;
            Rom[levelAddress++] = (byte) l.Palette;
            Rom[levelAddress++] = (byte)((l.StartAction << 4) | l.Type);
            Rom[levelAddress++] = (byte)(((l.XStart & 0x0F) << 4) | ((l.XStart & 0xF0) >> 4));
            Rom[levelAddress++] = (byte)(((yStart & 0x0F) << 4) | ((yStart & 0xF0) >> 4));

            if (l.Music < 15)
            {
                Rom[levelAddress++] = (byte) l.Music;
            }
            else
            {
                Rom[levelAddress++] = (byte)((l.Music - 15) << 4);
            }

            Rom[levelAddress++] = (byte)(((l.Time / 100) << 4) | ((l.Time - ((l.Time / 100) * 100)) / 10));

            switch (l.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    Rom[levelAddress++] = (byte)((l.ScrollType << 4) | (l.Length - 1));
                    break;

                case LevelLayout.Vertical:
                    Rom[levelAddress++] = (byte)((0x80) | (l.Length - 1));
                    break;
            }

            Rom[levelAddress++] = (byte)l.Unused1;
            Rom[levelAddress++] = (byte)l.Unused2;
            Rom[levelAddress++] = (byte)l.Unused3;

            foreach (var p in l.Pointers)
            {
            }

            Rom[levelAddress++] = (byte)0xFF;
            byte[] levelData = l.GetCompressedData();
            for (int i = 0; i < levelData.Length; i++)
            {
                Rom[levelAddress++] = levelData[i];
            }

            Rom[levelAddress] = (byte) 0xFF;

            Rom[spriteAddress++] = 0x01;

            switch (l.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    foreach (var s in from sprites in l.SpriteData orderby sprites.X select sprites)
                    {
                        Rom[spriteAddress++] = (byte)s.InGameID;
                        Rom[spriteAddress++] = (byte)s.X;
                        Rom[spriteAddress++] = (byte)s.Y;
                    }
                    break;

                case LevelLayout.Vertical:
                    foreach (var s in from sprites in l.SpriteData orderby sprites.Y select sprites)
                    {
                        Rom[spriteAddress++] = (byte)s.InGameID;
                        Rom[spriteAddress++] = (byte)s.X;
                        Rom[spriteAddress++] = (byte)s.Y;
                    }
                    break;
            }
            Rom[spriteAddress] = 0xFF;
            return true;
        }

        public bool WritePalette(List<PaletteInfo> paletteInfo)
        {
            int address = 0x3C010;
            foreach (var p in paletteInfo)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Rom[address++] = (byte)p[i, j];
                    }
                }
            }

            return true;
        }

        public bool Save()
        {
            FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Write);
            fs.Write(Rom, 0, Rom.Length);
            return true;
        }
    }
}
