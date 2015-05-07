﻿using System;
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
        }

        [DataMember]
        public Block[] Blocks { get; set; }

        [DataMember]
        public List<WorldInfo> Worlds { get; set; }
    }
}
