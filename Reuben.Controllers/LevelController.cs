using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reuben.Model;

namespace Reuben.Controllers
{
    public class LevelController
    {
        private Project project;
        public LevelController(Project controller)
        {
            project = controller;
        }

        public IEnumerable<string> GetLevelTypeNames()
        {
            return project.LevelTypes.Select(l => l.Name);
        }
    }
}
