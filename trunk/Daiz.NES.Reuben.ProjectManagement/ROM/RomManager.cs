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
            if (!Rom.IsPatchedRom)
            {
                ErrorMessage = "Rom has not been patched. Please patch with Reuben.ips";
                return false;
            }

            if (Rom.IsClean)
            {
                Rom.Sign(ProjectController.ProjectManager.CurrentProject.Guid);
            }

            //if (!VerifyRomGuid(ProjectController.ProjectManager.CurrentProject.Guid)) return false;



            levelDataPointer = 0x42010;
            byte levelIndex = 0;
            foreach (LevelInfo li in ProjectController.LevelManager.Levels)
            {
                levelIndexTable.Add(li.LevelGuid, levelIndex);
                levelTypeTable.Add(levelIndex++, li.LevelType);
            }

            CompileWorlds();
            CompileLevels();

            if (includeGfx)
            {
                Rom.ProtectionMode = RomWriteProtection.GraphicsData;
                SaveGraphics();
            }

            Rom.ProtectionMode = RomWriteProtection.PaletteData;
            WritePalette(ProjectController.PaletteManager.Palettes);

            Rom.ProtectionMode = RomWriteProtection.TSAData;
            SaveTSA();

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
                Rom[0x18C10 + (index * 4)] = (byte)bank;
                Rom[0x18C11 + (index * 4)] = (byte)((address & 0xFF00) >> 8);
                Rom[0x18C12 + (index * 4)] = (byte)(address & 0x00FF);
                Rom[0x18C13 + (index * 4)] = (byte)levelTypeTable[index];
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
                    bank = (byte)((levelDataPointer & 0x40FFF) / 0x2000);
                    address = (levelDataPointer - 0x10 - (bank * 0x2000) + 0xA000);

                    Rom.ProtectionMode = RomWriteProtection.WorldPointers;
                    Rom[0x18BD0 + ((wi.Ordinal) * 4)] = (byte)bank;
                    Rom[0x18BD1 + ((wi.Ordinal) * 4)] = (byte)((address & 0xFF00) >> 8);
                    Rom[0x18BD2 + ((wi.Ordinal) * 4)] = (byte)(address & 0x00FF);

                    Rom.ProtectionMode = RomWriteProtection.LevelData;
                    levelDataPointer = WriteWorld(w, levelDataPointer);

                    Rom.ProtectionMode = RomWriteProtection.AnyData;
                    Rom[0x15610 + wi.Ordinal] = (byte) (w.Length << 4);
                    Rom[0x17CD0 + wi.Ordinal] = (byte)((w.YStart - 0x0F) << 4);
                    
                    Rom[0x17CE0 + wi.Ordinal] = (byte)((w.XStart & 0x0F) << 4);
                    Rom[0x17CF0 + wi.Ordinal] = (byte)(w.XStart & 0xF0);

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
            int yStart = l.YStart - 1;
            Rom[levelAddress++] = (byte) l.ClearValue;
            Rom[levelAddress++] = (byte) l.GraphicsBank;
            Rom[levelAddress++] = (byte) l.Palette;
            Rom[levelAddress++] = (byte)((l.StartAction << 4) | l.Type);
            Rom[levelAddress++] = (byte)(((l.XStart & 0x0F) << 4) | ((l.XStart & 0xF0) >> 4));
            Rom[levelAddress++] = (byte)(((yStart & 0x0F) << 4) | ((yStart & 0xF0) >> 4));
            Rom[levelAddress++] = (byte)(ProjectController.MusicManager.MusicList[l.Music].Value);
            Rom[levelAddress++] = (byte)(((l.Time / 100) << 4) | ((l.Time - ((l.Time / 100) * 100)) / 10));

            switch (l.LevelLayout)
            {
                case LevelLayout.Horizontal:
                    Rom[levelAddress++] = (byte)((l.ScrollType << 4) | (l.Length - 1));
                    break;

                case LevelLayout.Vertical:
                    Rom[levelAddress++] = (byte)((0x80) | (l.Length - 1));
                    break;
            }

            Rom[levelAddress++] = (byte)l.Unused1;
            Rom[levelAddress++] = (byte)l.Unused2;
            Rom[levelAddress++] = (byte)l.Unused3;

            foreach (var p in l.Pointers)
            {
                switch (p.ExitType)
                {
                    case 0:
                        yStart = p.YExit;
                        break;

                    case 1:
                    case 2:
                    case 3:
                        yStart = p.YExit - 1;
                        break;
                }

                if (!levelIndexTable.ContainsKey(p.LevelGuid))
                {
                    Rom[levelAddress++] = 0;
                }
                else
                {
                    Rom[levelAddress++] = levelIndexTable[p.LevelGuid];
                }

                Rom[levelAddress++] = (byte)p.XEnter;
                Rom[levelAddress++] = (byte)p.YEnter;
                Rom[levelAddress++] = (byte)(((p.XExit & 0x0F) << 4) | ((p.XExit & 0xF0) >> 4));
                Rom[levelAddress++] = (byte)(((yStart & 0x0F) << 4) | ((yStart & 0xF0) >> 4));
                Rom[levelAddress++] = (byte)((p.ExitsLevel ? 0x00 : 0x80) | (p.ExitType + 1));
            }

            Rom[levelAddress++] = (byte)0xFF;
            byte[] levelData = l.GetCompressedData();
            for (int i = 0; i < levelData.Length; i++)
            {
                Rom[levelAddress++] = levelData[i];
            }

            Rom[levelAddress++] = (byte) 0xFF;

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
            Rom[levelAddress++] = (byte)(w.Unused1);

            foreach (var p in w.Pointers)
            {
                Rom[levelAddress++] = (byte)p.X;
                Rom[levelAddress++] = (byte)(p.Y - 0x0F) ;
                Rom[levelAddress++] = levelIndexTable[p.LevelGuid];
            }

            Rom[levelAddress++] = (byte)0xFF;
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
                Rom[levelAddress++] = (byte)s.Item;
            }

            Rom[levelAddress++] = (byte) 0xFF;
            return levelAddress;
        }

        public bool WriteBlockDefinitions(List<BlockDefinition> definitions)
        {
            int defCount = 0;
            foreach (var d in definitions)
            {
                int address = 0x3E010 + defCount * 0x400;

                for (int i = 0; i < 256; i++)
                {
                    Rom[address] = d[i][0, 0];
                    Rom[address + 0x100] = d[i][0, 1];
                    Rom[address + 0x200] = d[i][1, 0];
                    Rom[address + 0x300] = d[i][1, 1];
                    address++;
                }

                defCount++;
            }
            return true;
        }

        public bool WritePalette(List<PaletteInfo> paletteInfo)
        {
            int address = 0x3C010;
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
            int dataPointer = 0x100010;
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
            int dataPointer = 0x3E010;
            for (int i = 0; i < 15; i++)
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
