using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class GraphicsInfo : IXmlIO
    {
        public event EventHandler<TEventArgs<string>> NameChanged;
        public int Bank { get; private set; }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                if (NameChanged != null) NameChanged(this, new TEventArgs<string>(value));
            }
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement e = new XElement("graphics");
            e.SetAttributeValue("bank", Bank);
            e.SetAttributeValue("name", Name);
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "bank":
                        Bank = a.Value.ToInt();
                        break;

                    case "name":
                        Name = a.Value;
                        break;
                }
            }

            return true;
        }

        #endregion
    }
}
