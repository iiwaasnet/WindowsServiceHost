using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using WindowsServiceHost.Hosts;

namespace WindowsServiceHost
{
    [RunInstaller(true)]
    public abstract class WindowsService : Installer
    {
        private readonly ServiceProcessInstaller processInstaller;
        private readonly ServiceInstaller serviceInstaller;

        protected WindowsService()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();

            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            ApplyConfiguration();

            base.OnBeforeInstall(savedState);
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            ApplyConfiguration();

            base.OnBeforeUninstall(savedState);
        }

        private void ApplyConfiguration()
        {
            var config = GetServiceConfiguration();
            processInstaller.Account = config.Account;
            serviceInstaller.DisplayName = config.DisplayName;
            serviceInstaller.ServiceName = config.ServiceName;

            if (config.Installers != null)
            {
                Installers.AddRange(config.Installers.ToArray());
            }
        }

        protected abstract ServiceConfiguration GetServiceConfiguration();

        public void Run()
        {
            HostManager.CreateHost().Run(GetServiceConfiguration());
        }
    }
}