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
            LevelTypes.Add(new LevelType("Grassy/Water", 1));
            LevelTypes.Add(new LevelType("Dungeon", 2));
            LevelTypes.Add(new LevelType("Crystal", 3));
            LevelTypes.Add(new LevelType("Sky Castle", 4));
            LevelTypes.Add(new LevelType("Inner Castle", 5));
            LevelTypes.Add(new LevelType("Island/Sea", 6));
            LevelTypes.Add(new LevelType("Haunted Desert", 7));
            LevelTypes.Add(new LevelType("Unused", 8));
            LevelTypes.Add(new LevelType("Desert", 9));
            LevelTypes.Add(new LevelType("Pirate Ship", 10));
            LevelTypes.Add(new LevelType("Unused", 11));
            LevelTypes.Add(new LevelType("Snow/Forest", 12));
            LevelTypes.Add(new LevelType("Unused", 13));
            LevelTypes.Add(new LevelType("Underground", 14));

            foreach (var l in LevelTypes)
            {
                if (l.InGameID != 0)
                {
                    _typeTable.Add(l.InGameID, l);
                }
            }
        }
        public bool CreateNewLevel(string name, LevelType levelType, LevelLayout layout, WorldInfo worldinfo)
        {
            Level l = new Level();
            l.LevelLayout = layout;
            l.Palette = levelType.InGameID;
            switch (levelType.InGameID)
            {
                case 1:
                    l.GraphicsBank = 0x08;
                    l.ClearValue = 0x90;
                    break;

                case 2:
                    l.GraphicsBank = 0x10;
                    l.ClearValue = 0x90;
                    break;

                case 3:
                    l.GraphicsBank = 0x6C;
                    l.ClearValue = 0x90;
                    break;

                case 4:
                    l.GraphicsBank = 0x06;
                    l.ClearValue = 0x90;
                    break;

                case 5:
                    l.GraphicsBank = 0x58;
                    l.ClearValue = 0x90;
                    break;

                case 6:
                    l.GraphicsBank = 0x58;
                    l.ClearValue = 0x90;
                    break;

                case 7:
                    l.GraphicsBank = 0x5C;
                    l.ClearValue = 0x90;
                    break;

                case 8:
                    l.GraphicsBank = 0x58;
                    l.ClearValue = 0x90;
                    break;

                case 9:
                    l.GraphicsBank = 0x30;
                    l.ClearValue = 0x90;
                    break;

                case 10:
                    l.GraphicsBank = 0x34;
                    l.ClearValue = 0x90;
                    break;


                case 11:
                    l.GraphicsBank = 0x6E;
                    l.ClearValue = 0x90;
                    break;


                case 12:
                    l.GraphicsBank = 0x18;
                    l.ClearValue = 0x90;
                    break;


                case 13:
                    l.GraphicsBank = 0x38;
                    l.ClearValue = 0x90;
                    break;

                case 14:
                    l.GraphicsBank = 0x1C;
                    l.ClearValue = 0x90;
                    break;
            }

            l.Guid = Guid.NewGuid();
            l.Length = 0x0A;
            l.Type = levelType.InGameID;
            l.Music = 0x00;
            l.Time = 300;
            l.PaletteEffect = 0;
            l.InvincibleEnemies = false;
            l.VineBlocked = false;
            l.XStart = 0x00;
            l.YStart = 0x14;

            for(int j = 0; j < l.Height; j++)
            {
                for(int i = 0; i < l.Width; i++)
                {
                    l.LevelData[i, j] = (byte) l.ClearValue;
                }
            }

            LevelInfo li = new LevelInfo()
            {
                Name = name,
                LevelGuid = l.Guid,
                WorldGuid = worldinfo.WorldGuid,
                LevelType = l.Type,
                Layout = l.LevelLayout
            };

            Levels.Add(li);
            levelLookup.Add(l.Guid, li);

            if (l.Save())
            {

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

            if (File.Exists(string.Format("{0}{1}{2}.lvl", ProjectController.LevelDirectory, Path.DirectorySeparatorChar, li.LevelGuid)))
            {
                File.Delete(string.Format("{0}{1}{2}.lvl", ProjectController.LevelDirectory, Path.DirectorySeparatorChar, li.LevelGuid));
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
