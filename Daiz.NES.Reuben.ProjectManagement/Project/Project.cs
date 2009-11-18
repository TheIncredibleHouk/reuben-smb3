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
            x.SetAttributeValue("guid", WorkingDirectory);
            x.Add(ProjectController.PaletteManager.CreateElement());
            x.Add(ProjectController.LayoutManager.CreateElement());
            x.Add(ProjectController.WorldManager.CreateElement());
            x.Add(ProjectController.LevelManager.CreateElement());
            x.Add(ProjectController.SettingsManager.CreateElement());
            return x;
        }

        public bool LoadFromElement(XElement e)
        {
            Name = e.Attribute("name").Value;
            Guid = e.Attribute("guid").Value.ToGuid();
            XAttribute xa = e.Attribute("palettefile");

            if (xa == null)
                ProjectController.ColorManager.LoadDefaultColor();
            else
                ProjectController.ColorManager.LoadColorInfo(xa.Value);

            ProjectController.LayoutManager.LoadFromElement(e.Element("blocklayouts"));
            ProjectController.WorldManager.LoadFromElement(e.Element("worldinfo"));
            ProjectController.LevelManager.LoadFromElement(e.Element("levelinfo"));
            ProjectController.PaletteManager.LoadFromElement(e.Element("paletteinfo"));
            ProjectController.SettingsManager.LoadFromElement(e.Element("settings"));
            return true;
        }

        #endregion
    }
}
