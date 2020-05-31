using System;
using System.Collections.Generic;
using System.Text;

namespace CqrsWorkshop.Core.Commands
{
    public class CreateOrderCommand : ICommand
    {
        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
