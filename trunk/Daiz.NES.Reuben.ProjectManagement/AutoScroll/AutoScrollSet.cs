using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class AutoScrollSet : IXmlIO
    {
        public List<AutoScrollPoint> ScrollPoints { get; set; }

        public Guid ID { get; set; }
        public string Name { get; set; }

        public AutoScrollSet()
        {
            ID = Guid.NewGuid();
            ScrollPoints = new List<AutoScrollPoint>();
            ScrollPoints.Add(new AutoScrollPoint(0, 0, 1));
            ScrollPoints.Add(new AutoScrollPoint(240, 0, 1));
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("autoscroll");
            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("id", ID);
            foreach (AutoScrollPoint p in ScrollPoints)
            {
                e.Add(p.CreateElement());
            }

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            ScrollPoints.Clear();

            foreach (XAttribute a in e.Attributes())
            {
                switch (a.Name.LocalName.ToLower())
                {
                    case "id":
                        ID = a.Value.ToGuid();
                        break;

                    case "name":
                        Name = a.Value;
                        break;
                }
            }

            foreach (XElement x in e.Elements())
            {
                AutoScrollPoint p = new AutoScrollPoint();
                p.LoadFromElement(x);
                ScrollPoints.Add(p);
            }

            return true;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
