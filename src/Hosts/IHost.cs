namespace WindowsServiceHost.Hosts
{
    public interface IHost
    {
        void Run(ServiceConfiguration config);
    }
}