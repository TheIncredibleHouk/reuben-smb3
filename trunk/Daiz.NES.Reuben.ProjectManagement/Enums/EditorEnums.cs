using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public enum TileDrawMode
    {
        Pencil,
        Line,
        Fill,
        Rectangle,
        Outline,
        Scatter,
        Selection,
        Replace
    }

    public enum EditMode
    {
        Tiles,
        Sprites,
        Pointers,
        Scrolling
    }

    public enum MouseMode
    {
        RightClickSelection,
        RightClickTile
    }
}
