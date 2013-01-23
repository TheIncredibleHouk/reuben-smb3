using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Block
    {
        public event EventHandler DefinitionChanged;
        private byte[,] Definition;
        public BlockProperty BlockProperty { get; set; }
        public string Description { get; set; }
        public Block()
        {
            Definition = new byte[2,2];
            Description = "";
        }

        public byte this[int x, int y]
        {
            get { return Definition[x, y]; }
            set
            {
                Definition[x, y] = value;
                if (DefinitionChanged != null) DefinitionChanged(this, null);
            }
        }
    }
}
