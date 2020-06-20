using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Domain.Commands
{
    public class ShipOrderCommand
    {
        public string OrderCode { get; set; }
    }
}
