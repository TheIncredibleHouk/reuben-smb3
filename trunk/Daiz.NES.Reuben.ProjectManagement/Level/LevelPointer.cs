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
        public bool RedrawLevel { get; set; }
        public int XEnter { get; set; }
        public int YEnter { get; set; }
        public int XExit { get; set; }
        public int YExit { get; set; }
        public bool ExitsLevel { get; set; }
        public int World { get; set; }

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
            e.SetAttributeValue("world", World);
            e.SetAttributeValue("redraw", RedrawLevel);
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "levelguid":
                        LevelGuid = a.Value.ToGuid();
                        break;

                    case "exittype":
                        ExitType = a.Value.ToInt();
                        break;

                    case "xenter":
                        XEnter = a.Value.ToInt();
                        break;

                    case "yenter":
                        YEnter = a.Value.ToInt();
                        break;

                    case "xexit":
                        XExit = a.Value.ToInt();
                        break;

                    case "yexit":
                        YExit = a.Value.ToInt();
                        break;

                    case "exitslevel":
                        ExitsLevel = a.Value.ToBoolean();
                        break;

                    case "world":
                        World = a.Value.ToInt();
                        break;

                    case "redraw":
                        RedrawLevel = a.Value.ToBoolean();
                        break;
                }
            }
            return true;
        }

        #endregion
    }
}
