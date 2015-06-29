using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reuben.Model;
using Reuben.NESGraphics;

namespace Reuben.Controllers
{
    public class RomController
    {
        private Dictionary<Guid, byte> levelIndexTable = new Dictionary<Guid, byte>();
        private Dictionary<byte, int> levelAddressTable = new Dictionary<byte, int>();
        private Dictionary<byte, int> levelTypeTable = new Dictionary<byte, int>();
        private Dictionary<Guid, byte> paletteIndexTable = new Dictionary<Guid, byte>();

        private ProjectController localProjectController;
        private LevelController localLevelController;
        private WorldController localWorldController;
        private StringController localStringController;
        private GraphicsController localGraphicsController;

        public string LastOutput { get; private set; }

        public bool BuildRomFromSource(string directory, string romFile)
        {
            if (!File.Exists(directory + @"\nesasm.exe"))
            {
                throw new Exception("nesasm.exe file not found.");
            }

            StringBuilder output = new StringBuilder();
            using (StringWriter sw = new StringWriter(output))
            {
                Console.SetOut(sw);
                Process.Start(directory + @"\nesasm.exe smb3.asm");
                sw.Flush();
            }

            LastOutput = output.ToString();
            if (LastOutput.Contains("error"))
            {
                return false;
            }

            string asmHack = directory + @"\smb3.nes";
            string dataHack = romFile;
            byte[] asmData = new byte[0xC0010];
            byte[] lvlData = new byte[0xC0010];
            FileStream fs = File.Open(asmHack, FileMode.Open, FileAccess.ReadWrite);
            fs.Read(asmData, 0, asmData.Length);
            fs.Close();
            fs = File.Open(dataHack, FileMode.Open, FileAccess.ReadWrite);
            fs.Read(lvlData, 0, lvlData.Length);
            fs.Close();

            // 
            for (int i = 0x1E010; i < 0x26010; i++)
            {
                asmData[i] = lvlData[i];
            }

            for (int i = 0x28010; i < 0x30010; i++)
            {
                asmData[i] = lvlData[i];
            }

            for (int i = 0; i < 0x4000; i++)
            {
                asmData[0x7C010 + i] = asmData[0x3C010 + i];
            }

            for (int i = 0x40010; i < 0x7C010; i++)
            {
                asmData[i] = lvlData[i];
            }

            for (int i = 0x80010; i < 0xC0010; i++)
            {
                asmData[i] = lvlData[i];
            }


            File.WriteAllBytes(dataHack, asmData);
            return true;
        }

        public int BuildRomFromAssets(ProjectController project, LevelController level, WorldController world, StringController strings, GraphicsController graphics)
        {
            localProjectController = project;
            localLevelController = level;
            localWorldController = world;
            localStringController = strings;
            localGraphicsController = graphics;

            byte[] data = new byte[786448];

            WritePalettes(data);
            WriteBlockData(data);

            levelIndexTable.Clear();
            levelTypeTable.Clear();
            levelAddressTable.Clear();

            byte levelIndex = 0;
            foreach (LevelInfo li in level.LevelData.Levels)
            {
                levelIndexTable.Add(li.ID, levelIndex);
            }

            BuildWorlds(data);
            int lastAddress = BuildLevels(data);
            File.WriteAllBytes(localProjectController.Project.RomFile, data);
            return 0x7C00F - lastAddress;
        }

        private void WriteBlockData(byte[] data)
        {
            int address = 0x1E010;
            for (int i = 0; i < 16; i++)
            {
                Block[] blockData = localLevelController.LevelData.Types[i].Blocks;
                for (int j = 0; j < 0x400; j++)
                {
                    data[address] = (byte)blockData[j].UpperLeft;
                    data[address + 0x100] = (byte)blockData[j].LowerLeft;
                    data[address + 0x200] = (byte)blockData[j].UpperRight;
                    data[address + 0x300] = (byte)blockData[j].LowerRight;

                    address++;
                }
            }

            address = 0x2C010;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 0x100; j++)
                {
                    Block b = localLevelController.LevelData.Types[i].Blocks[j];
                    data[address++] = (byte)(b.BlockSolidity + b.BlockInteraction);
                }
            }
        }

        private void WriteBlockInteractions(byte[] data, int address, List<LevelType> types)
        {
            int baseAddress = address;
            for (int i = 1; i < 16; i++)
            {
                address = baseAddress + (i * 0x100);
                LevelType type = types[i];
                foreach (var k in type.FireBlockActors)
                {
                    data[address++] = (byte)k.BlockValue;
                    data[address++] = (byte)k.ActsLikeBlockValue;
                }

                foreach (var k in type.IceBlockActors)
                {
                    data[address++] = (byte)k.BlockValue;
                    data[address++] = (byte)k.ActsLikeBlockValue;
                }


                foreach (var k in type.PSwitchBlockActors)
                {
                    data[address++] = (byte)k.BlockValue;
                    data[address++] = (byte)k.ActsLikeBlockValue; ;
                }
            }
        }

        private void BuildWorlds(byte[] data)
        {
            int address = 0x28010;
            int currentWriteAddress = 0x28010;
            int bank;
            foreach (WorldInfo info in localWorldController.WorldData.Worlds.OrderBy(w => w.WorldNumber))
            {
                World world = localWorldController.LoadWorld(info.File);
                bank = (byte)(address / 0x2000);
                address = (currentWriteAddress - 0x10 - (bank * 0x2000) + 0xA000);

                data[0x22010 + ((info.WorldNumber) * 4)] = (byte)bank;
                data[0x22011 + ((info.WorldNumber) * 4)] = (byte)(address & 0x00FF);
                data[0x22012 + ((info.WorldNumber) * 4)] = (byte)((address & 0xFF00) >> 8);

                address = WriteWorld(data, world, address);

                data[0x15610 + info.WorldNumber] = (byte)(world.NumberOfScreens << 4);
            }
        }

        public int WriteWorld(byte[] data, World world, int address)
        {
            data[address++] = (byte)world.GraphicsBankID;
            data[address++] = (byte)world.PaletteID;
            data[address++] = Convert.ToByte(localStringController.GetMappedStringValue("music", world.MusicID.ToString()));
            data[address++] = (byte)world.NumberOfScreens;
            data[address++] = (byte)world.Pointers.Count;

            foreach (var p in world.Pointers)
            {
                if (!levelIndexTable.ContainsKey(p.LevelID))
                {
                    data[address++] = (byte)0xFF;
                }
                else
                {
                    data[address++] = levelIndexTable[p.LevelID];
                }
                data[address++] = (byte)(((p.X & 0xF0) >> 4) | ((p.X & 0x0F) << 4));
                data[address++] = (byte)(p.Y - 0x0F);
            }

            byte[] mapData = localWorldController.GetCompressedData(world);
            for (int i = 0; i < mapData.Length; i++)
            {
                data[address++] = mapData[i];
            }

            data[address++] = (byte)0xFF;
            data[address++] = (byte)0xFF;
            return address;
        }

        private int BuildLevels(byte[] data)
        {
            Level level;
            int address = 0x40010;
            byte index = 0;
            foreach (LevelInfo info in localLevelController.LevelData.Levels)
            {
                level = localLevelController.LoadLevel(info.File);
                levelAddressTable.Add(levelIndexTable[level.ID], address);
                levelTypeTable.Add(index++, level.TypeID);
                address = WriteLevel(data, level, address, info.Name);
            }

            int bank;
            int lastLevelPointer = address;

            foreach (var key in levelAddressTable.Keys)
            {
                lastLevelPointer = levelAddressTable[key];
                bank = (byte)((lastLevelPointer - 0x10) / 0x2000);
                address = (lastLevelPointer - 0x10 - (bank * 0x2000) + 0xA000);
                data[0x24010 + (index * 4)] = (byte)bank;
                data[0x24011 + (index * 4)] = (byte)(address & 0x00FF);
                data[0x24012 + (index * 4)] = (byte)((address & 0xFF00) >> 8);
                data[0x24013 + (index * 4)] = (byte)levelTypeTable[key];
            }

            return lastLevelPointer;
        }

        public int WriteLevel(byte[] data, Level level, int address, string name)
        {
            name = name.ToUpper();
            if (name.Length > 0x22)
            {
                name = name.Substring(0, 0x22);
            }
            else
            {
                name = name.PadRight(0x22, ' ');
            }

            int yStart = 0;
            yStart = level.StartY - 1;

            data[address++] = (byte)localLevelController.GetClearTile(level);
            data[address++] = (byte)level.GraphicsID;
            data[address++] = (byte)paletteIndexTable[level.PaletteID];
            data[address++] = (byte)((level.AnimationType << 6) | (level.NumberOfScreens - 1));
            data[address++] = (byte)(byte)(((level.StartX & 0x0F) << 4) | ((level.StartX & 0xF0) >> 4)); ;
            data[address++] = (byte)(byte)(((yStart & 0x0F) << 4) | ((yStart & 0xF0) >> 4)); ;
            data[address++] = Convert.ToByte(localStringController.GetMappedStringValue("music", level.MusicID.ToString()));
            data[address++] = (byte)0;
            data[address++] = (byte)((level.Pointers.Count << 4) | level.ScrollType);
            data[address++] = (byte)((level.InvincibleEnemeies ? 0x80 : 0x00) | (level.TemporaryProjectileBlockChanges ? 0x40 : 0x00) | (level.RhythmPlatforms ? 0x20 : 0x00) | (level.DPadControlsTiles ? 0x10 : 0x00) | level.PaletteEffectType);
            data[address++] = (byte)level.MiscByte1;
            data[address++] = (byte)level.MiscByte2;
            data[address++] = (byte)level.MiscByte3;

            for (int i = 0; i < 0x22; i++)
            {
                data[address++] = (byte)name[i];
            }

            foreach (var p in level.Pointers.OrderBy(pt => pt.X).ThenBy(pt => pt.Y))
            {
                if (p.ExitLevel)
                {
                    data[address++] = (byte)p.WorldNumberToExitTo;
                }
                else
                {
                    if (!levelIndexTable.ContainsKey(p.ToLevelID))
                    {
                        data[address++] = 0;
                    }
                    else
                    {
                        data[address++] = levelIndexTable[p.ToLevelID];
                    }
                }

                int yExit = p.ExitY;
                if (!p.ExitLevel)
                {
                    switch (p.ExitType)
                    {
                        default:
                            yExit = p.ExitY;
                            break;
                        case 0:
                        case 2:
                        case 3:
                        case 4:
                            yExit = p.ExitY - 1;
                            break;


                    }
                }

                data[address++] = (byte)p.X;
                data[address++] = (byte)p.Y;
                data[address++] = (byte)(((p.ExitX & 0x0F) << 4) | ((p.ExitX & 0xF0) >> 4));
                if (p.ExitLevel)
                {
                    yExit += 2;
                }

                data[address++] = (byte)(((yExit & 0x0F) << 4) | ((yExit & 0xF0) >> 4));
                data[address++] = (byte)((p.ExitLevel ? 0x80 : 0x00) | (p.RedrawLevel ? 0x40 : 0x00) | (p.KeepObjectData ? 0x20 : 0x00) | (p.DisableWeather ? 0x10 : 0x00) | (p.ExitType));
            }

            byte[] levelData = localLevelController.GetCompressedData(level);
            for (int i = 0; i < levelData.Length; i++)
            {
                data[address++] = levelData[i];
            }

            data[address++] = (byte)0xFF;
            foreach (var s in from sprites in level.Sprites orderby sprites.X select sprites)
            {
                data[address++] = (byte)s.ObjectID;
                data[address++] = (byte)s.X;
                switch (s.ObjectID)
                {
                    case 0xA2:
                        if (s.Property % 2 != 0)
                        {
                            data[address++] = (byte)((s.Property << 5) | (s.Y + 2));
                        }
                        else
                        {
                            data[address++] = (byte)((s.Property << 5) | s.Y);
                        }
                        break;

                    case 0x6C:
                    case 0x6D:
                    case 0x6E:
                    case 0x6F:
                    case 0x80:
                    case 0xA5:
                    case 0xA7:
                        data[address++] = (byte)((s.Property << 5) | (s.Y + 1));
                        break;

                    case 0xA1:
                    case 0xA3:
                        data[address++] = (byte)((s.Property << 5) | (s.Y - 1));
                        break;

                    default:
                        data[address++] = (byte)((s.Property << 5) | s.Y);
                        break;
                }

            }

            data[address++] = 0xFF;
            return address;
        }

        public bool WritePalettes(byte[] data)
        {
            int address = 0x2A010;
            byte index = 0;
            foreach (var palette in localGraphicsController.GraphicsData.Palettes)
            {
                paletteIndexTable[palette.ID] = index++;
                for (int i = 0; i < 16; i++)
                {
                    data[address++] = (byte)palette.BackgroundValues[i];
                }

                for (int i = 0; i < 16; i++)
                {
                    data[address++] = (byte)palette.SpriteValues[i];
                }
            }

            return true;
        }
    }
}
