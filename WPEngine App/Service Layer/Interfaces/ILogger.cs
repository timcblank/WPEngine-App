namespace Services
{
    interface ILogger
    {
        void Log(string msg);

        void Warn(string msg);

        void Error(string msg);
    }
}
