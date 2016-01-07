namespace WindowsServiceHost.Log
{
    internal interface ILogger
    {
        void Warn(string message);

        void Error(string message);

        void Info(string message);
    }
}