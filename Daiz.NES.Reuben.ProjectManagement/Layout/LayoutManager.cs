using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class LayoutManager : IXmlIO
    {
        public event EventHandler<TEventArgs<BlockLayout>> LayoutAdded;
        public event EventHandler<TEventArgs<BlockLayout>> LayoutRemoved;
        public List<BlockLayout> BlockLayouts { get; set; }

        #region IXmlIO Members

        public LayoutManager()
        {
            BlockLayouts = new List<BlockLayout>();
            LoadDefault();
        }

        public void CreateNewLayout(string newName)
        {
            BlockLayout bl = new BlockLayout();
            for (int i = 0; i < 256; i++)
            {
                bl.Layout[i] = -1;
            }
            bl.Name = newName;
            BlockLayouts.Add(bl);

            if (LayoutAdded != null)
            {
                LayoutAdded(this, new TEventArgs<BlockLayout>(bl));
            }
        }

        public void RemoveLayout(BlockLayout bl)
        {
            BlockLayouts.Remove(bl);
            if (LayoutRemoved != null)
            {
                LayoutRemoved(this, new TEventArgs<BlockLayout>(bl));
            }
        }

        public void LoadDefault()
        {
            BlockLayouts.Clear();
            BlockLayout bl = new BlockLayout();
            for (int i = 0; i < 256; i++)
            {
                bl.Layout[i] = i;
            }
            bl.IsDefault = true;
            bl.Name = "default";
            BlockLayouts.Add(bl);
        }

        public XElement CreateElement()
        {
            XElement x = new XElement("blocklayouts");
            foreach(var bl in BlockLayouts)
            {
                if(bl.IsDefault)
                    continue;

                x.Add(bl.CreateElement());
            }

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            BlockLayouts.Clear();
            LoadDefault();
            foreach (var b in e.Elements("layout"))
            {
                BlockLayout bl = new BlockLayout();
                bl.LoadFromElement(b);
                BlockLayouts.Add(bl);
            }

            return true;
        }

        #endregion
    }
}
