using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpriteInfo : IXmlIO
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public int Palette { get; set; }
        public bool HorizontalFlip { get; set; }
        public bool VerticalFlip { get; set; }
        public int Table { get; set; }
        public string Name { get; private set; }
        public List<int> Property { get; private set; }

        #region IXmlIO Members


        public bool LoadFromElement(XElement e)
        {
            X = e.Attribute("x").Value.ToInt();
            Y = e.Attribute("y").Value.ToInt();
            Value = e.Attribute("value").Value.ToIntFromHex();
            Palette = e.Attribute("palette").Value.ToInt();
            HorizontalFlip = e.Attribute("horizontalflip").Value.ToBoolean();
            VerticalFlip = e.Attribute("verticalflip").Value.ToBoolean();
            if (e.Attribute("table").Value.Contains('-')) Table = e.Attribute("table").Value.ToInt();
            else Table = e.Attribute("table").Value.ToIntFromHex();
            if (e.Attribute("property") != null)
            {
                Property = e.Attribute("property").Value.Split(',').Select(s => s.ToInt()).ToList();
            }
            else
            {
                Property = null;
            }
            return true;
        }

        public XElement CreateElement()
        {
            XElement x = new XElement("sprite");
            x.SetAttributeValue("x", X);
            x.SetAttributeValue("y", Y);
            x.SetAttributeValue("value", Value.ToHexString());
            x.SetAttributeValue("palette", Palette);
            x.SetAttributeValue("horizontalflip", HorizontalFlip);
            x.SetAttributeValue("verticalflip", VerticalFlip);
            if (Table < 0) x.SetAttributeValue("table", Table);
            else x.SetAttributeValue("table", Table.ToHexString());
            return x;
        }
        #endregion
    }

    public enum SpriteRotation
    {
        HorizontalFlip,
        VerticalFlip,
        HorizontalAndVerticalFlip
    }
}
