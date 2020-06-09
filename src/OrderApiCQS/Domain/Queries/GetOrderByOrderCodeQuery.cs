using MediatR;
using OrderApiCQS.Domain.Data.Entities;

namespace OrderApiCQS.Domain.Queries
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
