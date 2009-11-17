using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;  

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelManager : IXmlIO
    {
        public event EventHandler<TEventArgs<LevelInfo>> LevelAdded;
        public event EventHandler<TEventArgs<LevelInfo>> LevelRemoved;

        public Level CurrentLevel { get; private set; }
        public List<LevelType> LevelTypes { get; private set; }
        private Dictionary<int, LevelType> _typeTable;
        public List<LevelInfo> Levels { get; private set; }
        private Dictionary<Guid, LevelInfo> levelLookup;

        public LevelManager()
        {
            levelLookup = new Dictionary<Guid, LevelInfo>();
            _typeTable = new Dictionary<int, LevelType>();
            Levels = new List<LevelInfo>();
            LevelTypes = new List<LevelType>();
        }

        public void Default()
        {
            levelLookup.Clear();
            _typeTable.Clear();
            Levels.Clear();
            LevelTypes.Clear();
            LevelTypes.Add(new LevelType("World Map", 0));
            LevelTypes.Add(new LevelType("Plains", 1));
            LevelTypes.Add(new LevelType("Dungeon", 2));
            LevelTypes.Add(new LevelType("Hilly", 3));
            LevelTypes.Add(new LevelType("Sky", 4));
            LevelTypes.Add(new LevelType("Piranha Plant", 5));
            LevelTypes.Add(new LevelType("Water", 6));
            LevelTypes.Add(new LevelType("Mushroom", 7));
            LevelTypes.Add(new LevelType("Pipe", 8));
            LevelTypes.Add(new LevelType("Desert", 9));
            LevelTypes.Add(new LevelType("Ship", 10));
            LevelTypes.Add(new LevelType("Giant", 11));
            LevelTypes.Add(new LevelType("Ice", 12));
            LevelTypes.Add(new LevelType("Cloudy", 13));
            LevelTypes.Add(new LevelType("Underground", 14));

            foreach (var l in LevelTypes)
            {
                if(l.InGameID != 0)
                    _typeTable.Add(l.InGameID, l);
            }

            CurrentLevel = null;
        }
        public bool CreateNewLevel(string name, LevelType levelType, LevelLayout layout, WorldInfo worldinfo)
        {
            Level l = new Level();
            l.LevelLayout = layout;
            switch (levelType.InGameID)
            {
                case 1:
                    l.GraphicsBank = 0x08;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;

                case 2:
                    l.GraphicsBank = 0x10;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x02;
                    break;

                case 3:
                    l.GraphicsBank = 0x6C;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x86;
                    break;

                case 4:
                    l.GraphicsBank = 0x0C;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;

                case 5:
                    l.GraphicsBank = 0x5C;
                    l.AnimationBank = 0x3E;
                    l.ClearValue = 0x80;
                    break;

                case 6:
                    l.GraphicsBank = 0x58;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x8C;
                    break;

                case 7:
                    l.GraphicsBank = 0x58;
                    l.AnimationBank = 0x5E;
                    l.ClearValue = 0x42;
                    break;

                case 8:
                    l.GraphicsBank = 0x58;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;

                case 9:
                    l.GraphicsBank = 0x30;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;

                case 10:
                    l.GraphicsBank = 0x34;
                    l.AnimationBank = 0x6A;
                    l.ClearValue = 0x06;
                    break;


                case 11:
                    l.GraphicsBank = 0x6E;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;


                case 12:
                    l.GraphicsBank = 0x18;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x80;
                    break;


                case 13:
                    l.GraphicsBank = 0x38;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0xCC;
                    break;

                case 14:
                    l.GraphicsBank = 0x1C;
                    l.AnimationBank = 0x60;
                    l.ClearValue = 0x02;
                    break;
            }

            l.Guid = Guid.NewGuid();
            l.Length = 0x0A;
            l.Type = levelType.InGameID;
            l.Music = 0x00;
            l.Palette = 0;
            l.Time = 300;
            l.Unused1 = l.Unused2 = l.Unused3 = 0;
            l.XStart = 0x00;
            l.YStart = 0x14;

            for(int j = 0; j < l.Height; j++)
            {
                for(int i = 0; i < l.Width; i++)
                {
                    l.LevelData[i, j] = (byte) l.ClearValue;
                }
            }
            if (l.Save())
            {
                CurrentLevel = l;

                LevelInfo li = new LevelInfo()
                {
                    Name = name,
                    LevelGuid = l.Guid,
                    WorldGuid = worldinfo.WorldGuid,
                    LevelType = l.Type
                };

                Levels.Add(li);
                levelLookup.Add(l.Guid, li);

                if (LevelAdded != null) LevelAdded(this, new TEventArgs<LevelInfo>(li));
            }
            return true;
        }

        public bool AddLevel(LevelInfo li)
        {
            if(levelLookup.ContainsKey(li.LevelGuid)) return false;

            Levels.Add(li);
            levelLookup.Add(li.LevelGuid, li);
            if (LevelAdded != null)
            {
                LevelAdded(this, new TEventArgs<LevelInfo>(li));
            }

            return true;
        }

        public void RemoveLevel(LevelInfo li)
        {
            Levels.Remove(li);
            levelLookup.Remove(li.LevelGuid);

            if (File.Exists(ProjectController.LevelDirectory + @"\" + li.LevelGuid + ".lvl"))
            {
                File.Delete(ProjectController.LevelDirectory + @"\" + li.LevelGuid + ".lvl");
            }

            if(LevelRemoved != null)
            {
                LevelRemoved(this, new TEventArgs<LevelInfo>(li));
            }
        }

        public LevelInfo GetLevelInfo(Guid guid)
        {
            if (levelLookup.ContainsKey(guid))
            {
                return levelLookup[guid];
            }

            return null;
        }

        public LevelType GetLevelType(int inGameID)
        {
            if(_typeTable.ContainsKey(inGameID))
            {
                return _typeTable[inGameID];
            }

            return null;
        }
        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("levelinfo");
            foreach (var l in Levels)
            {
                x.Add(l.CreateElement());
            }

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            Levels.Clear();
            levelLookup.Clear();
            Default();
            foreach (var l in e.Elements("level"))
            {
                LevelInfo li = new LevelInfo();
                li.LoadFromElement(l);
                Levels.Add(li);
                levelLookup.Add(li.LevelGuid, li);
            }

            return true;
        }

        #endregion
    }
}
