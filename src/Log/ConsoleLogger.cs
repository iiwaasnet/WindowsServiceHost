using System;

namespace WindowsServiceHost.Log
{
    internal class ConsoleLogger : ILogger
    {
        public void Warn(string message)
        {
            Output(FormatOutput("WARN", message), ConsoleColor.Yellow);
        }

        public void Error(string message)
        {
            Output(FormatOutput("ERROR", message), ConsoleColor.Red);
        }

        public void Info(string message)
        {
            Output(FormatOutput("INFO", message), ConsoleColor.White);
        }

        private void Output(string message, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
            }
            finally
            {
                Console.ForegroundColor = previousColor;
            }
        }

        private string FormatOutput(string category, string message)
            => $"{DateTime.UtcNow} [{category}]: {message}";
    }
}