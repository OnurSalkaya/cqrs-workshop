using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using MassTransit;
using Nest;
using OrderApi.Domain.Events;
using System.Linq;
using GreenPipes;

namespace Projection.DailySalesCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ElkRepository().DeleteIndex();

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost"), hst =>
                  {
                      hst.Username("admin");
                      hst.Password("admin");
                  });

                cfg.ReceiveEndpoint("daily-total-sales-queue", e =>
                {
                    e.PrefetchCount = 1;
                    e.UseMessageRetry(r => r.Interval(3, 500));
                    e.Consumer<CalculateDailyTotalSalesHandler>();
                });
            });

            busControl.Start();

            Console.WriteLine("Projection.DailySalesCalculator is running...");
            Console.ReadLine();
        }
    }

    public class CalculateDailyTotalSalesHandler : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            ElkRepository elkRepository = new ElkRepository();

            await elkRepository.InsertOrUpdate(context.Message.OrderDate, context.Message.TotalPrice);
        }
    }

    public class DailySales
    {
        public string Id => Date.ToString("yyyyMMdd");

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class ElkRepository
    {
        private readonly IElasticClient _elasticClient;
        private readonly string elasticUrl = "http://localhost:9200";
        private readonly string _indexName = "daily-sales-index";

        public ElkRepository()
        {
            _elasticClient = GetElasticClient();
        }

        private IElasticClient GetElasticClient()
        {
            var nodes = new List<Uri>
            {
                new Uri(elasticUrl)
            };

            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool);

            return new ElasticClient(connectionSettings);
        }

        public async Task CreateIndex()
        {
            var indexExistResponse = await _elasticClient.Indices.ExistsAsync(Indices.Index(_indexName));
            if (!indexExistResponse.Exists)
            {
                var xxx = await _elasticClient.Indices.CreateAsync(_indexName, c => c
                  .Settings(s => s
                      .NumberOfShards(1)
                      .NumberOfReplicas(0))
                  .Map(ms => ms.AutoMap<DailySales>()));
            }
        }

        public async Task DeleteIndex()
        {
            await _elasticClient.Indices.DeleteAsync(Indices.Index(_indexName));

        }

        public async Task InsertOrUpdate(DateTime orderDate, decimal amount)
        {
            var documentId = orderDate.ToString("yyyyMMdd");
            var dailySales = await Search(documentId);

            if (dailySales == null)
            {
                dailySales = new DailySales()
                {
                    Date = orderDate,
                    TotalAmount = amount
                };
            }
            else
            {
                dailySales.TotalAmount += amount;
            }

            await _elasticClient.UpdateAsync<DailySales>(new Id(dailySales), u => u
                .Index(_indexName)
                .Doc(dailySales)
                .DocAsUpsert());
        }

        public async Task<DailySales> Search(string documentId)
        {
            var dailySales = default(DailySales);

            var searchResponse = await _elasticClient.SearchAsync<DailySales>(i => i
            .Index(_indexName)
            .Query(q =>
                q.Term(t =>
                    t.Field("_id").Value(documentId))));

            if (searchResponse.ApiCall.Success)
            {
                dailySales = searchResponse.Documents?.SingleOrDefault();
            }

            return dailySales;
        }
    }
}
