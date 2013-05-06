using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

using Daiz.Library;


namespace Daiz.NES.Reuben.ProjectManagement
{
    public class World
    {
        public event EventHandler<TEventArgs<Sprite>> SpriteAdded;
        public event EventHandler<TEventArgs<Sprite>> SpriteRemoved;
        public event EventHandler<TEventArgs<Point>> TileChanged;
        public event EventHandler<TEventArgs<TileInformation>> TilesModified;
        public Guid Guid { get; set; }
        public int Type { get { return 0; } }
        public int ClearValue { get { return 0x02; } }
        public int GraphicsBank { get; set; }
        public int Music { get; set; }
        public int Length { get; set; }
        public int Palette { get; set; }
        public List<WorldPointer> Pointers { get; private set; }
        public byte[,] LevelData { get; private set; }
        public List<Sprite> SpriteData { get; private set; }
        public LevelSettings Settings { get; private set; }

        public World()
        {
            Pointers = new List<WorldPointer>();
            SpriteData = new List<Sprite>();
            LevelData = new byte[0x40, 0x1B];
            Settings = new LevelSettings();
        }

        public void New(WorldInfo wi)
        {
            Guid = wi.WorldGuid;
            Length = 1;
            Palette = 0;
            GraphicsBank = 0x16;
            for (int i = 0; i < 0x10; i++)
            {
                for (int j = 0; j < 0x40; j++)
                {
                    LevelData[j, i] = 0x02;
                }
            }

            for (int i = 0; i < 0x40; i++)
            {
                LevelData[i, 0x10] = 0x4E;
                LevelData[i, 0x1A] = 0x4F;
            }
        }

        public int Width
        {
            get { return 0x40; }
        }

        public int Height
        {
            get { return 0x1B; }
        }

        public bool Save()
        {
            return Save(ProjectController.WorldDirectory + @"\" + Guid + ".map");
        }

        public bool Save(string filename)
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("world");
            root.SetAttributeValue("guid", Guid);
            root.SetAttributeValue("type", Type);
            root.SetAttributeValue("clearvalue", ClearValue);
            root.SetAttributeValue("graphicsbank", GraphicsBank);
            root.SetAttributeValue("music", Music);
            root.SetAttributeValue("length", Length);
            root.SetAttributeValue("palette", Palette);

            StringBuilder sb = new StringBuilder();

            bool first = true;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (first)
                    {
                        sb.Append(LevelData[j, i]);
                    }
                    else
                    {
                        sb.Append("," + LevelData[j, i]);
                    }
                    first = false;
                }
            }
            root.SetAttributeValue("worlddata", sb);

            root.SetAttributeValue("compresseddata", sb);

            XElement s = new XElement("spritedata");

            foreach (var spr in SpriteData)
            {
                s.Add(spr.CreateElement());
            }

            XElement p = new XElement("pointers");
            foreach (WorldPointer ptr in Pointers)
            {
                p.Add(ptr.CreateElement());
            }

            root.Add(p);
            root.Add(s);
            root.Add(Settings.CreateElement());

            xDoc.Add(root);
            xDoc.Save(filename);

            ProjectController.WorldManager.GetWorldInfo(Guid).LastModified = DateTime.Now;
            return true;
        }

        public bool Load(WorldInfo li)
        {
            if (File.Exists(ProjectController.WorldDirectory + @"\" + li.WorldGuid + ".map"))
            {
                return Load(ProjectController.WorldDirectory + @"\" + li.WorldGuid + ".map");
            }

            return false;
        }

        public bool Load(string filename)
        {
            string[] compressData = null;
            string[] levelData = null;

            XDocument xDoc;
            try
            {
                xDoc = XDocument.Load(filename);
            }
            catch
            {
                return false;
            }

            XElement world = xDoc.Element("world");

            foreach (var a in world.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "guid":
                        Guid = a.Value.ToGuid();
                        break;

                    case "graphicsbank":
                        GraphicsBank = a.Value.ToInt();
                        break;

                    case "music":
                        Music = a.Value.ToInt();
                        break;

                    case "length":
                        Length = a.Value.ToInt();
                        break;

                    case "palette":
                        Palette = a.Value.ToInt();
                        break;

                    case "compresseddata":
                        compressData = a.Value.Split(',');
                        break;

                    case "worlddata":
                        levelData = a.Value.Split(',');
                        break;
                }
            }

            int xPointer = 0, yPointer = 0;
            foreach (var c in levelData)
            {
                LevelData[xPointer, yPointer] = (byte)c.ToInt();
                xPointer++;

                if (xPointer >= Width)
                {
                    xPointer = 0;
                    yPointer++;
                    if (yPointer > Height) break;
                }
            }

            SpriteData.Clear();
            Pointers.Clear();
            foreach (var x in world.Elements())
            {
                switch (x.Name.LocalName)
                {
                    case "spritedata":
                        foreach (var spr in x.Elements("sprite"))
                        {
                            Sprite s = new Sprite();
                            s.LoadFromElement(spr);
                            SpriteData.Add(s);
                        }
                        break;

                    case "pointers":

                        foreach (var ptr in x.Elements("pointer"))
                        {
                            WorldPointer p = new WorldPointer();
                            p.LoadFromElement(ptr);
                            Pointers.Add(p);
                        }
                        break;

                    case "settings":
                        Settings.LoadFromElement(x);
                        break;
                }
            }

            return true;
        }

        public void AddSprite(Sprite sprite)
        {
            SpriteData.Add(sprite);
            if (SpriteAdded != null) SpriteAdded(this, new TEventArgs<Sprite>(sprite));
        }

        public void RemoveSprite(Sprite sprite)
        {
            SpriteData.Remove(sprite);
            if (SpriteRemoved != null) SpriteRemoved(this, new TEventArgs<Sprite>(sprite));
        }

        public void AddPointer()
        {
            WorldPointer p = new WorldPointer()
            {
                X = 8,
                Y = 0x16,
                LevelGuid = Guid.Empty
            };

            Pointers.Add(p);

        }

        public void RemovePointer(WorldPointer p)
        {
            Pointers.Remove(p);

        }

        public void SetTile(int x, int y, byte value)
        {
            int previous = LevelData[x, y];
            if (LevelData[x, y] == value) return;
            LevelData[x, y] = value;
            if (TileChanged != null) TileChanged(this, new TEventArgs<Point>(new Point(x, y)));
            if (TilesModified != null) TilesModified(this, new TEventArgs<TileInformation>(new TileInformation(previous, value)));
        }

        public byte[,] GetData(int X, int Y, int Width, int Height)
        {
            byte[,] data = new byte[Width, Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    data[j, i] = LevelData[X + j, Y + i];
                }
            }

            return data;
        }

        public byte[] GetCompressedData()
        {
            byte[] data = new byte[9 * 16 * Length];
            int counter = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int y = 0; y < 9; y++)
                {
                    for (int x = 0; x < 16; x++)
                    {
                        data[counter++] = LevelData[(i * 16) + x, y + 0x11];
                    }
                }
            }

            return data;
        }
    }
}

