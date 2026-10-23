using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BuildingBlocks.Messaging.MassTransit
{
    public static class Extentions
    {
        public static IServiceCollection AddMessageBroker(
    this IServiceCollection services,
    IConfiguration configuration,
    Assembly? assembly = null)
        {
            //var host = configuration["MessageBroker:Host"]
            //    ?? throw new InvalidOperationException(
            //        "MessageBroker:Host is missing.");

            //var username = configuration["MessageBroker:UserName"]
            //    ?? throw new InvalidOperationException(
            //        "MessageBroker:UserName is missing.");

            //var password = configuration["MessageBroker:Password"]
            //    ?? throw new InvalidOperationException(
            //        "MessageBroker:Password is missing.");

            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                if (assembly != null)
                {
                    config.AddConsumers(assembly);
                }

                config.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["MessageBroker:Host"]!), h =>
                    {
                        h.Username(configuration["MessageBroker:UserName"]!);
                        h.Password(configuration["MessageBroker:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

    }
}
