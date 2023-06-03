using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Explore.Implementation;
using RabbitMQ.Explore.Interface;
using RabbitMQ.Explore.Logging;
using RabbitMQ.Explore.Models;
using System;

namespace RabbitMQ.Explore.DIRegistrations
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterProductImporterLogic(this IServiceCollection services)
        {
            services.AddTransient<RabbitMQStartup>();

            services.AddTransient<RabbitMQWithSSL>();
            services.AddTransient<RabbitMQWithoutSSL>();
            services.AddTransient<IRabbitMQ>((serviceProvider) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                if (configuration.GetValue<bool>("RabbitMQ:SSLEnabled"))
                {
                    return serviceProvider.GetRequiredService<RabbitMQWithSSL>();
                }
                return serviceProvider.GetRequiredService<RabbitMQWithoutSSL>();
            });

            services.AddOptions<RabbitMQOptions>()
              .Configure<IConfiguration>((options, configuration) =>
              {
                  configuration.GetSection(RabbitMQOptions.SectionName).Bind(options);
              });

            services.AddOptions<LoggerOptions>()
             .Configure<IConfiguration>((options, configuration) =>
             {
                 configuration.GetSection(LoggerOptions.SectionName).Bind(options);
             });
            services.AddSingleton<ILogger, ConsoleLogger>();
            return services;
        }
    }
}
