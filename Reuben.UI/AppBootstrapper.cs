using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace Reuben.UI
{
  public class AppBootstrapper : BootstrapperBase
  {
      public AppBootstrapper()
      {
          Initialize();
      }

      protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
      {
          DisplayRootViewFor<AppViewModel>();
      }
  }
}
