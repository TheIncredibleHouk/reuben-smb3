using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class WorldPointer : IXmlIO
    {
        public Guid LevelGuid { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement e = new XElement("pointer");
            e.SetAttributeValue("levelguid", LevelGuid);
            e.SetAttributeValue("x", X);
            e.SetAttributeValue("y", Y);

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            LevelGuid = e.Attribute("levelguid").Value.ToGuid();
            X = e.Attribute("x").Value.ToInt();
            Y = e.Attribute("y").Value.ToInt();
            return true;
        }

        #endregion
    }
}
