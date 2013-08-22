using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class AutoScrollManager : IXmlIO
    {
        private Dictionary<Guid, AutoScrollSet> ScrollSets;

        public AutoScrollManager()
        {
            ScrollSets = new Dictionary<Guid, AutoScrollSet>();
        }

        public AutoScrollSet GetScrollSet(Guid id)
        {
            if (ScrollSets.ContainsKey(id))
            {
                return ScrollSets[id];
            }

            return null;
        }

        public AutoScrollSet AddScrollSet(string name)
        {
            AutoScrollSet s = new AutoScrollSet();
            s.Name = name;
            ScrollSets.Add(s.ID, s);
            return s;
        }

        public IEnumerable<AutoScrollSet> AutoScrollSets
        {
            get { return ScrollSets.Values; }
        }

        public XElement CreateElement()
        {
            XElement e = new XElement("autoscrollsets");
            foreach (AutoScrollSet s in AutoScrollSets)
            {
                e.Add(s.CreateElement());
            }

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (XElement x in e.Elements())
            {
                AutoScrollSet set = new AutoScrollSet();
                set.LoadFromElement(x);
                ScrollSets.Add(set.ID, set);
            }

            return true;
        }
    }
}
