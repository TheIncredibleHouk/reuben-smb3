﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class SpriteData
    {
        [DataMember]
        public List<SpriteDefinition> Definitions { get; set; }
    }
}