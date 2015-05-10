using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Reuben.Model
{
    [DataContract]
    public class ProjectNode
    {
        public ProjectNode()
        {
            Nodes = new List<ProjectNode>();
        }

        [DataMember]
        public NodeType Type { get; set; }

        [DataMember]
        public Guid ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<ProjectNode> Nodes { get; set; }
    }
}
