using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelSettings : IXmlIO
    {
        public bool ShowGrid { get; set; }
        public bool SpecialTiles { get; set; }
        public bool SpecialSprites { get; set; }
        public bool BlockProperties { get; set; }
        public bool ShowStart { get; set; }
        public bool Zoom { get; set; }
        public TileDrawMode DrawMode { get; set; }
        public EditMode EditMode { get; set; }
        public MouseMode MouseMode { get; set; }
        public int Layout { get; set; }
        public Color VGuideColor { get; set; }
        public Color HGuideColor { get; set; }
        public double ItemTransparency { get; set; }
        public double PropertyTransparency { get; set; }
        public bool ShowPointers { get; set; }

        public LevelSettings()
        {
            ShowGrid = false;
            SpecialTiles = false;
            SpecialSprites = false;
            BlockProperties = false;
            ShowStart = true;
            Zoom = false;
            DrawMode = TileDrawMode.Pencil;
            EditMode = EditMode.Tiles;
            MouseMode = MouseMode.RightClickSelection;
            Layout = 0;
            VGuideColor = Color.Blue;
            HGuideColor = Color.Red;
            ItemTransparency = .75;
            PropertyTransparency = .75;
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("settings");
            x.SetAttributeValue("showgrid", ShowGrid);
            x.SetAttributeValue("specialtiles", SpecialTiles);
            x.SetAttributeValue("specialsprites", SpecialSprites);
            x.SetAttributeValue("blockproperties", BlockProperties);
            x.SetAttributeValue("showstart", ShowStart);
            x.SetAttributeValue("zoom", Zoom);
            x.SetAttributeValue("drawmode", DrawMode);
            x.SetAttributeValue("editmode", EditMode);
            x.SetAttributeValue("mousemode", MouseMode);
            x.SetAttributeValue("layout", Layout);
            x.SetAttributeValue("vguidecolor", string.Format("{0},{1},{2}", VGuideColor.R, VGuideColor.G, VGuideColor.B));
            x.SetAttributeValue("hguidecolor", string.Format("{0},{1},{2}", HGuideColor.R, HGuideColor.G, HGuideColor.B));
            x.SetAttributeValue("itemtransparency", ItemTransparency);
            x.SetAttributeValue("propertytransparency", PropertyTransparency);
            x.SetAttributeValue("showpointers", ShowPointers);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            string[] split = null;

            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "showgrid":
                        ShowGrid = a.Value.ToBoolean();
                        break;

                    case "specialtiles":
                        SpecialTiles = a.Value.ToBoolean();
                        break;
                    case "specialsprites":
                        SpecialSprites = a.Value.ToBoolean();
                        break;

                    case "blockproperties":
                        BlockProperties = a.Value.ToBoolean();
                        break;

                    case "showstart":
                        ShowStart = a.Value.ToBoolean();
                        break;

                    case "zoom":
                        Zoom = a.Value.ToBoolean();
                        break;

                    case "drawmode":
                        DrawMode = (TileDrawMode)Enum.Parse(typeof(TileDrawMode), a.Value, true);
                        break;

                    case "editmode":
                        EditMode = (EditMode)Enum.Parse(typeof(EditMode), a.Value, true);
                        break;

                    case "mousemode":
                        MouseMode = (MouseMode)Enum.Parse(typeof(MouseMode), a.Value, true);
                        break;

                    case "layout":
                        Layout = a.Value.ToInt();
                        break;

                    case "vguidecolor":
                        split = a.Value.Split(',');
                        VGuideColor = Color.FromArgb(split[0].ToIntFromHex(), split[1].ToInt(), split[2].ToInt());
                        break;

                    case "hguidecolor":
                        split = a.Value.Split(',');
                        HGuideColor = Color.FromArgb(split[0].ToInt(), split[1].ToInt(), split[2].ToInt());
                        break;

                    case "itemtransparency":
                        ItemTransparency = a.Value.ToDouble();
                        break;

                    case "propertytransparency":
                        PropertyTransparency = a.Value.ToDouble();
                        break;

                    case "showpointers":
                        ShowPointers = a.Value.ToBoolean();
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}