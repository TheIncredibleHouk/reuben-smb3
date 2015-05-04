using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Reuben.UI.ProjectManagement
{
    public class PaletteInfo : IXmlIO
    {
        public event EventHandler<TEventArgs<string>> NameChanged;
        public event EventHandler<TEventArgs<DoubleValue<int, int>>> PaletteChanged;
        public bool IsSpecial { get; internal set; }

        private int[,] Data;

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

        private int _Background;
        public int Background
        {
            get { return _Background; }
            set
            {
                _Background = value;
                Data[0, 0] = Data[1, 0] = Data[2, 0] = Data[3, 0] = Data[4, 0] = Data[5, 0] = Data[6, 0] = Data[7, 0] = value;
                if (PaletteChanged != null)
                {
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(0, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(1, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(2, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(3, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(5, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(6, 0)));
                    PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int, int>(7, 0)));
                }
            }
        }

        public Guid Guid { get; set; }

        public PaletteInfo()
        {
            Data = new int[8,4];
            IsSpecial = false;
        }


        public int this[int index, int offset]
        {
            get { return Data[index, offset]; }
            set
            {
                Data[index, offset] = value;
                if (PaletteChanged != null) PaletteChanged(this, new TEventArgs<DoubleValue<int, int>>(new DoubleValue<int,int>(index, offset)));
            }
        }


        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement e = new XElement("palette");
            e.SetAttributeValue("background", Background.ToHexString());
            e.SetAttributeValue("data", ConvertData());
            e.SetAttributeValue("name", Name);
            e.SetAttributeValue("guid", Guid);
            return e;
        }

        public bool LoadFromElement(XElement e)
        {
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "background":
                        Background = a.Value.ToIntFromHex();
                        break;

                    case "name":
                        Name = a.Value;
                        break;

                    case "guid":
                        Guid = a.Value.ToGuid();
                        break;
                }
            }

            ConvertData(e);
            return true;
        }

        public string ConvertData()
        {
            StringBuilder sb = new StringBuilder();
            bool First = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (First)
                        sb.Append(Data[i, j].ToHexString());
                    else
                        sb.Append("," + Data[i, j].ToHexString());

                    First = false;
                }
            }

            return sb.ToString();
        }


        public void ConvertData(XElement e)
        {
            string[] data = e.Attribute("data").Value.Split(',');
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    Data[i, j] = data[i * 4 + j].ToIntFromHex();
                }
            }
        }
        #endregion
    }
}
