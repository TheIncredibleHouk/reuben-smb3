using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Daiz.Library;

namespace Daiz.NES.Reuben.ProjectManagement
{
    public class ProjectManager
    {
        public event EventHandler<TEventArgs<Project>> ProjectLoaded;
        public ProjectManager()
        {
            CurrentProject = null;
        }

        public Project CurrentProject { get; set; }

        public bool New(string name)
        {
            Project project = new Project();
            project.Name = name;
            project.WorkingDirectory = ProjectController.RootDirectory;
            CurrentProject = project;
            if (ProjectLoaded != null)
            {
                ProjectLoaded(this, new TEventArgs<Project>(CurrentProject));
            }
            return true;
        }

        public bool Save(string filename)
        {
            XDocument xDoc = new XDocument();
            xDoc.Add(CurrentProject.CreateElement());
            xDoc.Save(filename);
            return true;
        }

        public bool Load(string filename)
        {
            XDocument xDoc = XDocument.Load(filename);
            XElement projEl = xDoc.Element("project");
            Project p = new Project();
            p.LoadFromElement(projEl);
            CurrentProject = p;
            if (ProjectLoaded != null)
            {
                ProjectLoaded(this, new TEventArgs<Project>(CurrentProject));
            }
            
            return true;
        }
    }
}
