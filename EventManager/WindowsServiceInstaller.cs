using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
namespace BehinEventManager
{
    [RunInstaller(true)]
    public partial class WindowsServiceInstalle : Installer
    {
        /// <summary>
        /// Public Constructor for WindowsServiceInstaller.
        /// - Put all of your Initialization code here.
        /// </summary>
        public WindowsServiceInstalle()
        {
            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            //serviceInstaller.DisplayName = "BehinOPGDMS";
            serviceInstaller.DisplayName = "BehinBazr";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            // This must be identical to the WindowsService.ServiceBase name
            // set in the constructor of WindowsService.cs
            //serviceInstaller.ServiceName = "BehinOPGDMS";
            serviceInstaller.ServiceName = "BehinBazr";
            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}
