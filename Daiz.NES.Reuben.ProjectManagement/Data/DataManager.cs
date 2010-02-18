using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class DataManager :IXmlIO
    {
        public List<DataPiece> DataPieces { get; private set; }

        public DataManager()
        {
            DataPieces = new List<DataPiece>();
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            
        }

        public bool LoadFromElement(XElement e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
