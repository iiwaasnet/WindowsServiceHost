namespace WindowsServiceHost.Log
{
    internal static class LogManager
    {
        internal static ILogger CreateLogger(string source)
            => HostEnvironment.IsRunningAsService()
                   ? (ILogger) new EventLogger(source)
                   : (ILogger) new ConsoleLogger();
    }
}