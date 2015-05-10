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
        public List<SpriteInfo> SpriteInfo { get; private set; }

        [DataMember]
        public List<String> PropertyDescriptions { get; set; }

        [DataMember]
        public int GameID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Class { get; set; }

        [DataMember]
        public string Group { get; set; }


        public SpriteDefinition()
        {
            SpriteInfo = new List<SpriteInfo>();
            PropertyDescriptions = new List<string>();
        }
    }
}
