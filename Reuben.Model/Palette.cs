using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class Palette
    {
        public Palette()
        {
            BackgroundValues = new int[16];
            SpriteValues = new int[16];
            ID = Guid.NewGuid();
        }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public int[] BackgroundValues { get; set; }

        [DataMember]
        public int[] SpriteValues { get; set; }

        [DataMember]
        public string Name { get; set; }

        public int GetColorIndex(int column, int row)
        {
            if(column % 4 == 0)
            {
                column = 0;
            }

            if(column > 16)
            {
               column = 0;
            }

            if (column == 0)
            {
                return BackgroundValues[row];
            }

            if (column == 1)
            {
                return SpriteValues[row];
            }

            return 0x00;
        }
    }
}
