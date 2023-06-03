using RabbitMQ.Explore.Helper;
using RabbitMQ.Explore.Interface;

namespace RabbitMQ.Explore.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine(CustomTextFormater.FormatText(message));
        }
    }
}