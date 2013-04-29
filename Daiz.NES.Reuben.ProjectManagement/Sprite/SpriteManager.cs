using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpriteManager
    {
        public Dictionary<int, Dictionary<string, List<SpriteDefinition>>> SpriteGroups { get; private set; }
        private Dictionary<int, SpriteDefinition> SpriteDefinitions;
        public Dictionary<int, SpriteDefinition> MapSpriteDefinitions { get; private set; }

        public SpriteManager()
        {
            SpriteDefinitions = new Dictionary<int, SpriteDefinition>();
            SpriteGroups = new Dictionary<int, Dictionary<string, List<SpriteDefinition>>>();
            MapSpriteDefinitions = new Dictionary<int, SpriteDefinition>();
            SpriteGroups.Add(1, null);
            SpriteGroups.Add(2, null);
            SpriteGroups.Add(3, null);
        }

        public void LoadDefaultSprites()
        {
            SpriteDefinitions.Clear();
            MapSpriteDefinitions.Clear();
            SpriteGroups[1] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[2] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[3] = new Dictionary<string, List<SpriteDefinition>>();

            XDocument xDoc = XDocument.Parse(Resource.default_sprites);
            XElement root = xDoc.Element("sprites");
            foreach (var x in root.Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                SpriteDefinitions.Add(sp.InGameId, sp);

                if (!SpriteGroups[sp.Class].ContainsKey(sp.Group))
                {
                    SpriteGroups[sp.Class].Add(sp.Group, new List<SpriteDefinition>());
                }

                SpriteGroups[sp.Class][sp.Group].Add(sp);
            }
            foreach (var x in root.Element("mapsprites").Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                MapSpriteDefinitions.Add(sp.InGameId, sp);
            }
        }

        public bool LoadSpritesFromFile(string filename)
        {
            if (!File.Exists(filename)) return false;
            SpriteDefinitions.Clear();
            MapSpriteDefinitions.Clear();
            SpriteGroups[1] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[2] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[3] = new Dictionary<string, List<SpriteDefinition>>();

            XDocument xDoc = XDocument.Load(filename);
            XElement root = xDoc.Element("sprites");
            foreach (var x in root.Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                SpriteDefinitions.Add(sp.InGameId, sp);

                if (!SpriteGroups[sp.Class].ContainsKey(sp.Group))
                {
                    SpriteGroups[sp.Class].Add(sp.Group, new List<SpriteDefinition>());
                }

                SpriteGroups[sp.Class][sp.Group].Add(sp);
            }
            foreach (var x in root.Element("mapsprites").Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                MapSpriteDefinitions.Add(sp.InGameId, sp);
            }

            return true;
        }


        public bool Save(string filename)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("sprites");
            foreach (var s in SpriteDefinitions.Values)
            {
                root.Add(s.CreateElement());
            }

            XElement mapRoot = new XElement("mapsprites");
            foreach(var ms in MapSpriteDefinitions.Values)
            {
                mapRoot.Add(ms.CreateElement());
            }

            root.Add(mapRoot);
            xDoc.Add(root);
            xDoc.Save(filename);
            return true;
        }

        public SpriteDefinition GetDefinition(int value)
        {
            if (SpriteDefinitions.ContainsKey(value))
                return SpriteDefinitions[value];

            return null;
        }

        public SpriteDefinition GetMapDefinition(int value)
        {
            if (MapSpriteDefinitions.ContainsKey(value))
                return MapSpriteDefinitions[value];

            return null;
        }
    }
}