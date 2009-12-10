using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Rom
    {
        private string Filename;
        private byte[] data;
        private string fileName;

        public Rom()
        {
        }

        public byte this[int index]
        {
            get { return data[index]; }
            set
            {
                data[index] = value;
            }
        }

        public bool Load(string filename)
        {
            if (!File.Exists(filename)) return false;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();
            Filename = filename;
            return true;
        }

        public bool IsPatchedRom
        {
            get
            {
                return true;
            }
        }

        public bool IsClean
        {
            get
            {
                byte[] guid = new byte[16];
                for (int i = 0; i < 16; i++)
                {
                    guid[i] = Rom[0xFC000 + i];
                }

                return new Guid(guid) == Guid.Empty;
            }
        }

        public void Sign(Guid projectGuid)
        {
            byte[] guidArray = projectGuid.ToByteArray();
            for (int i = 0; i < 16; i++)
            {
                data[0xFC000 + i] = guidArray[i];
            };
        }
    }
}
