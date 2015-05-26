using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Reuben.Model;
using Reuben.Controllers;

namespace Reuben.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public static ASMEditor ASMEditor { get; set; }
    }
}
