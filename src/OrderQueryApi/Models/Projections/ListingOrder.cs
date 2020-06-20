using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderQueryApi.Models.Projections
{
    public class ListingOrder
    {
        public ObjectId _id { get; set; }

        public string OrderId { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
    }
}
