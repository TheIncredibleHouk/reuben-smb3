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
            Definition = new byte[2, 2];
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

        public static string Serialize(Block block)
        {
            return string.Format("{0}:{1}:{2}:{3}:{4}", block.Definition[0, 0], block.Definition[0, 1], block.Definition[1, 0], block.Definition[1, 1], block.BlockProperty);
        }

        public static Block Deserialize(string serialization)
        {
            if (!string.IsNullOrEmpty(serialization))
            {
                Block returnBlock = new Block();
                string[] values = serialization.Split(':');
                if (values.Length == 5)
                {
                    byte val1, val2, val3, val4;
                    BlockProperty propVal;

                    if (byte.TryParse(values[0], out val1) &&
                        byte.TryParse(values[1], out val2) &&
                        byte.TryParse(values[2], out val3) &&
                        byte.TryParse(values[3], out val4) &&
                        Enum.TryParse(values[4], true, out propVal))
                    {
                        returnBlock.Definition[0, 0] = val1;
                        returnBlock.Definition[0, 1] = val2;
                        returnBlock.Definition[1, 0] = val3;
                        returnBlock.Definition[1, 1] = val4;
                        returnBlock.BlockProperty = propVal;
                        return returnBlock;
                    }
                }
            }
            return null;
        }
    }
}
