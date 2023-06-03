using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Explore.DIRegistrations;

namespace RabbitMQ.Explore
{
    public class Program
    {
        /// <summary>
        /// Main Method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            using var host = Host.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = true;
            })
            .ConfigureServices((context, services) =>
            {
                services.RegisterProductImporterLogic();
            })
            .Build();

            var rabbitMQSartup = host.Services.GetRequiredService<RabbitMQStartup>();
            rabbitMQSartup.Run();
        }
    }
}
