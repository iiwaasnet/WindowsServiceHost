using System;

namespace WindowsServiceHost
{
    public static class HostEnvironment
    {
        public static bool IsRunningAsService()
           => !Environment.UserInteractive;
    }
}