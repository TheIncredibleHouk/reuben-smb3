using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class CompressionCommand
    {
        public CompressionCommandType CommandType { get; set; }

        public List<byte> Data { get; set; }

        public int RepeatTimes;

        public CompressionCommand()
        {
            Data = new List<byte>();
        }

        public byte[] GetData()
        {
            byte[] data = null;
            int i = 1;
            switch (CommandType)
            {
                case CompressionCommandType.SkipTile:
                     
                    if (RepeatTimes > 0x40)
                    {
                        throw new OverflowException("Repleat length too large for command");
                    }
                    
                    if (Data.Count > RepeatTimes)
                    {
                        throw new OverflowException("Data length too large for command.");
                    }

                    data = new byte[1];
                    data[0] = (byte)((int)CommandType | (RepeatTimes - 1));
                    return data;

                case CompressionCommandType.RepeatTile:
                     
                    if (RepeatTimes > 0x40)
                    {
                        throw new OverflowException("Repleat length too large for command");
                    }
                    
                    if (Data.Count > RepeatTimes)
                    {
                        throw new OverflowException("Data length too large for command.");
                    }

                    data = new byte[2];
                    data[0] = (byte)((int)CommandType | (RepeatTimes - 1));
                    data[1] = (byte)(Data[0]);
                    return data;

                case CompressionCommandType.RepeatPattern:
                    if (Data.Count > 0xFF)
                    {
                        throw new OverflowException("Data length too large for pattern.");

                    }

                    if (RepeatTimes >= 0x40)
                    {
                        throw new OverflowException("Repeat length too large for pattern.");
                    }
                    if (RepeatTimes < 1)
                    {
                        throw new ArgumentException("Repeat times too small. Repeat should occur at least twice.");
                    }

                    data = new byte[2 + Data.Count];
                    data[0] = (byte)((int)CommandType | RepeatTimes);
                    data[1] = (byte)Data.Count;
                    i = 2;
                    break;

                case CompressionCommandType.WriteRaw:
                    if (Data.Count > 0x40)
                    {
                        throw new OverflowException("Data too long for raw data write.");
                    }
                    else if (Data.Count == 0)
                    {
                        throw new ArgumentException("Data too small, write raw should never write 0 bytes.");
                    }
                    data = new byte[1 + Data.Count];
                    data[0] = (byte)((int)CommandType | Data.Count);
                    break;
            }

            
            foreach (byte b in Data)
            {
                data[i++] = b;
            }

            return data;
        }
    }
}
