using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Reuben.Model
{
    [DataContract]
    public class SpriteInfo
    {
        public SpriteInfo()
        {
            Properties = new List<int>();
        }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }

        [DataMember]
        public List<int> Properties { get; set; }

        [DataMember]
        public int Value { get; set; }

        [DataMember]
        public int Palette { get; set; }

        [DataMember]
        public bool HorizontalFlip { get; set; }

        [DataMember]
        public bool VerticalFlip { get; set; }

        [DataMember]
        public int Table { get; set; }

        [DataMember]
        public bool Overlay { get; set; }
        
    }
}
