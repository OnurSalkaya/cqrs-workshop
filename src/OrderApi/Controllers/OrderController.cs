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
        public async Task<IActionResult> CreateOrder(OrderRequest orderRequest)
        {
            await _orderService.CreateOrder(orderRequest);

            return Ok();
        }

        //[HttpPost("set-status")]
        //public async Task<IActionResult> SetStatusToOrder(OrderRequest orderRequest)
        //{
        //    await _orderService.CreateOrder(orderRequest);

        //    return Ok();
        //}
    }
}
