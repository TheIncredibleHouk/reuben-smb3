using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class WorldInfo : IXmlIO
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }
        public Guid WorldGuid { get; set; }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("world");
            x.SetAttributeValue("name", Name);
            x.SetAttributeValue("ordinal", Ordinal);
            x.SetAttributeValue("worldguid", WorldGuid);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            Name = e.Attribute("name").Value;
            Ordinal = e.Attribute("ordinal").Value.ToInt();
            WorldGuid = e.Attribute("worldguid").Value.ToGuid();
            return true;
        }

        #endregion
    }
}
