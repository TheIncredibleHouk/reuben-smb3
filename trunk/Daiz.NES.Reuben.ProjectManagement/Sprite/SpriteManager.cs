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
        private Dictionary<int, SpriteDefinition> _SpriteTable;
        private Dictionary<int, SpriteDefinition> SpriteDefinitions;

        public SpriteManager()
        {
            SpriteDefinitions = new Dictionary<int, SpriteDefinition>();
            _SpriteTable = new Dictionary<int, SpriteDefinition>();
            SpriteGroups = new Dictionary<int, Dictionary<string, List<SpriteDefinition>>>();
            SpriteGroups.Add(1, null);
            SpriteGroups.Add(2, null);
            SpriteGroups.Add(3, null);
        }

        public void LoadDefaultSprites()
        {
            _SpriteTable.Clear();
            SpriteDefinitions.Clear();
            SpriteGroups[1] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[2] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[3] = new Dictionary<string, List<SpriteDefinition>>();

            XDocument xDoc = XDocument.Parse(Resource.default_sprites);
            XElement root = xDoc.Element("sprites");
            foreach (var x in root.Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                _SpriteTable.Add(sp.InGameId, sp);
                SpriteDefinitions.Add(sp.InGameId, sp);

                if (!SpriteGroups[sp.Group].ContainsKey(sp.Class))
                {
                    SpriteGroups[sp.Group].Add(sp.Class, new List<SpriteDefinition>());
                }

                SpriteGroups[sp.Group][sp.Class].Add(sp);
            }
        }

        public bool LoadSpritesFromFile(string filename)
        {
            if (!File.Exists(filename)) return false;
            _SpriteTable.Clear();
            SpriteDefinitions.Clear();
            SpriteGroups[1] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[2] = new Dictionary<string, List<SpriteDefinition>>();
            SpriteGroups[3] = new Dictionary<string, List<SpriteDefinition>>();

            XDocument xDoc = XDocument.Load(filename);
            XElement root = xDoc.Element("sprites");
            foreach (var x in root.Elements("spritedefinition"))
            {
                SpriteDefinition sp = new SpriteDefinition();
                sp.LoadFromElement(x);

                _SpriteTable.Add(sp.InGameId, sp);
                SpriteDefinitions.Add(sp.InGameId, sp);

                if (!SpriteGroups[sp.Group].ContainsKey(sp.Class))
                {
                    SpriteGroups[sp.Group].Add(sp.Class, new List<SpriteDefinition>());
                }

                SpriteGroups[sp.Group][sp.Class].Add(sp);
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
    }
}