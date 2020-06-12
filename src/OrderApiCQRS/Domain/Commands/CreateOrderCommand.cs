using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApiCQRS.Domain.Commands
{
    public class CreateOrderCommand
    {
        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
