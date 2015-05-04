using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Reuben.UI.ProjectManagement
{
    public class ColorManager
    {
        public Color[] Colors { get; private set; }

        public ColorManager()
        {
            Default();
        }

        public void Default()
        {
            Colors = new Color[0x41];
            LoadDefaultColor();
        }

        public void LoadDefaultColor()
        {
            //byte[] data = Resource.default_palette;
            //for (var i = 0; i < 0x040; i++)
            //{
            //    Colors[i] = Color.FromArgb(data[i * 0x03], data[i * 0x03 + 1], data[i * 0x03 + 2]);
            //}
            //Colors[0x40] = Color.Empty;
        }

        public bool LoadColorInfo(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    FileStream fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[0x03 * 0x40];
                    fStream.Read(data, 0, 0x03 * 0x40);
                    fStream.Close();
                    for (var i = 0; i < 0x040; i++)
                    {
                        Colors[i] = Color.FromArgb(data[i * 0x03], data[i * 0x03 + 1], data[i * 0x03 + 2]);
                    }
                }

                catch
                {
                    return false;
                }
            }

            return false;
        }
    }
}
