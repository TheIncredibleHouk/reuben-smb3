using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class Block
    {
        [DataMember]
        public int UpperLeft { get; set; }

        [DataMember]
        public int UpperRight { get; set; }

        [DataMember]
        public int LowerLeft { get; set; }

        [DataMember]
        public int LowerRight { get; set; }

        [DataMember]
        public int BlockSolidity { get; set; }

        [DataMember]
        public int BlockInteraction { get; set; }

        [DataMember]
        public string Description { get; set; }

        public void SetTileByPoint(int x, int y, int value)
        {
            if (x == 0 && y == 0)
            {
                UpperLeft = value;
            }

            if (x == 1 && y == 0)
            {
                UpperRight = value;
            }

            if (x == 1 && y == 0)
            {
                LowerLeft = value;
            }

            if (x == 1 && y == 1)
            {
                LowerRight = value;
            }
        }

    }
}
