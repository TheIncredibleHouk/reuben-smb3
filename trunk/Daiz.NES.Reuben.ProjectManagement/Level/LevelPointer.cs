using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelPointer : IXmlIO
    {
        public Guid LevelGuid { get; set; }
        public int ExitType { get; set; }
        public int XEnter { get; set; }
        public int YEnter { get; set; }
        public int XExit { get; set; }
        public int YExit { get; set; }
        public bool ExitsLevel { get; set; }
    
        #region IXmlIO Members

        public XElement CreateElement()
        {
 	        XElement e = new XElement("pointer");
            e.SetAttributeValue("levelguid", LevelGuid);
            e.SetAttributeValue("exittype", ExitType);
            e.SetAttributeValue("xexit", XExit);
            e.SetAttributeValue("yexit", YExit);
            e.SetAttributeValue("xenter", XEnter);
            e.SetAttributeValue("yenter", YEnter);
            e.SetAttributeValue("exitslevel", ExitsLevel);

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            LevelGuid = e.Attribute("levelguid").Value.ToGuid();
            ExitType = e.Attribute("exittype").Value.ToInt();
            XEnter = e.Attribute("xenter").Value.ToInt();
            YEnter = e.Attribute("yenter").Value.ToInt();
            XExit = e.Attribute("xexit").Value.ToInt();
            YExit = e.Attribute("yexit").Value.ToInt();
            ExitsLevel = e.Attribute("exitslevel").Value.ToBoolean();
            return true;
        }

        #endregion
    }
}
