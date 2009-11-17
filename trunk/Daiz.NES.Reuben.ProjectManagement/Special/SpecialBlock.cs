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
        public int AppliesTo { get; private set; }
        public int Palette { get; private set; }
        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("block");
            x.SetAttributeValue("appliesto", AppliesTo.ToHexString());
            x.SetAttributeValue("upperleft", this[0, 0].ToHexString());
            x.SetAttributeValue("upperright", this[1, 0].ToHexString());
            x.SetAttributeValue("lowerleft", this[0, 1].ToHexString());
            x.SetAttributeValue("lowerright", this[1, 1].ToHexString());
            x.SetAttributeValue("palette", Palette);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            AppliesTo = e.Attribute("appliesto").Value.ToIntFromHex();
            this[0, 0] = (byte)e.Attribute("upperleft").Value.ToIntFromHex();
            this[1, 0] = (byte)e.Attribute("upperright").Value.ToIntFromHex();
            this[0, 1] = (byte)e.Attribute("lowerleft").Value.ToIntFromHex();
            this[1, 1] = (byte)e.Attribute("lowerright").Value.ToIntFromHex();
            Palette = e.Attribute("palette").Value.ToInt();
            return true;
        }

        #endregion
    }
}
