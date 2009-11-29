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
        public event EventHandler<TEventArgs<WorldPointer>> PointerAdded;
        public event EventHandler<TEventArgs<WorldPointer>> PointerRemoved;

        public Guid Guid { get; set; }
        public int Type { get { return 0; } }
        public int ClearValue { get { return 0x02; } }
        public int GraphicsBank { get; set; }
        public int AnimationBank { get; set; }
        public int Music { get; set; }
        public int Length { get; set; }
        public int XStart { get; set; }
        public int YStart { get; set; }
        public byte Unused1 { get; set; }
        public int Palette { get; set; }
        public List<WorldPointer> Pointers { get; private set; }
        public byte[,] LevelData { get; private set; }
        public List<Sprite> SpriteData { get; private set; }

        public World()
        {
            Pointers = new List<WorldPointer>();
            SpriteData = new List<Sprite>();
            LevelData = new byte[0x40, 0x1B];
        }

        public void New(WorldInfo wi)
        {
            Guid = wi.WorldGuid;
            Length = 1;
            Palette = 0;
            XStart = 0x02;
            YStart = 0x14;
            GraphicsBank = 0x14;
            AnimationBank = 0x16;
            for (int i = 0; i < 0x10; i++)
            {
                for (int j = 0; j < 0x40; j++)
                {
                    LevelData[j, i] = 0x02;
                }
            }

            for(int i = 0; i < 0x40; i++)
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
            XDocument xDoc = new XDocument();
            XElement root = new XElement("world");
            root.SetAttributeValue("guid", Guid);
            root.SetAttributeValue("type", Type);
            root.SetAttributeValue("clearvalue", ClearValue);
            root.SetAttributeValue("graphicsbank", GraphicsBank);
            root.SetAttributeValue("music", Music);
            root.SetAttributeValue("length", Length);
            root.SetAttributeValue("xstart", XStart);
            root.SetAttributeValue("ystart", YStart);
            root.SetAttributeValue("unused1", Unused1);
            root.SetAttributeValue("palette", Palette);
            root.SetAttributeValue("animationbank", AnimationBank);

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
            string fileName = ProjectController.WorldDirectory + @"\" + Guid + ".map";
            xDoc.Add(root);
            xDoc.Save(fileName);

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
            Guid = world.Attribute("guid").Value.ToGuid();
            GraphicsBank = world.Attribute("graphicsbank").Value.ToInt();
            Music = world.Attribute("music").Value.ToInt();
            Length = world.Attribute("length").Value.ToInt();
            XStart = world.Attribute("xstart").Value.ToInt();
            YStart = world.Attribute("ystart").Value.ToInt();
            Unused1 = (byte) world.Attribute("unused1").Value.ToInt();
            Palette = world.Attribute("palette").Value.ToInt();
            AnimationBank = world.Attribute("animationbank").Value.ToInt();

            string[] levelData = world.Attribute("worlddata").Value.Split(',');

            int xPointer = 0, yPointer = 0;
            foreach(var c in levelData)
            {
                LevelData[xPointer, yPointer] = (byte) c.ToInt();
                xPointer++;

                if(xPointer >= Width)
                {
                    xPointer = 0;
                    yPointer++;
                    if (yPointer > Height) break;
                }
            }

            foreach (var spr in world.Element("spritedata").Elements("sprite"))
            {
                Sprite s = new Sprite();
                s.LoadFromElement(spr);
                SpriteData.Add(s);
            }

            foreach(var ptr in world.Element("pointers").Elements("pointer"))
            {
                WorldPointer p = new WorldPointer();
                p.LoadFromElement(ptr);
                Pointers.Add(p);
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

            if (PointerAdded != null)
            {
                PointerAdded(this, new TEventArgs<WorldPointer>(p));
            }
        }

        public void RemovePointer(WorldPointer p)
        {
            Pointers.Remove(p);
            if (PointerRemoved != null)
            {
                PointerRemoved(this, new TEventArgs<WorldPointer>(p));
            }
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
            int dataPointer = 0;
            byte[] outputData = new byte[5000];
            List<byte> nextChunk = new List<byte>();
            CompressionCommand currentCommand = CompressionCommand.None;
            byte parameter = 0;
            int previousValue = -1;
            int repeatCount = 0;
            int clearCount = 0;
            int x;
            int breakAt = Length - 1;
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < 27; j++)
                {
                    x = i * 16;

                    for (int k = 0; k < 16; k++)
                    {
                        byte currentByte = LevelData[x + k, j];

                        switch (currentCommand)
                        {
                            case CompressionCommand.None:
                                nextChunk.Clear();
                                if (currentByte == ClearValue)
                                {
                                    currentCommand = CompressionCommand.Skip;
                                    nextChunk.Add(currentByte);
                                    parameter = 1;
                                }

                                else
                                {
                                    currentCommand = CompressionCommand.Write;
                                    nextChunk.Add(currentByte);
                                    parameter = 1;
                                    repeatCount = 0;
                                }
                                break;

                            case CompressionCommand.Skip:
                                if (currentByte == ClearValue && parameter < 0x3F)
                                {
                                    nextChunk.Add(currentByte);
                                    parameter++;
                                }
                                else
                                {
                                    outputData[dataPointer++] = (byte)(0 | parameter);
                                    currentCommand = CompressionCommand.None;
                                    k--;
                                }
                                break;

                            case CompressionCommand.Write:
                                if (currentByte == previousValue && currentByte != ClearValue)
                                {
                                    repeatCount++;
                                    clearCount = 0;

                                    if (parameter == 1)
                                    {
                                        currentCommand = CompressionCommand.Repeat;
                                        parameter++;
                                    }

                                    else if (repeatCount < 2)
                                    {
                                        parameter++;
                                        nextChunk.Add(currentByte);
                                    }
                                    else
                                    {
                                        nextChunk.RemoveAt(nextChunk.Count - 1);
                                        nextChunk.RemoveAt(nextChunk.Count - 1);

                                        outputData[dataPointer++] = (byte)(64 | nextChunk.Count);
                                        for (int l = 0; l < nextChunk.Count; l++)
                                        {
                                            outputData[dataPointer++] = nextChunk[l];
                                        }

                                        currentCommand = CompressionCommand.Repeat;
                                        parameter = 3;
                                        repeatCount = 0;
                                    }
                                }
                                else if (currentByte == ClearValue || parameter == 0x40)
                                {
                                    if (clearCount == 1 || parameter == 0x40)
                                    {
                                        if (clearCount == 1)
                                        {
                                            nextChunk.RemoveAt(nextChunk.Count - 1);
                                        }

                                        outputData[dataPointer++] = (byte)(64 | nextChunk.Count);
                                        for (int l = 0; l < nextChunk.Count; l++)
                                        {
                                            outputData[dataPointer++] = nextChunk[l];
                                        }

                                        currentCommand = CompressionCommand.None;
                                        if (clearCount == 1)
                                        {
                                            k--;
                                        }

                                        k--;
                                        clearCount = 0;
                                    }
                                    else
                                    {
                                        clearCount++;
                                        nextChunk.Add(currentByte);
                                        parameter++;
                                        repeatCount = 0;
                                    }

                                }
                                else
                                {
                                    nextChunk.Add(currentByte);
                                    parameter++;
                                    repeatCount = 0;
                                    clearCount = 0;
                                }
                                break;

                            case CompressionCommand.Repeat:
                                if (currentByte == previousValue && parameter < 0x3F)
                                {
                                    parameter++;
                                }
                                else
                                {
                                    outputData[dataPointer++] = (byte)(128 | parameter);
                                    outputData[dataPointer++] = (byte)previousValue;
                                    currentCommand = CompressionCommand.None;
                                    k--;
                                }
                                break;
                        }

                        if (k < 0)
                        {
                            k += 0x10;
                            j--;
                        }
                        previousValue = currentByte;
                    }
                }
            }

            switch (currentCommand)
            {
                case CompressionCommand.Skip:
                    outputData[dataPointer++] = (byte)(0 | parameter);
                    break;

                case CompressionCommand.Repeat:
                    outputData[dataPointer++] = (byte)(128 | parameter);
                    outputData[dataPointer++] = (byte)previousValue;
                    break;

                case CompressionCommand.Write:
                    outputData[dataPointer++] = (byte)(64 | parameter);
                    for (int l = 0; l < parameter; l++)
                    {
                        outputData[dataPointer++] = nextChunk[l];
                    }
                    break;
            }

            byte[] returnData = new byte[dataPointer];

            for (int i = 0; i < dataPointer; i++)
            {
                returnData[i] = outputData[i];
            }

            return returnData;
        }
    }
}

