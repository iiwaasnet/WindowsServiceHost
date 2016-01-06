using System;
using System.Collections.Generic;
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

        public IEnumerable<ServiceInstaller> Installers { get; set; }
    }
}