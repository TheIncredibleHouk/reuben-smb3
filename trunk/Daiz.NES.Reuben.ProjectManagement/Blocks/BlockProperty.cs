using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    [Flags]
    public enum BlockProperty
    {
        SolidTop    = 0x80,
        Solid       = 0x40,
        Water	    = 0x20,
        Foreground	= 0x10,
        Harmful		        = 0x01,
        Slick		        = 0x02,
        ConveyorLeft        = 0x03,
        ConveyorRight	    = 0x04,
        VerticalPipe  	    = 0x05,
        Unstable     	    = 0x06,
        Waterfall		    = 0x07,
        Climbable           = 0x08,
        SlopeBottomLeft30	= 0x09,
        SlopeTopLeft30      = 0x0A,
        SlopeBottomRight30  = 0x0B,
        SlopeTopRight30     = 0x0C,
        SlopeLeft45	        = 0x0D,
        SlopeRight45    	= 0x0E,
        SlopeFiller     	= 0x0F,
        MaskHi              = 0xF0,
        MaskLow             = 0x0F,
        Background          = 0x00
    }
}
