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
            LastAsmBuildDateTime = new DateTime(1900, 1, 1);
        }

        [DataMember]
        public DateTime LastAsmBuildDateTime { get; set; }

        [DataMember]
        public string GraphicsFile { get; set; }

        [DataMember]
        public string ExtraGraphicsFile { get; set; }

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
        public string ASMDirectory { get; set; }

        [DataMember]
        public string WorldsDirectory { get; set; }

        [DataMember]
        public string ProjectDirectory { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string RomFile { get; set; }

        [DataMember]
        public ProjectStructure Structure { get; set; }
    }
}
