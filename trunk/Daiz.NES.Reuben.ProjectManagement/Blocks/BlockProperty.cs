using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    [Flags]
    public enum BlockProperty
    {
        Solid = 0x40,
        Water = 0x20,
        Foreground = 0x10,
        SolidTopOnly = 0xC0,
        SolidBottomOnly = 0x90,
        SlopeBottomLeft30 = 0x81,
        SlopeTopLeft30 = 0x82,
        SlopeBottomRight30 = 0x83,
        SlopeTopRight30 = 0x84,
        SlopeLeft45 = 0x85,
        SlopeRight45 = 0x86,
        SlopeFiller = 0x87,
        SlopeTop = 0x88,
        SlopeCeilTopLeft30 = 0x89,
        SlopeCeilBottomLeft30 = 0x8A,
        SlopeCeilTopRight30 = 0x8B,
        SlopeCeilBottomRight30 = 0x8C,
        SlopeCeilLeft45 = 0x8D,
        SlopeCeilRight45 = 0x8E,
        SlopeCeiling = 0x8F,
        Harmful = 0x01,
        Slick = 0x02,
        ConveyorLeft = 0x03,
        ConveyorRight = 0x04,
        Unstable = 0x05,
        VerticalPipeLeft = 0x06,
        VerticalPipeRight = 0x07,
        HighGravity = 0x08,
        Climbable = 0x09,
        MaskLow = 0x0F,
        Alternative = 0x80,
        MaskHi = 0xF0,
        MaskAlternative = 0x7F,
        MaskSolid = 0xBF,
        MaskWater = 0XDF,
        MaskForeground = 0xEF,
        Background = 0x00
    }
}
