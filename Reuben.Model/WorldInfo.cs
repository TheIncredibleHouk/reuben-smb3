using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class WorldInfo
    {
        public WorldInfo()
        {
            Levels = new List<LevelInfo>();
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int WorldNumber { get; set; }

        [DataMember]
        public List<LevelInfo> Levels { get; set; }
    }
}
