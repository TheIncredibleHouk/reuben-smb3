using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public enum BlockProperty
    {
        MaskLow = 0x0F,
        MaskHi = 0xF0,
        Mask78Bits = 0xC0,
        Mask56Bits = 0x30,
        Mask123456Bits = 0x3F,
        Mask123478Bits = 0xCF,
        MaskPowerup = 0x7F,
        MaskForeground = 0xEF,
        MaskWater = 0xDF,
        Foreground = 0x10,
        Water = 0x20,
        SolidTop = 0x40,
        SolidBottom = 0x80,
        SolidAll = 0xC0,
        Harmful = 0x01,
        Slick = 0x02,
        MoveLeft = 0x03,
        MoveRight = 0x04,
        MoveUp = 0x05,
        MoveDown = 0x06,
        Unstable = 0x07,
        VerticalPipeLeft = 0x08,
        VerticalPipeRight = 0x09,
        HorizontalPipeBottom = 0x0A,
        Climbable = 0x0B,
        Unused1 = 0x0C,
        Unused2 = 0x0D,
        Unused3 = 0x0E,
        Unused4 = 0x0F,
        Background = 0x00
    }

    public static class BlockPropertyExtensions
    {
        public static string GetString(this BlockProperty bp)
        {
            string s = "";
            switch (bp & BlockProperty.Mask78Bits)
            {
                case BlockProperty.Background:
                    s += "Not Solid";
                    break;

                case BlockProperty.SolidTop:
                    s += "Solid On Top";
                    break;

                case BlockProperty.SolidBottom:
                    s += "Solid On Bottom";
                    break;

                case BlockProperty.SolidAll:
                    s += "Completely Solid";
                    break;
            }

            switch (bp & BlockProperty.Mask56Bits)
            {
                case BlockProperty.Background:
                    if (s == "Not Solid")
                    {
                        s += ", Background";
                    }
                    break;

                case BlockProperty.Foreground:
                    s += ", Foreground";
                    break;

                case BlockProperty.Water:
                    s += ", Water";
                    break;

                case BlockProperty.Water | BlockProperty.Foreground:
                    s += ", Water Foreground";
                    break;
            }

            if ((bp & BlockProperty.MaskLow) > BlockProperty.Background)
            {
                s += ", " + (bp & BlockProperty.MaskLow).ToString();
            }
            else
            {
                s += ", No Interaction";
            }

            return s;

        }
    }
}
