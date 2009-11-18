using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class LevelInfo : IXmlIO
    {
        public string Name { get; set; }
        public Guid WorldGuid { get; set; }
        public Guid LevelGuid { get; set; }
        public byte[] CompressionCache { get; private set; }
        public int LevelType { get; set; }

        public void SetCompressedDataCache(byte[] data)
        {
            CompressionCache = data;
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("level");
            x.SetAttributeValue("name", Name);
            x.SetAttributeValue("worldguid", WorldGuid);
            x.SetAttributeValue("levelguid", LevelGuid);
            x.SetAttributeValue("leveltype", LevelType);

            StringBuilder sb = new StringBuilder();
            bool first = true;
            if (CompressionCache != null)
            {
                for (int i = 0; i < CompressionCache.Length; i++)
                {
                    if (first)
                    {
                        sb.Append(CompressionCache[i]);
                    }
                    else
                    {
                        sb.Append("," + CompressionCache[i]);
                    }
                    first = false;
                }

                x.SetAttributeValue("compressioncache", sb);
                x.SetAttributeValue("compressionsize", CompressionCache.Length);
            }
            else
            {
                x.SetAttributeValue("compressioncache", "");
                x.SetAttributeValue("compressionsize", 0);
            }
            

            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            Name = e.Attribute("name").Value;
            WorldGuid = e.Attribute("worldguid").Value.ToGuid();
            LevelGuid = e.Attribute("levelguid").Value.ToGuid();
            LevelType = e.Attribute("leveltype").Value.ToInt();
            int CompressionSize = e.Attribute("compressionsize").Value.ToInt();
            CompressionCache = new byte[CompressionSize];

            string[] compressionString = e.Attribute("compressioncache").Value.Split(',');
            int i = 0;
            foreach (var s in compressionString)
            {
                CompressionCache[i++] = (byte)s.ToInt();
            }

            return true;
        }

        #endregion
    }
}
