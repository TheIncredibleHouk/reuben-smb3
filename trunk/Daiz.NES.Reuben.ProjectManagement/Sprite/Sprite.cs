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
        public int Visibility { get; set; }
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

                return ProjectController.SpriteManager.GetDefinition(InGameID).Name;
            }
        }

        public int Width
        {
            get
            {
                if (IsMapSprite)
                    return ProjectController.SpriteManager.GetMapDefinition(InGameID).Width;

                return ProjectController.SpriteManager.GetDefinition(InGameID).Width;
            }
        }

        public int Height
        {
            get
            {
                if(IsMapSprite)
                return ProjectController.SpriteManager.GetMapDefinition(InGameID).Height;

                return ProjectController.SpriteManager.GetDefinition(InGameID).Height;
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
            x.SetAttributeValue("visibility", Visibility);
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

                    case "visibility":
                        Visibility = a.Value.ToInt();
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}
