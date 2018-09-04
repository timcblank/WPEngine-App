using System;

namespace Services
{
    class Logger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine("WARNING: " + msg);
        }

        public void Error(string msg)
        {
            Console.WriteLine("ERROR: " + msg);
        }
    }
}
