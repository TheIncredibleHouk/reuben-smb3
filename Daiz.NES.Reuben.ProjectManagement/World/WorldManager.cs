using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class WorldManager : IXmlIO
    {
        public event EventHandler<TEventArgs<WorldInfo>> WorldAdded;
        private Dictionary<Guid, WorldInfo> worldLookup;
        public List<WorldInfo> Worlds { get; private set; }

        public WorldManager()
        {
            Default();
        }

        public void Default()
        {
            Worlds = new List<WorldInfo>();
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "First World", Ordinal = 0, WorldGuid = Guid.NewGuid() });
            Worlds.Add(new WorldInfo() { LastModified = DateTime.Now, Name = "No World", Ordinal = 25, WorldGuid = Guid.NewGuid(), IsNoWorld = true });

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

        public WorldInfo GetNoWorld()
        {
            return Worlds.Last();
        }

        public bool AddWorld(WorldInfo wi)
        {
            if (worldLookup.ContainsKey(wi.WorldGuid)) return false;

            wi.Ordinal = Worlds.Count - 1;
            Worlds.Insert(Worlds.Count - 1, wi);
            worldLookup.Add(wi.WorldGuid, wi);
            if (WorldAdded != null)
            {
                WorldAdded(this, new TEventArgs<WorldInfo>(wi));
            }
            return true;
        }

        public void RemoveWorld(WorldInfo wi)
        {
            Worlds.Remove(wi);
            worldLookup.Remove(wi.WorldGuid);

            if (File.Exists(string.Format("{0}{1}{2}.map", ProjectController.WorldDirectory, Path.DirectorySeparatorChar, wi.WorldGuid)))
            {
                File.Delete(string.Format("{0}{1}{2}.map", ProjectController.WorldDirectory, Path.DirectorySeparatorChar, wi.WorldGuid));
            }

            int worldIndex = 0;
            foreach (var w in Worlds)
            {
                if (!w.IsNoWorld)
                {
                    w.Ordinal = worldIndex++;
                }
            }
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
                if (!wi.IsNoWorld)
                {
                    wi.Ordinal = Worlds.Count;
                }
                
                Worlds.Add(wi);
                worldLookup.Add(wi.WorldGuid, wi);
            }

            return true;
        }

        #endregion
    }
}
