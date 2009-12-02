﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Level
    {
        public event EventHandler<TEventArgs<Sprite>> SpriteAdded;
        public event EventHandler<TEventArgs<Sprite>> SpriteRemoved;
        public event EventHandler<TEventArgs<Point>> TileChanged;
        public event EventHandler<TEventArgs<TileInformation>> TilesModified;
        public event EventHandler<TEventArgs<LevelPointer>> PointerAdded;
        public event EventHandler<TEventArgs<LevelPointer>> PointerRemoved;

        public Guid Guid { get; set; }
        public int Type { get;set; }
        public int ClearValue { get; set; }
        public int GraphicsBank { get; set; }
        public int AnimationBank { get; set; }
        public int Music { get; set; }
        public int Length { get; set; }
        public int Time { get; set; }
        public int XStart { get; set; }
        public int YStart { get; set; }
        public int StartAction { get; set; }
        public int ScrollType { get; set; }
        public byte Unused1 { get; set; }
        public byte Unused2 { get; set; }
        public byte Unused3 { get; set; }
        public int Palette { get; set; }
        public List<LevelPointer> Pointers { get; private set; }
        public byte[,] LevelData { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public byte[] CompressedData { get; private set; }
        public LevelSettings Settings { get; private set; }
        private bool ValidCompression;

        public List<Sprite> SpriteData { get; private set; }
        private LevelLayout _LevelLayout;

        public Level()
        {
            Pointers = new List<LevelPointer>();
            SpriteData = new List<Sprite>();
            ValidCompression = false;
            Settings = new LevelSettings();
        }

        public LevelLayout LevelLayout
        {
            get { return _LevelLayout; }
            set
            {
                _LevelLayout = value;
                switch (value)
                {
                    case LevelLayout.Horizontal:
                        LevelData = new byte[240, 27];
                        Width = 240;
                        Height = 27;
                        break;

                    case LevelLayout.Vertical:
                        LevelData = new byte[16, 225];
                        Width = 16;
                        Height = 225;
                        break;
                }
            }
        }


        public bool Save()
        {
            XDocument xDoc = new XDocument();
            XElement root = new XElement("level");
            root.SetAttributeValue("guid", Guid);
            root.SetAttributeValue("type", Type);
            root.SetAttributeValue("clearvalue", ClearValue);
            root.SetAttributeValue("graphicsbank", GraphicsBank);
            root.SetAttributeValue("music", Music);
            root.SetAttributeValue("length", Length);
            root.SetAttributeValue("time", Time);
            root.SetAttributeValue("xstart", XStart);
            root.SetAttributeValue("ystart", YStart);
            root.SetAttributeValue("unused1", Unused1);
            root.SetAttributeValue("unused2", Unused2);
            root.SetAttributeValue("unused3", Unused3);
            root.SetAttributeValue("palette", Palette);
            root.SetAttributeValue("animationbank", AnimationBank);
            root.SetAttributeValue("startaction", StartAction);
            root.SetAttributeValue("scrolltype", ScrollType);
            root.SetAttributeValue("layout", LevelLayout);
            root.SetAttributeValue("validcompression", ValidCompression);

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
            root.SetAttributeValue("leveldata", sb);

            sb.Length = 0;
            first = true;
            if (ValidCompression)
            {
                for (int i = 0; i < CompressedData.Length; i++)
                {
                    if (first)
                    {
                        sb.Append(CompressedData[i]);
                        first = false;
                    }
                    else
                    {
                        sb.Append("," + CompressedData[i]);
                    }
                }
                root.SetAttributeValue("compresseddata", sb);
            }

            XElement s = new XElement("spritedata");

            foreach (var spr in SpriteData)
            {
                s.Add(spr.CreateElement());
            }

            XElement p = new XElement("pointers");
            foreach (LevelPointer ptr in Pointers)
            {
                p.Add(ptr.CreateElement());
            }

            root.Add(p);
            root.Add(s);
            root.Add(Settings.CreateElement());
            string fileName = ProjectController.LevelDirectory + @"\" + Guid + ".lvl";
            xDoc.Add(root);
            xDoc.Save(fileName);

            return true;
        }

        public bool Load(LevelInfo li)
        {
            return Load(ProjectController.LevelDirectory + @"\" + li.LevelGuid + ".lvl");
        }
        
        public bool Load(string filename)
        {
            XDocument xDoc;
            string[] levelData = null;
            string[] compressData = null;
            SpriteData.Clear();
            
            try
            {
                xDoc = XDocument.Load(filename);
            }
            catch
            {
                return false;
            }

            XElement level = xDoc.Element("level");

            foreach (var a in level.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "layout":
                        LevelLayout = (LevelLayout)Enum.Parse(typeof(LevelLayout), a.Value, true);
                        break;

                    case "guid":
                        Guid = a.Value.ToGuid();
                        break;

                    case "type":
                        Type = a.Value.ToInt();
                        break;

                    case "clearvalue":
                        ClearValue = a.Value.ToInt();
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

                    case "time":
                        Time = a.Value.ToInt();
                        break;

                    case "xstart":
                        XStart = a.Value.ToInt();
                        break;

                    case "ystart":
                        YStart = a.Value.ToInt();
                        break;

                    case "unused1":
                        Unused1 = (byte)a.Value.ToInt();
                        break;

                    case "unused2":
                        Unused2 = (byte)a.Value.ToInt();
                        break;

                    case "unused3":
                        Unused3 = (byte)a.Value.ToInt();
                        break;

                    case "palette":
                        Palette = a.Value.ToInt();
                        break;

                    case "scrolltype":
                        ScrollType = a.Value.ToInt();
                        break;

                    case "animationbank":
                        AnimationBank = a.Value.ToInt();
                        break;

                    case "startaction":
                        StartAction = a.Value.ToInt();
                        break;

                    case "leveldata":
                        levelData = a.Value.Split(',');
                        break;

                    case "compresseddata":
                        compressData = a.Value.Split(',');
                        break;

                    case "validcompression":
                        ValidCompression = a.Value.ToBoolean();
                        break;
                }
            }

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

            if (compressData != null)
            {
                int index = 0;
                CompressedData = new byte[compressData.Length];
                foreach (var c in compressData)
                {
                    CompressedData[index++] = (byte) c.ToInt();
                }
            }

            foreach (var x in level.Elements())
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
                            LevelPointer p = new LevelPointer();
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
            LevelPointer p = new LevelPointer()
                {
                    ExitType = 0,
                    XExit = 0,
                    XEnter = 0,
                    YExit = 0,
                    YEnter = 0,
                    LevelGuid = Guid.Empty
                };

            Pointers.Add(p);

            if (PointerAdded != null)
            {
                PointerAdded(this, new TEventArgs<LevelPointer>(p));
            }
        }

        public void RemovePointer(LevelPointer p)
        {
            Pointers.Remove(p);
            if (PointerRemoved != null)
            {
                PointerRemoved(this, new TEventArgs<LevelPointer>(p));
            }
        }

        public void SetTile(int x, int y, byte value)
        {
            int previous = LevelData[x, y];
            if (LevelData[x, y] == value) return;
            LevelData[x, y] = value;
            if (TileChanged != null) TileChanged(this, new TEventArgs<Point>(new Point(x, y)));
            if (TilesModified != null) TilesModified(this, new TEventArgs<TileInformation>(new TileInformation(previous, value)));
            ValidCompression = false;
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
            if (ValidCompression) return CompressedData;
            switch (LevelLayout)
            {
                case LevelLayout.Horizontal:
                    CompressedData = GetCompressedDataHorizontal();
                    break;

                case LevelLayout.Vertical:
                    CompressedData = GetCompressedDataVertical();
                    break;
            }

            ValidCompression = true;
            return CompressedData;
        }

        public byte[] GetCompressedDataHorizontal()
        {
            int dataPointer = 0;
            byte[] outputData = new byte[8000];
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
                                else if (currentByte == ClearValue || parameter == 0x3F)
                                {
                                    if (clearCount == 1 || parameter == 0x3F)
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

                        if (j < 0)
                        {
                            j += 27;
                            i--;
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
                    outputData[dataPointer++] = (byte)(64 | nextChunk.Count);
                    for (int l = 0; l < nextChunk.Count; l++)
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

        public byte[] GetCompressedDataVertical()
        {
            int dataPointer = 0;
            byte[] outputData = new byte[5000];
            List<byte> nextChunk = new List<byte>();
            CompressionCommand currentCommand = CompressionCommand.None;
            byte parameter = 0;
            int previousValue = -1;
            int repeatCount = 0;
            int clearCount = 0;
            int y;
            for (int i = 0; i < this.Length; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    y = i * 15;

                    for (int k = 0; k < 16; k++)
                    {
                        byte currentByte = LevelData[k, y + j];

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
                                else if (currentByte == ClearValue || parameter == 0x3F)
                                {
                                    if (clearCount == 1 || parameter == 0x3F)
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

                        if (j < 0)
                        {
                            j += 27;
                            i--;
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

    public enum CompressionCommand
    {
        None = 5,
        Skip = 0,
        SkipRow = 1,
        Write = 2,
        Repeat = 3
    }

    public enum LevelLayout
    {
        Horizontal,
        Vertical
    }

    public enum AddressMode
    {
        Relative,
        Absolute
    }

    public struct TileInformation
    {
        public int Previous;
        public int Current;

        public TileInformation(int previous, int current)
        {
            Previous = previous;
            Current = current;
        }
    }
}
