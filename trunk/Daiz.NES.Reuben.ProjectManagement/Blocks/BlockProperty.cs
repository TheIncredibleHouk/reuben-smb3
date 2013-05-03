using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public enum BlockProperty
    {
        MaskHi = 0xF0,
        MaskSpecialTile = 0xF0,
        MaskPowerup = 0x7F,
        MaskForeground = 0xEF,
        MaskWater = 0xDF,
        Foreground = 0x10,
        Water = 0x20,
        WaterForeground = 0x30,
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
        Coin = 0x0C,
        Door = 0x0D,
        PSwitch = 0x0E,
        Cherry = 0x0F,
        CoinBlock = 0xF0,
        FireFlower = 0xF1,
        SuperLeaf = 0xF2,
        IceFlower = 0xF3,
        FrogSuit = 0xF4,
        FireFoxSuit = 0xF5,
        KoopaSuit = 0xF6,
        BooSuit = 0xF7,
        SledgeSuit = 0xF8,
        NinjaSuit = 0xF9,
        Starman = 0xFA,
        Vine = 0xFB,
        PSwitchBlock = 0xFC,
        Brick = 0xFD,
        Spinner = 0xFE,
        Unused2 = 0xFF,
        Background = 0x00
    }

    public static class BlockPropertyExtensions
    {
        public static string GetString(this BlockProperty bp)
        {
            string s = (bp & BlockProperty.MaskHi).ToString();

            if ((bp & BlockProperty.MaskHi) == BlockProperty.MaskHi)
            {
                s += ", " + bp;
            }
            else
            {
                if ((bp & BlockProperty.Cherry) != BlockProperty.Background)
                {
                    s += ", " + (bp & BlockProperty.Cherry);
                }
            }

            return s;

        }
    }
}
