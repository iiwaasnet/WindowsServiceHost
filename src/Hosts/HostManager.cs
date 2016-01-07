namespace WindowsServiceHost.Hosts
{
    internal static class HostManager
    {
        internal static IHost CreateHost()
            => HostEnvironment.IsRunningAsService()
                   ? (IHost) new ServiceHost()
                   : (IHost) new ConsoleHost();
    }
}