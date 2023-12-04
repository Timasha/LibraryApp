using ILogger = Server.Global.Logger.ILogger;
using LogLevel = Server.Global.Logger.LogLevel;

namespace Server.ConsoleLogger;

public class ConsoleLogger : ILogger
{
    public void Log(LogLevel logLevel, string msg)
    {
        switch (logLevel)
        {
            case LogLevel.Error:
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[error]");
                break;
            }
            case LogLevel.Fatal:
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("[fatal]");
                break;
            }
            case LogLevel.Warn:
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[warn]");
                break;
            }
            case LogLevel.Info:
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("[info]");
                break;
            }
            
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($" {DateTime.Now.ToString()}: {msg}");
    }
}