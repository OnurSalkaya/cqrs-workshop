using Microsoft.AspNetCore.Mvc;
using OrderApi.Models;
using OrderApi.Data.Entities;
using OrderApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            await _orderService.CreateOrder(createOrderRequest);

            return Ok();
        }

        [HttpPost("ship")]
        public async Task<IActionResult> ShipOrder(ShipOrderRequest shipOrderRequest)
        {
            await _orderService.ShipOrder(shipOrderRequest);
            return Ok();
        }
    }
}
