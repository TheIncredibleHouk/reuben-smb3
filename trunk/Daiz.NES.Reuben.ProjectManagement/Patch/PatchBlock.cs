using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class PatchBlock : IXmlIO
    {
        public long Address { get; set; }
        public List<int> Data { get; private set; }



        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("block");
            x.SetAttributeValue("address", Address);
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (var b in Data)
            {
                if (first)
                {
                    sb.Append(b.ToHexString());
                    first = false;
                }
                else
                {
                    sb.Append("," + b.ToHexString());
                }
            }
            x.SetValue(sb.ToString());
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            Address = e.Attribute("address").Value.ToInt();
            string[] split = e.Value.Split(',');
            foreach (var s in split)
            {
                if (s.Length == 0) continue;
                Data.Add(s.ToIntFromHex());
            }

            return true;
        }

        #endregion
    }
}
