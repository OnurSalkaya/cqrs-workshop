using MediatR;
using OrderApiCQS.Domain.Data.Entities;
using System;

namespace OrderApiCQS.Domain.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public Guid Id { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
