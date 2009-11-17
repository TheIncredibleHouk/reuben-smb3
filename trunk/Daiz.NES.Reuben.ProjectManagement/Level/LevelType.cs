using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelType
    {
        public string Name { get; set; }
        public int InGameID { get; set; }

        public LevelType(string name, int id)
        {
            Name = name;
            InGameID = id;
        }
    }
}
