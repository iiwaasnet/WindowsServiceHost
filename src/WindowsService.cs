using System;
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
        private const string ServiceNameArg = "servicename";
        private const string DisplayNameArg = "displayname";
        private const string UserNameArg = "username";
        private const string PasswordArg = "password";
        private const string AccountArg = "account";

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
            config = MergeWithCommandLineArgs(config);

            processInstaller.Account = config.Account;
            serviceInstaller.DisplayName = config.DisplayName;
            serviceInstaller.ServiceName = config.ServiceName;
            if (!string.IsNullOrWhiteSpace(config.UserName) && !string.IsNullOrWhiteSpace(config.Password))
            {
                processInstaller.Password = config.Password;
                processInstaller.Username = config.UserName;
            }

            if (config.Installers != null)
            {
                Installers.AddRange(config.Installers.ToArray());
            }
        }

        private ServiceConfiguration MergeWithCommandLineArgs(ServiceConfiguration config)
        {
            var serviceName = Context.Parameters[ServiceNameArg];
            var displayName = Context.Parameters[DisplayNameArg];
            var userName = Context.Parameters[UserNameArg];
            var password = Context.Parameters[PasswordArg];
            var account = Context.Parameters[AccountArg];

            config.ServiceName = string.IsNullOrWhiteSpace(serviceName) ? config.ServiceName : serviceName;
            config.DisplayName = string.IsNullOrWhiteSpace(displayName) ? config.DisplayName : displayName;

            if (!string.IsNullOrWhiteSpace(account))
            {
                config.Account = (ServiceAccount)Enum.Parse(typeof(ServiceAccount), account, true);
            }

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                config.Account = ServiceAccount.User;
                config.UserName = userName;
                config.Password = password;
            }

            return config;
        }

        protected abstract ServiceConfiguration GetServiceConfiguration();

        public void Run()
        {
            HostManager.CreateHost().Run(GetServiceConfiguration());
        }
    }
}