using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class Patch : IXmlIO
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public List<PatchBlock> PatchBlocks { get; private set; }

        public Patch()
        {
            PatchBlocks = new List<PatchBlock>();
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement e = new XElement("patch");
            e.SetAttributeValue("guid", Guid.ToString());
            e.SetAttributeValue("name", Name);

            foreach(var pb in PatchBlocks)
            {
                e.Add(pb.CreateElement());
            }

            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            PatchBlocks.Clear();
            Guid = e.Attribute("guid").Value.ToGuid();
            Name = e.Attribute("name").Value;
            foreach (var x in e.Elements("patchblock"))
            {
                PatchBlock pb = new PatchBlock();
                pb.LoadFromElement(x);
                PatchBlocks.Add(pb);
            }

            return true;
        }

        #endregion
    }
}
