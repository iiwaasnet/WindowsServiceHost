using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WindowsServiceHost
{
    public class ServiceConfiguration
    {
        public string ServiceName { get; set; }

        public string DisplayName { get; set; }

        public Action OnStart { get; set; }

        public Action OnStop { get; set; }

        public ServiceAccount Account { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public IEnumerable<Installer> Installers { get; set; }
    }
}