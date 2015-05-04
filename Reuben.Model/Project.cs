using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class Project
    {
        public Project()
        {
            LevelTypes = new List<LevelType>(){ };
            Palettes = new List<Palette>();
            Worlds = new List<WorldInfo>() { };
            NoWorld = new WorldInfo();
        }

        [DataMember]
        public List<LevelType> LevelTypes { get; set; }

        [DataMember]
        public List<Palette> Palettes { get; set; }

        [DataMember]
        public List<WorldInfo> Worlds { get; set; }

        [DataMember]
        public WorldInfo NoWorld { get; set; }

        [DataMember]
        public List<LevelInfo> Levels { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RomFile { get; set; }
    }
}
