using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Reuben.Model
{
    [DataContract]
    public class ProjectStructure
    {
        public ProjectStructure()
        {
            Nodes = new List<ProjectNode>();
        }

        [DataMember]
        public List<ProjectNode> Nodes { get; set; }
    }
}
