using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class AutoScrollManager
    {
        private Dictionary<Guid, AutoScrollSet> ScrollSets;

        public AutoScrollManager()
        {
            ScrollSets = new Dictionary<Guid, AutoScrollSet>();
        }

        public bool LoadAutoScrollSets(string fileName)
        {
            if (File.Exists(fileName))
            {
                XDocument doc = XDocument.Load(fileName);
                foreach (XElement e in doc.Root.Elements())
                {
                    AutoScrollSet s = new AutoScrollSet();
                    s.LoadFromElement(e);
                    ScrollSets.Add(s.ID, s);
                }

                return true;
            }

            return false;
        }

        public bool SaveAutoScrollSets(string fileName)
        {
            XDocument doc = new XDocument("autoscroll");
            foreach (AutoScrollSet set in ScrollSets.Values)
            {
                doc.Root.Add(set.CreateElement());
            }

            doc.Save(fileName);
            return true;
        }

        public AutoScrollSet GetScrollSet(Guid id)
        {
            if (ScrollSets.ContainsKey(id))
            {
                return ScrollSets[id];
            }

            return null;
        }
    }
}
