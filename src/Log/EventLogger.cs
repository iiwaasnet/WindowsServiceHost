using System.Diagnostics;

namespace WindowsServiceHost.Log
{
    public class EventLogger : ILogger
    {
        private readonly string source;

        public EventLogger(string source)
        {
            this.source = source;
        }

        public void Warn(string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Warning);
        }

        public void Error(string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Error);
        }

        public void Info(string message)
        {
            EventLog.WriteEntry(source, message, EventLogEntryType.Information);
        }
    }
}