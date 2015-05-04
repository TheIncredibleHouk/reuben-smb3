using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class BlockLayout : IXmlIO
    {

        public event EventHandler<TEventArgs<string>> Renamed;

        public bool IsDefault { get; internal set; }
        public int[] Layout { get; private set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (Renamed != null)
                {
                    Renamed(this, new TEventArgs<string>(value));
                }
            }
        }

        public BlockLayout()
        {
            Layout = new int[256];
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
 	        XElement e = new XElement("layout");
            StringBuilder sb = new StringBuilder();
            bool First = true;
            for(int i = 0; i < 256; i++)
            {
                if(First)
                {
                    sb.Append(Layout[i]);
                    First = false;
                }
                else
                {
                    sb.Append("," + Layout[i]);
                }
            }

            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("order", sb.ToString());
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "order":
                        string[] layout = e.Attribute("order").Value.Split(',');
                        int index = 0;
                        foreach (string s in layout)
                        {
                            Layout[index++] = s.ToInt();
                        }
                        break;

                    case "name":
                        Name = e.Attribute("name").Value;
                        break;
                }
            }
            return true;
        }

        #endregion
    }
}
