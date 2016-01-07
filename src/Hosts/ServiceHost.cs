using System;
using System.ServiceProcess;
using WindowsServiceHost.Log;

namespace WindowsServiceHost.Hosts
{
    internal class ServiceHost : ServiceBase, IHost
    {
        private ILogger logger;
        private ServiceConfiguration config;

        protected override void OnStart(string[] args)
        {
            try
            {
                config.OnStart();
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
            }
        }

        protected override void OnStop()
        {
            try
            {
                config.OnStop();
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
            }
        }

        public void Run(ServiceConfiguration config)
        {
            this.config = config;
            logger = LogManager.CreateLogger(config.ServiceName);
            ServiceName = config.ServiceName;

            Run(this);
        }
    }
}