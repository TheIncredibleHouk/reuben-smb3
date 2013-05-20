using System;
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

        public Guid Guid { get; set; }
        public int Type { get; set; }
        public int ClearValue { get; set; }
        public int MostCommonTile { get; private set; }
        public int GraphicsBank { get; set; }
        public int AnimationBank { get; set; }
        public int Music { get; set; }
        public int Length { get; set; }
        public int Time { get; set; }
        public int XStart { get; set; }
        public int YStart { get; set; }
        public int XAltStart { get; set; }
        public int YAltStart { get; set; }
        public int StartAction { get; set; }
        public int ScrollType { get; set; }
        public bool InvincibleEnemies { get; set; }
        public bool VineBlocked { get; set; }
        public int Weather { get; set; }
        public int WindDirection { get; set; }
        public int WindSpeed { get; set; }
        public int Palette { get; set; }
        public List<LevelPointer> Pointers { get; private set; }
        public byte[,] LevelData { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int ChallengeType { get; set; }
        public int SpecialLevelType { get; set; }
        
        public int MiscByte1 { get; set; }
        public int MiscByte2 { get; set; }
        public int MiscByte3 { get; set; }

        public LevelSettings Settings { get; private set; }

        public List<Sprite> SpriteData { get; private set; }
        private LevelLayout _LevelLayout;

        public Level()
        {
            Pointers = new List<LevelPointer>();
            SpriteData = new List<Sprite>();
            Settings = new LevelSettings();
            MiscByte1 = MiscByte2 = MiscByte3 = 0;
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
            return Save(ProjectController.LevelDirectory + @"\" + Guid + ".lvl");
        }

        public bool Save(string fileName)
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
            root.SetAttributeValue("xaltstart", XAltStart);
            root.SetAttributeValue("yaltstart", YAltStart);
            root.SetAttributeValue("invincibleenemies", InvincibleEnemies);
            root.SetAttributeValue("vineblocked", VineBlocked);
            root.SetAttributeValue("weather", Weather);
            root.SetAttributeValue("winddirection", WindDirection);
            root.SetAttributeValue("windspeed", WindSpeed);
            root.SetAttributeValue("palette", Palette);
            root.SetAttributeValue("animationbank", AnimationBank);
            root.SetAttributeValue("startaction", StartAction);
            root.SetAttributeValue("scrolltype", ScrollType);
            root.SetAttributeValue("layout", LevelLayout);
            root.SetAttributeValue("challengeleveltype", ChallengeType);
            root.SetAttributeValue("specialleveltype", SpecialLevelType);
            root.SetAttributeValue("misc1", MiscByte1);
            root.SetAttributeValue("misc2", MiscByte2);
            root.SetAttributeValue("misc3", MiscByte3);

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
           
            xDoc.Add(root);
            xDoc.Save(fileName);

            ProjectController.LevelManager.GetLevelInfo(Guid).LastModified = DateTime.Now;
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
            Pointers.Clear();

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

                    case "xaltstart":
                        XAltStart = a.Value.ToInt();
                        break;

                    case "yaltstart":
                        YAltStart = a.Value.ToInt();
                        break;

                    case "invincibleenemies":
                        InvincibleEnemies = a.Value.ToBoolean();
                        break;
                        
                    case "vineblocked":
                        VineBlocked = a.Value.ToBoolean();
                        break;

                    case "weather":
                        Weather = a.Value.ToInt();
                        break;

                    case "winddirection":
                        WindDirection = a.Value.ToInt();
                        break;

                    case "windspeed":
                        WindSpeed = a.Value.ToInt();
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

                    case "challengeleveltype":
                        ChallengeType = a.Value.ToInt();
                        break;

                    case "specialleveltype":
                        SpecialLevelType = a.Value.ToInt();
                        break;

                    case "misc1":
                        MiscByte1 = a.Value.ToInt();
                        break;

                    case "misc2":
                        MiscByte2 = a.Value.ToInt();
                        break;
                        
                    case "misc3":
                        MiscByte3 = a.Value.ToInt();
                        break;
                }
            }

            int xPointer = 0, yPointer = 0;
            int[] tileCount = new int[256];
            foreach (var c in levelData)
            {
                LevelData[xPointer, yPointer] = (byte)c.ToInt();
                tileCount[c.ToInt()]++;
                xPointer++;

                if (xPointer >= Width)
                {
                    xPointer = 0;
                    yPointer++;
                    if (yPointer > Height) break;
                }
            }

            int highestTileCount = -1;
            for (int i = 0; i < 256; i++)
            {
                if (tileCount[i] > highestTileCount)
                {
                    highestTileCount = MostCommonTile = i;
                    
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
        }

        public void RemovePointer(LevelPointer p)
        {
            Pointers.Remove(p);
        }

        public void SetTile(int x, int y, byte value)
        {
            if (x < 240 && y < 27)
            {
                int previous = LevelData[x, y];
                if (LevelData[x, y] == value) return;
                LevelData[x, y] = value;
                if (TileChanged != null) TileChanged(this, new TEventArgs<Point>(new Point(x, y)));
                if (TilesModified != null) TilesModified(this, new TEventArgs<TileInformation>(new TileInformation(previous, value)));
            }
        }

        public byte[,] GetData(int X, int Y, int Width, int Height)
        {
            byte[,] data = new byte[Width, Height];
            for (int i = 0; i < Height && (i + Y < 27); i++)
            {
                for (int j = 0; j < Width && (j + X < 240); j++)
                {
                    data[j, i] = LevelData[X + j, Y + i];
                }
            }

            return data;
        }

        public byte[] GetCompressedData()
        {
            switch (LevelLayout)
            {
                case LevelLayout.Horizontal:
                    return GetCompressedDataHorizontal();
            }

            return null;
        }

        public byte[] GetCompressedDataHorizontal()
        {
            List<byte> returnBytes = new List<byte>();
            CompressionPoint restoreToPoint = new CompressionPoint();
            CompressionCommand currentCommand = null;
            CompressionCommand attemptCommand = null;
            CompressionCommand useCommand = null;

            ResetPoint();

            while (!currentPoint.EOD)
            {
                // we're assuming writeraw, if we find a better command, we'll stop writeraw and use the better command
                SavePoint();
                useCommand = null;
                attemptCommand = TryPattern();
                if (attemptCommand != null)
                {
                    useCommand = attemptCommand;
                    restoreToPoint = currentPoint;
                }


                RestorePoint();
                attemptCommand = TryRepeat();
                if (attemptCommand != null)
                {
                    if (useCommand != null)
                    {
                        if (useCommand.GetData().Length > attemptCommand.GetData().Length)
                        {
                            useCommand = attemptCommand;
                            restoreToPoint = currentPoint;
                        }
                    }
                    else
                    {
                        useCommand = attemptCommand;
                        restoreToPoint = currentPoint;
                    }
                }

                RestorePoint();
                attemptCommand = TrySkip();
                if (attemptCommand != null)
                {
                    if (useCommand != null)
                    {
                        if (useCommand.GetData().Length > attemptCommand.GetData().Length)
                        {
                            useCommand = attemptCommand;
                            restoreToPoint = currentPoint;
                        }
                    }
                    else
                    {
                        useCommand = attemptCommand;
                        restoreToPoint = currentPoint;
                    }
                }

                if (useCommand != null)
                {
                    if (currentCommand != null)
                    {
                        returnBytes.AddRange(currentCommand.GetData());
                        currentCommand = null;
                    }

                    returnBytes.AddRange(useCommand.GetData());
                    currentPoint = restoreToPoint;
                    continue;
                }

                // made it here, we need to write raw
                RestorePoint();
                if (currentCommand == null)
                {
                    currentCommand = new CompressionCommand();
                    currentCommand.CommandType = CompressionCommandType.WriteRaw;
                }

                currentCommand.Data.Add(NextByte());
                SavePoint();

                if (currentCommand.Data.Count == 0x40)
                {
                    returnBytes.AddRange(currentCommand.GetData());
                    currentCommand = null;
                    SavePoint();
                }
            }

            if (currentCommand != null)
            {
                returnBytes.AddRange(currentCommand.GetData());
            }
            return returnBytes.ToArray();
        }

        private CompressionCommand TrySkip()
        {
            byte repeatTile;
            int repeatCount = 0;
            repeatTile = (byte)MostCommonTile;
            while (repeatTile == NextByte() && repeatCount < 0x40)
            {
                repeatCount++;
            }

            // no well repeatable tiles, return null
            if (repeatCount < 2)
            {
                return null;
            }

            CompressionCommand c = new CompressionCommand();
            c.CommandType = CompressionCommandType.SkipTile;
            c.RepeatTimes = repeatCount;
            PreviousByte();
            return c;

        }

        private CompressionCommand TryRepeat()
        {
            byte repeatTile;
            int repeatCount = 1;
            repeatTile = NextByte();
            while (repeatTile == NextByte() && repeatCount < 0x40)
            {
                repeatCount++;
            }

            // no well repeatable tiles, return null
            if (repeatCount == 1)
            {
                return null;
            }

            CompressionCommand c = new CompressionCommand();
            c.Data.Add(repeatTile);
            c.CommandType = CompressionCommandType.RepeatTile;
            c.RepeatTimes = repeatCount;
            PreviousByte();
            return c;
        }

        // the most complicated of the commands, we test to see if any patterns exist. if no patterns exist within 16 tiles, we return null
        private CompressionCommand TryPattern()
        {
            byte[] patternChunk = null;
            byte[] smallestChunk = null;
            bool hasMatch = false;
            CompressionCommand command = new CompressionCommand();
            CompressionPoint localPoint = currentPoint;
            command.CommandType = CompressionCommandType.RepeatPattern;

            // basically, try patterns up to 16 in size. We want the smallest pattern that can be repeated
            // we breakt at 1 as a pattern of 1 is a RepeatTile command
            for (int i = 16; !currentPoint.EOD && i > 1; i--)
            {
                patternChunk = new byte[i];

                // reset pointer before getting next pattern
                RestorePoint();

                // get pattern
                for (int j = 0; !currentPoint.EOD && j < i; j++)
                {
                    patternChunk[j] = NextByte();
                }

                if (currentPoint.EOD)
                {
                    continue;
                }

                // assume there is a match, if no match, set false and break
                hasMatch = true;
                for (int k = 0; !currentPoint.EOD && k < i; k++)
                {
                    if (patternChunk[k] != NextByte())
                    {
                        hasMatch = false;
                        break;
                    }
                }

                if (hasMatch)
                {
                    // we have a match, set as smallest matchableChunk
                    smallestChunk = patternChunk;
                }
            }

            // no smallestChunk then there was no discernable pattern
            if (smallestChunk == null)
            {
                return null;
            }

            // ok so we DO have a smallest chunk, let's get the number of times this repeats
            int repeatCount = 0;
            RestorePoint();

            // for a pattern repeat to exist we have to repeat at least twice, so 0x00 = 2 repeats, 0x03 = 6 repeats
            bool noRepeat = false;
            while (repeatCount < 0x40 && !noRepeat)
            {
                localPoint = currentPoint;
                for (int i = 0; i < smallestChunk.Length; i++)
                {
                    if (smallestChunk[i] != NextByte())
                    {
                        noRepeat = true;

                        break;
                    }
                }

                if (!noRepeat)
                {
                    // we made it here, so one pattern was found, yay!
                    repeatCount++;
                    localPoint = currentPoint;
                }
            }

            // return pointer back to the point that we tried last match
            currentPoint = localPoint;
            command.Data.AddRange(smallestChunk);
            command.RepeatTimes = repeatCount;
            return command;
        }

        private static CompressionPoint currentPoint, savedPoint;

        private void ResetPoint()
        {
            currentPoint = new CompressionPoint();
        }

        private void SavePoint()
        {
            savedPoint = currentPoint;
        }


        private void RestorePoint()
        {
            currentPoint = savedPoint;
        }

        private byte NextByte()
        {
            if (currentPoint.EOD)
            {
                return 0xFF;
            }

            byte data = LevelData[currentPoint.XPointer + (currentPoint.PagePointer * 0x10), currentPoint.YPointer];
            currentPoint.XPointer++;
            if (currentPoint.XPointer >= 0x10)
            {
                currentPoint.XPointer = 0;
                currentPoint.YPointer++;
                if (currentPoint.YPointer >= 27)
                {
                    currentPoint.YPointer = 0;
                    currentPoint.PagePointer++;
                    if (currentPoint.PagePointer >= Length)
                    {
                        currentPoint.EOD = true;
                    }
                }
            }

            return data;
        }

        private void PreviousByte()
        {
            currentPoint.XPointer--;
            if (currentPoint.XPointer < 0)
            {
                currentPoint.XPointer = 0x0F;
                currentPoint.YPointer--;
                if (currentPoint.YPointer < 0)
                {
                    currentPoint.YPointer = 26;
                    currentPoint.PagePointer--;
                }
            }
        }
    }

    public enum LevelLayout
    {
        Horizontal,
        Vertical
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
