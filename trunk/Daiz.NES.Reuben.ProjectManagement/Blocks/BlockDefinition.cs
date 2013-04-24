using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class BlockDefinition
    {
        public List<BlockTransition> FireBallTransitions { get; set; }
        public List<BlockTransition> IceBallTransitions { get; set; }
        public List<BlockTransition> PSwitchTransitions { get; set; }
        public byte VineTile { get; set; }
        public byte PSwitchTile { get; set; }
       
        public Block[] BlockList;

        public BlockDefinition()
        {
            FireBallTransitions = new List<BlockTransition>();
            IceBallTransitions = new List<BlockTransition>();
            PSwitchTransitions = new List<BlockTransition>();
            VineTile = 0x85;
            PSwitchTile = 0xC2;
            BlockList = new Block[256];
            for (int i = 0; i < 256; i++)
                BlockList[i] = new Block();
        }

        public Block this[int index]
        {
            get { return BlockList[index]; }
            set { BlockList[index] = value; }
        }

        public byte[] GetBlockData()
        {
            byte[] returnData = new byte[0x400];

            for (int i = 0; i < 256; i++)
            {
                returnData[i] = BlockList[i][0, 0];
                returnData[i + 0x100] = BlockList[i][0, 1];
                returnData[i + 0x200] = BlockList[i][1, 0];
                returnData[i + 0x300] = BlockList[i][1, 1];
            }

            return returnData;
        }
    }
}
