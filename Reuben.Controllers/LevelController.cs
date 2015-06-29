using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using Reuben.Model;

namespace Reuben.Controllers
{
    public class LevelController
    {
        public LevelData LevelData { get; set; }
        private string lastFile;

        public LevelController()
        {
            LevelData = new LevelData();
        }

        public void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            lastFile = fileName;
            LevelData = JsonConvert.DeserializeObject<LevelData>(File.ReadAllText(fileName));
        }

        public void Save()
        {
            Save(lastFile);
        }

        public void Save(string fileName)
        {
            lastFile = fileName;
            File.WriteAllText(fileName, JsonConvert.SerializeObject(LevelData, Formatting.Indented));
        }

        public void SaveLevel(Level level)
        {
            LevelInfo info = GetLevelInfoByID(level.ID);
            File.WriteAllText(info.File, JsonConvert.SerializeObject(level));
        }

        public Level LoadLevel(string fileName)
        {
            return JsonConvert.DeserializeObject<Level>(File.ReadAllText(fileName));
        }

        public LevelInfo GetLevelInfoByID(Guid id)
        {
            return LevelData.Levels.Where(l => l.ID == id).FirstOrDefault();
        }

        public byte GetClearTile(Level level)
        {
            Dictionary<byte, int> tileCount = new Dictionary<byte, int>();
            foreach (byte value in level.Data)
            {
                if (!tileCount.ContainsKey(value))
                {
                    tileCount[value] = 0;
                }

                tileCount[value]++;
            }

            int highestTileCount = -1;
            for (int i = 0; i < 256; i++)
            {
                if (tileCount[(byte)i] > highestTileCount)
                {
                    highestTileCount = i;

                }
            }

            return (byte)highestTileCount;
        }


        public byte[] GetCompressedData(Level level)
        {
            List<byte> returnBytes = new List<byte>();
            CompressionPoint restoreToPoint = new CompressionPoint();
            CompressionCommand currentCommand = null;
            CompressionCommand attemptCommand = null;
            CompressionCommand useCommand = null;
            int mostCommonTile = GetClearTile(level);

            ResetPoint();

            while (!currentPoint.EOD)
            {
                // we're assuming writeraw, if we find a better command, we'll stop writeraw and use the better command
                SavePoint();
                useCommand = null;
                attemptCommand = TryPattern(level);
                if (attemptCommand != null)
                {
                    useCommand = attemptCommand;
                    restoreToPoint = currentPoint;
                }


                RestorePoint();
                attemptCommand = TryRepeat(level);
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
                attemptCommand = TrySkip(level, mostCommonTile);
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

                currentCommand.Data.Add(NextByte(level));
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

        private CompressionPoint currentPoint, savedPoint;

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

        private byte NextByte(Level level)
        {
            if (currentPoint.EOD)
            {
                return 0xFF;
            }

            byte value = level.Data[currentPoint.XPointer + (currentPoint.PagePointer * 0x10), currentPoint.YPointer];
            currentPoint.XPointer++;
            if (currentPoint.XPointer >= 0x10)
            {
                currentPoint.XPointer = 0;
                currentPoint.YPointer++;
                if (currentPoint.YPointer >= 27)
                {
                    currentPoint.YPointer = 0;
                    currentPoint.PagePointer++;
                    if (currentPoint.PagePointer >= level.NumberOfScreens)
                    {
                        currentPoint.EOD = true;
                    }
                }
            }

            return value;
        }

        private CompressionCommand TrySkip(Level level, int mostCommonTile)
        {
            byte repeatTile;
            int repeatCount = 0;
            repeatTile = (byte)mostCommonTile;
            while (repeatTile == NextByte(level) && repeatCount < 0x40)
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

        private CompressionCommand TryRepeat(Level level)
        {
            byte repeatTile;
            int repeatCount = 1;
            repeatTile = NextByte(level);
            while (repeatTile == NextByte(level) && repeatCount < 0x3F)
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
        private CompressionCommand TryPattern(Level level)
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
                    patternChunk[j] = NextByte(level);
                }

                if (currentPoint.EOD)
                {
                    continue;
                }

                // assume there is a match, if no match, set false and break
                hasMatch = true;
                for (int k = 0; !currentPoint.EOD && k < i; k++)
                {
                    if (patternChunk[k] != NextByte(level))
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
            while (repeatCount < 0x3F && !noRepeat)
            {
                localPoint = currentPoint;
                for (int i = 0; i < smallestChunk.Length; i++)
                {
                    if (smallestChunk[i] != NextByte(level))
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
}
