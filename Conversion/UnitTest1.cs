using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Conversion
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Convert()
        {
            Reuben.UI.ProjectManagement.ProjectController.LoadProject(@"F:\ROM Hacking\Mario Adventure 3\Mario Adventure 3 Project\Reuben.rbn");

            Reuben.Controllers.ProjectController projectController = new Reuben.Controllers.ProjectController();
            projectController.NewProject("Koopa Kingdom Escape");

            int index = 0;
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
                projectController.AddPalette(p);
            }

            index = 0;
            foreach(var bd in Reuben.UI.ProjectManagement.ProjectController.BlockManager.AllDefinitions)
            {
                Reuben.Model.LevelType type = new Reuben.Model.LevelType();
                for(var i = 0; i < 256; i++)
                {
                    Reuben.Model.Block b = new Reuben.Model.Block();
                    b.UpperLeft = bd.BlockList[i][0, 0];
                    b.UpperRight = bd.BlockList[i][0, 1];
                    b.LowerLeft = bd.BlockList[i][1, 0];
                    b.LowerRight = bd.BlockList[i][1, 1];
                    b.Description = bd.BlockList[i].Description;
                    b.BlockSolidity = ((int)bd.BlockList[i].BlockProperty & 0xF0);
                    b.BlockInteraction = ((int)bd.BlockList[i].BlockProperty & 0x0F);
                    type.Blocks[i] = b;
                }

                for (var i = 0; i < 4; i++)
                {
                    var v = bd.FireBallTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    type.FireBlockActors[i] = a;
                }

                for(var i = 0; i < 4; i++)
                {
                    var v = bd.IceBallTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    type.IceBlockActors[i] = a;
                }

                for(var i = 0; i < 8; i++)
                {
                    var v = bd.PSwitchTransitions[i];
                    Reuben.Model.BlockActor a = new Reuben.Model.BlockActor();
                    a.BlockValue = v.FromValue;
                    a.ActsLikeBlockValue = v.ToValue;
                    type.PSwitchBlockActors[i] = a;
                }

                projectController.AddLevelType(type);
            }

            foreach (var w in Reuben.UI.ProjectManagement.ProjectController.WorldManager.Worlds)
            {
                Reuben.Model.WorldInfo info = w.IsNoWorld ? projectController.GetNoWorld() : new Reuben.Model.WorldInfo();
                info.Name = w.Name;
                info.WorldNumber = w.Ordinal;
                foreach (var l in Reuben.UI.ProjectManagement.ProjectController.LevelManager.Levels.Where(lv => lv.WorldGuid == w.WorldGuid))
                {
                    Reuben.Model.LevelInfo lInfo = new Reuben.Model.LevelInfo();
                    lInfo.LevelName = l.Name;
                    info.Levels.Add(lInfo);
                }
                
                projectController.AddWorldInfo(info);
            }

            projectController.SaveToFile(@"F:\ROM Hacking\Mario Adventure 3\Mario Adventure 3 Project\Reuben.json");
        }
    }
}
