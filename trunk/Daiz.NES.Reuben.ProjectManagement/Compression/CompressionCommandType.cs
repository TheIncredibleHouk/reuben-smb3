﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public enum CompressionCommandType
    {
        RepeatTile = 0x00,
        SkipTile = 0x40,
        RepeatPattern = 0x80,
        WriteRaw = 0xC0,
    }
}
