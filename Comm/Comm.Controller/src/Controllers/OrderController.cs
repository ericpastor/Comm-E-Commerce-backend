using System.Text.Json;
using System.Text.Json.Serialization;
using Comm.Business.src.DTOs;
using Comm.Business.src.Interfaces;
using Comm.Core.src.Entities;
using Comm.Core.src.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Comm.Controller.src.Controllers
{
    [Route("api/v1/[controller]s")]
    public class OrderController : BaseController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {
        private IOrderService _orderService;

        public OrderController(IOrderService service) : base(service)
        {
            _orderService = service;
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("customer-orders")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderCreateDto)
        {
            var createdOrder = await _orderService.CreateOneAsync(orderCreateDto);

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };

            var json = JsonSerializer.Serialize(createdOrder, options);

            return CreatedAtAction(nameof(CreateOrder), json);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllAsync([FromQuery] GetAllParams getAllParams)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
            };
            var orders = await _orderService.GetAllAsync(getAllParams);
            var json = JsonSerializer.Serialize(orders, options);
            return Ok(json);
        }
    }
}