namespace WindowsServiceHost.Hosts
{
    internal interface IHost
    {
        void Run(ServiceConfiguration config);
    }
}