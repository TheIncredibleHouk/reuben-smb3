using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class DataPiece
    {
        public long Address { get; set; }
        public byte[] Data { get; private set; }

        public DataPiece(long address, int size)
        {
            Address = address;
            Data = new byte[size];
        }
    }
}
