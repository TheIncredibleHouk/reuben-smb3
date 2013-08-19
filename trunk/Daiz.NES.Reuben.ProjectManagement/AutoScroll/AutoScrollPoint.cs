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
        public int HorizontalSpeed { get; set; }
        public int VerticalSpeed { get; set; }
        public int HorizontalFracSpeed { get; set; }
        public int VerticalFracSpeed { get; set; }
        public int Length { get; set; }

        public XElement CreateElement()
        {
            XElement e = new XElement("point");

            e.SetAttributeValue("hspeed", HorizontalSpeed);
            e.SetAttributeValue("vspeed", VerticalSpeed);
            e.SetAttributeValue("hfspeed", HorizontalFracSpeed);
            e.SetAttributeValue("vfspeed", VerticalFracSpeed);
            e.SetAttributeValue("length", Length);
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (XAttribute a in e.Attributes())
            {
                switch (a.Name.LocalName.ToLower())
                {

                    case "vspeed":
                        VerticalSpeed = e.Value.ToInt();
                        break;

                    case "vfspeed":
                        VerticalFracSpeed = e.Value.ToInt();
                        break;

                    case "hspeed":
                        HorizontalSpeed = e.Value.ToInt();
                        break;

                    case "hfspeed":
                        HorizontalFracSpeed = e.Value.ToInt();
                        break;

                    case "length":
                        Length = e.Value.ToInt();
                        break;
                
            }
        }
    }
}
