using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class SpecialDefinition : BlockDefinition, IXmlIO
    {
        public int LevelType { get; private set; }

        public SpecialDefinition()
        {
            BlockList = new Block[256];
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("specialblocks");
            x.SetAttributeValue("leveltype", LevelType);

            foreach(var b in BlockList)
            {
                if(b != null)
                {
                    x.Add(((SpecialBlock) b).CreateElement());
                }
            }
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (XAttribute a in e.Attributes())
            {
                switch (a.Name.LocalName.ToLower())
                {
                    case "leveltype":
                        LevelType = a.Value.ToInt();
                        break;
                }
            }

            foreach (var x in e.Elements("block"))
            {
                SpecialBlock sb = new SpecialBlock();
                sb.LoadFromElement(x);
                BlockList[sb.AppliesTo] = sb;
            }
            return true;
        }
        #endregion
    }
}
