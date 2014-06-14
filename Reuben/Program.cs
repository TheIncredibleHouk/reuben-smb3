using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Daiz.NES.Reuben
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] arguments)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (arguments.Length > 0)
            {
                Application.Run(new Main(arguments[0]));
            }
            else
            {
                Application.Run(new Main());
            }
        }
    }
}
