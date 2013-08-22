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
        private List<AutoScrollPoint> _ScrollPoints;

        public Guid ID { get; set; }
        public string Name { get; set; }

        public AutoScrollSet()
        {
            ID = Guid.NewGuid();
            _ScrollPoints = new List<AutoScrollPoint>();
            _ScrollPoints.Add(new AutoScrollPoint(0, 0));
            _ScrollPoints.Add(new AutoScrollPoint(240, 0));
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("autoscroll");
            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("id", ID);
            foreach (AutoScrollPoint p in _ScrollPoints)
            {
                e.Add(p.CreateElement());
            }

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
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
                _ScrollPoints.Add(p);
            }

            return true;
        }

        public ReadOnlyCollection<AutoScrollPoint> ScrollPoints
        {
            get { return _ScrollPoints.AsReadOnly(); }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
