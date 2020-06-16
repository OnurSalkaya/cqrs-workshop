using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OrderQueryApi.Models.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderQueryApi.Data.Repositories
{
    public interface IListingOrderRepository
    {
        Task Insert(ListingOrder listingOrderDocument);

        Task<ListingOrder> Get(string orderCode);
    }

    public class ListingOrderRepository : IListingOrderRepository
    {
        private readonly IMongoClient mongoClient;
        private readonly IMongoDatabase mongoDatabase;
        private readonly IMongoCollection<ListingOrder> mongoCollection;

        public ListingOrderRepository(IConfiguration configuration)
        {
            mongoClient = new MongoClient(configuration.GetConnectionString("OrderListMongo"));
            mongoDatabase = mongoClient.GetDatabase("WorkshopDb");
            mongoCollection = mongoDatabase.GetCollection<ListingOrder>("ListingOrder");
        }

        public async Task Insert(ListingOrder listingOrderDocument)
        {
            await mongoCollection.InsertOneAsync(listingOrderDocument);
        }

        public async Task<ListingOrder> Get(string orderCode)
        {
            var filter = Builders<ListingOrder>.Filter.Eq("OrderCode", orderCode);
            var entity = mongoCollection.Find(filter);
            return await entity.SingleOrDefaultAsync();
        }
    }
}
