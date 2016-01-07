using System;
using System.Threading;
using WindowsServiceHost.Log;

namespace WindowsServiceHost.Hosts
{
    internal class ConsoleHost : IHost
    {
        private readonly ILogger logger;
        private readonly ManualResetEvent exit;

        internal ConsoleHost()
        {
            logger = LogManager.CreateLogger(string.Empty);
            exit = new ManualResetEvent(false);
        }

        public void Run(ServiceConfiguration config)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            try
            {
                Console.Title = config.ServiceName;
                Console.CancelKeyPress += (_, args) => ConsoleOnCancelKeyPress(config, args);

                config.OnStart();

                logger.Info($"Service {config.ServiceName} is started. Press Ctrl+C to stop.");
                exit.WaitOne();
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
            }
            finally
            {
                exit.Dispose();
                logger.Info($"Service {config.ServiceName} is stopped.");
            }
        }

        private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            logger.Error($"Unhandled exception: {args.ExceptionObject.ToString()}");
        }

        private void ConsoleOnCancelKeyPress(ServiceConfiguration config, ConsoleCancelEventArgs consoleCancelEventArgs)
        {
            logger.Warn($"Stop signal detected.");

            consoleCancelEventArgs.Cancel = true;
            config.OnStop();
            exit.Set();
        }
    }
}