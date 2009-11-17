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
        public byte[] CompressedLevelData { get; private set; }
        public string Name { get; set; }
        public Guid WorldGuid { get; set; }
        public Guid LevelGuid { get; set; }
        public int LevelType { get; set; }
        public int CompressionSize { get; private set; }

        public void SetCompressedDataCache(byte[] data)
        {
            CompressedLevelData = data;
            CompressionSize = data.Length;
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
            if (CompressedLevelData != null)
            {
                for (int i = 0; i < CompressedLevelData.Length; i++)
                {
                    if (first)
                    {
                        sb.Append(CompressedLevelData[i]);
                    }
                    else
                    {
                        sb.Append("," + CompressedLevelData[i]);
                    }
                    first = false;
                }

                x.SetAttributeValue("compressioncache", sb);
                x.SetAttributeValue("compressionsize", CompressionSize);
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
            CompressionSize = e.Attribute("compressionsize").Value.ToInt();
            CompressedLevelData = new byte[CompressionSize];

            if (e.Attribute("compressioncache") != null && e.Attribute("compressioncache").Value != "")
            {
                string[] compressionString = e.Attribute("compressioncache").Value.Split(',');
                int i = 0;
                foreach (var s in compressionString)
                {
                    CompressedLevelData[i++] = (byte)s.ToInt();
                }
            }

            return true;
        }

        #endregion
    }
}
