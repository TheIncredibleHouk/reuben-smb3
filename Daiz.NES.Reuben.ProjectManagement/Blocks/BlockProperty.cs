using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public enum BlockProperty
    {
        MaskHi = 0xF0,
        MaskLo = 0xF,
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
        Key = 0xFF,
        Background = 0x00
    }

    public static class BlockPropertyExtensions
    {
        public static string GetString(this BlockProperty bp)
        {
            string s1 = ((BlockProperty.MaskHi) & bp).ToString();
            string s2 = "No Interaction";
            switch (bp & BlockProperty.MaskHi)
            {
                case BlockProperty.Background:
                case BlockProperty.Foreground:
                case BlockProperty.Water:
                case BlockProperty.WaterForeground:
                    switch ((int)(bp & BlockProperty.MaskLo))
                    {
                        case 1:
                            s2 = "Harmful";
                            break;

                        case 2:
                            s2 = "Deplete Air";
                            break;

                        case 3:
                            s2 = "Current Left";
                            break;

                        case 4:
                            s2 = "Current Right";
                            break;

                        case 5:
                            s2 = "Current Up";
                            break;

                        case 6:
                            s2 = "Current Down";
                            break;

                        case 7:
                            s2 = "Treasure";
                            break;

                        case 8:
                            s2 = "Locked Door";
                            break;

                        case 9:
                        case 10:
                        case 15:
                            
                            break;

                        case 11:
                            s2 = "Climbable";
                            
                            break;

                        case 12:
                            s2 = "Coin";
                            
                            break;

                        case 13:
                            s2 = "Door";
                            break;

                        case 14:
                            s2 = "Cherry";
                            break;
                    }
                    break;

                case BlockProperty.SolidTop:
                case BlockProperty.SolidBottom:
                case BlockProperty.SolidAll:
                    switch ((int)(bp & BlockProperty.MaskLo))
                    {
                        case 1:
                            s2 = "Harmful";
                            break;

                        case 2:
                            s2 = "Slick";
                            break;

                        case 3:
                            s2 = "Conveyor Left";
                            break;

                        case 4:
                            s2 = "Conveyor Right";
                            break;

                        case 5:
                            s2 = "Conveyor Up";
                            break;

                        case 6:
                            s2 = "Conveyor Down";
                            break;

                        case 7:
                            s2 = "Unstable";
                            break;

                        case 8:
                            s2 = "Vertical Enterable Pipe Left End";
                            break;

                        case 9:
                            s2 = "Vertical Enterable Pipe Right End";
                            break;

                        case 10:
                            s2 = "Horizontal Enterable Pipe Bottom";
                            break;

                        case 11:
                            s2 = "Climbable";
                            break;

                        case 12:
                        case 13:
                        case 15:
                            s2 = "Unused";
                            break;

                        case 14:
                            s2 = "P-Switch";
                            break;

                    }
                    break;

                default:
                    s2 = bp.ToString();
                    break;
            }

            return s1 + ", " + s2;

        }
    }
}
