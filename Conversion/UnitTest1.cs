using System;
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
            foreach (var r in Reuben.UI.ProjectManagement.ProjectController.PaletteManager.Palettes)
            {
                Reuben.Model.Palette p = new Reuben.Model.Palette();
                int index = 0;
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

            projectController.SaveToFile(@"F:\ROM Hacking\Mario Adventure 3\Mario Adventure 3 Project\Reuben.json");
        }
    }
}
