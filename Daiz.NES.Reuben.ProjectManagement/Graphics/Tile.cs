using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class Tile
    {
        public event EventHandler PixelsChanged;
        private byte[,] _Pixels;

        public Tile(byte[] Data)
        {
            _Pixels = new byte[8, 8];
            byte LeftBit, RightBit;

            BitArray LeftBitPlane = new BitArray(new byte[] { Data[0], Data[1], Data[2], Data[3], Data[4], Data[5], Data[6], Data[7] });
            BitArray RightBitPlane = new BitArray(new byte[] { Data[8], Data[9], Data[10], Data[11], Data[12], Data[13], Data[14], Data[15] });

            for (int j = 0; j < 8; j++)
            {
                for (int k = 0; k < 8; k++)
                {
                    RightBit = (byte)(LeftBitPlane[(j * 8) + k] ? 0x01 : 0x00);
                    LeftBit = (byte)(RightBitPlane[(j * 8) + k] ? 0x01 : 0x00);

                    _Pixels[7 - k, j] = (byte)((LeftBit << 1) | RightBit);
                }
            }
        }

        public byte this[int x, int y]
        {
            get { return _Pixels[x, y]; }
            set
            {
                _Pixels[x, y] = value;
                if (PixelsChanged != null) PixelsChanged(this, null);
            }
        }

        public byte[] GetInterpolatedData()
        {
            byte[] returnData = new byte[16];
            byte LeftBit, RightBit;
            for (int i = 0; i < 8; i++)
            {
                bool first = true;
                for (int j = 0; j < 8; j++)
                {
                    if (!first)
                    {
                        returnData[i] = (byte)(returnData[i] << 1);
                        returnData[i + 8] = (byte)(returnData[i + 8] << 1);
                    }
                    first = false;

                    LeftBit = (byte)((_Pixels[j, i] & 0x02) > 0x00 ? 0x01 : 0x00);
                    RightBit = (byte)((_Pixels[j, i] & 0x01) > 0x00 ? 0x01 : 0x00);
                    returnData[i] |= RightBit;
                    returnData[i + 8] |= LeftBit;
                }
            }

            return returnData;
        }
    }
}
