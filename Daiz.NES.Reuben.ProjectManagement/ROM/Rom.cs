using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Rom
    {
        private string Filename;
        private byte[] data;
        private bool[] dataProtection;

        public RomWriteProtection ProtectionMode { get; set; }

        public Rom()
        {
        }

        public byte this[int index]
        {
            get { return data[index]; }
            set
            {
                if (dataProtection[index])
                {
                    throw new Exception("Data at address " + index.ToString("X") + " has already been written to!");
                }

                bool canWrite = true;

                switch (ProtectionMode)
                {
                    case RomWriteProtection.LevelData:
                        canWrite = (index >= 0x40450 && index <= 0x7C00F);
                        break;

                    case RomWriteProtection.PaletteData:
                        canWrite = (index >= 0x3C010 && index <= 0x3c80F);
                        break;

                    case RomWriteProtection.TSAData:
                        canWrite = (index >= 0x3C810 && index <= 0x4000F);
                        break;

                    case RomWriteProtection.AnyData:
                        canWrite = true;
                        break;

                    case RomWriteProtection.GraphicsData:
                        canWrite = (index >= 0x80010);
                        break;

                    default:
                        canWrite = true;
                        break;
                }

                //if (!canWrite) throw new ArgumentOutOfRangeException("Cannot write to " + index + " because it is protected with " + ProtectionMode);

                data[index] = value;
                dataProtection[index] = true;
            }
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename)) return false;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            data = new byte[fs.Length];
            dataProtection = new bool[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            Filename = filename;
            AllowWrites();
            return true;
        }

        public bool Save()
        {
            FileStream fs = new FileStream(Filename, FileMode.Open, FileAccess.Write);
            fs.Write(data, 0, data.Length);
            fs.Close();
            AllowWrites();
            return true;
        }

        public void AllowWrites()
        {
            for(var i = 0; i < dataProtection.Length; i++)
            {
                dataProtection[i] = false;
            }
        }

        //public bool IsPatchedRom
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public bool IsClean
        //{
        //    get
        //    {
        //        byte[] guid = new byte[16];
        //        for (int i = 0; i < 16; i++)
        //        {
        //            guid[i] = data[0xFC000 + i];
        //        }

        //        return new Guid(guid) == Guid.Empty;
        //    }
        //}

        //public void Sign(Guid projectGuid)
        //{
        //    byte[] guidArray = projectGuid.ToByteArray();
        //    for (int i = 0; i < 16; i++)
        //    {
        //        data[0xFC000 + i] = guidArray[i];
        //    };
        //}
    }

    public enum RomWriteProtection
    {
        LevelData,
        TSAData,
        GraphicsData,
        PaletteData,
        WorldPointers,
        LevelPointers,
        AnyData
    }
}
