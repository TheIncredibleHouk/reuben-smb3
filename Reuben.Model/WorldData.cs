using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class WorldData
    {
        public WorldData()
        {
            Worlds = new List<WorldInfo>();
            Blocks = new Block[256];
            WorldLevelTable = new Dictionary<Guid, List<Guid>>();
            WorldLevelTable[Guid.Empty] = new List<Guid>();
        }

        [DataMember]
        public Block[] Blocks { get; set; }

        [DataMember]
        public List<WorldInfo> Worlds { get; set; }

        [DataMember]
        public Dictionary<Guid, List<Guid>> WorldLevelTable { get; set; }
    }
}
