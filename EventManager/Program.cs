using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EventManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Forms;

//namespace EventManager
//{
//    static class Program
//    {
//        / <summary>
//        / The main entry point for the application.
//        / </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.Run(new Form1());
//        }
//    }
//}
