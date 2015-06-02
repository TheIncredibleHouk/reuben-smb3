using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Reuben.Model;

namespace Reuben.UI.Controls
{
    public partial class SpriteInfoEditor : UserControl
    {
        public event EventHandler SpriteInfoChanged;
        public event EventHandler SpriteInfoSelected;
        public SpriteInfoEditor()
        {
            InitializeComponent();
        }

        private SpriteInfo _SpriteInfo;
        public SpriteInfo SpriteInfo
        {
            get
            {
                return _SpriteInfo;
            }
            set
            {
                _SpriteInfo = value;
            }
        }

        public void UpdateValues()
        {
            spriteValue.Text = _SpriteInfo.Value.ToString("X2");
            x.Text = _SpriteInfo.X.ToString();
            y.Text = _SpriteInfo.Y.ToString();
            hFlip.Checked = _SpriteInfo.HorizontalFlip;
            vFlip.Checked = _SpriteInfo.VerticalFlip;
            overlay.Checked = _SpriteInfo.Overlay;
            bank.Text = _SpriteInfo.Table.ToString("X2");
            Properties = _SpriteInfo.Properties;
            palette.Text = _SpriteInfo.Palette.ToString();
        }

        public void UpdateProperties(List<string> propertyList)
        {
            int index = 0;
            foreach (var a in properties.DropDownItems)
            {
                if (index >= propertyList.Count)
                {
                    ((ToolStripMenuItem)a).Visible = false;
                }
                else
                {
                    ((ToolStripMenuItem)a).Visible = true;
                    ((ToolStripMenuItem)a).Text = propertyList[index];
                    index++;
                }
            }
        }

        public bool Selected
        {
            get { return selected.Checked; }
            set
            {
                selected.Checked = value;
            }
        }

        public int Table
        {
            get
            {
                try
                {
                    int val = Convert.ToInt32(bank.Text, 16);
                    if (val < 0 || val > 255)
                    {
                        return val;
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    bank.Text = value.ToString("X2");
                }
            }
        }


        public bool Overlay
        {
            get
            {
                return overlay.Checked;
            }
            set
            {
                overlay.Checked = value;
            }
        }

        public int Value
        {
            get
            {
                try
                {
                    int val = Convert.ToInt32(spriteValue.Text, 16);
                    if (val < 0 || val > 255)
                    {
                        return val;
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    spriteValue.Text = value.ToString("X2");
                }
            }
        }

        public int X
        {
            get
            {
                try
                {
                    int val = Convert.ToInt32(x.Text, 16);
                    return val;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                x.Text = value.ToString();
            }
        }

        public int Y
        {
            get
            {
                try
                {
                    int val = Convert.ToInt32(y.Text, 16);
                    return val;
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                y.Text = value.ToString();
            }
        }


        public bool HorizontalFlip
        {
            get
            {
                return hFlip.Checked;
            }
            set
            {
                hFlip.Checked = value;
            }
        }


        public bool VerticalFlip
        {
            get
            {
                return vFlip.Checked;
            }
            set
            {
                vFlip.Checked = value;
            }
        }

        public List<int> Properties
        {
            get
            {
                List<int> selected = new List<int>();
                int index = 0;
                foreach (var a in properties.DropDownItems)
                {
                    if (((ToolStripMenuItem)a).Checked)
                    {
                        selected.Add(index);
                    }
                    index++;
                }

                return selected;
            }
            set
            {
                int index = 0;
                foreach (var a in properties.DropDownItems)
                {
                    if (value.Contains(index))
                    {
                        ((ToolStripMenuItem)a).Checked = true;
                    }
                    index++;
                }
            }
        }

        private void bank_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _SpriteInfo.Table = Math.Min(Convert.ToInt32(bank.Text, 16), 255);
            }
            catch
            {
                
            }

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void spriteValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _SpriteInfo.Value = Math.Min(Convert.ToInt32(spriteValue.Text, 16), 255);
            }
            catch
            {
                
            }

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void x_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _SpriteInfo.X = Convert.ToInt32(x.Text);
            }
            catch
            {
                
            }

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void y_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _SpriteInfo.Y = Convert.ToInt32(y.Text);
            }
            catch
            {
                
            }

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void overlay_CheckedChanged(object sender, EventArgs e)
        {
            _SpriteInfo.Overlay = overlay.Checked;

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void hFlip_CheckedChanged(object sender, EventArgs e)
        {
            _SpriteInfo.HorizontalFlip = hFlip.Checked;

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void vFlip_CheckedChanged(object sender, EventArgs e)
        {
            _SpriteInfo.VerticalFlip = vFlip.Checked;

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void toolStripMenuItem6_CheckedChanged(object sender, EventArgs e)
        {
            _SpriteInfo.Properties = Properties;

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void palette_TextChanged(object sender, EventArgs e)
        {
            try
            {
                _SpriteInfo.Palette = Convert.ToInt32(palette.Text);
            }
            catch
            {
            }

            if (SpriteInfoChanged != null)
            {
                SpriteInfoChanged(null, null);
            }
        }

        private void selected_CheckedChanged(object sender, EventArgs e)
        {
            if (SpriteInfoSelected != null)
            {
                SpriteInfoSelected(this, null);
            }
        }
    }
}
