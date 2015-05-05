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
        }

        [DataMember]
        public List<LevelInfo> Levels { get; set; }

        [DataMember]
        public List<LevelType> Types { get; set; }
    }
}
