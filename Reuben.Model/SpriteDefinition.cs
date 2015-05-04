using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class SpriteDefinition
    {
        [DataMember]
        public List<SpriteInfo> Sprites { get; private set; }

        [DataMember]
        public List<String> PropertyDescriptions { get; set; }

        [DataMember]
        public int GameID { get; set; }

        [DataMember]
        public int Width { get; set; }

        [DataMember]
        public int Height { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Class { get; set; }

        [DataMember]
        public string Group { get; set; }

        public int LeftDrawBox { get; set; }
        public int RightDrawBox { get; set; }
        public int TopDrawBox { get; set; }
        public int BottomDrawBox { get; set; }


        public SpriteDefinition()
        {
            Sprites = new List<SpriteInfo>();
            PropertyDescriptions = new List<string>();
        }
    }
}
