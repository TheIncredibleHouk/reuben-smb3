using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class LevelData
    {
        public LevelData()
        {
            Levels = new List<LevelInfo>();
            Types = new List<LevelType>();
            Overlays = new Block[256];
            for (int i = 0; i < 256; i++)
            {
                Overlays[i] = new Block();
            }

            OverlayPalette = new Palette();
        }

        [DataMember]
        public List<LevelInfo> Levels { get; set; }

        [DataMember]
        public List<LevelType> Types { get; set; }

        [DataMember]
        public Block[] Overlays { get; set; }

        [DataMember]
        public Palette OverlayPalette { get; set; }
    }
}
