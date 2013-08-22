using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class AutoScrollPoint : IXmlIO
    {
        public int ScrollToX { get; set; }
        public int ScrollToY { get; set; }
        public int Speed { get; set; }

        public AutoScrollPoint()
        {
        }

        public AutoScrollPoint(int x, int y)
        {
            ScrollToX = x;
            ScrollToY = y;
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("point");

            e.SetAttributeValue("tox", ScrollToX);
            e.SetAttributeValue("toy", ScrollToY);
            e.SetAttributeValue("speed", Speed);
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (XAttribute a in e.Attributes())
            {
                switch (a.Name.LocalName.ToLower())
                {

                    case "tox":
                        ScrollToX = e.Value.ToInt();
                        break;

                    case "toy":
                        ScrollToY = e.Value.ToInt();
                        break;

                    case "speed":
                        Speed = e.Value.ToInt();
                        break;
                }
            }

            return true;
        }
    }
}
