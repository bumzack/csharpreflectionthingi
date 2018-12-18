namespace DaoThingi.SqlThingis
{
    public interface ILogger
    {
        void Debug(string msg);
        void Info(string msg);
        void Error(string msg);
    }
}
