using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class World
    {
        public World()
        {
            Pointers = new List<WorldPointer>();
            Data = new byte[0x40, 0x1B];
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int GraphicsBankID { get; set; }

        [DataMember]
        public int MusicID { get; set; }

        [DataMember]
        public int NumberOfScreens { get; set; }

        [DataMember]
        public int PaletteID { get; set; }

        [DataMember]
        public List<WorldPointer> Pointers { get; set; }

        [DataMember]
        public byte[,] Data { get; set; }
    }
}
