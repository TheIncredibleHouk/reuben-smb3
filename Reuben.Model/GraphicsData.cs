using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Drawing;


namespace Reuben.Model
{
    [DataContract]
    public class GraphicsData
    {
        public GraphicsData()
        {
            Colors = new Color[0x40];
            Palettes = new List<Palette>();
        }

        [DataMember]
        public List<Palette> Palettes { get; set; }

        [DataMember]
        public Color[] Colors { get; set; }

    }
}
