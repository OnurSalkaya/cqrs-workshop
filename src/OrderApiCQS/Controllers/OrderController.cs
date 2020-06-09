using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using OrderApiCQS.Data.Entities;
using OrderApiCQS.Commands;
using OrderApiCQS.Queries;

namespace OrderApiCQS.Controllers
{
    [ApiController]
    [Route("order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            var order = await _mediator.Send(createOrderCommand);
            return Ok(order);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrder([FromQuery] string orderCode)
        {
            var order = await _mediator.Send(new GetOrderByOrderCodeQuery(orderCode));

            if (order == null)
                return NotFound();

            return Ok(order);
        }
    }
}
