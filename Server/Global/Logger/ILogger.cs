namespace Server.Global.Logger;

public interface ILogger
{
    public void Log(LogLevel logLevel, string msg);
}