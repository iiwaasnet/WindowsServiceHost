namespace WindowsServiceHost.Log
{
    public static class LogManager
    {
        public static ILogger CreateLogger(string source)
            => HostEnvironment.IsRunningAsService()
                   ? (ILogger) new EventLogger(source)
                   : (ILogger) new ConsoleLogger();
    }
}