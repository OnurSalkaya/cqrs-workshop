using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderQueryApi.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderQueryApi.Extensions
{
    public static class BusControlExtensions
    {
        private static readonly Dictionary<string, ISendEndpoint> Endpoints = new Dictionary<string, ISendEndpoint>();
        private static readonly object SyncLock = new object();

        public static async Task Send<T>(this IBusControl busControl, T message, string queue) where T : class
        {
            if (Uri.TryCreate(busControl.Address, queue, out Uri endpointUri))
            {
                ISendEndpoint sendEndpoint;
                string key = endpointUri.ToString();

                if (Endpoints.ContainsKey(key))
                {
                    sendEndpoint = Endpoints[key];
                }
                else
                {
                    lock (SyncLock)
                    {
                        if (Endpoints.ContainsKey(key))
                        {
                            sendEndpoint = Endpoints[key];
                        }
                        else
                        {
                            sendEndpoint = busControl.GetSendEndpoint(endpointUri).Result;
                            Endpoints.Add(key, sendEndpoint);
                        }
                    }
                }

                await sendEndpoint.Send(message);
            }
            else
            {
                throw new ConfigurationException($"We cannot initialize a new Uri from these sources: Address:{busControl.Address} Queue:{queue}");
            }
        }

        public static void ConfigureBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderCreatedEventHandler>();

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(configuration["RabbitMq:Host"]), hst =>
                    {
                        hst.Username(configuration["RabbitMq:Username"]);
                        hst.Password(configuration["RabbitMq:Password"]);
                    });

                    cfg.ReceiveEndpoint("order-created-event-queue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(3, 500));
                        ep.ConfigureConsumer<OrderCreatedEventHandler>(context);
                    });
                }));
            });

            services.AddMassTransitHostedService();
        }
    }
}
