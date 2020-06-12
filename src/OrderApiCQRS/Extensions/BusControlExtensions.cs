using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQRS.Extensions
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
    }
}
