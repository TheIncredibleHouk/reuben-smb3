using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reuben.UI.ProjectManagement
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
        Pointers
    }

    public enum MouseMode
    {
        RightClickSelection,
        RightClickTile
    }
}
