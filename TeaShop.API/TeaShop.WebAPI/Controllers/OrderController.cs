using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Order.Request.Add;
using TeaShop.Application.DTOs.Order.Request.Remove;
using TeaShop.Application.DTOs.Order.Request.Update;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.Service.Order.Command.AddOrder;
using TeaShop.Application.Service.Order.Command.RemoveOrder;
using TeaShop.Application.Service.Order.Command.UpdateOrder;
using TeaShop.Application.Service.Order.Query.GetAllOrders;
using TeaShop.Application.Service.Order.Query.GetOrderById;

namespace TeaShop.WebAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly ISender _sender;

        public OrderController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var result = await _sender.Send(new GetAllOrdersQuery());

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrderNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orders = result.Value;

            return Ok(orders);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(200, Type = typeof(OrderResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOrderByIdAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetOrderByIdQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrderNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = result.Value;

            return Ok(order);
        }

        [HttpPost]
        [Route("add")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddOrderAsync([FromBody] AddOrderRequestDto request)
        {
            var result = await _sender.Send(new AddOrderCommand(request));

            if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully added record.");
        }

        [HttpDelete]
        [Route("{id}/remove")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> RemoveOrderAsync([FromRoute] Guid? id)
        {
            var result = await _sender.Send(new RemoveOrderCommand(
                    new RemoveOrderRequestDto(id)
                ));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrderNotFound")
                return NotFound(result.Errors.ToList()[0].Message);
            else if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully removed record.");
        }

        [HttpPut]
        [Route("{id}/update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] Guid? id, [FromBody] UpdateOrderRequestDto request)
        {
            var result = await _sender.Send(new UpdateOrderCommand(id, request));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrderNotFound")
                return NotFound(result.Errors.ToList()[0].Message);
            else if (result.IsFailure)
                return BadRequest(result.Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully updated record.");
            
        }
    }
}
