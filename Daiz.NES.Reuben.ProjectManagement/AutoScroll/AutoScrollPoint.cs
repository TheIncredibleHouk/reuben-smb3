using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public struct AutoScrollPoint : IXmlIO
    {
        public int ScrollToX;
        public int ScrollToY;
        public int Speed;

        public AutoScrollPoint(int x, int y, int speed)
        {
            ScrollToX = x;
            ScrollToY = y;
            Speed = speed;
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
                        ScrollToX = a.Value.ToInt();
                        break;

                    case "toy":
                        ScrollToY = a.Value.ToInt();
                        break;

                    case "speed":
                        Speed = a.Value.ToInt();
                        break;
                }
            }

            return true;
        }
    }
}
