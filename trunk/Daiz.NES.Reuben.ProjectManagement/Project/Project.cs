using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class Project : IXmlIO
    {
        public Guid Guid { get; private set; }
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
            }
        }

        public string WorkingDirectory { get; set; }

        public Project()
        {
            Guid = Guid.NewGuid();
        }

        #region IXmlIO Members

        public XElement CreateElement()
        {
            XElement x = new XElement("project");
            x.SetAttributeValue("name", Name);
            x.SetAttributeValue("guid", Guid);
            x.Add(ProjectController.PaletteManager.CreateElement());
            x.Add(ProjectController.LayoutManager.CreateElement());
            x.Add(ProjectController.WorldManager.CreateElement());
            x.Add(ProjectController.LevelManager.CreateElement());
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            bool customPalette = false;
            foreach (var a in e.Attributes())
            {
                switch (a.Name.LocalName)
                {
                    case "name":
                        Name = e.Attribute("name").Value;
                        break;

                    case "guid":
                        Guid = e.Attribute("guid").Value.ToGuid();
                        break;

                    case "palettefile":
                        ProjectController.ColorManager.LoadColorInfo(a.Value);
                        customPalette = true;
                        break;
                }
            }

            if (!customPalette)
            {
                ProjectController.ColorManager.LoadDefaultColor();
            }

            foreach (var x in e.Elements())
            {
                switch (x.Name.LocalName)
                {
                    case "blocklayouts":
                        ProjectController.LayoutManager.LoadFromElement(x);
                        break;

                    case "worldinfo":
                        ProjectController.WorldManager.LoadFromElement(x);
                        break;

                    case "levelinfo":
                        ProjectController.LevelManager.LoadFromElement(x);
                        break;

                    case "paletteinfo":
                        ProjectController.PaletteManager.LoadFromElement(x);
                        break;
                }
            }
            return true;
        }

        #endregion
    }
}
