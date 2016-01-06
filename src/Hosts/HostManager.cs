namespace WindowsServiceHost.Hosts
{
    public static class HostManager
    {
        public static IHost CreateHost()
            => HostEnvironment.IsRunningAsService()
                   ? (IHost) new ServiceHost()
                   : (IHost) new ConsoleHost();
    }
}