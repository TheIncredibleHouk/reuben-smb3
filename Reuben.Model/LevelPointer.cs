using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class LevelPointer
    {
        [DataMember]
        public Guid ToLevelID { get; set; }

        [DataMember]
        public int ExitType { get; set; }

        [DataMember]
        public bool RedrawLevel { get; set; }

        [DataMember]
        public bool KeepObjectData { get; set; }

        [DataMember]
        public bool DisableWeather { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public int ExitX { get; set; }

        [DataMember]
        public int ExitY { get; set; }

        [DataMember]
        public bool ExitLevel { get; set; }

        [DataMember]
        public int WorldNumberToExitTo { get; set; }
    }
}
