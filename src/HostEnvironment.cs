using System;

namespace WindowsServiceHost
{
    internal static class HostEnvironment
    {
        internal static bool IsRunningAsService()
           => !Environment.UserInteractive;
    }
}