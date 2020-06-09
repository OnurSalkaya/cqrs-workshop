using MediatR;
using OrderApiCQS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQS.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
