using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Drawing;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SettingsManager : IXmlIO
    {
        private Dictionary<string, Setting> defaultSettings;
        private Dictionary<Guid, Dictionary<string, Setting>> levelSettings;

        public SettingsManager()
        {
            levelSettings = new Dictionary<Guid, Dictionary<string, Setting>>();
            defaultSettings = new Dictionary<string, Setting>();
            defaultSettings["ShowGrid"] = new Setting("ShowGrid", DataType.Boolean, false);
            defaultSettings["SpecialTiles"] = new Setting("SpecialTiles", DataType.Boolean, false);
            defaultSettings["SpecialSprites"] = new Setting("SpecialSprites", DataType.Boolean, false);
            defaultSettings["BlockProperties"] = new Setting("BlockProperties", DataType.Boolean, false);
            defaultSettings["ShowStart"] = new Setting("ShowStart", DataType.Boolean, false);
            defaultSettings["Zoom"] = new Setting("Zoom", DataType.Boolean, false);
            defaultSettings["Draw"] = new Setting("Draw", DataType.String, "Pencil");
            defaultSettings["Layout"] = new Setting("Layout", DataType.Integer, 0);
            defaultSettings["VGuideColor"] = new Setting("VGuideColor", DataType.Color, Color.Red);
            defaultSettings["HGuideColor"] = new Setting("HGuideColor", DataType.Color, Color.Blue);
            defaultSettings["TransSpecials"] = new Setting("TransSpecials", DataType.Decimal, .75);
            defaultSettings["TransProps"] = new Setting("TransProps", DataType.Decimal, .75);

        }

        public void Clear()
        {
            levelSettings.Clear();
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("settings");
            XElement d = new XElement("defaultsettings");

            foreach (var s in defaultSettings.Values)
            {
                d.Add(s.CreateElement());
            }

            foreach (var guid in levelSettings.Keys)
            {
                XElement l = new XElement("levelsettings");
                l.SetAttributeValue("guid", guid);

                foreach (var k in levelSettings[guid].Values)
                {
                    l.Add(k.CreateElement());
                }

                x.Add(l);
            }

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            if (e == null) return true;
            levelSettings.Clear();            

            if (e.Element("defaultsettings") != null)
            {
                defaultSettings.Clear();
                foreach (var d in e.Element("defaultsettings").Elements("setting"))
                {
                    Setting s = new Setting();
                    s.LoadFromElement(d);
                    defaultSettings.Add(s.Key, s);
                }
            }

            foreach (var l in e.Elements("levelsettings"))
            {
                Guid g= l.Attribute("guid").Value.ToGuid();
                levelSettings[g] = new Dictionary<string, Setting>();
                foreach (var s in l.Elements("setting"))
                {
                    Setting set = new Setting();
                    set.LoadFromElement(s);
                    levelSettings[g].Add(set.Key, set);
                }
            }

            return true;
        }

        public void AddLevelSettings(Guid levelGuid)
        {
            Dictionary<string, Setting> copy = new Dictionary<string,Setting>();
            foreach(var s in defaultSettings.Values)
            {
                copy.Add(s.Key, s);
            }
            levelSettings.Add(levelGuid, copy);
        }

        public bool HasLevelSettings(Guid levelGuid)
        {
            return levelSettings.ContainsKey(levelGuid);
        }

        public void SetDefaultSetting(string property, object value)
        {
            if (defaultSettings.ContainsKey(property))
            {
                defaultSettings[property].SetValue(value);
            }
        }

        public void SetLevelSetting(Guid guid, string property, object value)
        {
            if(levelSettings.ContainsKey(guid))
            {
                levelSettings[guid][property].SetValue(value);
            }
        }
        public T GetLevelSetting<T>(Guid guid, string property)
        {
            if(levelSettings.ContainsKey(guid))
            {
                Setting s = levelSettings[guid][property];
                return (T) s.Value;
            }

            return default(T);
        }
    }
    #endregion
}
