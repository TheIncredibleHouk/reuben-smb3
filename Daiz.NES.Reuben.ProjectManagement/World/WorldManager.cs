using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class WorldManager : IXmlIO
    {
        private Dictionary<Guid, WorldInfo> worldLookup;
        public List<WorldInfo> Worlds { get; private set; }

        public WorldManager()
        {
            Default();
        }

        public void Default()
        {
            Worlds = new List<WorldInfo>();
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Grass Land", Ordinal = 1, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Desert Land", Ordinal = 2, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Water Land", Ordinal = 3, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Giant Land", Ordinal = 4, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Sky Land", Ordinal = 5, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Winter Land", Ordinal = 6, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Pipe Land", Ordinal = 7, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "Dark Land", Ordinal = 8, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "No World", Ordinal = -1, WorldGuid = Guid.NewGuid() });

            worldLookup = new Dictionary<Guid, WorldInfo>();
            foreach (var w in Worlds)
            {
                worldLookup.Add(w.WorldGuid, w);
            }
        }

        public WorldInfo GetWorldInfo(Guid guid)
        {
            if (worldLookup.ContainsKey(guid))
            {
                return worldLookup[guid];
            }

            return null;
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("worldinfo");

            foreach (var w in Worlds)
            {
                x.Add(w.CreateElement());
            }

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            worldLookup.Clear();
            Worlds.Clear();

            foreach (var w in e.Elements("world"))
            {
                WorldInfo wi = new WorldInfo();
                wi.LoadFromElement(w);
                Worlds.Add(wi);
                worldLookup.Add(wi.WorldGuid, wi);
            }

            return true;
        }

        #endregion
    }
}
