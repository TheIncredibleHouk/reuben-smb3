using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelInfo : IXmlIO
    {
        public string Name { get; set; }
        public Guid WorldGuid { get; set; }
        public Guid LevelGuid { get; set; }
        public int LastCompressionSize { get; set; }
        public int LevelType { get; set; }
        public DateTime LastModified { get; set; }
        public LevelLayout Layout { get; set; }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("level");
            x.SetAttributeValue("name", Name);
            x.SetAttributeValue("worldguid", WorldGuid);
            x.SetAttributeValue("levelguid", LevelGuid);
            x.SetAttributeValue("leveltype", LevelType);
            x.SetAttributeValue("lastcompressionsize", LastCompressionSize);
            x.SetAttributeValue("lastmodified", LastModified);
            x.SetAttributeValue("layout", Layout);

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "name": Name = a.Value;
                        break;

                    case "worldguid":
                        WorldGuid = a.Value.ToGuid();
                        break;

                    case "levelguid": LevelGuid = a.Value.ToGuid();
                        break;

                    case "leveltype": LevelType = a.Value.ToInt();
                        break;

                    case "lastcompressionsize": LastCompressionSize = a.Value.ToInt();
                        break;

                    case "lastmodified": LastModified = a.Value.ToDateTime();
                        break;

                    case "layout": Layout = (LevelLayout)Enum.Parse(typeof(LevelLayout), a.Value, true);
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}
