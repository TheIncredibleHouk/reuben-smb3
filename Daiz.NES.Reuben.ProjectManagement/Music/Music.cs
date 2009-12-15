using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Music : IXmlIO
    {
        public string Name { get; set; }
        public int Value { get; set; }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement e = new XElement("track");
            e.SetAttributeValue("value", Value.ToHexString());
            e.SetAttributeValue("name", Name);
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "value":
                        Value = a.Value.ToIntFromHex();
                        break;

                    case "name":
                        Name = a.Value;
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}
