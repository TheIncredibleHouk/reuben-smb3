using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Daiz.NES.Reuben.ProjectManagement;

namespace Daiz.NES.Reuben
{
    public static class ProjectController
    {
        public static Project CurrentProject { get; private set; }

        public static void CreateNewProject()
        {
            InputForm iForm = new InputForm();
            string projectName = iForm.GetInput("Please enter the name of your project");
            if (projectName != null)
            {
                CurrentProject = ProjectManager.CreateNewProject(projectName);
            }
        }
    }
}
