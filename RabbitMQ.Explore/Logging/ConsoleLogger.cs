using RabbitMQ.Tls.Mtls.Explore.Helper;
using RabbitMQ.Tls.Mtls.Explore.Interface;

namespace RabbitMQ.Tls.Mtls.Explore.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            System.Console.WriteLine(CustomTextFormater.FormatText(message));
        }
    }
}