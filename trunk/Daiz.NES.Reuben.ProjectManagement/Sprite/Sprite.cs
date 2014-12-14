using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Sprite : IXmlIO
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Property { get; set; }
        public int InGameID { get; set; }
        public bool IsMapSprite { get; set; }
        public int Item { get; set; }
        public bool IsViewable { get; internal set; }

        public Sprite()
        {
            IsViewable = true;
        }

        public string Name
        {
            get
            {
                if (IsMapSprite)
                    return ProjectController.SpriteManager.GetMapDefinition(InGameID).Name;

                SpriteDefinition def = ProjectController.SpriteManager.GetDefinition(InGameID);
                if (def == null)
                {
                    return "Unknown";
                }
                return def.Name;
            }
        }

        public int Width
        {
            get
            {
                if (IsMapSprite)
                    return ProjectController.SpriteManager.GetMapDefinition(InGameID).Width;
                SpriteDefinition def = ProjectController.SpriteManager.GetDefinition(InGameID);
                if (def == null)
                {
                    return 16;
                }

                return def.Width;
            }
        }

        public int Height
        {
            get
            {
                if (IsMapSprite)
                    return ProjectController.SpriteManager.GetMapDefinition(InGameID).Height;

                SpriteDefinition def = ProjectController.SpriteManager.GetDefinition(InGameID);
                if (def == null)
                {
                    return 16;
                }

                return def.Height;
            }
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("sprite");
            x.SetAttributeValue("x", X);
            x.SetAttributeValue("y", Y);
            x.SetAttributeValue("value", InGameID);
            x.SetAttributeValue("item", Item);
            x.SetAttributeValue("property", Property);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "x":
                        X = a.Value.ToInt();
                        break;

                    case "y":
                        Y = a.Value.ToInt();
                        break;

                    case "value":
                        InGameID = a.Value.ToInt();
                        break;

                    case "item":
                        Item = a.Value.ToInt();
                        break;

                    case "property":
                        Property = a.Value.ToInt();
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}
