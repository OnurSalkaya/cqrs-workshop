using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Commands
{
    public class ShipOrderCommand : IRequest
    {
        public string OrderCode { get; set; }
    }
}
