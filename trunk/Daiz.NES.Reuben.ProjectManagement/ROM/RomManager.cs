using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class ROMManager
    {
        Dictionary<Guid, byte> levelIndexTable;
        Dictionary<Guid, byte> worldIndexTable;
        Dictionary<byte, int> levelAddressTable;
        Dictionary<byte, int> levelTypeTable;

        public string ErrorMessage { get; private set; }

        public Rom Rom;
        private int levelDataPointer;

        public ROMManager()
        {
            levelIndexTable = new Dictionary<Guid, byte>();
            worldIndexTable = new Dictionary<Guid, byte>();
            levelAddressTable = new Dictionary<byte, int>();
            levelTypeTable = new Dictionary<byte, int>();
        }

        public bool CompileRom(string fileName, bool includeGfx)
        {
            Rom = new Rom();
            if (!Rom.Load(fileName)) return false;
            //if (!Rom.IsPatchedRom)
            //{
            //    ErrorMessage = "Rom has not been patched. Please patch with Reuben.ips";
            //    return false;
            //}

            //if (Rom.IsClean)
            //{
            //    Rom.Sign(ProjectController.ProjectManager.CurrentProject.Guid);
            //}

            //if (!VerifyRomGuid(ProjectController.ProjectManager.CurrentProject.Guid)) return false;
            Rom.ProtectionMode = RomWriteProtection.PaletteData;
            WritePalettes(ProjectController.PaletteManager.Palettes);


            Rom.ProtectionMode = RomWriteProtection.TSAData;
            SaveTSA();

            byte levelIndex = 0;
            foreach (LevelInfo li in ProjectController.LevelManager.Levels)
            {
                levelIndexTable.Add(li.LevelGuid, levelIndex);
                levelTypeTable.Add(levelIndex++, li.LevelType);
            }

            levelDataPointer = 0x28010;
            CompileWorlds();

            levelDataPointer = 0x40010;
            CompileLevels();

            if (includeGfx)
            {
                Rom.ProtectionMode = RomWriteProtection.GraphicsData;
                SaveGraphics();
            }

            Rom.Save();
            return true;
        }

        private bool CompileLevels()
        {

            Level l = new Level();
            Rom.ProtectionMode = RomWriteProtection.LevelData;
            foreach (LevelInfo li in ProjectController.LevelManager.Levels)
            {
                l.Load(li);
                levelAddressTable.Add(levelIndexTable[l.Guid], levelDataPointer);
                levelDataPointer = WriteLevel(l, levelDataPointer);
                if (levelDataPointer >= 0xFC000)
                    return false;
            }

            int bank, address;

            Rom.ProtectionMode = RomWriteProtection.LevelPointers;
            foreach (var index in levelAddressTable.Keys)
            {
                levelDataPointer = levelAddressTable[index];
                bank = (byte)(levelDataPointer / 0x2000);
                address = (levelDataPointer - 0x10 - (bank * 0x2000) + 0xA000);
                Rom[0x24010 + (index * 4)] = (byte)bank;
                Rom[0x24011 + (index * 4)] = (byte)(address & 0x00FF);
                Rom[0x24012 + (index * 4)] = (byte)((address & 0xFF00) >> 8);
                Rom[0x24013 + (index * 4)] = (byte)levelTypeTable[index];
            }
            return true;
        }

        private bool CompileWorlds()
        {
            World w = new World();
            int bank, address;
            foreach (WorldInfo wi in from world in ProjectController.WorldManager.Worlds orderby world.Ordinal select world)
            {
                if (w.Load(wi))
                {
                    worldIndexTable.Add(wi.WorldGuid, (byte)wi.Ordinal);
                    bank = (byte)(levelDataPointer / 0x2000);
                    address = (levelDataPointer - 0x10 - (bank * 0x2000) + 0xA000);

                    Rom.ProtectionMode = RomWriteProtection.WorldPointers;
                    Rom[0x22010 + ((wi.Ordinal) * 4)] = (byte)bank;
                    Rom[0x22011 + ((wi.Ordinal) * 4)] = (byte)(address & 0x00FF);
                    Rom[0x22012 + ((wi.Ordinal) * 4)] = (byte)((address & 0xFF00) >> 8);

                    Rom.ProtectionMode = RomWriteProtection.LevelData;
                    levelDataPointer = WriteWorld(w, levelDataPointer);

                    Rom.ProtectionMode = RomWriteProtection.AnyData;
                    Rom[0x15610 + wi.Ordinal] = (byte)(w.Length << 4);

                    if (levelDataPointer >= 0xFC000)
                        return false;
                }
            }


            return true;
        }


        public bool VerifyRomGuid(Guid projectGuid)
        {
            byte[] guid = new byte[16];
            for (int i = 0; i < 16; i++)
            {
                guid[i] = Rom[0xFC000 + i];
            }

            Guid compareGuid = new Guid(guid);
            return compareGuid == projectGuid;
        }



        public int WriteLevel(Level l, int levelAddress)
        {
            int yStart = 0;
            int ayStart = 0;

            switch (l.StartAction)
            {
                default:
                    yStart = l.YStart - 1;
                    ayStart = l.YAltStart - 1;
                    break;

                case 2:
                    yStart = l.YStart;
                    ayStart = l.YAltStart;
                    break;
            }
            Rom[levelAddress++] = (byte)l.MostCommonTile;
            Rom[levelAddress++] = (byte)l.GraphicsBank;
            Rom[levelAddress++] = (byte)l.Palette;
            Rom[levelAddress++] = (byte)((l.StartAction << 4) | (l.Length - 1));
            Rom[levelAddress++] = (byte)(byte)(((l.XStart & 0x0F) << 4) | ((l.XStart & 0xF0) >> 4)); ;
            Rom[levelAddress++] = (byte)(byte)(((yStart & 0x0F) << 4) | ((yStart & 0xF0) >> 4)); ;
            Rom[levelAddress++] = (byte)(byte)(((l.XAltStart & 0x0F) << 4) | ((l.XAltStart & 0xF0) >> 4)); ;
            Rom[levelAddress++] = (byte)(byte)(((ayStart & 0x0F) << 4) | ((ayStart & 0xF0) >> 4)); ;
            Rom[levelAddress++] = (byte)(ProjectController.MusicManager.MusicList[l.Music].Value);
            Rom[levelAddress++] = (byte)(((l.Time / 100) << 4) | ((l.Time - ((l.Time / 100) * 100)) / 10));
            Rom[levelAddress++] = (byte)((l.Pointers.Count << 4) | l.ScrollType);
            Rom[levelAddress++] = (byte)((l.InvincibleEnemies ? 0x80 : 0x00) | (l.Weather << 5) | (l.WindDirection << 4) | (l.WindSpeed));


            foreach (var p in l.Pointers)
            {
                if (p.ExitsLevel)
                {
                    Rom[levelAddress++] = (byte)p.World;
                }
                else
                {
                    if (!levelIndexTable.ContainsKey(p.LevelGuid))
                    {
                        Rom[levelAddress++] = 0;
                    }
                    else
                    {
                        Rom[levelAddress++] = levelIndexTable[p.LevelGuid];
                    }
                }

                int yExit = p.YExit - 1;
                if (!p.ExitsLevel)
                {
                    switch (p.ExitType)
                    {
                        default:
                            yExit = p.YExit;
                            break;

                        case 1:
                            yExit = p.YExit - 1;
                            break;
                    }
                }

                Rom[levelAddress++] = (byte)p.XEnter;
                Rom[levelAddress++] = (byte)p.YEnter;
                Rom[levelAddress++] = (byte)(((p.XExit & 0x0F) << 4) | ((p.XExit & 0xF0) >> 4));
                if (p.ExitsLevel)
                {
                    yExit += 3;
                }

                Rom[levelAddress++] = (byte)(((yExit & 0x0F) << 4) | ((yExit & 0xF0) >> 4));
                Rom[levelAddress++] = (byte)((p.ExitsLevel ? 0x80 : 0x00) | (p.ExitType + 1));
            }

            byte[] levelData = l.GetCompressedData();
            for (int i = 0; i < levelData.Length; i++)
            {
                Rom[levelAddress++] = levelData[i];
            }

            Rom[levelAddress++] = (byte)0xFF;


            if (l.SpecialLevelType > 0)
            {
                l.SpriteData.Add(new Sprite() { InGameID = 0x34, X = l.SpecialLevelType - 1 });
            }

            if (l.ChallengeType > 0)
            {
                l.SpriteData.Add(new Sprite() { InGameID = 0xD4, Y = l.ChallengeType });
            }

            switch (l.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    foreach (var s in from sprites in l.SpriteData orderby sprites.X select sprites)
                    {
                        Rom[levelAddress++] = (byte)s.InGameID;
                        Rom[levelAddress++] = (byte)s.X;
                        switch (s.InGameID)
                        {
                            case 0x6C:
                            case 0x6D:
                            case 0x6E:
                            case 0x6F:
                            case 0x80:
                            case 0xA5:
                            case 0xA7:
                                Rom[levelAddress++] = (byte)(s.Y + 1);
                                break;

                            default:
                                Rom[levelAddress++] = (byte)s.Y;
                                break;
                        }

                    }
                    break;

                case LevelLayout.Vertical:
                    foreach (var s in from sprites in l.SpriteData orderby sprites.Y select sprites)
                    {
                        Rom[levelAddress++] = (byte)s.InGameID;
                        Rom[levelAddress++] = (byte)s.X;
                        switch (s.InGameID)
                        {
                            case 0x6C:
                            case 0x6D:
                            case 0x6E:
                            case 0x6F:
                            case 0x80:
                                Rom[levelAddress++] = (byte)(s.Y + 1);
                                break;

                            default:
                                Rom[levelAddress++] = (byte)s.Y;
                                break;
                        }
                    }
                    break;
            }
            Rom[levelAddress++] = 0xFF;
            return levelAddress;
        }


        public int WriteWorld(World w, int levelAddress)
        {
            Rom[levelAddress++] = (byte)w.GraphicsBank;
            Rom[levelAddress++] = (byte)w.Palette;
            Rom[levelAddress++] = (byte)(ProjectController.MusicManager.MusicList[w.Music].Value);
            Rom[levelAddress++] = (byte)w.Length;
            Rom[levelAddress++] = (byte)w.Pointers.Count;

            foreach (var p in w.Pointers)
            {
                if (!levelIndexTable.ContainsKey(p.LevelGuid))
                {
                    Rom[levelAddress++] = (byte)0xFF;
                }
                else
                {
                    Rom[levelAddress++] = levelIndexTable[p.LevelGuid];
                }
                Rom[levelAddress++] = (byte)(((p.X & 0xF0) >> 4) | ((p.X & 0x0F) << 4));
                Rom[levelAddress++] = (byte)((p.AltLevelEntrance ? 0x80 : 0x00) | (p.Y - 0x0F));
            }

            byte[] levelData = w.GetCompressedData();
            for (int i = 0; i < levelData.Length; i++)
            {
                Rom[levelAddress++] = levelData[i];
            }

            Rom[levelAddress++] = (byte)0xFF;
            foreach (var s in from sprites in w.SpriteData orderby sprites.X select sprites)
            {
                Rom[levelAddress++] = (byte)s.InGameID;
                Rom[levelAddress++] = (byte)s.X;
                Rom[levelAddress++] = (byte)(s.Y - 0x0F);
            }

            Rom[levelAddress++] = (byte)0xFF;
            return levelAddress;
        }

        public bool WritePalettes(List<PaletteInfo> paletteInfo)
        {
            int address = 0x2A010;
            foreach (var p in paletteInfo)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Rom[address++] = (byte)p[i, j];
                    }
                }
            }

            return true;
        }



        private void SaveGraphics()
        {
            List<GraphicsBank> allGfx = ProjectController.GraphicsManager.GraphicsBanks;
            int dataPointer = 0x80010;
            foreach (var b in allGfx)
            {
                byte[] bankData = b.GetInterpolatedData();
                for (int i = 0; i < 1024; i++)
                {
                    Rom[dataPointer++] = bankData[i];
                }
            }
        }

        private void SaveTSA()
        {
            List<BlockDefinition> lookupTable = ProjectController.BlockManager.AllDefinitions;
            int dataPointer = 0x1E010;
            for (int i = 0; i < 14; i++)
            {
                byte[] blockData = lookupTable[i].GetBlockData();
                for (int j = 0; j < 0x400; j++)
                {
                    Rom[dataPointer++] = blockData[j];
                }
            }
        }
    }
}
