using MediatR;
using OrderApiCQS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQS.Queries
{
    public class GetOrderByOrderCodeQuery : IRequest<Order>
    {
        public string OrderCode { get; set; }

        public GetOrderByOrderCodeQuery(string orderCode)
        {
            OrderCode = orderCode;
        }
    }
}
