using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaShop.Application.DTOs.Order.Request.Add;
using TeaShop.Application.DTOs.Order.Request.Remove;
using TeaShop.Application.DTOs.Order.Request.Update;
using TeaShop.Application.DTOs.Order.Request.UpdateStatus;
using TeaShop.Application.DTOs.Order.Response;
using TeaShop.Application.Service.Identity.Employee.Command.UpdateOrderStatus;
using TeaShop.Application.Service.Order.Command.AddOrder;
using TeaShop.Application.Service.Order.Command.RemoveOrder;
using TeaShop.Application.Service.Order.Command.UpdateOrder;
using TeaShop.Application.Service.Order.Query.GetAllOrders;
using TeaShop.Application.Service.Order.Query.GetCustomerOrders;
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
        [Route("customer/all")]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllCustomerOrdersAsync()
        {
            var id = new Guid(User.FindFirst("Id")?.Value!);
            var result = await _sender.Send(new GetCustomerOrdersQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrdersNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orders = result.Value;

            return Ok(orders);
        }

        [HttpGet]
        [Route("customer/{id}/all")]
        [Authorize(Roles = "Employee,Manager,MainManager")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderResponseDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAllCustomerOrdersAsync([FromRoute] Guid id)
        {
            var result = await _sender.Send(new GetCustomerOrdersQuery(id));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Orders.OrdersNotFound")
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
        [Authorize(Roles = "Client")]
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
        [Authorize(Roles = "Client")]
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
        [Authorize(Roles = "Client")]
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

        [HttpPut]
        [Route("{id}/update-status")]
        [Authorize(Roles = "Employee,Manager,MainManager")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] Guid? id, [FromBody] UpdateOrderStatusRequestDto request)
        {
            var result = await _sender.Send(new UpdateOrderStatusCommand(id, request));

            if (result.IsFailure && result.Errors.ToList()[0].Code == "Order.OrderNotFound")
                return NotFound(result.Errors.ToList()[0].Message);

            if (result.IsFailure)
                return BadRequest(result.Errors.ToList()[0].Message);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok("Successfully updated status.");
        }
    }
}
