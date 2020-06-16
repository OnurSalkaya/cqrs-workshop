using Microsoft.AspNetCore.Mvc;
using OrderQueryApi.Data.Repositories;
using OrderQueryApi.Models.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderQueryApi.Controllers
{
    [ApiController]
    [Route("order")]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IListingOrderRepository _listingOrderRepository;

        public OrderController(IListingOrderRepository listingOrderRepository)
        {
            _listingOrderRepository = listingOrderRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> GetOrder([FromQuery] string orderCode)
        {
            var listingOrder = await _listingOrderRepository.Get(orderCode);

            if (listingOrder == null)
                return NotFound();

            return Ok(listingOrder);
        }
    }
}
