using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class LevelType
    {
        public LevelType()
        {
            BlocksAffectedByFire = new byte[8];
            BlocksAffectedByIce = new byte[8];
            BlockActors = new byte[8];
            Blocks = new byte[256];
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int DefaultGraphicsID { get; set; }

        [DataMember]
        public int DefaultPaletteID { get; set; }

        [DataMember]
        public byte[] BlocksAffectedByFire { get; set; }

        [DataMember]
        public byte[] BlocksAffectedByIce { get; set; }

        [DataMember]
        public byte[] BlockActors { get; set; }

        [DataMember]
        public byte[] Blocks { get; set; }
    }
}
