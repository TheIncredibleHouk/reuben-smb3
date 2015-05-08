using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NEW = Reuben.Model;
using OLD = Reuben.UI.ProjectManagement;
using Reuben.Controllers;

namespace Conversion
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Convert()
        {

            OLD.ProjectController.LoadProject(@"F:\ROM Hacking\Mario Adventure 3\Mario Adventure 3 Project\Reuben.rbn");

            ProjectController projectController = new ProjectController();
            projectController.NewProject("Koopa Kingdom Escape");

            List<Guid> processed = new List<Guid>();
            OLD.WorldInfo noWorld = null;
            foreach (var wi in OLD.ProjectController.WorldManager.Worlds)
            {
                if (wi.IsNoWorld)
                {
                    noWorld = wi;
                    continue;
                }

                projectController.Project.Structure.Nodes.Add(new NEW.ProjectNode() { Name = wi.Name, ID = wi.WorldGuid });
            }

            projectController.Project.Structure.Nodes.Add(new NEW.ProjectNode() { Name = "No World", ID = Guid.Empty });

            List<OLD.LevelInfo> needsPostProcessing = new List<OLD.LevelInfo>();
            foreach (var li in OLD.ProjectController.LevelManager.Levels)
            {
                if (li.BonusAreaFor != Guid.Empty)
                {
                    needsPostProcessing.Add(li);
                    continue;
                }

                var projectNode = projectController.Project.Structure.Nodes.Where(n => n.ID == li.WorldGuid).FirstOrDefault() ?? projectController.Project.Structure.Nodes.Last();
                projectNode.Nodes.Add(new NEW.ProjectNode() { Name = li.Name, ID = li.LevelGuid });
            }

            foreach (var li in needsPostProcessing)
            {
                ((projectController.Project.Structure.Nodes.Where(n => n.ID == li.WorldGuid).FirstOrDefault() ?? projectController.Project.Structure.Nodes.Last()).Nodes.Where(n => n.ID == li.BonusAreaFor).FirstOrDefault() ?? projectController.Project.Structure.Nodes.Last()).Nodes.Add(new NEW.ProjectNode() { Name = li.Name, ID = li.LevelGuid });
            }


            projectController.Save(@"F:\ROM Hacking\Reuben\Mario Adventure 3 Project\Koopa Kingdom Escape.json");

            int index = 0;
            GraphicsController graphics = new GraphicsController();
            OLD.ProjectController.ColorManager.LoadColorInfo(@"F:\ROM Hacking\Mario Adventure 3\Mario Adventure 3 Project\default.pal");

            foreach (var r in Reuben.UI.ProjectManagement.ProjectController.PaletteManager.Palettes)
            {
                Reuben.Model.Palette p = new Reuben.Model.Palette();

                index = 0;
                for (var i = 0; i < 4; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        p.BackgroundValues[index++] = (byte)r[i, j];
                    }
                }

                index = 0;
                for (var i = 4; i < 8; i++)
                {
                    for (var j = 0; j < 4; j++)
                    {
                        p.SpriteValues[index++] = (byte)r[i, j];
                    }
                }
                p.Name = r.Name;
                p.ID = r.Guid;
                graphics.GraphicsData.Palettes.Add(p);
            }

            for (index = 0; index < 0x40; index++)
            {
                graphics.GraphicsData.Colors[index] = OLD.ProjectController.ColorManager.Colors[index];
            }

            graphics.SavePalettes(projectController.Project.PaletteFile);

            index = 0;
            LevelController levels = new LevelController();
            foreach (var bd in Reuben.UI.ProjectManagement.ProjectController.BlockManager.AllDefinitions)
            {
                NEW.LevelType newLevelType = new NEW.LevelType();
                for (var i = 0; i < 256; i++)
                {
                    Reuben.Model.Block b = new Reuben.Model.Block();
                    b.UpperLeft = bd.BlockList[i][0, 0];
                    b.UpperRight = bd.BlockList[i][0, 1];
                    b.LowerLeft = bd.BlockList[i][1, 0];
                    b.LowerRight = bd.BlockList[i][1, 1];
                    b.Description = bd.BlockList[i].Description;
                    b.BlockSolidity = ((int)bd.BlockList[i].BlockProperty & 0xF0);
                    b.BlockInteraction = ((int)bd.BlockList[i].BlockProperty & 0x0F);
                    newLevelType.Blocks[i] = b;
                }

                for (var i = 0; i < 4; i++)
                {
                    var v = bd.FireBallTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    newLevelType.FireBlockActors[i] = a;
                }

                for (var i = 0; i < 4; i++)
                {
                    var v = bd.IceBallTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    newLevelType.IceBlockActors[i] = a;
                }

                for (var i = 0; i < 8; i++)
                {
                    var v = bd.PSwitchTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    newLevelType.PSwitchBlockActors[i] = a;
                }

                levels.LevelData.Types.Add(newLevelType);
            }

            foreach (var lInfo in OLD.ProjectController.LevelManager.Levels)
            {
                NEW.LevelInfo newInfo = new NEW.LevelInfo();
                newInfo.Name = lInfo.Name;
                newInfo.File = projectController.Project.LevelsDirectory + "\\" + newInfo.Name + ".json";
                newInfo.ID = lInfo.LevelGuid;
                levels.LevelData.Levels.Add(newInfo);
            }

            levels.Save(projectController.Project.LevelDataFile);

            foreach (var lInfo in OLD.ProjectController.LevelManager.Levels)
            {
                var oldLevel = new OLD.Level();
                oldLevel.Load(lInfo);
                var newLevel = new NEW.Level();
                newLevel.AnimationType = oldLevel.AnimationType;
                newLevel.Data = oldLevel.LevelData;
                newLevel.DPadControlsTiles = oldLevel.DpadTiles;
                newLevel.EventType = oldLevel.EventType;
                newLevel.GraphicsID = oldLevel.GraphicsBank;
                newLevel.ID = oldLevel.Guid;
                newLevel.InvincibleEnemeies = oldLevel.InvincibleEnemies;
                newLevel.LevelType = oldLevel.Type;
                newLevel.MiscByte1 = oldLevel.MiscByte1;
                newLevel.MiscByte2 = oldLevel.MiscByte2;
                newLevel.MiscByte3 = oldLevel.MiscByte3;
                newLevel.MusicID = oldLevel.Music;
                newLevel.NumberOfScreens = oldLevel.Length;
                newLevel.PaletteEffectType = oldLevel.PaletteEffect;
                newLevel.PaletteID = oldLevel.Palette;
                foreach (var oldPointer in oldLevel.Pointers)
                {
                    var newPointer = new NEW.LevelPointer();
                    newPointer.X = oldPointer.XEnter;
                    newPointer.Y = oldLevel.YStart;
                    newPointer.DisableWeather = oldPointer.DisableWeather;
                    newPointer.ExitLevel = oldPointer.ExitsLevel;
                    newPointer.ExitType = oldPointer.ExitType;
                    newPointer.ExitX = oldPointer.XEnter;
                    newPointer.ExitY = oldPointer.YExit;
                    newPointer.KeepObjectData = oldPointer.KeepObjects;
                    newPointer.RedrawLevel = oldPointer.RedrawLevel;
                    newPointer.WorldNumberToExitTo = oldPointer.World;
                    newLevel.Pointers.Add(newPointer);
                }

                newLevel.RhythmPlatforms = oldLevel.RhythmPlatforms;
                foreach (var oldSprite in oldLevel.SpriteData)
                {
                    var newSprite = new NEW.Sprite();
                    newSprite.X = oldSprite.X;
                    newSprite.Y = oldSprite.Y;
                    newSprite.ObjectID = oldSprite.InGameID;
                    newSprite.Property = oldSprite.Property;
                    newLevel.Sprites.Add(newSprite);
                }

                newLevel.StartActionType = oldLevel.StartAction;
                newLevel.StartX = oldLevel.XStart;
                newLevel.StartY = oldLevel.YStart;
                newLevel.TemporaryProjectileBlockChanges = oldLevel.ProjectileBlocksTemporary;
                newLevel.TypeID = oldLevel.Type;
                levels.SaveLevel(newLevel);
            }
            WorldController worlds = new WorldController();

            foreach (var w in Reuben.UI.ProjectManagement.ProjectController.WorldManager.Worlds)
            {
                Reuben.Model.WorldInfo newInfo = new Reuben.Model.WorldInfo();
                if (!w.IsNoWorld)
                {


                    newInfo.Name = w.Name;
                    newInfo.WorldNumber = w.Ordinal;
                    newInfo.File = projectController.Project.WorldsDirectory + "\\" + newInfo.Name + ".json";
                    newInfo.ID = w.WorldGuid;
                    worlds.WorldData.Worlds.Add(newInfo);

                }
            }

            index = 0;
            foreach (var bd in Reuben.UI.ProjectManagement.ProjectController.BlockManager.AllDefinitions[0].BlockList)
            {
                var b = new NEW.Block();
                b.UpperLeft = bd[0, 0];
                b.UpperRight = bd[0, 1];
                b.LowerLeft = bd[1, 0];
                b.LowerRight = bd[1, 1];
                b.Description = bd.Description;
                b.BlockSolidity = ((int)bd.BlockProperty & 0xF0);
                b.BlockInteraction = ((int)bd.BlockProperty & 0x0F);
                worlds.WorldData.Blocks[index++] = b;
            }

            worlds.Save(projectController.Project.WorldDataFile);


            foreach (var w in Reuben.UI.ProjectManagement.ProjectController.WorldManager.Worlds)
            {
                Reuben.Model.World newWorld = new NEW.World();
                if (!w.IsNoWorld)
                {
                    OLD.World oldWorld = new OLD.World();
                    oldWorld.Load(w);
                    newWorld.Data = oldWorld.LevelData;
                    newWorld.GraphicsBankID = oldWorld.GraphicsBank;
                    newWorld.MusicID = oldWorld.Music;
                    newWorld.NumberOfScreens = oldWorld.Length;
                    newWorld.PaletteID = oldWorld.Palette;
                    newWorld.ID = oldWorld.Guid;
                    foreach (var p in oldWorld.Pointers)
                    {
                        NEW.WorldPointer newPointer = new NEW.WorldPointer();
                        newPointer.X = p.X;
                        newPointer.Y = p.Y;
                        newPointer.LevelID = p.LevelGuid;
                        newWorld.Pointers.Add(newPointer);
                    }

                    worlds.SaveWorld(newWorld);
                }
            }

            //foreach(var d in Reuben.UI.ProjectManagement.ProjectController
        }
    }
}
