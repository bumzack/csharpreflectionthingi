using DaoThingi.Reflection;
using System;
 

namespace DaoThingi.SqlThingis.Implementation
{
    [Injectable]
    public class Logger : ILogger
    {
        public Logger() { }

        public void Debug(string msg)
        {
            Console.WriteLine($"Debug: {msg}");
        }

        public void Error(string msg)
        {
            Console.WriteLine($"Error: {msg}");
        }

        public void Info(string msg)
        {
            Console.WriteLine($"Info: {msg}");
        }
    }
}
