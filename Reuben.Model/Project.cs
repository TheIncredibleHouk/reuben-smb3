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
            Structure = new ProjectStructure();
        }

        [DataMember]
        public string GraphicsFile { get; set; }

        [DataMember]
        public string PaletteFile { get; set; }

        [DataMember]
        public string WorldDataFile { get; set; }

        [DataMember]
        public string LevelDataFile { get; set; }

        [DataMember]
        public string StringDataFile { get; set; }

        [DataMember]
        public string SpriteDataFile { get; set; }

        [DataMember]
        public string LevelsDirectory { get; set; }

        [DataMember]
        public string WorldsDirectory { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RomFile { get; set; }

        [DataMember]
        public ProjectStructure Structure { get; set; }
    }
}
