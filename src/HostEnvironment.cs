using System;
using System.Diagnostics;
using System.Management;

namespace WindowsServiceHost
{
    public static class HostEnvironment
    {
        internal static bool IsRunningAsService()
            => !Environment.UserInteractive;

        public static string GetServiceName()
        {
            var processId = Process.GetCurrentProcess().Id;
            var query = $"SELECT * FROM Win32_Service where ProcessId = {processId}";
            var searcher = new ManagementObjectSearcher(query);

            foreach (var queryObj in searcher.Get())
            {
                return queryObj["Name"].ToString();
            }

            return null;
        }
    }
}