using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpecialBlock : Block, IXmlIO
    {
        public int LevelType { get; private set; }
        public int AppliesTo { get; private set; }
        public int Palette { get; private set; }
        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("block");
            x.SetAttributeValue("levetype", LevelType);
            x.SetAttributeValue("appliesto", AppliesTo);
            x.SetAttributeValue("upperleft", this[0, 0]);
            x.SetAttributeValue("upperright", this[0, 1]);
            x.SetAttributeValue("lowerleft", this[1, 0]);
            x.SetAttributeValue("lowerright", this[1, 1]);
            x.SetAttributeValue("palette", Palette);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            LevelType = e.Attribute("leveltype").Value.ToInt();
            AppliesTo = e.Attribute("appliesto").Value.ToInt();
            this[0, 0] = (byte)e.Attribute("upperleft").Value.ToInt();
            this[1, 0] = (byte)e.Attribute("upperright").Value.ToInt();
            this[0, 1] = (byte)e.Attribute("lowerleft").Value.ToInt();
            this[1, 1] = (byte)e.Attribute("lowerright").Value.ToInt();
            Palette = e.Attribute("palette").Value.ToInt();
            return true;
        }

        #endregion
    }
}
