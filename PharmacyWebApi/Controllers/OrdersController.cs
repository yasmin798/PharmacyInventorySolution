using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using PharmacyBusiness.DTOs;
using PharmacyBusiness.Services;
using System.Security.Claims;

namespace PharmacyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // every endpoint in this controller requires login
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetMyOrders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var orders = await _orderService.GetByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null) return NotFound($"Order with Id {id} not found.");
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var (success, error, order) = await _orderService.CreateAsync(userId, dto);

            if (!success) return BadRequest(error);

            return CreatedAtAction(nameof(GetById), new { id = order!.Id }, order);
        }
    }
}
