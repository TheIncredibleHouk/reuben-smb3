using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Setting : IXmlIO
    {
        public string Key { get; private set; }
        public DataType DataType { get; private set; }
        public object Value { get; private set; }

        public Setting()
        {
        }

        public Setting(string key, DataType d, object value)
        {
            Key = key;
            DataType = d;
            Value = value;
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("setting");
            x.SetAttributeValue("datatype", DataType);
            if (DataType != DataType.Color)
            {
                x.SetAttributeValue("value", Value);
            }
            else
            {
                Color c = (Color) Value;
                x.SetAttributeValue("value", c.R.ToHexString() + "," + c.G.ToHexString() + "," + c.B.ToHexString());
            }
            x.SetAttributeValue("key", Key);
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            DataType = (DataType)Enum.Parse(typeof(DataType), e.Attribute("datatype").Value, true);
            string value = e.Attribute("value").Value;
            switch (DataType)
            {
                case DataType.Boolean:
                    Value = value.ToBoolean();
                    break;

                case DataType.Color:
                    string[] split = value.Split(',');
                    Value = Color.FromArgb(split[0].ToIntFromHex(), split[1].ToIntFromHex(), split[2].ToIntFromHex());
                    break;

                case DataType.Decimal:
                    Value = value.ToDouble();
                    break;

                case DataType.Integer:
                    Value = value.ToInt();
                    break;

                case DataType.String:
                    Value = value;
                    break;
            }
            Key = e.Attribute("key").Value;
            return true;
        }

        public void SetValue(object val)
        {
            Value = val;
        }
        #endregion
    }

    public enum DataType
    {
        Integer,
        Decimal,
        String,
        Boolean,
        Color
    }
}
