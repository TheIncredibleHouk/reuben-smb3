using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class PaletteManager : IXmlIO
    {
        public event EventHandler<TEventArgs<PaletteInfo>> PaletteAdded;
        public event EventHandler<TEventArgs<PaletteInfo>> PaletteRemoved;
        public event EventHandler PalettesSaved;

        public List<PaletteInfo> Palettes { get; private set; }
        private Dictionary<Guid, PaletteInfo> paletteLookup;

        public PaletteManager()
        {
            Palettes = new List<PaletteInfo>();
            paletteLookup = new Dictionary<Guid, PaletteInfo>();
            Default();
        }

        public void Default()
        {
            Palettes.Clear();
            AddNewPalette("Default");
        }
        public void AddNewPalette(string name)
        {
            PaletteInfo pi = new PaletteInfo();
            pi.Background = 0x3C;
            pi[0, 1] = 0x0F;
            pi[0, 2] = 0x30;
            pi[0, 3] = 0x3C;
            pi[1, 1] = 0x0F;
            pi[1, 2] = 0x36;
            pi[1, 3] = 0x27;
            pi[2, 1] = 0x0F;
            pi[2, 2] = 0x2A;
            pi[2, 3] = 0x1A;
            pi[3, 1] = 0x0F;
            pi[3, 2] = 0x31;
            pi[3, 3] = 0x21;
            pi[4, 1] = 0x16;
            pi[4, 2] = 0x36;
            pi[4, 3] = 0x0F;
            pi[5, 1] = 0x0F;
            pi[5, 2] = 0x30;
            pi[5, 3] = 0x16;
            pi[6, 1] = 0x0F;
            pi[6, 2] = 0x30;
            pi[6, 3] = 0x2A;
            pi[7, 1] = 0x0F;
            pi[7, 2] = 0x36;
            pi[7, 3] = 0x27;
            pi.Guid = Guid.NewGuid();
            pi.Name = name;
            Palettes.Add(pi);
            paletteLookup.Add(pi.Guid, pi);
            if (PaletteAdded != null) PaletteAdded(this, new TEventArgs<PaletteInfo>(pi));
        }

        public void RemovePalette(PaletteInfo pi)
        {
            Palettes.Remove(pi);
            paletteLookup.Remove(pi.Guid);
            if (PaletteRemoved != null) PaletteRemoved(this, new TEventArgs<PaletteInfo>(pi));
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement palettes = new XElement("paletteinfo");
            foreach (var p in ProjectController.PaletteManager.Palettes)
            {
                palettes.Add(p.CreateElement());
            }

            if (PalettesSaved != null)
            {
                PalettesSaved(this, null);
            }

            return palettes;
        }

        public bool LoadFromElement(XElement e)
        {
            Palettes.Clear();
            paletteLookup.Clear();
            foreach (var p in e.Elements("palette"))
            {
                PaletteInfo pi = new PaletteInfo();
                pi.LoadFromElement(p);
                Palettes.Add(pi);
                paletteLookup.Add(pi.Guid, pi);
            }
            return true;
        }

        #endregion
    }
}
