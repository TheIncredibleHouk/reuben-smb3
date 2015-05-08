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
    public partial class PaletteList : ComboBox
    {
        public PaletteList()
        {
            this.DoubleBuffered = true;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.DropDownHeight = 200;
            this.DropDownWidth = 256 + 32;
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;

            this.DrawItem += PaletteList_DrawItem;
            this.MeasureItem += PaletteList_MeasureItem;

        }

        void PaletteList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 60;
            e.ItemWidth = 256 + 16;
        }

        void PaletteList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            if (e.Index < 0 || Palettes == null || ColorReference == null)
            {
                return;
            }

            PaletteView view = ((PaletteView)this.Items[e.Index]);

            if ((e.State & DrawItemState.ComboBoxEdit) != DrawItemState.ComboBoxEdit && e.Index > -1)
            {
                e.Graphics.DrawImage(view.GetImage(), new Rectangle(e.Bounds.Left + 4, e.Bounds.Top + 24, 256, 32));
                e.Graphics.DrawString(view.Palette.Name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + 8, e.Bounds.Top + 4);
            }
            else
            {
                e.Graphics.DrawString(view.Palette.Name, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left, e.Bounds.Top);
            }
            
        }

        public List<Palette> Palettes { get; set; }
        public Color[] ColorReference { get; set; }

        public void UpdateList()
        {
            this.BeginUpdate();
            this.Items.Clear();
            this.Items.AddRange(Palettes.Select(s => new PaletteView(s, ColorReference)).ToArray());
            this.EndUpdate();
        }

        public Palette SelectedPalette
        {
            get
            {
                if(this.SelectedIndex < 0 || this.SelectedIndex > Palettes.Count - 1)
                {
                    return null;
                }

                return Palettes[this.SelectedIndex];
            }
            set
            {
                if (Palettes != null)
                {
                    this.SelectedIndex = Palettes.IndexOf(value);
                }
            }
        }
    }
}
