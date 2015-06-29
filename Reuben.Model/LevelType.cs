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
            FireBlockActors = new BlockActor[4];
            IceBlockActors = new BlockActor[4];
            PSwitchBlockActors = new BlockActor[8];
            Blocks = new Block[256];
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int DefaultGraphicsID { get; set; }

        [DataMember]
        public int DefaultGraphicsID2 { get; set; }

        [DataMember]
        public Guid DefaultPaletteID { get; set; }

        [DataMember]
        public BlockActor[] FireBlockActors { get; set; }

        [DataMember]
        public BlockActor[] IceBlockActors { get; set; }

        [DataMember]
        public BlockActor[] PSwitchBlockActors { get; set; }

        [DataMember]
        public Block[] Blocks { get; set; }
    }
}
