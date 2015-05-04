using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class StringResource
    {
        [DataMember]
        public Dictionary<string, string> ResourceTable { get; set; }

        [DataMember]
        public Dictionary<string, List<string>> ResourceLists { get; set; }
    }
}
