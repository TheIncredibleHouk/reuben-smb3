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
        public int LastCompressionSize { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsNoWorld { get; set; }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("world");
            x.SetAttributeValue("name", Name);
            x.SetAttributeValue("ordinal", Ordinal);
            x.SetAttributeValue("worldguid", WorldGuid);
            x.SetAttributeValue("lastmodified", LastModified);
            x.SetAttributeValue("lastcompressedsize", LastCompressionSize);
            x.SetAttributeValue("isnoworld", IsNoWorld);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "name":
                        Name = a.Value;
                        break;

                    case "ordinal":
                        Ordinal = a.Value.ToInt();
                        break;

                    case "worldguid":
                        WorldGuid = a.Value.ToGuid();
                        break;

                    case "lastmodified":
                        LastModified = a.Value.ToDateTime();
                        break;

                    case "lastcompressedsize":
                        LastCompressionSize = a.Value.ToInt();
                        break;

                    case "isnoworld":
                        IsNoWorld = a.Value.ToBoolean();
                        break;
                }
            }

            return true;
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
